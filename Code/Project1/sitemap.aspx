<%@ Register TagPrefix="includeControlSitemap" TagName="sitemap" Src="includes/sitemap.ascx" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>
<%@ Register TagPrefix="includeSiteMapHeader" TagName="siteMapHeader" Src="includes/siteMapHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.sitemap" Codebehind="sitemap.aspx.vb" %>
<%@ Reference Control="~/includes/ucmenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="includes/ucMenu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<HTML>
	<HEAD>
		<title>Site Map - OWN</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<link rel="stylesheet" rev="stylesheet" href="main.css">	
		<link rel="stylesheet" rev="stylesheet" href="owner/owner.css">	
 	   <link href="css/ucmenu.css" rel="stylesheet" type="text/css" />
	</HEAD>
		<body>
		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td vAlign="top" width="50%"><IMG height="10" src="images/blank.gif" width="10" border="0"></td>
				<td id="page-title" width="740" align="left" vAlign="top">
									<table width="740" border="0" cellspacing="0" cellpadding="0">
                    	<tr>
                    		<td align="center"><table width="730" height="70" border="0" cellpadding="0" cellspacing="0">
                    				<tr>
                    					<td align="left" valign="middle"><a href="javascript:history.go(-1)" onMouseOver="self.status=document.referrer;return true"><img src="<%= sHeaderImage %>" alt="Bluegreen Resorts" width="271" height="20" border="0" /></a></td>
                    					<td align="right"><table border="0" cellspacing="6" cellpadding="0">
                    							<%--<tr>
                    								<td align="right"><table border="0" cellspacing="0" cellpadding="0">
                    										<tr>
                    											<td width="36" align="left"><a href="corporate/index.aspx"><img src="images/go.gif" width="28" height="28" border="0"></a></td>
                    											<td width="36" align="left"><a href="explore/home.aspx"><img src="images/see.gif" width="28" height="28" border="0"></a></td>
                    											<td><a href="owner/home.aspx"><img src="images/do.gif" width="28" height="28" border="0"></a></td>
               											</tr>
                    										</table></td>
               								</tr>--%>
                    							<tr>
                    								<td align="right"><td align="right">
                                    <img src="Owner/images/phone.gif" alt="Phone us at 800.456.2582" border="0" />
                                </td></td>
               								</tr>
                    							</table></td>
               					</tr>
                    				</table>
                        			<img src="images/no_nav.gif" width="740" height="25"></td>
                   		</tr>
                    	</table>
				
				
				<h2 id="sitemap">Site Map</h2>
					<!-- end header bar and title -->
					<!-- page content -->
					<!-- site map -->
					<div align="left"><includecontrolsitemap:sitemap id="sitemap" runat="server"></includecontrolsitemap:sitemap></div>
					<!-- end site map -->
					<!-- end page content -->
					<!-- footer -->
					
				</td>
				<td vAlign="top" width="50%"><IMG height="10" src="images/blank.gif" width="10" border="0"></td>
			</tr>
		</table>
<includeControlFooter:footer id="footer" runat="server"></includeControlFooter:footer>
		</body>
</HTML>
