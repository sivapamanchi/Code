Namespace BluegreenOnline

Partial Class loginUnsuccessful
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
    Protected WithEvents imgGoBack As System.Web.UI.WebControls.ImageButton


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

        Public sUnsecured As String = System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL")

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'If owner is a sampler only user then show that panel
            Dim sSampler As String
            Try
                sSampler = Request.QueryString("ut")
            Catch ex As Exception
                sSampler = ""
            End Try

            If sSampler = "SAMPLER" Then
                sSampler = "SAMPLERPLUS"
                'If IsSamplerPlus(Session("ownernumber")) Then
                '    sSampler = "SAMPLERPLUS"
                'End If
            End If

            If sSampler = "VALUESAMPLER" Then
                pnlSampler.Visible = True
                pnlNonSampler.Visible = False
            ElseIf sSampler = "SAMPLERPLUS" Then
                pnlSampler.Visible = False
                pnlNonSampler.Visible = False
            ElseIf sSampler = "Players" Then
                PnlPlayer.Visible = True
                pnlSampler.Visible = False
                pnlNonSampler.Visible = False
                PnlPono.Visible = False
            ElseIf sSampler = "Pono" Then
                PnlPono.Visible = True
                pnlSampler.Visible = False

                pnlNonSampler.Visible = False
                PnlPlayer.Visible = False
            Else
                pnlSampler.Visible = False
                pnlNonSampler.Visible = True
            End If

            Session.Clear()
            Session.Abandon()
            Session.RemoveAll()

            If Request.Cookies("ASP.NET_SessionId") IsNot Nothing Then
                Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)
            End If

        End Sub

        Private Function IsSamplerPlus(ByVal arvact As String) As Boolean

            Dim dbConn As New clsDBConnectivity
            Dim dataReader As SqlClient.SqlDataReader
            Dim SamplerPlus As Boolean = False

            dbConn.dbCmnd.CommandText = "bluegreenonline.dbo.uspGetSamplerPlusOwnerInfo"
            dbConn.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
            dbConn.dbCmnd.Parameters.Clear()
            dbConn.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@arvact", System.Data.SqlDbType.Float)).Value = arvact

            dataReader = dbConn.dbCmnd.ExecuteReader()

            If dataReader.HasRows Then

                dataReader.Read()

                If dataReader("ARSLTY") = "L" And dataReader("ARHOME") = "52" Then
                    SamplerPlus = True

                End If
            End If

            datareader.close()
            dbconn.close()

            Return SamplerPlus

        End Function

    End Class
End Namespace
