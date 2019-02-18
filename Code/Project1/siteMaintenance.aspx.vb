Namespace BluegreenOnline
    Partial Class siteMaintenance
        Inherits System.Web.UI.Page

        Public strUpTime As String
        Public sURLUnsecure As String

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            strUpTime = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings("AS400UpTime")).ToShortTimeString()
            sURLUnsecure = System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL")
        End Sub

    End Class
End Namespace
