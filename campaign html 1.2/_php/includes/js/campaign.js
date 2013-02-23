/*
Campaign JS
Code and stuff you need for the Campaign theme
*/

			var J = jQuery.noConflict();
			J(document).ready(function(){
				
				// Children Flyout on Menu
				function mainmenu(){
				J("#main_menu ul li ul").css({display: "none"}); // Opera Fix
					J("#main_menu ul li").hover(function(){
						J(this).find('ul:first').css({visibility: "visible",display: "none"}).show(300);
						},function(){
						J(this).find('ul:first').css({visibility: "hidden"});
					});
				}
								
				mainmenu();
				
				// The Slider
				J(function(){
					J('#slides').slides({
						preload: true,
						preloadImage: '../images/loading.gif',
						play: 8000,
						pause: 10000,
						hoverPause: true,
						effect: 'fade' // 'fade' or 'slide'
					});
				});

				/* uncomment this to turn on the sticky header. This only works properly with the body class "body_span"
					J("#header").sticky({topSpacing:0});
				*/
				
				// Turn on that SlabText
        		function slabTextHeadlines() {
        			J('.slabload').fadeIn(1000); // fade in after it's loaded
                	J(".slabwrap h1").slabText({
                		// Don't slabtext the headers if the viewport is under 380px
                		"viewportBreakpoint":380
                	});
        		};
        
        		// give it a second to load fonts
        		J(window).load(function() {
         	       setTimeout(slabTextHeadlines, 1000);
       			});	
				
				// Fancybox
				J(".lightbox").fancybox({
					'transitionIn'		: 'fade',
					'transitionOut'		: 'fade'
				});
				
				//Animates comment links, the logo and toggles on hover, no IE
				if(!J.browser.msie){
					// Animates the soc nets on hover
					J("#socnets").delegate("img", "mouseover mouseout", function(e) {
						if (e.type == 'mouseover') {
							J("#socnets a img").not(this).dequeue().animate({opacity: "0.3"}, 300);
    					} else {
							J("#socnets a img").not(this).dequeue().animate({opacity: "1"}, 300);
   						}
					});
					
					J("#the_logo").hover(function(){
						J(this).fadeTo(100, 0.8); 
					},function(){
						J(this).fadeTo(100, 1);
					});
					
					// Recent Blog Post hovers
					J('.single_latest img').hover(function(){
						J(this).stop().fadeTo(200, .8);
					},function(){
						J(this).stop().fadeTo(200, 1);
					});
					// Blog Post hovers
					J('.attachment-blog_image').hover(function(){
						J(this).stop().fadeTo(200, .8);
					},function(){
						J(this).stop().fadeTo(200, 1);
					});
				};
				
				// Email Capture effects
				J("input.campaign-email-capture-name").focus(function(srcc) {
					if (J(this).val() == J(this)[0].title) {
						J(this).removeClass("campaign-email-capture-name-active");
						J(this).val("");
					}
				});
    			J("input.campaign-email-capture-name").blur(function() {
					if (J(this).val() == "") {
						J(this).addClass("campaign-email-capture-name-active");
						J(this).val(J(this)[0].title);
					}
				});
				J("input.campaign-email-capture-name").blur();
				
				J("input.campaign-email-capture-email").focus(function(srcc) {
					if (J(this).val() == J(this)[0].title) {
						J(this).removeClass("campaign-email-capture-email-active");
						J(this).val("");
					}
				});
    			J("input.campaign-email-capture-email").blur(function() {
					if (J(this).val() == "") {
						J(this).addClass("campaign-email-capture-email-active");
						J(this).val(J(this)[0].title);
					}
				});
				J("input.campaign-email-capture-email").blur(); 

			});			