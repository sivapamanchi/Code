Namespace BluegreenOnline

    Partial Class TaviscaRedirectWait
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim bookingTypeQueryString As String = String.Empty
            If Request.QueryString.Count > 0 Then
                bookingTypeQueryString = Request.QueryString.GetValues("type")(0)
            End If

            Select Case bookingTypeQueryString.ToLower()
                Case "flight"
                    Session("TaviscaBookingType") = "Air"
                Case "car"
                    Session("TaviscaBookingType") = "Car"
                Case "hotel"
                    Session("TaviscaBookingType") = "Hotel"
                Case Else
                    Session("TaviscaBookingType") = "Air"
            End Select
        End Sub

    End Class

End Namespace
