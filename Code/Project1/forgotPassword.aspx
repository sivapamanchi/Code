<%@ Reference Page="~/explore/contact.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.forgotPassword" Codebehind="forgotPassword.aspx.vb" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>
<%@ Reference Control="~/includes/ucmenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="includes/ucMenu.ascx" %>
<!DOCTYPE html>
<!--[if lt IE 9 ]><html itemscope itemtype="https://schema.org/WebPage" lang="en" class="ie ie8 no-js"><![endif]-->
<!--[if IE 9 ]>	 <html itemscope itemtype="https://schema.org/WebPage" lang="en" class="ie ie9 no-js"><![endif]-->
<!--[if (gte IE 9)|!(IEMobile)|!(IE)]><!--><html itemscope itemtype="https://schema.org/WebPage" class="no-js"><!--<![endif]-->
<head>
<meta http-equiv="X-UA-Compatible" content="IE=9,IE=10">
<meta charset="UTF-8">
<title>Forgot Password | Bluegreen Owner</title>
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

<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js?ver=1.11.2"></script>

<style>
	.site-content.cover-image { background-image: url( 'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-daytona-seabreeze-pool?$bgv-hero-home$' );}
</style>
</head>
<body>
    
        <a href="#site-content" class="sr-only sr-only-focusable skip-to-content" title="Skip to the content">Skip to the content</a>


        <header class="site-header navbar navbar-fixed-top" id="top" role="banner" itemscope="itemscope" itemtype="http://schema.org/WPHeader">
            <div class="container">
                <span itemscope itemtype="https://schema.org/Organization" class="site-brand">
                    <a href="default.aspx" class="navbar-brand" rel="home">Bluegreen Vacations</a>
                    <link itemprop="logo" content="https://s7.bluegreenvacations.com/is/image/BGV/bluegreen-vacations-logo-left-65K-1?$png-alpha$&wid=1000">
                    <meta itemprop="url" content="https://www.bluegreenowner.com/">
                    <meta itemprop="legalName" content="Bluegreen Vacations Unlimited, Inc.">
                </span>
            </div>
            <!-- .container -->
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
                    </div>
                    <!-- .row -->
                </div>
                <!-- .container -->
            </div>
            <!-- .alert-upgrade -->


            <div class="site-content cover-image" id="site-content">
                <div class="container">
                    <div class="row">
                        <div class="col-xs-12 col-md-7 col-lg-6">

                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <h1 class="h1 color-primary text-lowercase"><strong>Forgot</strong> Password</h1>
                                     <%If lblErrors.Visible Then%>
                                        <div class="alert alert-danger alert-dismissable" role="alert">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                            <asp:Image id="imgAlert" runat="server" Visible="False" ImageUrl="images/alert.gif"></asp:Image>
								     	    <asp:label id="lblErrors" runat="server" Visible="False" CssClass="alert alert-dismissable" ></asp:label>
                                        </div>
                                     <% End If%>
                                   <p>Don't worry if you've forgotten your Bluegreen Owner password, we will email it to you.</p>
                                    <form id="forgotPasswordForm" runat="server" autocomplete="on" method="post">
                                        <div class="form-group">
                                            <label class="sr-only" for="user-email">Email address</label>
                                            <asp:TextBox  type="email" name="txtEmailAddress" runat="server" class="form-control" id="txtEmailAddress" placeholder="email"></asp:TextBox>
                                        </div>
                                        <!-- .form-group -->

                                        <p>Please provide 1 of the following 2 forms of <strong>the primary account holder's information</strong></p>

                                        <div class="row row-collapse">
                                            <div class="col-xs-12 col-md-4">
                                                <div class="form-group">
                                                    <label class="sr-only" for="user-social-numbers">Last 4 Digits of Social Security number</label>
                                                    <asp:TextBox runat="server"  type="text" name="txtSSN"   class="form-control" id="txtSSN"></asp:TextBox>
                                                    <span class="help-block"><strong>The last 4 digits</strong> of your social security number</span>
                                                </div>
                                                <!-- .form-group -->
                                            </div>
                                            <div class="col-xs-12 col-md-2 text-center-md text-center-lg">
                                                <strong class="form-input-height font-size-18 color-primary">OR</strong>
                                            </div>
                                            <div class="col-xs-12 col-md-6">
                                                <div class="form-group">
                                                    <label class="sr-only" for="user-phone">10 Digit Phone number</label>
                                                    <asp:TextBox runat="server" type="text" name="txtPhone" class="form-control" id="txtPhone"></asp:TextBox>
                                                    <span class="help-block"><strong>Your 10 digit</strong> primary home phone number</span>
                                                </div>
                                                <!-- .form-group -->
                                            </div>
                                        </div>
                                        <!-- .row -->                                        										
                                        <asp:Button runat="server" Text="Send My Password" ID="imgSubmit" CssClass="btn btn-primary btn-block"></asp:Button>                                        
                                    </form>
                                </div>
                                <!-- .panel-body -->
                            </div>
                            <!-- .panel -->

                        </div>
                    </div>
                    <!-- .row -->
                </div>
                <!-- .container -->
            </div>
            <!-- .site-content -->
        </div>
        <!-- .site-body -->


        <footer id="colophon" class="site-footer" role="contentinfo" itemscope itemtype="https://schema.org/WPFooter">
            <div class="container">
                <hr>
                <div class="row" itemscope itemtype="https://schema.org/WPSideBar">
                    <div class="col-xs-12">
                        <div class="copyright">
                            <p>
                                <img src="https://s7.bluegreenvacations.com/is/image/BGV/bluegreen-vacations-logo-share-happiness-left-65K?$png-alpha$&amp;wid=290" class="scene7-image" alt="Bluegreen Vacations | Share Happiness"></p>
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
                            <p>&copy; <script type="text/javascript">
                                          var currentTime = new Date()
                                          var year = currentTime.getFullYear()
                                          document.write(year);
</script> Bluegreen Vacations Unlimited, Inc. All rights reserved. 4960 Conference Way North, Suite 100, Boca Raton, FL 33431 United States</p>
                            <p>All trademarks, service marks, brand names and logos not owned by Bluegreen Corporation remain the property of their respective owners.</p>
                            <p class="text-uppercase">
                                <img src="https://s7.bluegreenvacations.com/is/image/BGV/equal-housing-logo-black?$png-alpha$&amp;op_colorize=999999&amp;wid=36" class="pull-right logo-eho" alt="Equal Housing Opportunity"><strong>This advertising material is being used for the purpose of soliciting the sale of timeshare interests.</strong>
                                <br>
                                <strong>This advertising material is being used for the purpose of soliciting sales of a vacation ownership plan.</strong>
                                <br>
                                <strong>This advertising material is being used for the purpose of soliciting the sale of time-share property or interests in time-share property.</strong>
                            </p>

                        </div>
                        <!-- .copyright -->
                    </div>
                </div>
                <!-- .row -->

            </div>
            <!-- .container -->
        </footer>
        <!-- .footer -->

        <a href="#site-content" class="sr-only sr-only-focusable skip-to-content" title="Skip to the content" tabindex="1">Skip to the content</a>

        <script type="text/javascript" src="assets/js/third-party/modernizr.js"></script>
        <script type="text/javascript" src="assets/js/third-party/bootstrap/bootstrap.js"></script>
        <script type="text/javascript" src="assets/js/script.js"></script>
        <script>
            document.getElementById('txtEmailAddress').focus();
</script>  
</body>
</html>