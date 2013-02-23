<?php include('includes/header.php'); ?>
<div id="page" class="posts-wrap">
	<h2 class="entry-title">Event Page</h2>
	<div class="entry-content" id="page-content">
		<div id="event-meta">
			<dl class="column">
				<dt class="event-label event-label-name">Event:</dt>
				<dd itemprop="name" class="event-meta event-meta-name"><span class="summary">Campaign Party Event</span></dd>
				<dt class="event-label event-label-date">Date:</dt>
				<dd class="event-meta event-meta-date"><meta itemprop="startDate" content="2012-07-23-12:00:00"/>July 23, 2012</dd>
				<dt class="category-label">Category:</dt>
				<dd class="category-meta"><a href="http://themes.designcrumbs.com/campaign/events/category/campaign-party/" rel="tag">Campaign Party</a>
				</dd>
				<dt class="event-label event-label-organizer">Organizer:</dt>
				<dd class="vcard author event-meta event-meta-author"><span class="fn url"><a href="#">Campaign</a></span></dd>
				<dt class="event-label event-label-organizer-phone">Phone:</dt>
				<dd itemprop="telephone" class="event-meta event-meta-phone">(555) 555-5555</dd>
				<dt class="event-label event-label-email">Email:</dt>
				<dd itemprop="email" class="event-meta event-meta-email"><a href="#">email@website.com</a></dd>
				<dt class="event-label event-label-updated">Updated:</dt>
				<dd class="event-meta event-meta-updated"><span class="date updated">February 24, 2012</span></dd>
			</dl>
			<dl class="column" itemprop="location" itemscope itemtype="http://schema.org/Place">
				<dt class="event-label event-label-venue">Venue:</dt>
				<dd itemprop="name" class="event-meta event-meta-venue">
					City Hall
				</dd>
				<dt class="event-label event-label-address">
					Address:<br />
					<a class="gmap" itemprop="maps" href="http://maps.google.com/maps?f=q&#038;source=s_q&#038;hl=en&#038;geocode=&#038;q=121+N+La+Salle+St+Chicago+IL+60602+United+States" title="Click to view a Google Map" target="_blank">Google Map</a>
				</dt>
				<dd class="event-meta event-meta-address">
					<div itemprop="address" itemscope itemtype="http://schema.org/PostalAddress">
						<span itemprop="streetAddress">121 N La Salle St</span>, <span itemprop="addressRegion">Chicago, IL</span>, <span itemprop="postalCode">60602</span>, <span itemprop="addressCountry">United States</span>
					</div>
				</dd>
			</dl>
		</div>
		<div class="entry">
			<div class="summary">
				<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Morbi commodo, ipsum sed pharetra gravida, orci magna rhoncus neque, id pulvinar odio lorem non turpis. Nullam sit amet enim. Suspendisse id velit vitae ligula volutpat condimentum. Aliquam erat volutpat.
				</p>
			</div>
		</div>
		<div class="clear"></div>
	</div>
	<div class="clear"></div>
</div>
<!-- end .posts-wrap -->
<?php include('includes/sidebar.php'); ?>
<?php include('includes/footer.php'); ?>