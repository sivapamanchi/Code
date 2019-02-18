Namespace BluegreenOnline

Partial Class sitemap
        Inherits System.Web.UI.Page


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


        Public sHeaderImage As String
        Public sHeaderImageAlt As String
        Public SHeaderImageURL As String
        Public sSSLURL As String = System.Configuration.ConfigurationManager.AppSettings("bxgwebSecureURL")
        Public sURL As String = System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL")


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            Dim sbImage As StringBuilder = New StringBuilder()
            Dim bgOwner As Bluegreenowner = Bluegreenowner.CurrentOwner

            sbImage.Append(sSSLURL)
            sbImage.Append("images/")


            Try

                If bgOwner Is Nothing Then

                    sbImage.Append("ResortsLogo.gif")

                    sHeaderImage = sbImage.ToString
                    SHeaderImageURL = "default.aspx"

                Else

                    If Session("IsTravelerPlusEligible") = "TRUE" Then
                        sbImage.Append("tp_logo.gif")
                        sHeaderImage = sbImage.ToString

                        sHeaderImageAlt = "Traveler Plus"
                        SHeaderImageURL = "/travelerplus/home.aspx"


                    Else

                        sbImage.Append("ResortsLogo.gif")

                        sHeaderImage = sbImage.ToString
                        SHeaderImageURL = "default.aspx"






                    End If




                End If



            Catch ex As Exception

            End Try




        End Sub

End Class

End Namespace
