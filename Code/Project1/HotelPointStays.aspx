<%@ Reference Control="~/includes/ucmenu.ascx" %>
<%@ Register TagPrefix="includeControlBannerRotation" TagName="BannerRotation" Src="~/TravelerPlus/includes/bannerRotation.ascx" %>
<%@ Register TagPrefix="includeControlOwnerPalette" TagName="OwnerPalette" Src="~/TravelerPlus/includes/OwnerPalette.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="~/includes/ucMenu.ascx" %>
<%@ Register TagPrefix="includeControlTPMenu" TagName="TPMenus" Src="~/TravelerPlus/includes/tpDropDownMenu.ascx" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="~/TravelerPlus/includes/footer.ascx" %>
<%@ Register TagPrefix="traveler" TagName="nav" Src="~/TravelerPlus/includes/TPleftNav.ascx" %>
<%@ Page Language="VB" AutoEventWireup="false" Inherits="BluegreenOnline.TravelerPlus_owner_HotelPointStays" Codebehind="HotelPointStays.aspx.vb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Hotel PointStays</title>
<link rel="stylesheet" type="text/css" href="/TravelerPlus/owner/owner.css" />
<link rel="stylesheet" type="text/css" href="/css/ucmenu.css" />
<link rel="stylesheet" type="text/css" href="/TravelerPlus/owner/css/style.css" /> 

<script language=javascript>
function goThere(url, width, height) { 
	if (url.match(/^-/)) { 
		url = url.replace(/^-/, "");
			if ((!newWindow) || (newWindow.closed == true)) { 
				var newWindow = window.open(url, 'newWin', 'width=' + width + ',height=' + height);
				newWindow.focus();
			} else { 
				newWindow.location.href = url;
				newWindow.focus();
			}
		} else { 
			self.location.href = url;
		}
	}
</script>
</head>
<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">

<uc1:ucmenu id="UcMenu1" runat="server"></uc1:ucmenu>

<table cellSpacing="0" cellPadding="0" border="0" width="730" align="center" style="padding-top:15px;">
	<tr>
		<td valign="top" width="185">
			<traveler:nav ID="lNav" runat="server" />
  <a href="https://bluegreenowner.com/travelerplus/owner/ownerAdventures.aspx">
                                <img height="150" src="/TravelerPlus/owner/images/oa-down-under.jpg" width="180" border="0" alt="" />
                            </a>      
			<br />
			<br /><includecontrolbannerrotation:bannerrotation id="BannerRotation" runat="server"></includecontrolbannerrotation:bannerrotation>
		</td>
		<td width="48"><img src="images/blank.gif" width="48" /></td>
		<td valign="top" align="left" width="497">
			<table cellSpacing="0" cellPadding="0" border="0" width="100%">
				<tr>
					<td class="bcrumb" valign="top">
						<a class="bcrumblink" href='<%= Session("UnsecuredURL") %>default.aspx'>Bluegreen</a> / 
						<a class="bcrumblink" href="TravelerPlus/owner/home.aspx">Owners</a> / 
						<a class="bcrumblink" href="home.aspx">Traveler Plus</a> / 
						Hotel PointStays</td>
				</tr>
				<tr>
					<td id="page-title" width="497">
						<h2 id="travelerplus-HotelPointStays">Hotel PointStays</h2></td>
				</tr>
				<tr>
					<td valign="bottom" width="493">
						<h2>Get more mileage out of your Points and expand your vacation horizons with this special Traveler Plus benefit.</h2>
						<p><img src="/TravelerPlus/owner/images/photo-Hotel01.jpg" alt="Hotel PointStays" width="165" height="200" align="right" />As a Bluegreen Traveler Plus member, we know your travels take you across the country and around the world. Use your Bluegreen Vacation Points for stays in participating 4- and 5-star hotels throughout the U.S. and abroad, and enjoy hospitality from Amsterdam to Alaska, Britain to Brazil, or California to Connecticut.</p>
						<p>Whether you're looking for chic boutique accommodations or an upscale, luxury hotel  look no further than Hotel PointStays. browse through our vast selection before you call to make your reservation. Become more worldly as you travel overseas to stylish hotels in more than 70 countries, including Europe, the Far East and Australia. Let your imagination be your guide and get ready to see the sights! You’ll find plenty of hotels throughout the U.S. too!</p>
						<p>Annual, borrowed and rented vacation Points can be used for reservations at participating hotels.</p>
						<p class="mainHead">Call Traveler Plus at <span class="callOutText">800.459.1597</span> and book today!</p>
						<p><a href="https://bluegreenowner.com/TravelerPlus/owner/specialsDetail.aspx?ID=570" target="_blank">Click here</a> to see a complete listing of worldwide hotels.</p>
						<br />
						<table width="498" border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td width="235" valign="top">
									<img src="/TravelerPlus/owner/images/photo-Hotel02.jpg" alt="Featured U.S. Destinations" width="235" height="101" />
									<br />
									<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
										<tr>
											<td width="50%">
												<ul>
													<li>New York City</li>
													<li>Chicago</li>
													<li>Los Angeles</li>
													<li>Atlanta</li>
													<li>Baltimore</li>
												</ul>
											</td>
											<td width="50%">
												<ul>
													<li>Dallas</li>
													<li>Omaha</li>
													<li>San Francisco</li>
													<li>Miami</li>
													<li>Boston</li>
														</ul>
											</td>
										</tr>
									</table>
								</td>
								<td width="28"><img src="/TravelerPlus/owner/images/vertDottedLine.gif" width="28" height="232" /></td>
								<td width="235" valign="top">
									<img src="/TravelerPlus/owner/images/photo-Hotel03.jpg" alt="Featured International Destinations" width="235" height="101" />
									<br />
									<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
										<tr>
											<td width="50%">
												<ul>
															<li>Rome</li>
															<li>Paris</li>
															<li>Frankfurt</li>
															<li>Barcelona</li>
															<li>London</li>
														</ul>
											</td>
											<td width="50%">
												<ul>
															<li>Beijing</li>
															<li>Amsterdam</li>
															<li>Sydney</li>
															<li>Prague</li>
															<li>Geneva</li>
														</ul>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<!-- footer -->
						<includecontrolfooter:footer id=footer runat="server"></includecontrolfooter:footer>
						<!-- end footer -->
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<script type="text/javascript" src="https://ssl.google-analytics.com/urchin.js" [^]></script>
<script type="text/javascript">
	_uacct = "UA-2018410-2";
	urchinTracker();
</script> 
</body>
</html>
