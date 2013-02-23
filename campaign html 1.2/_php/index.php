<?php include('includes/header.php'); ?>
<div id="slides_wrap">
	<div id="slides">
		<div class="slidearea slides_container">
			<div>
				<div class="slide_image_wrap">
					<a href="#" title="Campaign Slide">
						<img width="600" height="300" src="images/_demo//htmlslide1.png" alt="Campaign Slide" />
					</a>
				</div>
			</div>
			<div>
				<iframe src="http://player.vimeo.com/video/7449107?portrait=0" width="600" height="300" frameborder="0"></iframe>
			</div>
			<div>
				<div class="slide_image_wrap">
					<div class="slide_text_overlay">
						This slide has a little bit of text overlaid on the bottom.
					</div>
					<a href="#" title="Campaign Slide">
						<img width="600" height="300" src="images/_demo/htmlslide2.png" alt="Campaign Slide" />
					</a>
				</div>
			</div>
		</div>
	</div>
	<div id="slide_widget" class="campaign_email_slide">
		<div id="slide_widget_inner">
			<div class="widget campaign_email_capture_wrap">
				<h3 class="widgettitle">Join The Campaign</h3>
				<span>This form does't work, but you can easily put in code from your email list manager.</span>
				<div id="campaign_email_capture">
					<form action="#" method="post">
						<div>
							<input type="text" class="campaign-email-capture-name campaign-email-capture-name-active" name="campaign-email-capture-name" title="Your Name" />
							<input type="text" class="campaign-email-capture-email campaign-email-capture-email-active" name="campaign-email-capture-email" title="Your Email" />
							<input type="submit" class="campaign-email-capture-submit" value="Subscribe To Updates" name="Submit" />
						</div>
					</form>
				</div>
			</div>
		</div>
	</div>
	<div class="clear"></div>
</div>
<div id="home_widgets">
	<div id="home_widget_wrap">
		<div class="widget">
			<h3 class="widgettitle">Call To Action</h3>
			<div>You can fill this banner up with whatever you want. These three sections and their buttons serve as a perfect call to action to navigate your users around the site..</div>
			<a href="#" class="button button_gray">Contribute</a>
		</div>
		<div class="widget">
			<h3 class="widgettitle">Collect Emails</h3>
			<div>Campaign makes it easy to capture the names and emails of your constituents. You can easily put in code from your list manager to sign up users in a jiffy.</div>
			<a href="#" class="button button_gray">Get In Touch</a>
		</div>
		<div class="widget">
			<h3 class="widgettitle">Create Events</h3>
			<div>Campaign is also styled for events. List your upcoming events in the sidebar of each page and link each and every one to their very own events page template.</div>
			<a href="#" class="button button_gray">Sign Me Up</a>
		</div>
		<div class="clear"></div>
	</div>
</div>
<div class="posts-wrap">
	<div id="home_video_wrap">
		<div id="home_video">
			<iframe src="http://player.vimeo.com/video/31241445?portrait=0" width="380" height="223" frameborder="0"></iframe>
		</div>
		<div id="home_video_desc">
			<h4>Video Title</h4>
			<p>This is a short description of the video. Simply put in the embed code for whatever video you want here. This is an HTML template, so whatever you want put here will work just fine.<br /><br />
				Get your point across with video <em>and</em> a little bit of text.
			</p>
		</div>
		<div class="clear"></div>
	</div>
	<div id="home_latest_posts">
		<h4 class="entry-title" id="latest-posts-title">Latest Posts</h4>
		<div class="single_latest left">
			<a href="blog-single.php" title="Single Post" class="single_latest_img_link">
				<img width="170" height="120" src="images/latest_fallback.png" alt="Image" />
			</a>
			<h5><a href="blog-single.php" title="Single Post">Single Post</a></h5>
			<div class="meta">
				Posted on February 21, 2012
			</div>
		</div>
		<div class="single_latest left">
			<a href="blog-single.php" title="Single Post" class="single_latest_img_link">
				<img width="170" height="120" src="images/latest_fallback.png" alt="Image" />
			</a>
			<h5><a href="blog-single.php" title="Single Post">Single Post</a></h5>
			<div class="meta">
				Posted on February 21, 2012
			</div>
		</div>
		<div class="single_latest left">
			<a href="blog-single.php" title="Single Post" class="single_latest_img_link">
				<img width="170" height="120" src="images/latest_fallback.png" alt="Image" />
			</a>
			<h5><a href="blog-single.php" title="Single Post">Single Post</a></h5>
			<div class="meta">
				Posted on February 21, 2012
			</div>
		</div>
		<div class="clear"></div>
	</div>
</div>
<?php include('includes/sidebar.php'); ?>
<?php include('includes/footer.php'); ?>