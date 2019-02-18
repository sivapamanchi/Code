Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Data
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Data.SqlClient
Imports ReservationLibrary
Imports System.IO
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Globalization


Partial Class PremierOwnerReport
    Inherits VSSABaseClass
    Public owner As New BXG_Owner
    Public tmpOwner As New BXG_Owner
    Public m_iRowIdx As Integer



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Environment.UserName.Length > 8 Then
            Me.lblAdminName.Text = Environment.UserName.Substring(1, 8)
            Session("adminName") = Environment.UserName.Substring(1, 8)
        Else
            Me.lblAdminName.Text = Environment.UserName
            Session("adminName") = Environment.UserName
        End If

        'lblrowDetailsLegend.Visible = False
        'lblError.Visible = False

        If Not IsPostBack Then


            FromCheckInDate.Text = Date.Today.AddMonths(11).AddDays(1).ToString("MM/dd/yyyy")
            ToCheckInDate.Text = Date.Today.AddMonths(11).AddDays(3).ToString("MM/dd/yyyy")

            populateResorts()
            '--------------------------------------------------------------------------------------------------------
            'Before any process process fire-up load the black list of arvact(s) not allowed to log In.
            '--------------------------------------------------------------------------------------------------------
            'If (OwnerBlackList.LoadOwnerBlackList()) Then

            '    If Not IsNothing(Request.QueryString("arvact")) Then

            '    End If
            'Else
            '    pnlGeneralMessage.Visible = True
            '    Me.lblGeneralMessage.ForeColor = Drawing.Color.Red
            '    Me.lblGeneralMessage.Text = "Error loading owner's blacklisted. Please contact the IT Dept for further information."

            'End If

        End If


    End Sub
    Private Sub populateResorts()

        cboResortID.Visible = True

        cboResortID.Items.Clear()

        Dim li1 As New ListItem
        'li1.Text = "Please choose..."
        'li1.Value = "0"
        'cboResortID.Items.Add(li1)
        Dim listOfResorts As DataTable = OwnerDB.GetResorts(cboResortID.Text)

        For Each row As DataRow In listOfResorts.Rows
            Dim resortName As String = StrConv(row("ResortName"), VbStrConv.ProperCase)
            resortName = checkResortName(resortName)
            Dim li As New ListItem

            If (Session("OwnerContractType") = "Sampler") Or (Session("OwnerContractType") = "Sampler24") Then
                If (row("AllowSampler24Owner") Or row("AllowVCSamplerOwner")) Then
                    li.Text = resortName & ", " & row("City") & ", " & row("StateCode")
                    li.Value = row("ResortID")
                    cboResortID.Items.Add(li)
                End If

            Else 'Vacation Club Owner
                li.Text = resortName & ", " & row("City") & ", " & row("StateCode")
                li.Value = row("ResortID")
                cboResortID.Items.Add(li)
            End If

        Next row

    End Sub

    Private Function checkResortName(ByVal resortName As String) As String
        checkResortName = resortName.Trim()
        Dim suffix() As String = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X"}
        Dim resorts() As String = checkResortName.Split(" ")
        Dim containsSuffix As Boolean = False
        Dim index As Integer = 0
        If resorts.Length > 1 Then
            For count As Integer = 0 To suffix.Length - 1
                For counter As Integer = 0 To resorts.Length - 1
                    If resorts(counter).ToUpper() = suffix(count).ToUpper() Then
                        containsSuffix = True
                        index = counter
                    End If
                Next
                If containsSuffix Then
                    checkResortName = resortName.Replace(resorts(index), suffix(count))
                    containsSuffix = False
                    index = 0
                End If
            Next
        End If
        Return checkResortName
    End Function

    Public Sub imgCheckAvailability_Click(sender As Object, e As EventArgs) Handles imgCheckAvailability.Click
        Dim errorFree As Boolean = True
        Dim errorMsg As String = String.Empty
        lblDateValError.Visible = False


        If Not (FromCheckInDate.Text = "") Then
            If (ValidateDateTimeForError(FromCheckInDate.Text) = False) Then
                errorMsg += "<br/>Date of Arrival From : Please enter  Date in MM/DD/YYYY format"
                errorFree = False
            End If
        End If
        If Not (ToCheckInDate.Text = "") Then
            If (ValidateDateTimeForError(ToCheckInDate.Text) = False) Then
                errorMsg += "<br/>Date of Arrival To : Please enter  Date in MM/DD/YYYY format"
                errorFree = False
            End If
        End If
        If Not (FromEnteredDate.Text = "") Then
            If (ValidateDateTimeForError(FromEnteredDate.Text) = False) Then
                errorMsg += "<br/>Date of Entered From : Please enter  Date in MM/DD/YYYY format"
                errorFree = False
            End If
        End If
        If Not (ToEnteredDate.Text = "") Then
            If (ValidateDateTimeForError(ToEnteredDate.Text) = False) Then
                errorMsg += "<br/>Date of Entered To : Please enter  Date in MM/DD/YYYY format"
                errorFree = False
            End If
        End If

        If Not (errorFree) Then
            lblDateValError.Visible = True
            lblDateValError.Text = errorMsg
            divSearchResults.Visible = False
            Return
        End If


        CleanUpErrorandPoints()

        'Session("sortExp") = Nothing
        'Session("sortOrd") = Nothing

        If (errorFree) Then
            Dim requestId As Integer = 0
            Dim resortName As String = String.Empty
            Dim requestStatus As String = String.Empty
            Dim dateArrivalFrom As DateTime = DateTime.MinValue
            Dim dateArrivalTo As DateTime = DateTime.MaxValue

            Dim dateEnteredFrom As DateTime = DateTime.MinValue
            Dim dateEnteredTo As DateTime = DateTime.MaxValue

            If Not cboResortID.Items.Count() = cboResortID.GetSelectedIndices().Count Then
                For Each item As ListItem In cboResortID.Items
                    If item.Selected Then
                        '' message += item.Text + " " + item.Value + "\n"
                        resortName += item.Value + ","
                    End If
                Next
                resortName = resortName.TrimEnd(",")
            End If
            If Not cboRequestStatus.Items.Count() = cboRequestStatus.GetSelectedIndices().Count Then
                For Each item As ListItem In cboRequestStatus.Items
                    If item.Selected Then
                        requestStatus += item.Value + ","
                    End If
                Next
                requestStatus = requestStatus.TrimEnd(",")
            End If

            'End If
            If Not (FromCheckInDate.Text = "") Then
                dateArrivalFrom = Convert.ToDateTime(FromCheckInDate.Text)
            End If
            If Not (ToCheckInDate.Text = "") Then
                dateArrivalTo = Convert.ToDateTime(ToCheckInDate.Text)
            End If
            If Not (FromEnteredDate.Text = "") Then
                dateEnteredFrom = Convert.ToDateTime(FromEnteredDate.Text)
            End If
            If Not (ToEnteredDate.Text = "") Then
                dateEnteredTo = Convert.ToDateTime(ToEnteredDate.Text)
            End If

            If Not (FromCheckInDate.Text = "") Then
                If (Convert.ToDateTime(dateArrivalFrom) > Convert.ToDateTime(dateArrivalTo)) Then
                    errorMsg += "<br/>Date of Arrival : From-Date Cannot be greater than To-Date"
                    errorFree = False
                End If
            End If
            If Not (FromEnteredDate.Text = "") Then
                If (Convert.ToDateTime(dateEnteredFrom) > Convert.ToDateTime(dateEnteredTo)) Then
                    errorMsg += "<br/>Date of Entered : From-Date Cannot be greater than To-Date"
                    errorFree = False
                End If
            End If

            gridSearchResults.PageIndex = 0
            setFilterData(requestId, resortName, requestStatus, dateArrivalFrom, dateArrivalTo, dateEnteredFrom, dateEnteredTo)
            getSearchResults(requestId, resortName, requestStatus, dateArrivalFrom, dateArrivalTo, dateEnteredFrom, dateEnteredTo)
        Else
            lblDateValError.Visible = True
            lblDateValError.Text = errorMsg
        End If


    End Sub

    Public Function ValidateDateTimeForError(ByVal checkInputValue As String) As Boolean
        Dim returnError As Boolean
        Dim dateVal As DateTime
        If DateTime.TryParseExact(checkInputValue, "MM/dd/yyyy",
                System.Globalization.CultureInfo.CurrentCulture,
                DateTimeStyles.None, dateVal) Then
            returnError = True
        End If
        Return returnError
    End Function

    Protected Sub setFilterData(ByVal _requestID As Integer, ByVal _resortName As String, ByVal _requestStatus As String, ByVal _dateArrivalFrom As DateTime, ByVal _dateArrivalTo As DateTime, ByVal _dateEnteredFrom As DateTime, ByVal _dateEnteredTo As DateTime)
        Dim sessionFilterData = New filterData()
        sessionFilterData.RequestID = _requestID
        sessionFilterData.ResortName = _resortName
        sessionFilterData.RequestStatus = _requestStatus
        sessionFilterData.DateArrivalFrom = _dateArrivalFrom
        sessionFilterData.DateArrivalTo = _dateArrivalTo
        sessionFilterData.DateEnteredFrom = _dateEnteredFrom
        sessionFilterData.DateEnteredTo = _dateEnteredTo

        Session("filterData") = sessionFilterData
    End Sub
    Protected Sub gridSearchResults_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gridSearchResults.PageIndex = e.NewPageIndex
        'gridSearchResults.DataSource = ViewState("grdSearchViewstate")
        gridSearchResults.DataSource = Session("grdSearchViewstate")
        gridSearchResults.DataBind()
    End Sub

    Protected Function getSearchResults(ByVal _requestID As Integer, ByVal _resortName As String, ByVal _requestStatus As String, ByVal _dateArrivalFrom As DateTime, ByVal _dateArrivalTo As DateTime, ByVal _dateEnteredFrom As DateTime, ByVal _dateEnteredTo As DateTime) As DataTable
        ' Dim connection As New SqlConnection(ConfigurationManager.ConnectionStrings("VSSA.bxgwebDBConnection").ConnectionString)

        'Dim connection As New SqlConnection("Data Source=SMADHAV-PC\SQLEXPRESS;Initial Catalog=PrimerOwner;Integrated Security=True;")
        Dim connection As New SqlConnection(ConfigurationManager.AppSettings("VSSA.bxgwebDBConnection").ToString())

        Dim cmnd As SqlCommand = connection.CreateCommand
        Dim dt = New DataTable()
        'ViewState.Remove("grdSearchViewstate")
        Session.Remove("grdSearchViewstate")

        ''Using dt As New DataTable()
        Try

            Using connection
                connection.Open()
                cmnd.CommandTimeout = 300

                Using cmnd
                    cmnd.CommandType = CommandType.StoredProcedure
                    cmnd.CommandText = "USPGetPremierOwnerWaitList"
                    'cmnd.Parameters.AddWithValue("@RequestID", _requestID)
                    'cmnd.Parameters.AddWithValue("@Arvact", bxgOwner.Arvact.ToString())
                   cmnd.Parameters.Clear()
                    cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RequestID", System.Data.SqlDbType.BigInt)).Value = _requestID
                    cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Arvact", System.Data.SqlDbType.VarChar, 50)).Value = DBNull.Value
                    cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ResortName", System.Data.SqlDbType.NVarChar)).Value = IIf(String.IsNullOrEmpty(_resortName), DBNull.Value, _resortName)
                    cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.NVarChar)).Value = IIf(String.IsNullOrEmpty(_requestStatus), DBNull.Value, _requestStatus)
                    cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DateArrivalFrom", System.Data.SqlDbType.DateTime)).Value = IIf((_dateArrivalFrom = DateTime.MinValue), DateTime.Parse("1/1/1900"), _dateArrivalFrom)
                    cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DateArrivalTo", System.Data.SqlDbType.DateTime)).Value = IIf((_dateArrivalTo = DateTime.MinValue), DBNull.Value, _dateArrivalTo)
                    cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DateEnteredFrom", System.Data.SqlDbType.DateTime)).Value = IIf((_dateEnteredFrom = DateTime.MinValue), DateTime.Parse("1/1/1900"), _dateEnteredFrom)
                    cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DateEnteredTo", System.Data.SqlDbType.DateTime)).Value = IIf((_dateEnteredTo = DateTime.MinValue), DBNull.Value, _dateEnteredTo)

                    Using sda As New SqlDataAdapter()
                        sda.SelectCommand = cmnd
                        sda.Fill(dt)
                    End Using

                End Using

            End Using
            'If (dt.Rows.Count > 0) Then
            divSearchResults.Visible = True
            gridSearchResults.Visible = True
            Try
                If Not (Session("sortExp") = Nothing) And Not (Session("sortOrd") = Nothing) Then
                    dt.DefaultView.Sort = Session("sortExp").ToString() & Session("sortOrd").ToString()
                End If
            Catch ex As Exception

            End Try


            'Dim dataView As DataView = dt.DefaultView
            'dataView.RowFilter = "Status like '%Pending%'"
            'gridSearchResults.PageIndex = 0

            gridSearchResults.DataSource = updateResortNamesinDT(dt.WithNoOfDays())

            gridSearchResults.DataBind()
            'ViewState("grdSearchViewstate") = gridSearchResults.DataSource
            Session("grdSearchViewstate") = gridSearchResults.DataSource
            'Else
            '    divSearchResults.Visible = False
            '    gridSearchResults.Visible = False
            '    lblError.Visible = True
            '    lblError.Text = "No Records Found ! Please Refine Search."
            'End If
            rowDetailsFieldSet.Visible = False
            lblError.Visible = False

        Catch ex As Exception
            Dim errorString As String = ex.Message.ToString()

        Finally

            If connection.State = ConnectionState.Open Then
                connection.Close() ' closing the connection
            End If

        End Try
        Return dt
    End Function

    Protected Function updateResortNamesinDT(ByVal _dataTable As DataTable) As DataTable
        'Dim returnDatatable As DataTable
        For Each row As DataRow In _dataTable.Rows
            'row("Resortname") = Utilities.getResortInfo(Convert.ToInt32(Convert.ToString(row("Resortname")))).ResortName
            Dim resortDetails = getResortDetailRow(Convert.ToInt32(Convert.ToString(row("Resortname"))))
            For Each rowDet In resortDetails.Rows
                row("Resortname") = rowDet.Item("ResortName")
            Next
        Next row
        Return _dataTable
    End Function

    Private Function getResortDetailRow(ByVal resortId As Integer) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("VSSA.bxgwebDBConnection").ToString()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("[dbo].[uspSelectResortInfo]")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    cmd.Parameters.Clear()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@ResortID", resortId)
                    cmd.Parameters.AddWithValue("@action", 2)
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using
        End Using
    End Function
    Protected Sub gridSearchResults_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        'Try
        CleanUpErrorandPoints()
        lblError.Visible = False
        lblError.Text = ""

        lblUpdate.Attributes.Add("enabled", "enabled")
        'ddlUpdateStatus.Attributes.Add("enabled", "enabled")
        txtConfirmationNumber.Attributes.Add("enabled", "enabled")
        txtInternalNotes.Attributes.Add("enabled", "enabled")
        txtSpecialReq.Attributes.Add("enabled", "enabled")

        If (e.CommandName = "RequestIdUpdate") Then
            Dim gvr As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            gvr.RowState = DataControlRowState.Selected
            Dim requestId As String = e.CommandArgument.ToString()
            Dim rowDetail = updateResortNamesinDT(GetResortDetailById(requestId))
            If (rowDetail.Rows.Count > 0 And rowDetail.Rows(0)("Status").ToString().Trim <> "") Then
                lblError.Visible = False
                rowDetailsFieldSet.Visible = True

                'textResortName.Enabled = False
                lblRequest.Text = requestId
                textResortName.Text = rowDetail.Rows(0)("ResortName")
                Try
                    ddlUpdateStatus.SelectedValue = rowDetail.Rows(0)("Status")
                Catch ex As Exception

                End Try
                txtConfirmationNumber.Text = IIf(rowDetail.Rows(0)("ConfirmationNumber") Is DBNull.Value, "", rowDetail.Rows(0)("ConfirmationNumber"))
                txtInternalNotes.InnerText = IIf(rowDetail.Rows(0)("InternalNotes") Is DBNull.Value, "", rowDetail.Rows(0)("InternalNotes"))
                txtSpecialReq.InnerText = IIf(rowDetail.Rows(0)("SpecialRequest") Is DBNull.Value, "", rowDetail.Rows(0)("SpecialRequest"))


                If IsDBNull(rowDetail.Rows(0)("PremierLevel")) Or rowDetail.Rows(0)("PremierLevel").ToString().Trim = "" Then
                    lblError.Visible = True
                    lblError.Text = "No Valid Data available for Premier Level .Please refine selection."

                    lblOwnerLevel.Text = ""
                    lblArvactNum.Text = IIf(IsDBNull(rowDetail.Rows(0)("ARVACT")), "", rowDetail.Rows(0)("ARVACT"))
                    lblGuestName.Text = IIf(IsDBNull(rowDetail.Rows(0)("GuestLastName")), rowDetail.Rows(0)("GuestFirstName"), rowDetail.Rows(0)("GuestFirstName") + " " + rowDetail.Rows(0)("GuestLastName"))
                    lblGuestEmail.Text = IIf(IsDBNull(rowDetail.Rows(0)("Email")), "", rowDetail.Rows(0)("Email"))
                    lblGuestPhone.Text = IIf(IsDBNull(rowDetail.Rows(0)("Phone")), "", rowDetail.Rows(0)("Phone"))

                    If IsDBNull(rowDetail.Rows(0)("CheckInDate")) Then
                        lblCheckInDate.Text = ""
                    Else
                        lblCheckInDate.Text = Date.Parse(rowDetail.Rows(0)("CheckInDate")).ToString("MM/dd/yyyy")
                    End If

                    If IsDBNull(rowDetail.Rows(0)("CheckOutDate")) Then
                        lblCheckOutDate.Text = ""
                    Else
                        lblCheckOutDate.Text = Date.Parse(rowDetail.Rows(0)("CheckOutDate")).ToString("MM/dd/yyyy")
                        lblNumberOfNights.Text = (Convert.ToDateTime(rowDetail.Rows(0)("CheckOutDate")) - Convert.ToDateTime(rowDetail.Rows(0)("CheckInDate"))).TotalDays
                    End If

                    lblUnitType.Text = IIf(IsDBNull(rowDetail.Rows(0)("VillaSize")), "", rowDetail.Rows(0)("VillaSize"))


                    If IsDBNull(rowDetail.Rows(0)("DateCompleted")) Then
                        lblDateCompleted.Text = ""
                    Else
                        lblDateCompleted.Text = Date.Parse(rowDetail.Rows(0)("DateCompleted")).ToString("MM/dd/yyyy")
                    End If

                    lblNumOfGuests.Text = rowDetail.Rows(0)("NoOfGuests")
                    lblLastModifiedBy.Text = IIf(rowDetail.Rows(0)("AgentName") Is DBNull.Value, "", rowDetail.Rows(0)("AgentName"))
                Else
                    lblOwnerLevel.Text = rowDetail.Rows(0)("PremierLevel")
                    lblArvactNum.Text = rowDetail.Rows(0)("ARVACT")
                    lblGuestName.Text = IIf(IsDBNull(rowDetail.Rows(0)("GuestLastName")), rowDetail.Rows(0)("GuestFirstName"), rowDetail.Rows(0)("GuestFirstName") + " " + rowDetail.Rows(0)("GuestLastName"))
                    lblGuestEmail.Text = rowDetail.Rows(0)("Email")
                    lblGuestPhone.Text = rowDetail.Rows(0)("Phone")

                    If IsDBNull(rowDetail.Rows(0)("CheckInDate")) Then
                        lblCheckInDate.Text = ""
                    Else
                        lblCheckInDate.Text = Date.Parse(rowDetail.Rows(0)("CheckInDate")).ToString("MM/dd/yyyy")
                    End If
                    If IsDBNull(rowDetail.Rows(0)("CheckOutDate")) Then
                        lblCheckOutDate.Text = ""
                    Else
                        lblCheckOutDate.Text = Date.Parse(rowDetail.Rows(0)("CheckOutDate")).ToString("MM/dd/yyyy")
                        lblNumberOfNights.Text = (Convert.ToDateTime(rowDetail.Rows(0)("CheckOutDate")) - Convert.ToDateTime(rowDetail.Rows(0)("CheckInDate"))).TotalDays
                    End If

                    lblUnitType.Text = IIf(IsDBNull(rowDetail.Rows(0)("VillaSize")), "", rowDetail.Rows(0)("VillaSize"))



                    If IsDBNull(rowDetail.Rows(0)("DateCompleted")) Then
                        lblDateCompleted.Text = ""
                    Else
                        lblDateCompleted.Text = Date.Parse(rowDetail.Rows(0)("DateCompleted")).ToString("MM/dd/yyyy")
                    End If

                    lblNumOfGuests.Text = rowDetail.Rows(0)("NoOfGuests")
                    lblLastModifiedBy.Text = IIf(rowDetail.Rows(0)("AgentName") Is DBNull.Value, "", rowDetail.Rows(0)("AgentName"))

                End If

                'textResortName.Attributes.Add("Width", textResortName.Text.Length.ToString())
            Else
                rowDetailsFieldSet.Visible = False
                lblError.Visible = True
                lblError.Text = "No Valid Data available.Please refine selection."
            End If
        End If
        lblUpdate.Attributes.Remove("disabled")
        'Catch ex As Exception

        'End Try
    End Sub

    Public Function GetResortDetailById(ByVal requestID As Integer) As DataTable

        'Dim constr As String = "Data Source=SMADHAV-PC\SQLEXPRESS;Initial Catalog=PrimerOwner;Integrated Security=True;"
        Dim constr As String = ConfigurationManager.AppSettings("VSSA.bxgwebDBConnection").ToString()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("[dbo].[USPGetPremierOwnerWaitList]")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    cmd.Parameters.Clear()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@RequestID", requestID)
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using
        End Using
    End Function


    Protected Sub lblUpdate_Click(sender As Object, e As EventArgs) Handles lblUpdate.Click
        If (ValidateForm()) Then
            ' Dim constr As String = "Data Source=SMADHAV-PC\SQLEXPRESS;Initial Catalog=PrimerOwner;Integrated Security=True;"

            Dim constr As String = ConfigurationManager.AppSettings("VSSA.bxgwebDBConnection").ToString()
            Dim RequestID As Integer = 0
            Dim rowDetail = GetResortDetailById(Convert.ToInt64(lblRequest.Text))
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("[dbo].[USPCreateUpdatePremierOwnerWaitList]")
                    Using sda As New SqlDataAdapter()

                        cmd.Connection = con
                        cmd.Parameters.Clear()
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RequestID", System.Data.SqlDbType.BigInt)).Value = Convert.ToInt64(lblRequest.Text)
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ConfirmationNumber", System.Data.SqlDbType.VarChar, 200)).Value = txtConfirmationNumber.Text.ToString()
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ARVACT", System.Data.SqlDbType.VarChar, 20)).Value = lblArvactNum.Text.ToString()
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OwnerName", System.Data.SqlDbType.VarChar, 50)).Value = rowDetail.Rows(0)("OwnerName").ToString()

                        Dim guestName As String = lblGuestName.Text.ToString()
                        Dim index As Integer
                        Dim guestFirstName As String
                        Dim guestLastName As String

                        If guestName.NthIndexOf(" ", 2) > 0 Then
                            index = guestName.NthIndexOf(" ", 2)
                            guestFirstName = guestName.Substring(0, index)
                            guestLastName = guestName.Substring(index + 1, guestName.Length - (index + 1))
                        ElseIf guestName.NthIndexOf(" ", 1) > 0 Then
                            index = guestName.NthIndexOf(" ", 1)
                            guestFirstName = guestName.Substring(0, index)
                            guestLastName = guestName.Substring(index + 1, guestName.Length - (index + 1))
                        Else
                            guestFirstName = guestName
                            guestLastName = String.Empty
                        End If

                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@GuestFirstName", System.Data.SqlDbType.VarChar, 50)).Value = guestFirstName
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@GuestLastName", System.Data.SqlDbType.VarChar, 50)).Value = guestLastName
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Phone", System.Data.SqlDbType.VarChar, 20)).Value = rowDetail.Rows(0)("Phone").ToString()
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 50)).Value = rowDetail.Rows(0)("Email").ToString()
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PremierLevel", System.Data.SqlDbType.VarChar, 10)).Value = lblOwnerLevel.Text.ToString()
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DateOfRequest", System.Data.SqlDbType.DateTime)).Value = Convert.ToDateTime(rowDetail.Rows(0)("DateOfRequest"))
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ResortName", System.Data.SqlDbType.VarChar, 50)).Value = rowDetail.Rows(0)("ResortName").ToString()
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CheckInDate", System.Data.SqlDbType.DateTime)).Value = Convert.ToDateTime(rowDetail.Rows(0)("CheckInDate"))
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CheckOutDate", System.Data.SqlDbType.DateTime)).Value = Convert.ToDateTime(rowDetail.Rows(0)("CheckOutDate"))
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@VillaSize", System.Data.SqlDbType.VarChar, 50)).Value = rowDetail.Rows(0)("VillaSize").ToString()
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NoOfGuests", System.Data.SqlDbType.Int)).Value = Convert.ToInt32(lblNumOfGuests.Text)
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SpecialRequest", System.Data.SqlDbType.VarChar, 200)).Value = txtSpecialReq.InnerText.ToString()
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.VarChar, 50)).Value = ddlUpdateStatus.SelectedValue
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@InternalNotes", System.Data.SqlDbType.VarChar, 500)).Value = txtInternalNotes.InnerText.ToString()
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DateCompleted", System.Data.SqlDbType.DateTime)).Value = Date.Now()
                        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AgentName", System.Data.SqlDbType.VarChar, 20)).Value = Environment.UserName
                        'lblLastModifiedBy.Text

                        Try
                            con.Open()
                            cmd.ExecuteNonQuery()
                            RequestID = Convert.ToInt32(cmd.Parameters("@RequestID").Value)

                            If RequestID > 0 Then
                                If Not Session("filterData") Is Nothing Then
                                    Dim filterData = New filterData()
                                    filterData = Session("filterData")
                                    getSearchResults(filterData.RequestID, filterData.ResortName, filterData.RequestStatus, filterData.DateArrivalFrom, filterData.DateArrivalTo, filterData.DateEnteredFrom, filterData.DateEnteredTo)

                                Else
                                    getSearchResults(0, String.Empty, String.Empty, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue)

                                End If
                                rowDetailsFieldSet.Visible = False
                                lblUpdate.Attributes.Add("disabled", "disabled")
                                divCss.Attributes.Add("Class", "alert-success")
                                lblUpdateStatusMsg.Text = "Successfully updated the Record."

                            Else
                                rowDetailsFieldSet.Visible = False
                                lblUpdate.Enabled = True
                                divCss.Attributes.Add("Class", "alert-danger")
                                lblUpdateStatusMsg.Text = "Failed to updated the Record."

                            End If
                        Catch ex As Exception
                            lblUpdateStatusMsg.Attributes.Add("CssClass", "alert-danger")
                            lblUpdateStatusMsg.Text = ex.Message.ToString()
                        Finally
                            con.Close()
                        End Try


                    End Using
                End Using
            End Using
        End If
    End Sub
    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=PremierOwnerReport.xls")
        Response.ContentType = "application/ms-excel"
        Response.ContentEncoding = System.Text.Encoding.Unicode
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
        Dim table As DataTable
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            If Not Session("filterData") Is Nothing Then
                Dim filterData = New filterData()
                filterData = Session("filterData")
                table = getSearchResults(filterData.RequestID, filterData.ResortName, filterData.RequestStatus, filterData.DateArrivalFrom, filterData.DateArrivalTo, filterData.DateEnteredFrom, filterData.DateEnteredTo)
            Else
                table = getSearchResults(0, String.Empty, String.Empty, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue)
            End If

            Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"
            Response.Write(style)
            Response.Output.Write(ConvertDataTableToHTMLString(table, "", "", "2", "2", True, True))
            Response.Flush()
            Response.[End]()
        End Using
    End Sub


    Public Shared Function ConvertDataTableToHTMLString(dt As System.Data.DataTable, filter As String, sort As String, fontsize As String, border As String, headers As Boolean, _
     useCaptionForHeaders As Boolean) As String
        'dt.Columns.Remove("GuestName")
        Dim ColumnHeaders() As String = {"Request ID", "Confirmation #", "ARVACT", "Owner Name", "Guest First Name", "Guest Last Name", "Phone", "Email", "Premier Level", "Date of Request", "Resort", "Check-In Date", "Check-Out Date", "Number of Nights", "Villa Size", "Number of Guests", "Special Requests", "Status", "Internal Notes", "Date Completed", "Agent Name"}
        Dim sb As New StringBuilder()
        sb.Append((Convert.ToString("<table border='") & border) + "'b>")
        If headers Then
            'write column headings
            sb.Append("<tr>")
            Dim i As Integer = 0
            For Each dc As System.Data.DataColumn In dt.Columns
                If useCaptionForHeaders Then
                    sb.Append("<td><b><font face=Arial size=2>" + ColumnHeaders(i) + "</font></b></td>")
                Else
                    sb.Append("<td><b><font face=Arial size=2>" + ColumnHeaders(i) + "</font></b></td>")
                End If
                i = i + 1
            Next
            sb.Append("</tr>")
        End If

        'write table data
        For Each dr As System.Data.DataRow In dt.[Select](filter, sort)
            sb.Append("<tr>")
            For Each dc As System.Data.DataColumn In dt.Columns
                If dc.DataType.Name = "DateTime" Then
                    If IsDBNull(dr(dc)) Then
                        sb.Append((Convert.ToString("<td><font face=Arial size=") & fontsize) + ">" + "" + "</font></td>")
                    Else
                        sb.Append((Convert.ToString("<td><font face=Arial size=") & fontsize) + ">" + Date.Parse(dr(dc)).ToString("MM/dd/yyyy") + "</font></td>")
                    End If

                Else
                    sb.Append((Convert.ToString("<td><font face=Arial size=") & fontsize) + ">" + dr(dc).ToString() + "</font></td>")
                End If

            Next
            sb.Append("</tr>")
        Next
        sb.Append("</table>")

        Return sb.ToString()
    End Function

    '=======================================================
    'Service provided by Telerik (www.telerik.com)
    'Conversion powered by NRefactory.
    'Twitter: @telerik
    'Facebook: facebook.com/telerik
    '=======================================================

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered 
        Return
    End Sub
    Private Function ValidateForm() As Boolean
        Dim RequiredFieldMissed As Boolean = True
        CleanUpErrorandPoints()

        If (ddlUpdateStatus.SelectedValue = "Reservation Confirmed" And String.IsNullOrEmpty(txtConfirmationNumber.Text)) Then
            RequiredFieldMissed = False
            divCss.Attributes.Add("Class", "alert-danger")
            lblUpdateStatusMsg.Text = "Please Fill the Confirmation number."
        End If

        Return RequiredFieldMissed
    End Function
    Private Sub CleanUpErrorandPoints()
        divCss.Attributes.Add("Class", "")
        lblUpdateStatusMsg.Text = ""
        lblError.Text = ""
        lblDateValError.Text = ""
    End Sub
    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortExpression As String = e.SortExpression
        If Session("sortExp") Is Nothing Then
            Session("sortExp") = sortExpression
        Else
            If Session("sortExp") <> sortExpression Then
                Session("sortExp") = sortExpression
            End If
        End If

        Dim direction As String = String.Empty
        If SortDirection = SortDirection.Ascending Then
            SortDirection = SortDirection.Descending
            direction = " DESC"
        Else
            SortDirection = SortDirection.Ascending
            direction = " ASC"
        End If

        If Session("sortOrd") Is Nothing Then
            Session("sortOrd") = direction
        Else
            If Session("sortOrd") <> direction Then
                Session("sortOrd") = direction
            End If
        End If

        If Not Session("filterData") Is Nothing Then
            Dim filterData = New filterData()
            filterData = Session("filterData")
            Dim table As DataTable = getSearchResults(filterData.RequestID, filterData.ResortName, filterData.RequestStatus, filterData.DateArrivalFrom, filterData.DateArrivalTo, filterData.DateEnteredFrom, filterData.DateEnteredTo)
            table.DefaultView.Sort = sortExpression & direction

            gridSearchResults.DataSource = table.WithNoOfDays()
        Else
            Dim table As DataTable = getSearchResults(0, String.Empty, String.Empty, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue)
            table.DefaultView.Sort = sortExpression & direction
            gridSearchResults.DataSource = table.WithNoOfDays()
        End If


        gridSearchResults.DataBind()
    End Sub

    Public Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property
    Public Class filterData

        Private _requestID As String
        Private _resortName As String
        Private _requestStatus As String

        Private _dateArrivalFrom As Date
        Private _dateArrivalTo As Date
        Private _dateEnteredFrom As Date
        Private _dateEnteredTo As Date
        Public Property DateArrivalFrom() As Date
            Get
                Return _dateArrivalFrom
            End Get
            Set(ByVal value As Date)
                _dateArrivalFrom = value
            End Set
        End Property

        Public Property DateArrivalTo() As Date
            Get
                Return _dateArrivalTo
            End Get
            Set(ByVal value As Date)
                _dateArrivalTo = value
            End Set
        End Property

        Public Property DateEnteredFrom() As Date
            Get
                Return _dateEnteredFrom
            End Get
            Set(ByVal value As Date)
                _dateEnteredFrom = value
            End Set
        End Property

        Public Property DateEnteredTo() As Date
            Get
                Return _dateEnteredTo
            End Get
            Set(ByVal value As Date)
                _dateEnteredTo = value
            End Set
        End Property

        Public Property RequestID() As String
            Get
                Return _requestID
            End Get
            Set(ByVal value As String)
                _requestID = value
            End Set
        End Property

        Public Property ResortName() As String
            Get
                Return _resortName
            End Get
            Set(ByVal value As String)
                _resortName = value
            End Set
        End Property
        Public Property RequestStatus() As String
            Get
                Return _requestStatus
            End Get
            Set(ByVal value As String)
                _requestStatus = value
            End Set
        End Property

    End Class

    Protected Sub ddlUpdateStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUpdateStatus.SelectedIndexChanged
        If ddlUpdateStatus.SelectedItem.Value = "Reservation Confirmed" Then
            txtConfirmationNumber.Attributes.Remove("disabled")
        Else
            txtConfirmationNumber.Attributes.Add("disabled", "disabled")
            txtConfirmationNumber.Text = ""
        End If
    End Sub

    Protected Sub lbNewSearch_Click(sender As Object, e As EventArgs) Handles lbNewSearch.Click
        Session("filterData") = Nothing
        Response.Redirect("PremierOwnerReport.aspx")
    End Sub
End Class

Module StringExtender

    <Extension()>
    Public Function NthIndexOf(target As String, value As String, n As Integer) As Integer

        Dim m As Match = Regex.Match(target, "((" & Regex.Escape(value) & ").*?){" & n & "}")

        If m.Success Then
            Return m.Groups(2).Captures(n - 1).Index
        Else
            Return -1
        End If
    End Function
End Module

Module DatatableExtensions

    <System.Runtime.CompilerServices.Extension> _
    Public Function WithNoOfDays(ByVal dtDatatable As DataTable) As DataTable
        If (dtDatatable.Columns.Contains("NoofNights")) Then
        Else
            dtDatatable.Columns.Add("NoofNights", Type.[GetType]("System.String")).SetOrdinal(13)
        End If
        For Each row As DataRow In dtDatatable.Rows
            row("NoofNights") = (Convert.ToDateTime(row("CheckOutDate")) - Convert.ToDateTime(row("CheckInDate"))).TotalDays
        Next
        Return dtDatatable
    End Function

End Module

