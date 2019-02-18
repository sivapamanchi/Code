Imports System.Xml
Imports System.Net
Imports System.IO



Namespace BluegreenOnline

    Partial Class TaviscaRedirectProcess
        Inherits System.Web.UI.Page
        Public bxgOwner As New OwnerWS.Owner
        Dim TPexpired As Boolean

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            If Session("BXGOwner") Is Nothing Then
                Session("_path_info") = Request.RawUrl
                Response.Redirect("http://" & Request.ServerVariables("HTTP_HOST") & "/default.aspx?sess=timeout", True)
            End If

            Dim ownerTypeForTavisca As String
            Dim taviscaSSOXml As String

            Dim bookingType As String
            bxgOwner = Session("BXGOwner")

            Dim ccc As DateTime = DateAdd(DateInterval.Day, 0, Session("Expires"))
            If DateTime.Compare(ccc, Now) < 0 Then
                TPexpired = True
            End If

            If (TPexpired = False And Session("OwnerContractType") = "Vacation Club") Then
                ownerTypeForTavisca = "TravelerPlus"
            Else
                ownerTypeForTavisca = "NonTravelerPlus"
            End If

            If Session("TaviscaBookingType") Is Nothing Then
                bookingType = "Air"
            Else
                bookingType = Session("TaviscaBookingType")
            End If

            taviscaSSOXml = ProcessSSO(bxgOwner, bookingType, ownerTypeForTavisca)

            Dim myremotepost As New RemotePost
            If ConfigurationManager.AppSettings("PCEnvironment") = "Production" Then
                'myremotepost.Url = "http://mystiquefrontoffice.travelnxt.com/BlueGreenOwners/ssodispatch?cid=bluegreenowner&update=true"
                myremotepost.Url = "https://travel.bluegreenowner.com/ssodispatch?update=true"
            Else
                'myremotepost.Url = "http://qa-mystiquefrontoffice.travelnxt.com/BlueGreenOwners/ssodispatch?cid=bluegreenowner&update=true"
                'myremotepost.Url = "https://bluegreenowners.travelnxt.com/ssodispatch?cid=bluegreenowners_uat&update=true"
                myremotepost.Url = "http://qa-bluegreenowner.travelnxt.com/ssodispatch?cid=bluegreenowner_MYS&update=true"
            End If


            myremotepost.Add("content", taviscaSSOXml)
            myremotepost.Add("type", "sso")
            myremotepost.Post()

        End Sub

        Public Function ProcessSSO(bxgOwner As OwnerWS.Owner, ByVal searchType As String, ByVal ownerType As String) As String

            Dim ssoDocument As New XmlDocument
            'Dim bxgOwner As New OwnerWS.Owner
            If ConfigurationManager.AppSettings("PCEnvironment") = "Production" Then
                ssoDocument.Load(HttpContext.Current.Server.MapPath("~/partner/Tavisca/SSO_XML_PROD.xml"))
            Else
                ssoDocument.Load(HttpContext.Current.Server.MapPath("~/partner/Tavisca/SSO_XML_QA.xml"))
            End If

            Dim nodeTravelerProfile As XmlNode = ssoDocument.SelectNodes("/SSOPacket")(0)
            Dim newnodeTravelerProfile As XmlNode = nodeTravelerProfile.CloneNode(True)
            ssoDocument.SelectNodes("/SSOPacket")(0).RemoveAll()

            'Set default search Type to be displayed at Tavisca end
            newnodeTravelerProfile.SelectSingleNode("/ProductSettings/ProductSetting/Type[.=""" + searchType + """]").ParentNode().SelectSingleNode("ShowAsDefault").InnerText = "Y"
            'code added by Siva to allow the tpEmployee access the Tavisca web site
            If Session("IsTravelerPlusEmployee") = "TRUE" Then
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("ReferenceId").InnerText = bxgOwner.Arvact
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Username").InnerText = Session("TPEmployeeEmail")
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Password").InnerText = Session("TPEmployeeEmail").ToLower() + "A" + bxgOwner.Arvact
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Name").SelectSingleNode("FirstName").InnerText = Session("EmployeeFirstName")
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Name").SelectSingleNode("LastName").InnerText = Session("EmployeeLastName")
            Else
                'update element values for the newnodeTravelerProfile
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("ReferenceId").InnerText = bxgOwner.Arvact
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Username").InnerText = bxgOwner.Email
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Password").InnerText = bxgOwner.Email.ToLower() + "A" + bxgOwner.Arvact

                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Role").InnerText = ownerType
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Name").SelectSingleNode("Title").InnerText = bxgOwner.namePrefix
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Name").SelectSingleNode("FirstName").InnerText = bxgOwner.firstName
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Name").SelectSingleNode("MiddleName").InnerText = bxgOwner.middleName
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Name").SelectSingleNode("LastName").InnerText = bxgOwner.lastName
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Name").SelectSingleNode("Suffix").InnerText = bxgOwner.nameSuffix
                'We are not maintaining DOB in Owner session so Date of birth is hardcoded, 
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("DOB").InnerText = "1/1/1985"
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Gender").InnerText = ""
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Emails").SelectSingleNode("Email").SelectSingleNode("Address").InnerText = bxgOwner.Email
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Addresses").SelectSingleNode("Address").SelectSingleNode("AddressLine1").InnerText = HttpUtility.HtmlEncode(bxgOwner.Address1)
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Addresses").SelectSingleNode("Address").SelectSingleNode("AddressLine2").InnerText = HttpUtility.HtmlEncode(bxgOwner.Address2) + " " + HttpUtility.HtmlEncode(bxgOwner.Address3)
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Addresses").SelectSingleNode("Address").SelectSingleNode("City").InnerText = HttpUtility.HtmlEncode(bxgOwner.City)
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Addresses").SelectSingleNode("Address").SelectSingleNode("State").InnerText = bxgOwner.StateAbr
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Addresses").SelectSingleNode("Address").SelectSingleNode("Zip").InnerText = bxgOwner.PostalCode
                newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Addresses").SelectSingleNode("Address").SelectSingleNode("Country").InnerText = bxgOwner.CountryCode

                If (String.IsNullOrEmpty(bxgOwner.HomePhone)) Then
                    newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Phones").SelectSingleNode("Phone").SelectSingleNode("Type").InnerText = "Other"
                    newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Phones").SelectSingleNode("Phone").SelectSingleNode("Number").InnerText = bxgOwner.AlternatePhone
                Else
                    newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Phones").SelectSingleNode("Phone").SelectSingleNode("Type").InnerText = "Home"
                    newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("Phones").SelectSingleNode("Phone").SelectSingleNode("Number").InnerText = bxgOwner.HomePhone
                End If

                If (ownerType.ToLower() = "travelerplus") Then
                    newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("ProgramInformation").SelectSingleNode("ProgramCurrency").SelectSingleNode("Available").InnerText = bxgOwner.PointsTotal
                Else
                    newnodeTravelerProfile.SelectSingleNode("TravelerProfile").SelectSingleNode("ProgramInformation").SelectSingleNode("ProgramCurrency").SelectSingleNode("Available").InnerText = ""
                End If
            End If
            'append the new node to the document
            ssoDocument.DocumentElement.AppendChild(newnodeTravelerProfile)
            'Return WebUtility.HtmlEncode(newnodeTravelerProfile.OuterXml().ToString())
            Return newnodeTravelerProfile.OuterXml().ToString()

        End Function
    End Class

    Public Class RemotePost

        Private Inputs As New System.Collections.Specialized.NameValueCollection

        Public Url As String = ""
        Public Method As String = "post"
        Public FormName As String = "taviscassoform"

        Public Sub Add(name As String, value As String)

            Inputs.Add(name, value)
        End Sub

        Public Sub Post()


            System.Web.HttpContext.Current.Response.Clear()
            'System.Web.HttpContext.Current.Response.ContentType = "xml"
            System.Web.HttpContext.Current.Response.Write("<html><head>")

            System.Web.HttpContext.Current.Response.Write(String.Format("</head><body onload=""document.{0}.submit()"">", FormName))

            System.Web.HttpContext.Current.Response.Write(String.Format("<form name=""{0}"" method=""{1}"" action=""{2}"" >", FormName, Method, Url))
            For i As Integer = 0 To Inputs.Keys.Count - 1
                System.Web.HttpContext.Current.Response.Write(String.Format("<input name=""{0}"" type=""hidden"" value=""{1}""/>", Inputs.Keys(i), Inputs(Inputs.Keys(i))))
            Next

            System.Web.HttpContext.Current.Response.Write("</form>")
            System.Web.HttpContext.Current.Response.Write("</body></html>")
            System.Web.HttpContext.Current.Response.End()
        End Sub
    End Class
   
End Namespace
