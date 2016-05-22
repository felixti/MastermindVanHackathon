/*
	Autor: CS [csprofile.github.io]
	Date: 22/05/2016
	File name: onload.js
	Short description: Prevent lower resolution and initialize home screen.
*/


$(window).on('load', function(){
	var width = $(window).width();
	var height = $(window).height();
	if((width < 1024 || height < 768)){
		$(".wrongSize").css("display","block");
		$("#container").css("display","none");
	}
});

$(document).ready(function(){
	homeScreen();
});