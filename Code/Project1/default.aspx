
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline._default" Codebehind="default.aspx.vb" %>
<!DOCTYPE >
<!--[if lt IE 9 ]><html itemscope itemtype="https://schema.org/WebPage" lang="en" class="ie ie8 no-js"><![endif]-->
<!--[if IE 9 ]>	 <html itemscope itemtype="https://schema.org/WebPage" lang="en" class="ie ie9 no-js"><![endif]-->
<!--[if (gte IE 9)|!(IEMobile)|!(IE)]><!--><html itemscope itemtype="https://schema.org/WebPage" class="no-js"><!--<![endif]-->
<head>
<meta http-equiv="X-UA-Compatible" content="IE=9,IE=10">
<meta charset="UTF-8">
<title>Sign In | Bluegreen Owner</title>
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
	/* If JavaScript is disabled, load this image */
	.no-js .site-content.cover-image { background-image: url( 'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-christmas-mountain-village-cabin-night?$bgv-hero-home$' );}
</style>
    <script type="text/javascript">
	<!--
    //Make sure this page is not SSL
    //var locprot = new String(window.location.protocol);
    //if (locprot == 'https:') {
    //	window.location.replace(window.location.href.replace('https:', 'http:'));
    //}
    //Automatically submit the login form if someone is loggin in from another area
    function SubMe() {
        if ((document.frmPerformLogin.txtEmail.value != 'Email Address') && (document.frmPerformLogin.txtPassword.value != '')) {
            document.frmPerformLogin.submit();
        }
    }
    //-->
	</script>
	<script type="text/javascript">
        	

	    /* Find the querystring to see if the user is being redirected from 
		mobile banking website, if the user is redirected from mobile website
		make sure the user will not be redirected to m.suntrust.com again */



	    var qString = window.location.search.substring(1);
	    var qCompare = "redirect=true";
	    var mobileagent = navigator.userAgent.toLowerCase();

	    //	alert(mobileagent);

	    /*Functions for creating and reading Cookie*/


	    function createMbCookie(cookieName, cValue) {

	        var cookieExDate = new Date();
	        cookieExDate.setMinutes(cookieExDate.getMinutes() + 10);
	        document.cookie = cookieName + "=" + escape(cValue) + "; expires=" + cookieExDate.toGMTString() + "; path=/";
	    }

	    function getMbCookie(MbCookieName) {
	        if (document.cookie.length > 0) {
	            var cookieBegin = document.cookie.indexOf(MbCookieName + "=");
	            if (cookieBegin != -1) {
	                return 1;
	            }
	        }

	        else {
	            return null;
	        }
	    }

	</script>

    <script type="text/javascript" language="javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="js/theplugin/iealert.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="js/theplugin/iealert/style.css" />
    <script type="text/javascript">
        $(document).ready(function () {

            $('#btnSubmit').click(function () {
                otSetCookie();
                $("#frmPerformLogin").submit();               
            });

            $("body").iealert({
                support: "ie7",
                title: "Oops! Your browser is not supported.",
                text: "The browser you are using to visit Bluegreen Online is out of date.  This website will not function properly unless you upgrade to a newer version of Internet Explorer by clicking the Update Internet Explorer button below. Also consider using other browsers like <a href='http://www.mozilla.org/en-US/firefox/new/' target='_blank'>Firefox</a>, <a href='http://www.apple.com/safari/' target='_blank'>Safari</a> or <a href='https://www.google.com/intl/en/chrome/browser/' target='_blank'>Chrome</a>",
                upgradeTitle: "Update Internet Explorer",
                upgradeLink: "http://windows.microsoft.com/en-us/internet-explorer/download-ie",
                overlayClose: false,
                closeBtn: true
            });

            $("input").keypress(function (e) {
                if (e.which == 13) {
                    $('#btnSubmit').click();
                    return false;
                }
                return true;
            });

        });
    </script>
   
</head>
<body id="randomClassElement">
     <%  If sReferrer = "" Then%>
       
    <%Else%>
         <script type ="text/javascript" >
             //test 2
             var MEDALLIA;
             //debugger;
             if (MEDALLIA === undefined) {
                 MEDALLIA = {};
             }

             MEDALLIA.Intercept = function (m_data, m_callback) {
                 try {
                     var m_Util = MEDALLIA._util,
                         m_that = this,
                         m_config = {
                             qaParam: 'M_QAMODE',
                             qaCookie: 'M_INTERCEPT_QAMODE',
                             quarantineCookie: 'M_INTERCEPT_QUARANTINE',
                             surveyURL: 'https://survey.medallia.com/?bluegreen-webint',
                             daysToQuarantine: null,
                             domain: '.bluegreenowner.com'
                         },
                         sUrl = window.location.href,
                         m_result = null, m_params = {};

                     if (!m_data) {
                         m_data = {};
                     }

                     if (!m_callback && MEDALLIA.Invite) {
                         m_callback = MEDALLIA.Invite;
                     } else {
                         return false;
                     }
                     if (sUrl.indexOf(m_config.qaParam + '=1') > 0) {
                         m_Util.setCookie(m_config.qaCookie, true, 365, arguments[2] || m_config.domain);
                     } else if (sUrl.indexOf(m_config.qaParam + '=0') > 0) {
                         m_Util.setCookie(m_config.qaCookie, false, null, arguments[2] || m_config.domain);
                     }

                     m_params = {
                         "owner_id": m_data.owner_id,
                         "owner_type": m_data.owner_type,
                         "tpstatus": m_data.tpstatus
                     };
                     m_result = function () {
                         m_config.daysToQuarantine = 90.0;

                         // Prescaling throttle and quarantine check
                         // Since we're excluding people here, not including them, we use the greater-than operator.
                         if (Math.random() > .25) {
                             return false;
                         }

                         // Global JavaScript Block from prod
                         // Define functions and/or variables needed by trigger conditions here.

                         // Triggers
                         if ((Math.random() < 1) && (true)) { // Trigger always_true
                             return {
                                 "delay": 0,
                                 "params": {}
                             };
                         }
                         return false;
                     }();

                     if (m_result) {
                         if (m_result.params) {
                             m_Util.merge(m_result.params, m_params);
                         }

                         setTimeout(function () {
                             m_callback.call(m_that,
                                             {
                                                 url: m_config.surveyURL + '&' + m_Util.serialize(m_params),
                                                 quarantine: function (domain, days) {
                                                     m_Util.setCookie(m_config.quarantineCookie, true, days || m_config.daysToQuarantine, domain || m_config.domain);
                                                 }
                                             });
                         }, m_result.delay || 0);
                     }
                 } catch (e) {
                     return false;
                 }
             };

             MEDALLIA._util = {

                 pageSize: function () {
                     var body = document.body,
                         bodyOffsetWidth = body.offsetWidth,
                         bodyOffsetHeight = body.offsetHeight,
                         bodyScrollWidth = body.scrollWidth,
                         bodyScrollHeight = body.scrollHeight,
                         pageDimensions = [0, 0];

                     if (typeof document.documentElement != 'undefined' && typeof document.documentElement.scrollWidth != 'undefined') {
                         pageDimensions[0] = document.documentElement.scrollWidth;
                         pageDimensions[1] = document.documentElement.scrollHeight;
                     }
                     if (bodyOffsetWidth > pageDimensions[0]) {
                         pageDimensions[0] = bodyOffsetWidth;
                     }
                     if (bodyOffsetHeight > pageDimensions[1]) {
                         pageDimensions[1] = bodyOffsetHeight;
                     }
                     if (bodyScrollWidth > pageDimensions[0]) {
                         pageDimensions[0] = bodyScrollWidth;
                     }
                     if (bodyScrollHeight > pageDimensions[1]) {
                         pageDimensions[1] = bodyScrollHeight;
                     }

                     return pageDimensions;
                 },

                 pageWidth: function () {
                     return document.body.scrollWidth;
                 },

                 getWindowHeight: function () {
                     var de = document.documentElement;
                     return self.innerHeight || (de && de.clientHeight) || document.body.clientHeight;
                 },

                 getWindowWidth: function () {
                     var de = document.documentElement;
                     return self.innerWidth || (de && de.clientWidth) || document.body.clientWidth;
                 },

                 scrollX: function () {
                     var de = document.documentElement;
                     return self.pageXOffset || (de && de.scrollLeft) || document.body.scrollLeft;
                 },

                 scrollY: function () {
                     var de = document.documentElement;
                     return self.pageYOffset || (de && de.scrollTop) || document.body.scrollTop;
                 },


                 appendStyle: function (css) {
                     var el = document.createElement('style');
                     el.type = 'text/css';

                     if (el.styleSheet) {
                         el.styleSheet.cssText = css;
                     } else {
                         el.appendChild(document.createTextNode(css));
                     }

                     document.getElementsByTagName('head')[0].appendChild(el);
                 },

                 hasClass: function (el, str) {
                     return el.className.match(new RegExp('(\\s|^)' + str + '(\\s|$)'));
                 },

                 addClass: function (el, str) {
                     if (!this.hasClass(el, str)) { el.className += " " + str; }
                 },

                 removeClass: function (el, str) {
                     if (this.hasClass(el, str)) {
                         var reg = new RegExp('(\\s|^)' + str + '(\\s|$)');
                         el.className = el.className.replace(reg, ' ');
                     }
                 },

                 preventDefault: function (e) {
                     if (e.preventDefault) {
                         e.preventDefault();
                     } else {
                         e.returnValue = false;
                     }
                 },

                 getEvent: function (ev) {
                     return ev || window.event;
                 },

                 getTarget: function (ev) {
                     ev = this.getEvent(ev);

                     var t = ev.target || ev.srcElement;

                     try {
                         if (3 == t.nodeType) {
                             return t.parentNode;
                         }
                     } catch (e) { }

                     return t;
                 },

                 on: function (elem, evt, func, bubble) {
                     bubble = bubble || false;

                     var el = (typeof (elem) == 'string') ? this.get(elem) : elem;

                     if (window.addEventListener) {
                         el.addEventListener(evt, func, bubble);
                         return true;
                     } else if (window.attachEvent) {
                         el.attachEvent('on' + evt, func);
                         return true;
                     } else {
                         return false;
                     }
                 },

                 removeEvent: function (elem, evt, func, bubble) {
                     var el = (typeof (elem) == 'string') ? this.get(elem) : elem;

                     if (window.removeEventListener) {
                         el.removeEventListener(evt, func, bubble);
                         return true;
                     } else if (window.detachEvent) {
                         el.detachEvent('on' + evt, func);
                         return true;
                     } else {
                         return false;
                     }
                 },


                 getCookie: function (name) {
                     var nameEQ = name + "=";
                     var ca = document.cookie.split(';');
                     for (var i = 0; i < ca.length; i++) {
                         var c = ca[i];
                         while (c.charAt(0) === ' ') { c = c.substring(1, c.length); }
                         if (c.indexOf(nameEQ) === 0) { return c.substring(nameEQ.length, c.length); }
                     }
                     return null;
                 },

                 setCookie: function (name, value, days, domain) {
                     var expires;
                     if (days) {
                         var date = new Date();
                         date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                         expires = "; expires=" + date.toGMTString();
                     } else {
                         expires = "";
                     }

                     var cookStr = name + "=" + value + expires + "; path=/";
                     if (domain) {
                         cookStr += '; domain=' + domain;
                     }

                     document.cookie = cookStr;
                 },

                 merge: function (s, r) {
                     for (var p in s) {
                         if (s[p].constructor == Object) {
                             try {
                                 r[p] = arguments.callee(s[p], r[p]);
                             } catch (e) {
                                 r[p] = s[p];
                             }
                         } else {
                             r[p] = s[p];
                         }
                     }
                     return r;
                 },

                 serialize: function (obj) {
                     var str = '';
                     if (typeof (obj) == 'object') {
                         for (var key in obj) {
                             if (obj[key]) {
                                 str += key + '=' + obj[key] + '&';
                             }
                         }
                     }
                     return str.replace(/&$/, '');
                 }
             };

             // Script assumes inclusion with Interceptor and Util
             MEDALLIA.Invite = function (o) {
                 try {

                     if (!(this instanceof arguments.callee)) {
                         return new arguments.callee(o);
                     }

                     var M_util = MEDALLIA._util;

                     var el = document.createElement('div'),
                         mask = el.cloneNode(false),
                         ie6 = /MSIE 6/i.test(navigator.userAgent) || (document.compatMode == 'BackCompat' && /*@cc_on!@*/false),
                         config = {
                             'protocol': 'https:' == document.location.protocol ? 'https://' : 'http://',
                             'lang': 'en',

                             'trans': {
                                 'en': {
                                     'title': "Please Take Our Survey",
                                     'msg': "Thank you for visiting our website.  Would you please give us some feedback about your experience?  Click the 'Continue' button to take the survey or the 'Not Now' button to skip it.",
                                     'no': "Not Now",
                                     'yes': "Continue"
                                 }
                             },

                             'newWindowWidth': 1050,
                             'newWindowHeight': 800,
                             'popunder': true,

                             'optOutDays': 90,
                             'visible': true,
                             'useMask': true,

                             'html': function () {
                                 var str = '<div class="MEDALLIA_panel">';
                                 str += '<div class="MEDALLIA_panel_inner">';
                                 // str += '<img class="MEDALLIA_brand" src="' + this.protocol + 'support.bluegreenowner.com/clear.gif" alt="" width="1" height="1">';
                                 str += '<h1>' + this.trans[this.lang].title + '</h1><br><br><br>';
                                 str += '<p class="MEDALLIA_message">' + this.trans[this.lang].msg + '</p>';
                                 str += '<div class="MEDALLIA_actions"><a href="#" class="MEDALLIA_bt MEDALLIA_continueBtn">' + this.trans[this.lang].yes + '</a><a href="#" class="MEDALLIA_bt MEDALLIA_cancelBtn">' + this.trans[this.lang].no + '</a></div>';
                                 str += '<a class="MEDALLIA_power" title="Learn more about Medallia" href="http://www.medallia.com" target="_blank">Powered by Medallia</a>';
                                 str += '</div>';
                                 str += '</div>';
                                 return str;
                             },
                             'url': '',
                             'css': '.MEDALLIA_mask{visibility:hidden; position:fixed; top:0; left:0; filter:alpha(opacity=80); opacity:.8; background-color:#000; z-index:999990;}.MEDALLIA_overlay{visibility:hidden; font:normal 13px/1 Helvetica,Arial,sans-serif !important; margin:0; padding:0; position:fixed; z-index:999992;}.MEDALLIA_overlay *{margin:0;padding:0;border:0;vertical-align:baseline;font-size:13px !important;color:#666 !important;}.MEDALLIA_panel{position:absolute; width:500px; background-color:#fff;* border:6px solid #00569e !important;text-align:left !important;text-align:left;zoom:1 !important;-moz-box-shadow:0 5px 35px rgba(0,0,0,0.8) !important;-webkit-box-shadow:0 5px 35px rgba(0,0,0,0.8) !important;-o-box-shadow:0 5px 35px #333 !important;box-shadow:0 5px 35px rgba(0,0,0,0.8) !important;}.MEDALLIA_panel, x:-moz-any-link, x:default{border:6px solid #00569e !important;}body:first-of-type .MEDALLIA_panel {border:0 !important;}@media all and (-webkit-min-device-pixel-ratio:10000), not all and (-webkit-min-device-pixel-ratio:0){head~body .MEDALLIA_panel { border:6px solid #00569e !important;}}* html .MEDALLIA_panel{border:6px solid #00569e !important;}.MEDALLIA_panel_inner{border:1px solid #fff !important;padding:30px !important;}.MEDALLIA_brand{margin-bottom:38px !important;}.MEDALLIA_panel p{color:#666 !important;line-height:1.33 !important;}.MEDALLIA_panel h1,.MEDALLIA_panel h2,.MEDALLIA_panel h3,.MEDALLIA_panel h4,.MEDALLIA_panel h5,.MEDALLIA_panel h6{font-size:22px !important;font-weight:bold !important;letter-spacing:-.5px !important;line-height:1.2 !important;margin:6px 0 !important; color:#333 !important;text-transform:none !important;letter-spacing:-.025em !important;}.MEDALLIA_panel h2,.MEDALLIA_panel h3,.MEDALLIA_panel h4,.MEDALLIA_panel h5,.MEDALLIA_panel h6{font-size:18px !important;font-weight:bold !important;letter-spacing:-.5px !important;line-height:1.2 !important;margin:0 0 6px !important; color:#333 !important}.MEDALLIA_panel h3{font-size:16px !important;}.MEDALLIA_panel h4{font-size:14px !important;}.MEDALLIA_panel h5,.MEDALLIA_panel h6{font-size:13px !important;text-transform:uppercase !important;letter-spacing:0 !important;}.MEDALLIA_panel h6{font-size:11px !important;}.MEDALLIA_actions{padding:15px 0 !important;}.MEDALLIA_bt{cursor:pointer !important;display:inline-block !important;font:bold 14px/1 Helvetica,Arial,sans-serif !important;overflow:visible !important;position:relative !important;white-space:normal !important;}.MEDALLIA_bt:active{top:1px !important;}.MEDALLIA_bt::-moz-focus-inner{border:0 !important;padding:0 !important;padding-top:8px !important;}.MEDALLIA_actions .MEDALLIA_continueBtn{background-color:#00569e !important;}.MEDALLIA_continueBtn{background-image:url(//support.bluegreenowner.com/assets/bt-grad.png !important);background-repeat:repeat-x !important;border:0 !important;color:#fff !important;margin-right:10px !important;padding:10px 15px !important;text-decoration:none !important;text-shadow:0 -1px 0 rgba(0,0,0,0.25) !important;-moz-border-radius:5px !important;-webkit-border-radius:5px !important;border-radius:5px !important;-moz-box-shadow:0 1px 2px rgba(0,0,0,0.5) !important;-webkit-box-shadow:0 1px 2px rgba(0,0,0,0.5) !important;box-shadow:0 1px 2px rgba(0,0,0,0.5) !important;}.MEDALLIA_continueBtn:hover{background-color:#006DC9 !important;}.MEDALLIA_cancelBtn{background:none;color:#888;font-weight:normal !important;padding:10px 15px;position:relative;text-decoration:underline;}.MEDALLIA_cancelBtn:hover{color:#222 !important;background:none !important;text-decoration:underline !important;}a.MEDALLIA_power{background:transparent url(//support.bluegreenowner.com/assets/powered-by-medallia.png) no-repeat !important;display:block !important;height:20px !important;margin:0 0 -2px auto !important;overflow:hidden;text-decoration:none !important;text-indent:-9999px !important;width:152px !important;}a.MEDALLIA_power:hover{text-decoration:none !important;}* html .MEDALLIA_bt{background:transparent;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src="//support.bluegreenowner.com/assets/bt-grad.png",sizingMethod="scale");border-width:1px !important;border-color:transparent !important;}* html a.MEDALLIA_power{background:transparent !important;cursor:pointer !important;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src="//support.bluegreenowner.com/assets/powered-by-medallia.png",sizingMethod="crop");}.MEDALLIA_Invite_on select, .MEDALLIA_Invite_on embed, .MEDALLIA_Invite_on object {visibility:hidden !important;}.MEDALLIA_Invite_on .MEDALLIA_overlay, .MEDALLIA_Invite_on .MEDALLIA_mask{visibility:visible !important;}',
                             'timer': -1
                         };

                     M_util.merge(o, config);
                     M_util.appendStyle(config.css);

                     el.className = 'MEDALLIA_overlay';
                     el.innerHTML = config.html();
                     el = document.body.appendChild(el);

                     if (ie6) {
                         el.style.position = 'absolute';
                     }

                     if (config.useMask) {
                         mask.className = 'MEDALLIA_mask';
                         mask = document.body.appendChild(mask);
                         if (ie6) {
                             mask.style.position = 'absolute';
                         }
                     }

                     el.onclick = config.handleClick || function (e) {
                         e = M_util.getEvent(e);
                         var t = M_util.getTarget(e);

                         if (M_util.hasClass(t, 'MEDALLIA_continueBtn')) {
                             M_util.preventDefault(e);
                             var left = (screen.width / 2) - (config.newWindowWidth / 2),
                                 top = (screen.height / 2) - (config.newWindowHeight / 2),
                                 opts = 'scrollbars=1,height=' + config.newWindowHeight + ',width=' + config.newWindowWidth + ',top=' + top + ', left=' + left;

                             var MEDALLIA_WINDOW = window.open(config.url, 'MEDALLIA_WINDOW', opts);

                             if (config.popunder) {
                                 window.focus();
                             } else {
                                 MEDALLIA_WINDOW.focus();
                             }

                             config.quarantine();
                             hide();

                         } else if (M_util.hasClass(t, 'MEDALLIA_cancelBtn')) {
                             M_util.preventDefault(e);
                             config.quarantine(null, config.optOutDays);
                             hide();
                             window.focus();
                         }
                     };

                     function show(o) {
                         if (o) {
                             M_util.merge(o, config);
                         }

                         M_util.on(window, 'resize', synch);

                         if (ie6) {
                             M_util.on(window, 'scroll', function () {
                                 if (!config.timer) {
                                     config.timer = -1;
                                 }

                                 clearTimeout(config.timer);

                                 config.timer = setTimeout(function () {
                                     center();
                                 }, 1);
                             });
                         }

                         M_util.addClass(document.body, 'MEDALLIA_Invite_on');
                     }

                     function hide() {
                         M_util.removeEvent(window, 'resize', synch);

                         if (ie6) {
                             M_util.removeEvent(window, 'scroll', center);
                         }

                         M_util.removeClass(document.body, 'MEDALLIA_Invite_on');
                     }

                     function resizeMask() {
                         var p = ie6 ? M_util.pageSize() : [M_util.getWindowWidth(), M_util.getWindowHeight()];
                         mask.style.width = p[0] + 'px';
                         mask.style.height = p[1] + 'px';
                     }

                     function center() {
                         var w = M_util.getWindowWidth() / 2,
                             h = M_util.getWindowHeight() / 2;

                         if (ie6) {
                             w += M_util.scrollX();
                             h += M_util.scrollY();
                         }

                         p = [w - (500 / 2), h - (el.firstChild.offsetHeight / 2)];

                         el.style.left = p[0] + 'px';
                         el.style.top = p[1] + 'px';
                     }

                     function synch() {
                         if (config.useMask) {
                             resizeMask();
                         }

                         center();
                     }

                     synch();

                     if (config.visible) {
                         show();
                     }

                     return {
                         synch: synch,
                         show: show,
                         hide: hide
                     };

                 } catch (e) {
                     return false;
                 }

             };/*<div id="timeSpentContainer" style="text-align: center"><div style="font-size:xx-small; text-align: left; width: 750px; margin: 0 auto; color:#bbb;">
Struts 17<br>    Process Action 16<br>    Velocity Parse 0<br>    Velocity Render 0<br>Total: 18 ms (1 other)<br>Queries: 18 / 6 ms (4 / 1 ms, 14 / 4 ms, 0 / 0 ms, 0 / 0 ms)</div></div>
*/



             var jsLang = "en_US";
             if (jsLang.toLowerCase().indexOf("en") >= 0) {
                 $(document).ready(function () {
                     setTimeout(function () {
                         MEDALLIA.Intercept({ owner_id: '<%=OwnerId%>', owner_type: '<%=OwnerType%>', tpstatus: '<%=TPStatus%>', lng: 'EN' });
                     }, 1000);
                 })
             }
        </script> 
   <% End If%>



<a href="#site-content" class="sr-only sr-only-focusable skip-to-content" title="Skip to the content">Skip to the content</a>


<header class="site-header navbar navbar-fixed-top" id="top" role="banner" itemscope="itemscope" itemtype="http://schema.org/WPHeader">
	<div class="container">
		<span itemscope itemtype="https://schema.org/Organization" class="site-brand">
			<a href="./explore/home.aspx" class="navbar-brand" rel="home">Bluegreen Vacations</a>
			<link itemprop="logo" content="https://s7.bluegreenvacations.com/is/image/BGV/bluegreen-vacations-logo-left-65K-1?$png-alpha$&wid=1000">
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
				<div class="col-xs-12 col-md-6 col-lg-5">

					<!-- This panel is ONLY displayed after a user logs out and is redirected to this page. -->
					<!-- <div class="panel panel-default">
						<div class="panel-body">
							<h1 class="h1 color-primary text-lowercase"><strong>Give Us Your</strong> Feedback</h1>
							<p>Thank you for visiting Bluegreen Vacations. Would you please give us some feedback about your experience?</p>
							<a href="#" class="btn btn-primary">Take the Survey</a> <a href="#" class="btn btn-link">No Thanks</a>
						</div>
					</div> -->


					<div class="panel panel-default">
						<div class="panel-body">
							<h1 class="h1 color-primary text-lowercase"><strong>Owner</strong> Sign In</h1>

							<!-- This alert is only displayed when required -->
                            <%If sMessage <> "" Then%>
                                     <% Response.Write(sMessage)%>
                            <% Else%>
                                <%If Request.QueryString("error") = "NoACK" Then%>			
                                        <%Response.Write(GetMessage("The email address and/or password you entered does not match our records."))%> 
                                <% End If%>
                            <% End If%>

							<form id="frmPerformLogin" method="post" action="<%= sSSLURL %>loginwait.aspx" autocomplete="off">
                                <input type="hidden" name="IsTutorialTransfer" value="<%= sTutorialTransfer %>" />
				            	<input type="hidden" name="IsTravelerPlusOwner" value="<%= sTravelerPlusLogin %>" />
				            	<input type="hidden" name="IsEncoreRewardsOwner" value="<%= sEncoreRewardsLogin %>" />
					            <input type="hidden" name="AgentLoginID" value='<%= sAgentLoginID %>' />
                   
								<div class="form-group">
									<label class="sr-only" for="user-email">Email address</label>
                                    <input type="text" name="username" style="display:none" value="fake input" />
									<input type="email" name="txtEmail" class="form-control" id="txtEmail" value="<%= sEmail %>" placeholder="email">
								</div><!-- .form-group -->
								<div class="form-group">
									<label class="sr-only" for="user-password">Password</label>
									<input type="password" name="txtPassword" class="form-control" id="txtPassword" value="<%=sPass%>" placeholder="password" maxlength="10">									
								</div><!-- .form-group -->
								<div class="checkbox">
									<label><input type="checkbox" name="cbRememberMe" id="cbRememberMe" value="1"> remember me</label>
								</div><!-- .checkbox -->
                                <button class="btn btn-primary btn-block" id="btnSubmit" type="button"  >Sign In</button>								                              
							</form>
							<p><a href="forgotPassword.aspx"><strong>forgot password?</strong></a></p>
						</div>
					</div>


					<div class="panel panel-default">
						<div class="panel-body">
							<h1 class="hidden h1 text-lowercase"><strong>Not Registered?</strong></h1>
							<h1 class="h1 text-lowercase"><strong>Owner</strong> Registration</h1>
							<p>If you're a Bluegreen Vacations Owner, but haven't registered before, there's no better time than now!</p>
							<p>Once you register you can take full advantage of your Bluegreen Vacations ownership, online:</p>
							<ul class="list-disc">
								<li>Book Points and Bonus Time reservations</li>
								<li>Earn rewards with Bluegreen Rewards referrals</li>
								<li>Enjoy exclusive owner offers and promotions</li>
							</ul>
							<p><a href="<%=System.Configuration.ConfigurationManager.AppSettings("bxgwebSecureURL") %>register.aspx" class="btn btn-default btn-block">Register Today</a></p>
							<p><a href="owner/popLoginHelp.aspx" class="font-size-16"><strong>need help?</strong></a></p>

							<hr class="hr-thin">

							<p><strong>Not an owner?</strong>
								<br>Let a fun-filled family getaway serve as your introduction to the Bluegreen Vacation Club. <a href="http://www.bluegreengetaways.com/home?lo=136275&utm_source=bgo&utm_medium=loginpg&utm_campaign=bgobgg" target="_blank">Learn more</a></p>

						</div>
					</div>


				</div>
			</div><!-- .row -->
		</div><!-- .container -->
	</div><!-- .site-content -->
</div><!-- .site-body -->


<footer id="colophon" class="site-footer" role="contentinfo" itemscope itemtype="https://schema.org/WPFooter">
	<div class="container">
		<hr>
		<div class="row" itemscope itemtype="https://schema.org/WPSideBar">
			<div class="col-xs-12">
				<div class="text-center copyright">
					<p><img src="https://s7.bluegreenvacations.com/is/image/BGV/bluegreen-vacations-logo-share-happiness-left-65K?$png-alpha$&amp;wid=290" class="scene7-image" alt="Bluegreen Vacations | Share Happiness"></p>
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
					<p><img src="https://s7.bluegreenvacations.com/is/image/BGV/equal-housing-logo-black?$png-alpha$&amp;op_colorize=999999&amp;wid=24" class="logo-eho" alt="Equal Housing Opportunity"> &copy; <script type="text/javascript">
					                                                                                                                                                                                                 var currentTime = new Date()
					                                                                                                                                                                                                 var year = currentTime.getFullYear()
					                                                                                                                                                                                                 document.write(year);
</script> Bluegreen Vacations Unlimited, Inc. All rights reserved. 4960 Conference Way North, Suite 100, Boca Raton, FL 33431 United States</p>

				</div><!-- .copyright -->
			</div>
		</div><!-- .row -->

	</div><!-- .container -->
</footer><!-- .footer -->


<script type="text/javascript" src="assets/js/third-party/modernizr.js"></script>
<script type="text/javascript" src="assets/js/third-party/bootstrap/bootstrap.js"></script>
<script type="text/javascript" src="assets/js/script.js"></script>
<script>
    document.getElementById('txtEmail').focus();

    var background_images = [
    //	'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-christmas-mountain-village-cabin-night?$bgv-hero-home$',
        'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-beach-kids-playing-shells?$bgv-hero-home$',
        'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-outdoor-vacation-father-son-fishing?$bgv-hero-home$',
        'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-cibola-vista-pool-exterior-view?$bgv-hero-home$',
        'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-seaglass-tower-interior-living-room?$bgv-hero-home$',
        'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-shenandoah-crossing-exterior-yurt?$bgv-hero-home$',
        'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-studio-homes-living-room?$bgv-hero-home$',
        'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-the-soundings-seaside-resort-exterior-beach?$bgv-hero-home$',
        'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-the-fountains-family-balcony?$bgv-hero-home$',
        'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-wilderness-club-exterior-night?$bgv-hero-home$',
        'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-wilderness-club-exterior-pool?$bgv-hero-home$',
        'https://s7.bluegreenvacations.com/is/image/BGV/bgo-homepage-winter-vacation-couple-snow?$bgv-hero-home$'
    ];

    var length = background_images.length,
        image = Math.floor(length * Math.random())

    document.getElementById('site-content').style.backgroundImage = "url( '" + background_images[image] + "' )";
</script>
</body>
</html>
<script language="javascript" type="text/javascript">
    var BXGCookie = new String("BGXRememberMe");
    otCheckRemCookie();
    function otCheckRemCookie() {
	<% if (sEmail = "") then %>
        var BXGCookie = new String("BGXRememberMe");
        var remCookie = new String(document.cookie);
        var ttxtEmail = document.getElementById("txtEmail");
        var pos = remCookie.indexOf(BXGCookie);
        if (pos != -1) {
            var pos2 = remCookie.indexOf(";", pos + 1);
            var pos1 = pos + BXGCookie.length + 1;
            if (pos2 != -1)
                ttxtEmail.value = remCookie.substr(pos1, (pos2 - pos1));
            else
                ttxtEmail.value = remCookie.substr(pos1);
            if (ttxtEmail.value != "")
                document.getElementById("cbRememberMe").checked = true;
        }
	<% end if %>
    }
    function otSetCookie() {
        var ttxtEmail = document.getElementById("txtEmail");
        if (document.getElementById("cbRememberMe").checked) {
            //if (ttxtEmail.value == "") {
            //    //alert("Please enter a valid username first");
            //    ttxtEmail.focus();
            //    ctrl.checked = false;
            //    return;
            //}
            document.cookie = BXGCookie + "=" + ttxtEmail.value + ";path=/; expires=Tue, 31-Dec-2030 00:00:01 GMT;";
        }
        else {
            document.cookie = "";
            document.cookie = BXGCookie + "=" + ";path=/;  expires=Fri, 31 Dec 1999 23:59:59 GMT;";
        }
    }
</script>