<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="siteMaintenance.aspx.vb" Inherits="BGO.BluegreenOnline.siteMaintenance" %>
<!DOCTYPE html>
<!--[if lt IE 9 ]><html itemscope itemtype="http://schema.org/WebPage" lang="en" class="ie ie8 no-js"><![endif]-->
<!--[if IE 9 ]>	 <html itemscope itemtype="http://schema.org/WebPage" lang="en" class="ie ie9 no-js"><![endif]-->
<!--[if (gte IE 9)|!(IEMobile)|!(IE)]><!--><html itemscope itemtype="http://schema.org/WebPage" class="no-js"><!--<![endif]-->
<head>
<meta http-equiv="X-UA-Compatible" content="IE=9,IE=10">
<meta charset="UTF-8">
<title>Maintenance Page | Bluegreen Owner</title>
<meta http-equiv="cleartype" content="on">
<meta http-equiv="imagetoolbar" content="no">
<meta name="MobileOptimized" content="320">
<meta name="HandheldFriendly" content="True">
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
<meta name="theme-color" content="#ffffff">

<link rel="manifest" href="images/favicon/manifest.json">
<link rel="icon" type="image/png" sizes="16x16"   href="images/favicon/favicon-16x16.png">
<link rel="icon" type="image/png" sizes="32x32"   href="images/favicon/favicon-32x32.png">
<link rel="icon" type="image/png" sizes="96x96"   href="images/favicon/favicon-96x96.png">
<link rel="icon" type="image/png" sizes="194x194" href="images/favicon/favicon-194x194.png">
<link rel="icon" type="image/png" sizes="192x192" href="images/favicon/android-chrome-192x192.png">
<!--[if IE]><link rel="shortcut icon" href="images/favicon/favicon.ico"><![endif]-->
<link rel="apple-touch-icon" sizes="57x57"	 href="images/favicon/apple-touch-icon-57x57.png">
<link rel="apple-touch-icon" sizes="60x60"	 href="images/favicon/apple-touch-icon-60x60.png">
<link rel="apple-touch-icon" sizes="72x72"	 href="images/favicon/apple-touch-icon-72x72.png">
<link rel="apple-touch-icon" sizes="76x76"	 href="images/favicon/apple-touch-icon-76x76.png">
<link rel="apple-touch-icon" sizes="114x114" href="images/favicon/apple-touch-icon-114x114.png">
<link rel="apple-touch-icon" sizes="120x120" href="images/favicon/apple-touch-icon-120x120.png">
<link rel="apple-touch-icon" sizes="144x144" href="images/favicon/apple-touch-icon-144x144.png">
<link rel="apple-touch-icon" sizes="152x152" href="images/favicon/apple-touch-icon-152x152.png">
<link rel="apple-touch-icon" sizes="180x180" href="images/favicon/apple-touch-icon-180x180.png">
<link rel="mask-icon" href="images/favicon/safari-pinned-tab.svg" color="#0099cc">
<meta name="apple-mobile-web-app-capable" content="no">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="apple-mobile-web-app-title" content="Bluegreen Vacations">
<meta name="msapplication-TileColor" content="#0099cc">
<meta name="msapplication-TileImage" content="images/favicon/mstile-144x144.png">
<meta name="msapplication-config" content="images/favicon/browserconfig.xml">
<meta name="application-name" content="Bluegreen Vacations">

<link rel="stylesheet" type="text/css" href="assets/css/reset.css" media="all">
<link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" media="screen">
<link rel="stylesheet" type="text/css" href="assets/fonts/cartogothic/style.css?ver=1.0" media="screen">
<link rel="stylesheet" type="text/css" href="style.css" media="all">

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js?ver=1.11.2"></script>

<style>
	.site-content.cover-image { background-image: url( 'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-outdoor-vacation-father-son-fishing?$bgv-hero-home$' );}
</style>
</head>
<body>
<a href="#site-content" class="sr-only sr-only-focusable skip-to-content" title="Skip to the content">Skip to the content</a>


<header class="site-header navbar navbar-fixed-top" id="top" role="banner" itemscope="itemscope" itemtype="http://schema.org/WPHeader">
	<div class="container">
		<span itemscope itemtype="http://schema.org/Organization" class="site-brand">
			<a href="login.html" class="navbar-brand" rel="home">Bluegreen Vacations</a>
			<link itemprop="logo" content="http://s7.bluegreenvacations.com/is/image/BGV/bluegreen-vacations-logo-left-65K-1?$png-alpha$&wid=1000">
			<meta itemprop="url" content="https://www.bluegreenowner.com/">
			<meta itemprop="legalName" content="Bluegreen Vacations Unlimited, Inc.">
		</span>
	</div><!-- .container -->
</header>


<div class="site-body">
	<div class="alert alert-upgrade alert-dismissable row-space-0" role="alert">
		<button type="button" class="close" data-dismiss="alert" aria-label="Close">
			<span aria-hidden="true">&times;</span>
		</button>
		<div class="container">
			<div class="row">
				<div class="col-xs-12 col-md-8 col-lg-9">
					<p><strong>Oh no! Your browser is not supported.</strong> This website will not function properly unless you update to a newer version of Internet Explorer. Also consider using a different browser like <a href="https://www.google.com/intl/en/chrome/browser/" target="_blank" class="alert-link">Google Chrome</a>, <a href="http://www.apple.com/safari/" target="_blank" class="alert-link">Apple Safari</a> or <a href="http://www.mozilla.org/en-US/firefox/new/" target="_blank" class="alert-link">Mozilla Firefox</a>.</p>
				</div>
				<div class="col-xs-12 col-md-4 col-lg-3">
					<a href="http://windows.microsoft.com/en-us/internet-explorer/download-ie" class="pull-right-md pull-right-lg btn btn-danger btn-knockout" target="_blank">Update Internet Explorer</a>
				</div>
			</div><!-- .row -->
		</div><!-- .container -->
	</div><!-- .alert-upgrade -->

	<div class="site-content cover-image" id="site-content">
		<div class="container">
			<div class="row">
				<div class="col-xs-12 col-md-7 col-lg-6">

					<div class="panel panel-default">
						<div class="panel-body">
							<h1 class="h1 text-lowercase">Our site is currently down for maintenance</h1>
							<p>We apologize for the inconvenience. If you need further assistance, please contact us at <strong>800.456.2582</strong>, 8 am to 9 pm Monday-Friday and Saturday 9 am to 5:30 pm.</p>
							

						</div><!-- .panel-body -->
					</div><!-- .panel -->

				</div>
			</div><!-- .row -->
		</div><!-- .container -->
	</div><!-- .site-content -->
</div><!-- .site-body -->



<footer id="colophon" class="site-footer" role="contentinfo" itemscope itemtype="http://schema.org/WPFooter">
	<div class="container">
		<hr>
		<div class="row" itemscope itemtype="http://schema.org/WPSideBar">
			<div class="col-xs-12">
				<div class="text-center copyright">
					<p><img src="http://s7.bluegreenvacations.com/is/image/BGV/bluegreen-vacations-logo-share-happiness-left-65K?$png-alpha$&amp;wid=290" class="scene7-image" alt="Bluegreen Vacations | Share Happiness"></p>
					<a href="#view-menu" data-toggle="collapse" data-target="#site-footer-nav" class="hidden-md hidden-lg btn btn-sm btn-default collapsed">
						<span class="view-text">View Menu</span>
						<span class="collapse-text">Close Menu</span>
					</a>
					<ul id="site-footer-nav" class="site-footer-nav collapse navbar-collapse list-inline list-inline-thin list-inline-piped">
						<li><a href="https://www.bluegreenvacations.com/about/" target="_blank">About Us</a></li>
						<li><a href="https://www.bluegreenvacations.com/privacy-policy/" target="_blank">Privacy</a></li>
						<li><a href="https://www.bluegreenvacations.com/terms-of-use/" target="_blank">Terms</a></li>
						<li><a href="https://www.bluegreenvacations.com/legal-notices/" target="_blank">Legal</a></li>
						<li><a href="http://colorfulplaces.com/" target="_blank">Colorful Places</a></li>
					</ul>
					<p><img src="http://s7.bluegreenvacations.com/is/image/BGV/equal-housing-logo-black?$png-alpha$&amp;op_colorize=999999&amp;wid=24" class="logo-eho" alt="Equal Housing Opportunity"> &copy; 
                        <script type="text/javascript">
                            var currentTime = new Date()
                            var year = currentTime.getFullYear()
                            document.write(year);
                        </script> 
                        Bluegreen Vacations Unlimited, Inc. All rights reserved. 4960 Conference Way North, Suite 100, Boca Raton, FL 33431 United States</p>

				</div><!-- .copyright -->
			</div>
		</div><!-- .row -->

	</div><!-- .container -->
</footer><!-- .footer -->


<script type="text/javascript" src="assets/js/third-party/modernizr.js"></script>
<script type="text/javascript" src="assets/js/third-party/bootstrap/bootstrap.js"></script>
<script type="text/javascript" src="assets/js/script.js"></script>

</body>
</html>