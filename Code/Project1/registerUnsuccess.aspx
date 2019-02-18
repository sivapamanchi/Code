<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.registerUnsuccess" Codebehind="registerUnsuccess.aspx.vb" %>

<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <title>Register with Bluegreen Online - Unsuccess</title>
    <link rel="stylesheet" href="css/bgOnline.css" type="text/css" />
    <link rel="stylesheet" rev="stylesheet" href="Owner/owner.css" />
</head>
<body>
<table width="740" border="0" cellspacing="0" cellpadding="0" align="center">
	<tr>
		<td width="133">
            
			<a href="<%=System.Configuration.ConfigurationManager.AppSettings("bxgwebSecureURL")%>" target="_blank"  title="Bluegreen Resorts">
				<img src="images/ResortsLogo.gif" width="271" height="20" border="0" alt="" /></a></td>
		<td width="309">
				<img src="owner/images/blank.gif" width="350" height="30" border="0" />
                </td>
		<td width="169">
			<img src="Owner/images/phone.gif" alt="Phone us at 800.456.2582" width="112" height="13" border="0" /></td>
	</tr>
	
</table>
   <img src="images/header-register.jpg" width="740" height="235" alt="Register with Bluegreen Online" />
    <!-- Enroll Unsuccessful -->
    <div class="mainContent">
        <p align="center" style="color: #cc0000; font-size: 20px; font-weight: bold;">
            <img height="20" src="images/alert.gif" width="24" border="0" />&nbsp;Information
            Does Not Match!</p>
        <p align="center" style="color: #cc0000; font-size: 15px;">
            You have not successfully enrolled in Bluegreen Online.</p>
        <p>
            The information you provided does not match our records. Please use the <strong>BACK</strong>
            button to check your owner number, social security number and home phone number
            to see that you entered them correctly. To look up your owner (account) number,
            <a href="#">click here</a>.</p>
        <p>
            If you are a new Bluegreen owner, it takes approximately 30-45 days for your contract
            information to become effective in our computer system. Once your information is
            processed, you will be able to enroll in Bluegreen Online Services. Please check
            back periodically or call <strong>800.456.CLUB (2582)</strong> to inquire about
            your status. In the meantime, <a href="#">click here</a> to explore the Bluegreen
            Vacation Club.</p>
        <p align="center" style="padding: 10px; background: #efefef;">
            If you need further assistance, please call us at <strong>800.456.CLUB (2582)</strong>
            or send us an <a href="#">email</a>.</p>
        <hr />
    </div>
    <div class="mainContent">
        <form id="registerSuccessForm" method="post" runat="server">
            <p align="center">
                <asp:ImageButton ID="imgGoBack" runat="server" ImageUrl="Owner/images/gobackBtn.gif">
                </asp:ImageButton>
            </p>
        </form>
    </div>
    <br />
    <includeControlFooter:footer id="footer" runat="server">
    </includeControlFooter:footer>
</body>
</html>
