<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.SalesLogin" Codebehind="SalesLogin.aspx.vb" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>
<%@ Reference Control="~/includes/ucmenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="includes/ucMenu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
<title>Bluegreen Vacation Club</title>
<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
<meta content="JavaScript" name="vs_defaultClientScript">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<link rel="stylesheet" rev="stylesheet" href="Owner/owner.css" />
<link rev="stylesheet" href="styles.css" rel="stylesheet">
<script language="javascript" src="scripts/rollover.js"></script>
<script language="javascript">
<!--

//Make sure this page is not SSL
//var locprot = new String(window.location.protocol);
//if (locprot == 'https:')
//{		
//window.location.replace(window.location.href.replace('https:','http:'));
//}

//Use for populating the default value in the email field when gaining/losing focus
function EmailFlip(inOut, cBox)
{
if (inOut == 'IN')
{
	if (cBox.value == 'Email Address') {
		cBox.value = '';
		}
} else {
	if (cBox.value == '') {
		cBox.value = 'Email Address';
		}
}
}

//Use for populating the default value in the password field when gaining/losing focus
function PasswordFlip(inOut, cBox)
{
if (inOut == 'IN')
{
	if (cBox.value == 'Password') {
		cBox.value = '';
		}
} else {
	if (cBox.value == '') {
		cBox.value = 'Password';
		}
}
}

//Automatically submit the login form if someone is loggin in from another area
function SubMe()
{
if ((document.frmPerformLogin.txtEmail.value != 'Email Address') && (document.frmPerformLogin.txtPassword.value != ''))
{
	document.frmPerformLogin.submit();
}
}


//Open the popup video testimonial
function popUp(URL) 
{
day = new Date();
id = day.getTime();
eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=0,location=0,statusbar=0,menubar=0,resizable=0,width=415,height=595,left = 75,top = 70');");
}

//-->
</script>
<script src="scripts/AC_RunActiveContent.js" type="text/javascript"></script>
<link rel="stylesheet" rev="stylesheet" href="main.css">	
<link rel="stylesheet" rev="stylesheet" href="owner/owner.css">	
<link href="css/ucmenu.css" rel="stylesheet" type="text/css" />
</head>
<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0" onload="SubMe()">
<form name="frmPerformLogin" method="post" action="<%= sSSLURL %>loginwait.aspx">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td align="center">
	        <table width="740" border="0"  cellpadding="0" cellspacing="0">
			    <tr>
				    <td align="left" valign="middle" height="50"><a href="default.aspx"><img src="images/ResortsLogo.gif" alt="Bluegreen Resorts" width="271" height="20" border="0" hspace="5" /></a></td>
				    <td align="right">
				        <table border="0" cellspacing="5" cellpadding="0">
						    
			                <tr>
				                <td align="right"><img src="images/phone.gif" alt="Phone us at 800.456.2582"  border="0" /></td>
			                </tr>
				        </table>
				    </td>
                </tr>
                <tr>
                    <td colspan="2"><img src="images/no_nav.gif" width="740" height="25"></td>
			    </tr>
			    <tr>
				    <td colspan="2" valign="top"><br /><h1 class="hl"><img src="images/title_Sales.gif" width="740" height="100" alt="Sales Assets" /></h1></td>
			    </tr>			    
		    </table>
		    <table class="text" width="740" border="0" cellpadding="0" cellspacing="0">
			    <tr>
				    <td colspan="2" valign="top">
				        <p>*These presentations require at least Flash Player 8. To find out if you have the latest version or to download Flash, visit <a class="textlink" href="http://www.adobe.com/shockwave/welcome/" target="_blank">Adobe's web site</a>. </p>
				        <p>Set your screen resolution to 1024x768. Once you click on a presentation, remember to press the "F11" key at the top of your keyboard to hide the browser and maximize the presentation. </p>
				    </td>
			    </tr>
			    <tr>
				    <td width="50%" valign="top"><br /><br />
				        <ul>
					        <li><a href="sales/WallTour.zip" class="textlink" style="font-weight:bold">Wall Tour Map</a></li>
				            <br /><br /><br /><br />
					        <li><a onClick="newWin=window.open('sales/download.html','newWin','toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700,height=700,left=50,top=20'); return false;" href="#" class="textlink" style="font-weight:bold">Sales Presentation Download / CD Instructions*</a></li>
					        <br /><br />
					        <!--
					        <br /><br />				            
				            <li><a href="sales/bluegreenRCI.swf" target="_blank" class="textlink" style="font-weight:bold">RCI Presentation*</a></li>
					        <br /><br />			
				            -->					        
						</ul>
                    </td>				    
				    <td width="50%" valign="top"><br /><br />
				        <ul>		            
   				            <li><a href="http://www.bluegreenonline.com/saleskiosk/" target="_blank" class="textlink" style="font-weight:bold">Sales Kiosk*</a></li>
						    <br /><br /><br /><br />						    				            
						    <li><a href="sales/photoMontage.swf" target="_blank" class="textlink" style="font-weight:bold">Photo Montage</a></li>
				            <br /><br />
                        </ul>
                    </td>
			    </tr>
		    </table>
		</td>
	</tr>
</table>
</form>
<includeControlFooter:footer id="footer" runat="server"></includeControlFooter:footer>
<script language="JScript" type="text/jscript" src="js/ClickFix.js"></script>
</body>
</html>