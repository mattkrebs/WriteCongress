<div id="sidebar">
	<?php if ($page_file_name != "index.php") { ?>
	<div class="widget campaign_email_capture_wrap">
		<h3 class="widgettitle">Join The Movement</h3>
		<span>This form does't work, but it's styled so you can insert code from your email list manager.</span>
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
	<?php } ?>
	<div class="widget"><h4 class="widgettitle">Upcoming Events</h4>
		<ul class="upcoming">
			<li class="event_list_item">
				<div class="when">
					July 23, 2012 <small>(All Day)</small>
				</div>
				<div class="event">
					<a href="page-event.php">Campaign Party Event</a>
				</div>
			</li>
			<li class="event_list_item">
				<div class="when">
					November 6, 2012 8:00 am -<br/>November 6, 2012 5:00 pm
				</div>
				<div class="event">
					<a href="page-event.php">Election Day</a>
				</div>
			</li>
		</ul>
		<div class="dig-in"><a href="page-event.php">View All Events</a></div>
	</div>
	<div class="widget_testimonial widget"><h4 class="widgettitle">Testimonial Card</h4>
		<div class="the_testimonial">I support Campaign because it keeps me working. It's time to get out and vote!</div>
		<div class="the_testimonial_author">
			<strong>- Jake Caputo</strong>
			<span><a href="http://themeforest.net/user/designcrumbs/portfolio?ref=designcrumbs" title="Design Crumbs">Design Crumbs</a></span>
		</div>
		<div class="clear"></div>
	</div>
	<?php if (($page_file_name == "blog.php") || ($page_file_name == "blog-single.php")) { ?>
	<div class="widget">
		<div class="slabload">
			<div class="slabwrap">
				<h1 class="slabtextdone">
					<span class="slabtext">ELECTION DAY IS</span>
					<span class="slabtext">NOV. 6, 2012</span>
					<span class="slabtext">DON'T FORGET</span>
					<span class="slabtext">TO GET OUT</span>
					<span class="slabtext">AND VOTE</span>
				</h1>
			</div>
		</div>
	</div>
	<div class="widget"><h4 class="widgettitle">Blog Categories</h4>
		<ul>
			<li><a href="#" title="View all posts filed under General">General</a></li>
			<li><a href="#" title="View all posts filed under Issues">Issues</a></li>
		</ul>
	</div>
	<div class="widget"><h4 class="widgettitle">Archives</h4>
		<ul>
			<li><a href="#" title="February 2012">February 2012</a></li>
			<li><a href="#" title="January 2012">January 2012</a></li>
		</ul>
	</div>
	<?php } ?>
</div>