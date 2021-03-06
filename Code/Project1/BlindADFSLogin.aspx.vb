﻿Public Class BlindADFSLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim uid As String = Session("LoginEmail")
        Dim pwd As String = Session("LoginPassword")
        Dim redirectURL As String = Request.Params(0)
        Dim adfsURL As String = ConfigurationManager.AppSettings("ADFS_URL")
        Dim formAction As String = "{0}/adfs/ls/?wa=wsignin1.0&amp;wtrealm={1}&amp;wctx=rm%3d0%26id%3dpassive%26ru%3d%252f{2}&amp;wct={3}"
        Dim formOptions As String = "{0}:443/adfs/ls/?wa=wsignin1.0&amp;wtrealm={1}&amp;wctx=rm%3d0%26id%3dpassive%26ru%3d%252f{2}&amp;wct={3}"
        Dim realm As String = Server.HtmlEncode(ConfigurationManager.AppSettings("RealmID"))
        Dim utcTime As String = DateTime.Now.ToUniversalTime().ToString() ' the original URL from ADFS appeared to use "zulu time", but passing a UTC seems to work the same

        formAction = String.Format(formAction, adfsURL, realm, redirectURL, utcTime)
        formOptions = String.Format(formOptions, adfsURL, realm, redirectURL, utcTime)

        Response.Write("<!DOCTYPE html PUBLIC """" """">")

        Response.Write("<html xmlns=""http://www.w3.org/1999/xhtml"" lang=""en-US"">")
        Response.Write("<head runat=""server"">")
        Response.Write("<META content=""IE=10.0000"" http-equiv=""X-UA-Compatible"">")

        Response.Write("<META http-equiv=""X-UA-Compatible"" content=""IE=10.000"">")
        Response.Write("<META name=""viewport"" content=""width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"">")

        Response.Write("<META http-equiv=""content-type"" content=""text/htmlcharset=UTF-8"">")
        Response.Write("<META http-equiv=""cache-control"" content=""no-cache,no-store"">")
        Response.Write("<META http-equiv=""pragma"" content=""no-cache"">")
        Response.Write("<META http-equiv=""expires"" content=""-1"">")
        Response.Write("<META name=""mswebdialog-title"" content=""Connecting to Development BGV FS"">")
        Response.Write("<title>Sign In</title>")

        'Response.Write("<LINK href=""Sign%20In_files/style.css"" rel=""stylesheet"" type=""text/css"">")
        Response.Write("<STYLE>.illustrationClass {background-image:url(" + adfsURL + "/portal/illustration/illustration.png?id=183128A3C941EDE3D9199FA37D6AA90E0A7DFE101B37D10B4FEDA0CF35E11AFD)}</STYLE>")

        Response.Write("<META name=""GENERATOR"" content=""MSHTML 11.00.9600.17924"">")
        Response.Write("</head>")
        Response.Write("<body class=""body"" dir=""ltr"" onload=""Login.submitLoginRequest()"" >")

        Response.Write("<DIV id=""noScript"" style=""width: 100% height: 100% position: static z-index: 100"">")
        Response.Write("<H1>JavaScript required</H1>")
        Response.Write("<P>JavaScript is required. This web browser does not support JavaScript or JavaScript in this web browser is not enabled.</P>")
        Response.Write("<P>To find out if your web browser supports JavaScript or to enable JavaScript, see web browser help.</P>")
        Response.Write("</DIV>")

        Response.Write("<script type=""text/javascript"">")
        Response.Write("document.getElementById(""noScript"").style.display = ""none""")
        Response.Write("</script>")

        Response.Write("<DIV id=""fullPage"">")
        Response.Write("<DIV class=""float"" id=""brandingWrapper"">")
        Response.Write("<DIV id=""branding""></DIV>")
        Response.Write("</DIV>")
        Response.Write("<DIV class=""float"" id=""contentWrapper"">")
        Response.Write("<DIV id=""content"">")
        Response.Write("<DIV id=""header"" style=""display:none"">                    Development BGV FS                 </DIV>")
        Response.Write("<DIV id=""workArea"">")
        Response.Write("<DIV class=""groupMargin"" id=""authArea"">")
        Response.Write("<DIV id=""loginArea"">")
        Response.Write("<DIV class=""groupMargin"" id=""loginMessage"" style=""display:none"">Sign in with your organizational account</DIV>")
        Response.Write("<!--{0} is the protocol and address of the ADFS server {1} is the HTML Encoded Realm {2} is the date/time stamp-->")
        Response.Write("<!--the action should end up looking something like this: " + adfsURL + "/adfs/ls/?wa=wsignin1.0&ampwtrealm=https%3a%2f%2fwww.bluegreenowner.com/%2fbgsso&ampwctx=rm%3d0%26id%3dpassive%26ru%3d%252fBGSSO%252fhome.aspx&ampwct=2015-07-20T17%3a29%3a50Z-->")
        Response.Write("<FORM id=""loginForm"" onkeypress=""if (event &amp&amp event.keyCode == 13) Login.submitLoginRequest()"" ")
        Dim strFormAction As String = String.Format("action=""{0}"" ", formAction)
        Response.Write(strFormAction)
        Response.Write("method=""post"" novalidate=""novalidate"" autocomplete=""off"" runat=""server"">")
        Response.Write("<DIV class=""fieldMargin error smallText"" id=""error"">")
        Response.Write("<LABEL id=""errorText"" for=""""></LABEL>")
        Response.Write("</DIV>")

        Response.Write("<DIV id=""formsAuthenticationArea"" style=""display:none"" >")
        Response.Write("<DIV id=""userNameArea"">")
        Response.Write("<INPUT name=""UserName"" tabindex=""1"" class=""text fullWidth"" id=""userNameInput"" spellcheck=""false"" type=""email"" placeholder=""someone@example.com"" value=""" + uid + """ autocomplete=""off"" runat=""server""> ")
        Response.Write("</DIV>")
        Response.Write("<DIV id=""passwordArea"">")
        Response.Write("<INPUT name=""Password"" tabindex=""2"" class=""text fullWidth"" id=""passwordInput"" type=""password"" value=""" + pwd + """ runat=""server"" autocomplete=""off""> ")
        Response.Write("</DIV>")
        Response.Write("<DIV id=""kmsiArea"" style=""display: none"">")
        Response.Write("<INPUT name=""Kmsi"" tabindex=""3"" id=""kmsiInput"" type=""checkbox"" value=""true"">")
        Response.Write("<LABEL for=""kmsiInput"">Keep me signed in</LABEL>")
        Response.Write("</DIV>")
        Response.Write("<DIV class=""submitMargin"" id=""submissionArea"">")
        Response.Write("<SPAN tabindex=""4"" class=""submit"" id=""submitButton"" onkeypress=""if (event &amp&amp event.keyCode == 32) Login.submitLoginRequest()"" onclick=""return Login.submitLoginRequest()"">Sign in</SPAN>      ")
        Response.Write("</DIV>")
        Response.Write("</DIV>")

        Response.Write("<INPUT name=""AuthMethod"" id=""optionForms"" type=""hidden"" value=""FormsAuthentication""> ")
        Response.Write("</FORM>")
        Response.Write("<DIV id=""authOptions"">")
        Response.Write(String.Format("<FORM id=""options"" action=""{0}"" method=""post"">", formOptions))
        Response.Write("<SCRIPT type=""text/javascript"">")
        Response.Write("function SelectOption(option) {")
        Response.Write("var i = document.getElementById('optionSelection');")
        Response.Write("i.value = option;")
        Response.Write("document.forms['options'].submit();")
        Response.Write("return false;")
        Response.Write("}")
        Response.Write("</SCRIPT>")
        Response.Write("<INPUT name=""AuthMethod"" id=""optionSelection"" type=""hidden""> ")

        Response.Write("<DIV class=""groupMargin""></DIV>")
        Response.Write("</FORM>")
        Response.Write("</DIV>")
        Response.Write("<DIV class=""groupMargin"" id=""introduction""></DIV>")
        Response.Write("</DIV>")
        Response.Write("</DIV>")
        Response.Write("</DIV>")
        Response.Write("<DIV id=""footerPlaceholder""></DIV>")
        Response.Write("</DIV>")
        Response.Write("<DIV id=""footer"" style=""display:none"">")
        Response.Write("<DIV class=""floatReverse"" id=""footerLinks"">")
        Response.Write("<DIV><SPAN id=""copyright"">© 2016 Microsoft</SPAN></DIV>")
        Response.Write("</DIV>")
        Response.Write("</DIV>")
        Response.Write("</DIV>")
        Response.Write("</DIV>")

        Response.Write("</body>")
        Response.Write("</html>")
    End Sub
End Class