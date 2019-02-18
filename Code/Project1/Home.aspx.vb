Imports System.Data.SqlClient
Imports System.Collections.Generic
Namespace BluegreenOnline
    Partial Class HomeTP
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ImageButton1 As System.Web.UI.WebControls.ImageButton
        Protected WithEvents Form3 As System.Web.UI.HtmlControls.HtmlForm
        Protected WithEvents checkinmonth As System.Web.UI.HtmlControls.HtmlSelect
        Protected WithEvents checkinday As System.Web.UI.HtmlControls.HtmlSelect
        Protected WithEvents checkinyear As System.Web.UI.HtmlControls.HtmlSelect
        Protected WithEvents checkoutmonth As System.Web.UI.HtmlControls.HtmlSelect
        Protected WithEvents checkoutday As System.Web.UI.HtmlControls.HtmlSelect
        Protected WithEvents checkoutyear As System.Web.UI.HtmlControls.HtmlSelect
        Protected WithEvents travelers As System.Web.UI.HtmlControls.HtmlSelect
        Protected WithEvents siteID As System.Web.UI.HtmlControls.HtmlSelect
        Protected WithEvents experience As System.Web.UI.HtmlControls.HtmlSelect
        Protected WithEvents imgCalendarCheckIn As System.Web.UI.WebControls.ImageButton
        Protected WithEvents imgCalendarCheckOut As System.Web.UI.WebControls.ImageButton
        Protected WithEvents calCheckIn As System.Web.UI.WebControls.Calendar
        Protected WithEvents calCheckOut As System.Web.UI.WebControls.Calendar
        Protected WithEvents destination As System.Web.UI.WebControls.DropDownList
        Protected WithEvents res_resort As System.Web.UI.WebControls.DropDownList
        Protected WithEvents imgGoDest As System.Web.UI.WebControls.ImageButton
        Protected WithEvents frmGoDest As System.Web.UI.HtmlControls.HtmlForm
        Protected WithEvents RadioBtnOneWay As System.Web.UI.WebControls.RadioButton
        Protected WithEvents RadioBtnRoundTrip As System.Web.UI.WebControls.RadioButton
        Protected WithEvents ImgFeaturedPic As System.Web.UI.WebControls.Image


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Public arlFeatureResults As New ArrayList
        Public showGYC As Boolean



        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim UcMenu1 As ucMenu = Page.FindControl("UcMenu1")
            UcMenu1.CrTPOwnerMenu()
            'LoadFeatures()
            SwapImage()
            Session("gycBanner") = showGYC
            Session("selConnection") = showGYC
            If Session("IsTravelerPlusEmployee") = "TRUE" Or Session("IsTravelerPlusOwner") = "TRUE" Or Session("IsTravelerPlusEligible") = "TRUE" Then
                If Session("IsTravelerPlusEmployee") <> "TRUE" And Session("OwnerNumber") <> "" Then

                    If Request.QueryString("notified") = 1 Then
                        Exit Sub
                    Else
                        Dim C As New clsDBConnectivityVC
                        Dim dbDataReader As SqlClient.SqlDataReader
                        Dim bRenewal As Boolean = False
                        Dim bPleaseWait As Boolean = False

                        C.dbCmnd.CommandText = "SamplerPlus.dbo.uspRenewalValidation"
                        C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                        C.dbCmnd.Parameters.Clear()
                        C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@memberid", System.Data.SqlDbType.VarChar, 50)).Value = Session("OwnerNumber")
                        C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Exists", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output

                        dbDataReader = C.dbCmnd.ExecuteReader()

                        While dbDataReader.Read
                            If dbDataReader.Item(0) = 2 Then
                                'Account is within 60 days and TP owner may renew
                                bRenewal = True
                            ElseIf dbDataReader.Item(0) = 3 Then
                                'Account is over 60 days and has expired
                                Session("expired") = 1
                                bPleaseWait = True
                            End If
                        End While

                        dbDataReader.Close()
                        C.Close()
                        C = Nothing

                        'Redirect AFTER allowing connections to close
                        If bRenewal Then
                            Session("expiration") = 1
                            Response.Redirect("ownerRenewalNotify.aspx")
                        ElseIf bPleaseWait Then
                            Session("expiration") = 2

                            Response.Redirect("../../owner/vctravelerplus.aspx")
                        End If
                    End If
                Else
                    Exit Sub
                End If
            Else
                'Response.Redirect("../../default.aspx")
                Response.Redirect("~/default.aspx")
            End If
        End Sub
#Region "SUBS"

        'Will swap images SummerBanner.gif and Barclays_banner.jpg if purchase date is later than 08/01/2007
        Private Sub SwapImage()
            Try
                'Retrieving DataSet contaning GetLastSaleDateByArvact() query result set
                Dim SaleDateDataSet As DataSet = GetLastSaleDateByArvact()
                'Making sure table is not empty
                If SaleDateDataSet.Tables(0).Rows.Count = 0 Then
                    Exit Sub
                End If
                'Retrieving table row contaning owner record
                Dim SaleDateRow As DataRow = SaleDateDataSet.Tables(0).Rows(0)
                'latest purchase date as integer
                Dim SaleDate = SaleDateRow("ARSLDT")
                'Make sure date(YMMDD) range is correct; set newBanner session variable to true if it is
                If (SaleDate >= 60701) Then
                    showGYC = True 'Flag for the image swap javascript function on the clientside
                Else
                    'Any other case set newBanner session variable to false 
                    showGYC = False
                End If
            Catch ex As Exception
                showGYC = False
            End Try
        End Sub

        Private Function GetLastSaleDateByArvact() As DataSet
            'as400obj : instance of SQL_BASE that takes the connectionstring in the constructor
            Dim as400obj As New SQL_BASE(System.Configuration.ConfigurationManager.AppSettings("bxgwebDBConnection"))
            Dim cmd As SqlCommand = Nothing
            Dim conn As SqlConnection = Nothing
            Dim ds As New DataSet()

            Try
                Dim da As New SqlDataAdapter()
                'Dim query As String = "SELECT arsldt, arvact from CAACCTV where arvact=" & Session("OwnerNumber")
                'calling the setcommandObject method from the SQL_BASE class , which initializes the command and conn objects  
                Dim spname As String = "uspOwnerContractDateAS400"
                as400obj.setCommandObject(cmd, conn, spname, True)
                'Retrieving parameter from sqlcollection and assagning value for stored procedure
                cmd.Parameters("@arvact").Value = Session("OwnerNumber")
                da.SelectCommand = cmd
                da.Fill(ds)
            Catch ex As Exception
                as400obj.Write_Log(ex.Message)
            Finally
                If Not cmd Is Nothing And Not conn Is Nothing Then
                    cmd.Dispose()
                    conn.Close()
                    conn.Dispose()
                End If
            End Try
            Return ds
        End Function





        Private Sub ImgBtnQuickSearchAir_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnQuickSearchAir.Click
            PnlAir.Visible = True
            PnlCar.Visible = False
            ImgBtnQuickSearchAir.ImageUrl = "./images/quicksearchAir.gif"
            ImgBtnQuickSearchCar.ImageUrl = "./images/quicksearchCar.gif"
        End Sub

        Private Sub ImgBtnQuickSearchCar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnQuickSearchCar.Click
            PnlCar.Visible = True
            PnlAir.Visible = False
            ImgBtnQuickSearchCar.ImageUrl = "./images/quicksearchCar_on.gif"
            ImgBtnQuickSearchAir.ImageUrl = "./images/quicksearchAir_off.gif"
        End Sub

        Private Sub LoadFeatures()
            'Determine which section/usertype we are displaying specials for
            Dim searchParam As String
            searchParam = "CO"

            Dim C As New clsDBConnectivity
            Dim dbDataReader As SqlClient.SqlDataReader
            C.dbCmnd.CommandText = "uspSelectFeatures"
            C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
            C.dbCmnd.Parameters.Clear()
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Section", System.Data.SqlDbType.VarChar, 50)).Value = searchParam

            dbDataReader = C.dbCmnd.ExecuteReader()

            While dbDataReader.Read()
                If dbDataReader("ImageType") = "Full Size" Then
                    Dim frSpecial As New FeatureResult
                    frSpecial.ContentDataID = dbDataReader("ContentDataID") & ""
                    frSpecial.ShortDescr = dbDataReader("ContentDataShortDesc") & ""
                    frSpecial.ImageName = dbDataReader("ImageName") & ""
                    frSpecial.ImageTitle = dbDataReader("ImageTitle") & ""
                    frSpecial.Title = dbDataReader("Title") & ""
                    'rptFeatures.Visible = True
                    ImgFeaturedPic.ImageUrl = "../../MS/FeatureImages/" & frSpecial.ImageName
                End If
            End While



            'rptFeatures.DataSource = arlFeatureResults
            'rptFeatures.DataBind()

            dbDataReader.Close()
            C.Close()
            C = Nothing

        End Sub
#End Region
        Private Class FeatureResult
            Public FullDescr As String
            Public ShortDescr As String
            Public Title As String
            Public ImageName As String
            Public ImageTitle As String
            Public ContentID As String
            Public ContentDataID As String
        End Class

    End Class

End Namespace
