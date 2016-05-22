/*
	Autor: CS [csprofile.github.io]
	Date: 22/05/2016
	File name: homeScreen.js
	Short description: Screen to capture player information and game settings.
	PS: Inputs use thirdparty cssConsole.js to get text;
*/

var homeScreen = function(){
	var inputText = [
		strgs['ChooseRoom']
		,strgs['GameLevel'] 
	];
	
	var inputControll = 0;
	var playerName;
	var gameLevel;
	var game = $.masterMind();
	var gameIsRuning = false;
	
	$('#gameInput').cssConsole({
		inputName:'console',
		charLimit: 60,
		onEnter: function(){
			var val = $('#gameInput').find('input').val();
			$("#gameInputLabel").append("<div><p>" + val + "</p></div>");
			
			switch(inputControll){
				case 0:
					playerName = val;
					for (var v in strgs){
						if (strgs.hasOwnProperty(v)) {
							 strgs[v] = strgs[v].replace(new RegExp("@@PLAYERNAME@@", 'g'), playerName);
						}
					}
					
					break;
				case 1:
					if(val != 0){
						//Load here to get session code
						$("#gameInputLabel").append("<div><p>"+strgs['APIProblems']+"</p></div>");
					}
					break;
				case 2:
					gameLevel = val;
					break;
			}
			
			if(inputControll < 2){
				$("#gameInputLabel").append("<div><p>>"+inputText[inputControll]+"</p></div>");	
				$("#gameInput").cssConsole('reset');
				inputControll ++;
			}else{
				game.configure({gameLevel:gameLevel, playerName:playerName});
				$("#container").fadeOut('fast',function(){
					$(".inputs").empty();
					$("#container").fadeIn('slow',function(){
						$('body').dblclick(function(){
							$('body').unbind('dblclick');
							$("#container").fadeOut('slow',function(){
								$("#gameMsgs").css("display","none");
								$("#container").fadeIn('fast',function(){
									if(!gameIsRuning){
										game.play();
										gameIsRuning = true;
									}
								});
							});
						});
						$(".inputs").typed({
							strings: [strgs['Intro1'],strgs['Intro2']],
							typeSpeed: 50,
							showCursor: false,
							callback: function() {
								if(!gameIsRuning){
									$("#container").fadeOut('slow',function(){
										$("#gameMsgs").css("display","none");
										$("#container").fadeIn('fast',function(){
											game.play();
											gameIsRuning = true;
										});
									});
								}
							}
						});
					});
				});
			}
			
		}
	}).click();
	
	$('body').click(function(){
		$('#gameInput').click();
	});
}