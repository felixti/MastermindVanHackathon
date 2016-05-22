/*
	Autor: CS [csprofile.github.io]
	Date: 22/05/2016
	File name: game.js
	Short description: Implementation of new vision of master mind game, with multilevel and addressing a hacker short history in homage to vanHackathon.
	Functions:
		$.masterMind > To create game object;
		$(e).configure > To configure game;
		$(e).createRoom > To create a new room (Not implemented in front-end, check back-end files);
		$(e).play > Create board and start the game;
			$(e).play.defineSettings > Create settings of board based on game configuration;
			$(e).play.buildBoard > Create board based on settings;
			$(e).play.nextLine > Called after each move, check if player have to continue playning;
			$(e).play.gameOver > Called by $(e).play.nextLine if game is over;
			$(e).play.gameWin > Called by $(e).play.nextLine if players wins the game;
			$(e).play.startTimer > Start level 4 timer;
			
	Test cheat:
		Game Level: 666 > Password is set to 012 to test end;
		Game Level: 999 > Level 4 timer is set to 1 sec. to test game over;
*/

$.masterMind = function(){
	var _settings;
	var _room;
	var _move = new Array();
	var _password = [];
	
	this.configure = function(options){
		_settings =  $.extend({
			 playerName:'undefined'
			,gameLevel:1
        }, options );
	}
	
	this.createRoom = function(){
		_room =  $.extend({
			name:'defRoom'
        }, options);
	}
	
	this.play = function(){
		var boardMatrix = {};
		var divFitSize;
		var maxPN;
		var timer;
		var level5Time = 150;
		
		function defineSettings(){
			switch(_settings.gameLevel){
				case '0':
					boardMatrix = {x:8 , y:3};
					break;
				case '1':
					boardMatrix = {x:8 , y:4};
					break;
				case '2':
					boardMatrix = {x:6 , y:4};
					break;
				case '3':
					boardMatrix = {x:6 , y:5};
					break;
				case '4':
					boardMatrix = {x:12 , y:8};
					break;
				case '5':
					boardMatrix = {x:12 , y:8};
					break;
				default:
					boardMatrix = {x:8 , y:3};
					break;
			};
			
			divFitSize = {
				 w: 100 / boardMatrix.y
				,h: 100 / boardMatrix.x
			};
			
			maxPN = boardMatrix.y - 1;
			
			for(var x = 0; x<=maxPN ; x++){
				_password.push(Math.floor(Math.random() * (maxPN + 1)) + 0);
			}
			
			if(_settings.gameLevel == 666){  //Test end
				_password = [0,1,2];
			}else if(_settings.gameLevel == 999){ //Test gameover
				level5Time = 1;
				_settings.gameLevel = 4;
			}
		}
		
		function buildBoard(){
			var piecePlace;
			var piecePlaceCol;
			var piecePlaceText;
			var colType;
			var resultPlace;
			var sideDiv;
			var hackBtn;
			
			var mainDiv = $("<div>")
				.addClass("row board")
				.attr("id","board")
				.hide();
			
			for(var _x=0 ; _x < boardMatrix.x ; _x++){
				var totPiecePlace = boardMatrix.x-2;
				_move[_x] = new Array();
				colType = _x == 0 ? 'colorAnwser' : _x == boardMatrix.x - 1 ? 'avaibleColors' : 'piecePlaceCol';
				
				piecePlaceCol = $("<div>")
					.css({height:divFitSize.h + '%'})
					.addClass('boardColor')
					.addClass("col-md-8")
					.addClass(colType)
					.attr("id",_x + "_" +colType);
					
				//Cols for score and timer
				if(colType == 'piecePlaceCol'){
					hackBtn = $("<button>")
						.addClass("btn")
						.attr("id","button_"+_x)
						.attr("lineRef",_x)
						.html("hack(line"+(totPiecePlace - _x + 1)+")")
						.click(function(){nextLine(this);});
						
					if(_x != totPiecePlace){
						hackBtn.addClass("disabled");
					}
					
					sideDiv = $("<div>")
						.css({height:divFitSize.h + '%'})
						.addClass('sideDiv')
						.addClass("col-md-2 btnPlace")
						.append(hackBtn);
				}else{
					sideDiv = $("<div>")
						.css({height:divFitSize.h + '%'})
						.addClass('sideDiv')
						.addClass("col-md-2")
						.html("");
						
					if(colType == 'avaibleColors' && _settings.gameLevel == 5){
						piecePlaceText = $("<div>")
							.addClass('valign2 timerPlaceText')
							.appendTo(sideDiv);
							
						sideDiv
							.attr("id","timer")
							.addClass('valign1');
						startTimer(level5Time, piecePlaceText);
					}
				}
					
				//Cols for board
				if(colType != 'colorAnwser'){
					for(var _y=0 ; _y < boardMatrix.y ; _y++){
						_move[_x][_y] = null;
						
						
						piecePlace = $("<div>")
							.css({
								 width:divFitSize.w + '%'
								,height:'100%'
								,'float':'left'
							})
							.attr("matrixRef",function(){
								return JSON.stringify({x:_x , y:_y});
							})
							.addClass('emptyPlace valign1')
							.appendTo(piecePlaceCol)
							
						piecePlaceText = $("<div>")
							.addClass('valign2 piecePlaceText')
							.appendTo(piecePlace);
							
						if(colType == 'avaibleColors'){
							piecePlaceText
								.html(_y)
								.attr('unselectable', 'on')
								.css('user-select', 'none')
								.on('selectstart', false);
								
							piecePlace
								.addClass("color"+_y)
								.draggable({
									 revert: true
									,helper: "clone"
									,opacity: .4
									,containment: ".container"
								});
						}else if(colType == 'piecePlaceCol'){
							piecePlace.droppable({
								drop:function(event, ui){
									var color = ui.draggable.css("background-color");
									var text = ui.draggable.html();
									var moviment = JSON.parse($(this).attr("matrixRef"));
									
									$(ui.helper).remove();
									$(this).css('background-color',color);
									$(this).html(text);
									$(this).removeClass('emptyPlace');
									_move[moviment.x][moviment.y] = $(ui.draggable.html()).html();
								},
								over: function(event, ui){
									$(this).attr("prev-bg-color",$(this).css('background-color'));
									$(this).css({'background-color':ui.draggable.css("background-color")});
								},
								out: function(event, ui){
									if($(this).hasClass('emptyPlace')){
										$(this).css({'background-color':''});
									}else{
										$(this).css({'background-color':$(this).attr("prev-bg-color")});
									}
								}
							});
							
							if(_x != totPiecePlace){
								piecePlace.droppable( "option", "disabled", true );
							}
						}
						
					}
				}
				
				$(mainDiv).append(piecePlaceCol);
				$(mainDiv).append(sideDiv);
				
				if(colType == 'colorAnwser'){
					var Msg = strgs['GameInstructions'].replace("@@PASSLENGHT@@",Array(boardMatrix.y + 1).join("*"));
					piecePlaceCol
					.addClass('answerDiv')
					.typed({
						strings: [Msg],
						typeSpeed: 20,
						showCursor: false
					});
				}
				
			}
			
			$(mainDiv).fadeIn('slow');
			$("#container").append(mainDiv);
			$('body').removeClass("noise");
			$('body').addClass("inGame");
			
		}
		
		function nextLine(button){
			var colorMatch = 0;
			var positionMatch = 0;
			var passUnic;
			var moveUnic;
			var thisMove = _move[$(button).attr('lineRef')];
			
			if(!$(button).hasClass('disabled')){
				var objectToVerify = $('#' + $(button).attr('lineRef') + "_piecePlaceCol");
				var someEmptyPlce = false;
				
				for(var val of thisMove){
					if (!val){
						someEmptyPlce = true;
						var emptyPlaces = objectToVerify.find('.emptyPlace');
						emptyPlaces.addClass('highlightEmptyPlace');
						setTimeout(function(){
							emptyPlaces.removeClass('highlightEmptyPlace');
						},200);
						return;
					}
				}
				
				if(!someEmptyPlce){
					$(button).html('Hacking...');
					
					
					//Game logic [Must to be in API]
					passUnic = _password.filter(function(itm,i,_password){
						return i==_password.indexOf(itm);
					});
					moveUnic = thisMove.filter(function(itm,i,_password){
						return i==thisMove.indexOf(itm);
					});
					
					for(var m = 0; m < thisMove.length ; m++){
						if(thisMove[m] == _password[m]){
							positionMatch ++;
						}
					}
					
					for(var ux = 0; ux < moveUnic.length ; ux++){
						for (var uy = 0; uy < passUnic.length ; uy++){
							if(moveUnic[ux] == passUnic[uy]){
								colorMatch ++;
							}
						}
					}
					
					
					if(positionMatch == boardMatrix.y){
						gameWin();
					}else if($(button).attr('lineRef') == 1){
						gameOver();
					}else{
						$(button).html("{colorMatch:"+colorMatch+",<br/>positionMatch:"+positionMatch+"}");
						$("#button_"+ ($(button).attr('lineRef') - 1)).removeClass("disabled");
						$('#' + ($(button).attr('lineRef') - 1) + "_piecePlaceCol > div").droppable( "option", "disabled", false );
						
						$("#button_"+ ($(button).attr('lineRef'))).addClass("disabled");
						objectToVerify.find('.ui-droppable').droppable( "option", "disabled", true);
						
						objectToVerify.find('div').css({ opacity: 0.5 });
					}
				}
			}
		}
		
		function gameWin(){
			$("#container").fadeOut('fast',function(){
				$('body').addClass("noise");
				$('body').removeClass("inGame");
				$("#gameMsgs").css("display","inline");
				$(".col-md-12").empty();
				$(".col-md-12").append("<div class='inputs valign2'></div>");
				$("#board").remove();
				$("#container").fadeIn('fast',function(){
					$(".inputs").typed({
						strings: [strgs['End1'],strgs['End2']],
						typeSpeed: 50,
						showCursor: false
					});
				});
			})
		}
		
		function gameOver(){
			$("#container").fadeOut('fast',function(){
				$('body').addClass("noise");
				$('body').removeClass("inGame");
				$("#gameMsgs").css("display","inline");
				$(".col-md-12").empty();
				$(".col-md-12").append("<div class='inputs valign2'></div>");
				$("#board").remove();
				$("#container").fadeIn('fast',function(){
					$(".inputs").typed({
						strings: [strgs['GameOver']],
						typeSpeed: 50,
						showCursor: false
					});
				});
			});
		}
		
		function startTimer(duration, display) {
			var t = duration, minutes, seconds;
			timer = setInterval(function () {
				minutes = parseInt(t / 60, 10);
				seconds = parseInt(t % 60, 10);

				minutes = minutes < 10 ? "0" + minutes : minutes;
				seconds = seconds < 10 ? "0" + seconds : seconds;

				display.html(minutes + ":" + seconds);

				if (--t < 0) {
					clearInterval(timer);
					gameOver();
				}
			}, 1000);
		}
		
		defineSettings();
		buildBoard();
		
	}
	
	return this;
}