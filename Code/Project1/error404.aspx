<%@ Register TagPrefix="includeControlSitemap" TagName="sitemap" Src="includes/sitemap.ascx" %>
<%@ Register TagPrefix="includeControlerro404" TagName="erro404" Src="includes/erro404.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.error404" Codebehind="error404.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Bluegreen Vacation Club</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK rev="stylesheet" href="styles.css" rel="stylesheet">
		<script language="javascript" src="scripts/rollover.js"></script>
	</HEAD>
	<body>
		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td vAlign="top" width="50%"><IMG height="10" src="images/blank.gif" width="10" border="0"></td>
				<td vAlign="top" width="740">
					<table width="740" border="0" cellspacing="0" cellpadding="0">
                    	<tr>
                    		<td align="center"><table width="730" height="70" border="0" cellpadding="0" cellspacing="0">
                    				<tr>
                    					<td align="left" valign="middle"><a href="default.aspx"><img src="images/ResortsLogo.gif" alt="Bluegreen Resorts" width="271" height="20" border="0" /></a></td>
                    					<td align="right"></td>
               					</tr>
                    				</table>
                        			<img src="images/no_nav.gif" width="740" height="25"></td>
                   		</tr>
                    	</table>
					<p class="text" ><img src="images/404_error.gif" width="740" height="42"><br>
						<br>
						We're sorry, the page you have requested cannot be found. You may have used an outdated link or may have typed the address (URL) incorrectly.<br>
					You might find what you're looking for in one of these areas: </p>
                    <includecontrolsitemap:sitemap id="Sitemap" runat="server"></includecontrolsitemap:sitemap>
					<!-- end site map -->
					<!-- end page content -->
					<!-- footer -->
					<div align="center">
						<p><IMG height="10" src="images/blank.gif" width="740" border="0"><br>
						<IMG height="1" src="images/rule999.gif" width="740" border="0"></p>
						<p class="foottext"><A class="foottextlink" onclick="newWin=window.open('privacy.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
								href="#">Privacy</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onclick="newWin=window.open('terms.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
								href="#">Terms</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onclick="newWin=window.open('legal.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
								href="#">Legal</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" href="corporate/index.aspx">
								About Us</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" href="default.aspx">
								Home</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" href="sitemap.aspx">Site 
								Map</A></p>
						<p class="copytext">ENTIRE SITE COPYRIGHT ©<%= year(today()) %>
							BLUEGREEN VACATIONS UNLIMITED, INC. ALL RIGHTS RESERVED.</p>
						<p><IMG  src="images/logo-footer.gif" width="258" height="13" border="0"></p>
					</div>
				</td>
				<td vAlign="top" width="50%"><IMG height="10" src="images/blank.gif" width="10" border="0"></td>
			</tr>
		</table>
		<!-- end footer --></body>
</HTML>
