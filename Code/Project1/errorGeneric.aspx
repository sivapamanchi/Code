<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.errorGeneric" Codebehind="errorGeneric.aspx.vb" %>
<%@ Register TagPrefix="includeControlSitemap" TagName="sitemap" Src="includes/sitemap.ascx" %>
<%@ Register TagPrefix="includeControlerrorGen" TagName="errorGen" Src="includes/errorGeneric.ascx" %>
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
				<td vAlign="top" width="50%" background="images/smBkgd1_lft.gif"><IMG height="10" src="images/blank.gif" width="10" border="0"></td>
				<td vAlign="top" width="740">
					<includecontrolerrorGen:errorGen id="errorGen" runat="server"></includecontrolerrorGen:errorGen>
					<includecontrolsitemap:sitemap id="Sitemap" runat="server"></includecontrolsitemap:sitemap>
					<!-- end site map -->
					<!-- end page content -->
					<!-- footer -->
					<div align="center">
						<p><IMG height="30" src="images/blank.gif" width="740" border="0"><br>
							<IMG height="1" src="images/rule999.gif" width="740" border="0"></p>
						<p class="foottext"><A class="foottextlink" onclick="newWin=window.open('privacy.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
								href="#">Privacy</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onclick="newWin=window.open('terms.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
								href="#">Terms</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onclick="newWin=window.open('legal.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
								href="#">Legal</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" href="corporate/index.aspx">
								About Us</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" href="default.aspx">
								Home</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" href="sitemap.aspx">Site 
								Map</A></p>
						<p class="copytext">ENTIRE SITE COPYRIGHT ©<%= year(today()) %>
							BLUEGREEN CORPORATION. ALL RIGHTS RESERVED.</p>
						<p><IMG height="21" src="images/bxg_sml_999.gif" width="65" border="0"></p>
					</div>
				</td>
				<td vAlign="top" width="50%"><IMG height="10" src="images/blank.gif" width="10" border="0"></td>
			</tr>
		</table>
		<!-- end footer --></body>
</HTML>
