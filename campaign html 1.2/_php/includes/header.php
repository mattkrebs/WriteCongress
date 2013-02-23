<?php // gets the page file name, this later is called to place the slider on the home page only
$currentFile = $_SERVER["PHP_SELF"];
$parts = Explode('/', $currentFile);
($page_file_name = ($parts[count($parts) - 1])); ?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" dir="ltr" lang="en-US">
	<head>
		<title>Campaign HTML Template</title>
		<meta name="Keywords" content=" " />
		<meta name="Description" content=" " />
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
		
		<!-- CSS -->
		<link rel="stylesheet" href="css/style.css" type="text/css" media="screen" />
		<!-- The next line calls the font for the headings. Use it like this: "style-fontname.css". Options are bitter, droidsans, droidserif, franchise, museo, nevis, or rokkitt */ -->
		<link rel="stylesheet" type="text/css" href="fonts/style-nevis.css" media="screen" />
		<link rel="stylesheet" type="text/css" href="includes/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
		
		<!-- The Favicon -->
		<link rel="shortcut icon" href="images/favicon.png" />
	</head>
	<!-- options for the body classes are // content_left, content_right // bg_linen, bg_freckles, bg_cork, bg_fabric, bg_pinstripes, bg_none // body_boxed, body_span // scheme_blue, scheme_red, scheme_green, scheme_yellow -->
	<body class="content_left bg_linen body_boxed scheme_blue <?php if ($page_file_name == "page-full.php") { ?>no_sidebar<?php } ?>">
		<div id="main_wrap">
			<div class="wrapper" id="header">
				<div id="pre_header"></div>
				<div class="container">
					<div id="logo_wrap">
						<div id="the_logo">
							<a href="#" title="Campaign" class="left the_logo">
								<img src="images/campaignlogo.png" alt="Campaign" id="logo" />
							</a>
						</div>
						<form method="get" id="searchform" action="#">
							<div>
								<input type="text" class="search_input" value="Search" name="s" id="s" onfocus="if (this.value == 'Search') {this.value = '';}" onblur="if (this.value == '') {this.value = 'Search';}" />
								<input type="submit" id="searchsubmit" value="Search" />
								<div class="clear"></div>
							</div>
						</form>
						<div class="clear"></div>
					</div>
					<div id="clear"></div>
				</div>
				<div id="main_menu">
					<div class="container">
						<div id="main_menu_wrap">
							<div class="menu-main-container">
								<ul id="menu-main" class="menu">
									<li><a href="index.php">Home</a></li>
									<li><a href="#">Drop Downs +</a>
										<ul class="sub-menu">
											<li><a href="#">Child +</a>
												<ul class="sub-menu">
													<li><a href="#">GrandChild</a></li>
													<li><a href="#">GrandChild</a></li>
													<li><a href="#">GrandChild</a></li>
												</ul>
											</li>
											<li><a href="#">Menu Child +</a>
												<ul class="sub-menu">
													<li><a href="#">The Drop Down</a></li>
													<li><a href="#">Flyouts +</a>
														<ul class="sub-menu">
															<li><a href="#">Are Endless +</a>
																<ul class="sub-menu">
																	<li><a href="#">Make As Many</a></li>
																	<li><a href="#">As You Want</a></li>
																</ul>
															</li>
														</ul>
													</li>
												</ul>
											</li>
										</ul>
									</li>
									<li><a href="blog.php">Blog</a></li>
									<li><a href="page.php">Page</a></li>
									<li><a href="page-contact.php">Contact</a></li>
									<li><a href="#">More Page Layouts +</a>
										<ul class="sub-menu">
											<li><a href="page-extras.php">Extras</a></li>
											<li><a href="page-full.php">Full Width</a></li>
										</ul>
									</li>
								</ul>
							</div>
							<a id="donate_now" class="button" href="#" title="Make A Donation">Make A Donation</a>
						</div>
					</div>
				</div>
			</div>
			<div class="wrapper" id="content"> <!-- #content ends in footer.php -->
				<div class="container">