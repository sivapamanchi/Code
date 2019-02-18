Imports ReservationLibrary
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Globalization
Imports LoggingProcess
Partial Class ReservationPrint
    Inherits VSSABaseClass
#Region "Fields"
    Private lstReservationConfirmations As List(Of ReservationConfirmation)
    Private reservations As New ReservationConfirmations
#End Region
#Region "Page Events"

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSubmit.Click
        ExecuteSearch()
    End Sub
    Protected Sub btnPrinConfirmations_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPrinConfirmations.Click
        PrintConfirmation()
    End Sub
#End Region

#Region "Repeater Binding Events"

    ''' <summary>
    ''' Reservations Repeater Item Command Event
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rptReservations_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptReservations.ItemCommand
        Select Case e.Item.ItemType
            'make sure item within the header row is clicked on
            Case ListItemType.Header
                'If user clicks on select all link, loop through repeater and select all rows
                Select Case e.CommandName
                    Case "selectAll"
                        Dim lbl As LinkButton = e.CommandSource
                        If lbl.Text = "Select All" Then
                            For Each row As RepeaterItem In Me.rptReservations.Items
                                Dim chkPrint As CheckBox = CType(row.FindControl("chkPrint"), CheckBox)
                                chkPrint.Checked = True
                                lbl.Text = "Unselect All"
                                Me.btnPrinConfirmations.Focus()
                            Next
                        ElseIf lbl.Text = "Unselect All" Then
                            For Each row As RepeaterItem In Me.rptReservations.Items
                                Dim chkPrint As CheckBox = CType(row.FindControl("chkPrint"), CheckBox)
                                chkPrint.Checked = False
                                lbl.Text = "Select All"
                                Me.btnPrinConfirmations.Focus()
                            Next
                        End If
                End Select
        End Select
    End Sub

    ''' <summary>
    ''' Reservations Repeater ItemDatabound Event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rptReservations_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptReservations.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem, ListItemType.Item
                'Retrieving the data associated to that specific row as data is being bound
                Dim _reservationConfirmations As ReservationConfirmation = DirectCast(e.Item.DataItem, ReservationConfirmation)
                'Retrieving and setting the display Controls
                Dim lblConfNumber As Label = CType(e.Item.FindControl("lblConfirmationNumber"), Label)
                Dim lblResortName As Label = CType(e.Item.FindControl("lblResortName"), Label)
                Dim lblCheckInDate As Label = CType(e.Item.FindControl("lblCheckinDate"), Label)
                Dim lblCheckoutDate As Label = CType(e.Item.FindControl("lblCheckoutDate"), Label)
                Dim lblReservationType As Label = CType(e.Item.FindControl("lblReservationType"), Label)
                Dim lblCreatedDate As Label = CType(e.Item.FindControl("lblCreatedDate"), Label)
                lblConfNumber.Text = _reservationConfirmations.ReservationNumber
                lblResortName.Text = _reservationConfirmations.ResortName
                lblCheckInDate.Text = _reservationConfirmations.CheckInDate
                lblCheckoutDate.Text = _reservationConfirmations.CheckOutDate
                lblReservationType.Text = Utilities.GetReservationDescription(_reservationConfirmations.ReservationType)
                lblCreatedDate.Text = _reservationConfirmations.daycreated
                If lblCheckInDate.Text = "" Then
                    lblCheckInDate.Text = "N/A"
                End If
                If lblCheckoutDate.Text = "" Then
                    lblCheckoutDate.Text = "N/A"
                End If
        End Select
    End Sub


    Private Function GetResortInformation(ByVal ResortID As Integer) As ResortInfo

        Dim resInfo As ResortInfo = New ResortInfo()

        resInfo = Utilities.getResortInfo(ResortID)

        Return resInfo
    End Function


#End Region





#Region "Functions/Helper Subs"
    ''' <summary>
    ''' Helper Function that executes the search process when the btnSubmit_Click event is triggered
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExecuteSearch()
        'instantiate a search object
        Dim searchObject As New ReservationLibrary.ReservationSearchCriteria
        searchObject.IsAS400Query = True

        If searchObject.Errors.Count = 0 Then
            Me.rowError.Visible = False
            Me.lstErrors.Text = ""

            'Retrieve confirmations to print using the GetPrintedConfirmations function call. Store result in collection 

            lstReservationConfirmations = reservations.getPrintQueuedReservations()
            Dim rsConfirmations As New ReservationConfirmations

            'validating result set
            If lstReservationConfirmations Is Nothing Or lstReservationConfirmations.Count = 0 Then
                Me.rowError.Visible = True
                Me.lstErrors.Text = "<li>Your search did not match any record</li><br/>"
                Me.rptReservations.Visible = False
                Me.btnPrinConfirmations.Visible = False
                Exit Sub
                'If there are results, make sure there were no internal errors during data retrieval    
            ElseIf lstReservationConfirmations IsNot Nothing Or lstReservationConfirmations.Count > 0 Then
                For Each reservation As ReservationConfirmation In lstReservationConfirmations
                    reservation.AS400ProjectNumber = reservation.ResortID
                    Dim resortInfo As ResortInfo = GetResortInformation(reservation.ResortID)

                    reservation.ResortName = resortInfo.ResortName
                    reservation.ResortAddress = resortInfo.Address
                    reservation.ResortCity = resortInfo.City
                    reservation.ResortCountry = resortInfo.CountryCode
                    reservation.ResortPhone = resortInfo.Phone
                    reservation.ResortState = resortInfo.State
                    reservation.ResortID = resortInfo.ResortID
                    reservation.ResortState = resortInfo.State

                    Dim unitDesc As String = Utilities.getUnitDescription(reservation.ResortID, reservation.AS400UnitType)
                    reservation.UnitView = unitDesc
                    reservation.ReservationType = reservation.Restype 'Utilities.GetReservationDescription(reservation.Restype)

                    'BT Total Charges

                    If reservation.ResAmount > 0 And reservation.Restype = "B" Then
                        reservation.TotalCharge = TaxRate(reservation, resortInfo)
                    End If


                    If reservation.LstErrors.Count > 0 Then
                        For Each errorMessage As String In reservation.LstErrors
                            Me.rowError.Visible = True
                            Me.lstErrors.Text = "<li>" & errorMessage & "</li><br/>"
                            Me.rptReservations.Visible = False
                            Me.btnPrinConfirmations.Visible = False
                            Exit Sub
                        Next
                    End If
                Next
            Else
                Me.rowError.Visible = False
                Me.lstErrors.Text = ""
                Me.rptReservations.Visible = True
                Me.btnPrinConfirmations.Visible = True
            End If

            'Only show print button if there are rows in the repeater
            If lstReservationConfirmations.Count = 0 Then
                Me.btnPrinConfirmations.Visible = False
                Me.rptReservations.Visible = False
            Else
                Me.btnPrinConfirmations.Visible = True
                Me.rptReservations.Visible = True
            End If
            Dim utils As New Utilities




            utils.SortReservationsByDate(lstReservationConfirmations, Utilities.SortingOption.Descending)
            Me.rptReservations.DataSource = lstReservationConfirmations
            rptReservations.DataBind()
            'storing datasource in session for later use
            Session("lstFilteredConfirmations") = lstReservationConfirmations
        Else
            Me.rowError.Visible = True
            Me.lstErrors.Text = ""
            For Each errorMessage As String In searchObject.Errors
                Me.lstErrors.Text &= "<li>" & errorMessage & "</li><br/>"
            Next
        End If

    End Sub

    ''' <summary>
    ''' Helper Function that executes the print confirmation process when the btnPrintconfirmation_Click event is trigggered
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PrintConfirmation()
        'list that will hold the reservation objects that the user desires to print
        Dim lstConfirmationsToPrint As New List(Of ReservationConfirmation)
        'string that will entail the template body for one object at a time
        Dim strTemplateBody = ""
        'String that will concatenate the html templates to be printed
        Dim strConfirmationTemplates As String = ""
        'adding objects with print info to lstConfirmationsToPrint Note: lstConfirmationsToPrint is being modified by reference
        Me.FilterRepeater(lstConfirmationsToPrint)


        'Building HTML template with objects info based on user selection
        For Each obj As ReservationConfirmation In lstConfirmationsToPrint
            'Make sure to select the right template, depending on the reservation type

            strTemplateBody = RetrieveEmailTemplate("PrintTemplates\EmailConfirmationTemplate.html")
            If strTemplateBody = "" Then
                Exit Sub
            End If
            'Get owner address
            obj = GetOwnerInfo(obj)
            Dim resort As ResortInfo = Utilities.getResortInfo(obj.AS400ProjectNumber)
            'concatenate each object email template in string separated by pagebreaks
            strConfirmationTemplates &= Utilities.modifyPrintBody(strTemplateBody, obj) & "<hr style='page-break-after:always;'/>"
            'Setting the network login name property
            If Environment.UserName IsNot Nothing Then
                'making sure the username is never greater than 10 characters. Failure to do so will cause an exception in the insertcomments as400 procedure
                If Environment.UserName.Length > 10 Then
                    obj.NetworkLoginName = Environment.UserName.Substring(0, 10)
                Else
                    obj.NetworkLoginName = Environment.UserName
                End If
            End If
            'setting printing flag on AS400 for each reservation
            obj.Delivery = "P"
            obj.conftype = "R"
            obj.ResStatus = "New Print Confirmation"

            If obj.Status Is Nothing Or String.IsNullOrEmpty(obj.Status) Then
                obj.Status = "OK"
            End If

            Try
                Dim result As String = Utilities.inserAs400Comments(obj, Session("adminName"))

            Catch ex As Exception
                audit = New AuditorLog()

                audit.Title = "Tracing on inserAs400Comments"
                audit.ApplicationName = "BXG.Website.VSSA"
                audit.EventType = "BXG.Website.VSSA.inserAs400Comments"
                audit.Priority = 5
                audit.Message = "Error -> " & ex.Message & ", "
                audit.ObjData = ""
                audit.LogErrors()
            End Try


            Try

                Dim updateResults As Boolean = ReservationConfirmations.UpdateConfirmationPrintDate(obj)
            Catch ex As Exception
                audit = New AuditorLog()

                audit.Title = "Tracing on ReservationConfirmations.UpdateConfirmationPrintDate"
                audit.ApplicationName = "BXG.Website.VSSA"
                audit.EventType = "BXG.Website.ReservationConfirmations.UpdateConfirmationPrintDate"
                audit.Priority = 5

                audit.Message = "Error -> " & ex.Message & ", "
                'to be consumed by Microsoft objectDumper
                audit.ObjData = ""
                audit.LogErrors()
            End Try

           

           
        Next
        'If  lstConfirmationsToPrint is empty, user did not select a record. 
        If lstConfirmationsToPrint.Count = 0 Then
            Me.lstErrors.Text = "<li>Please select a confirmation template</li><br/>"
            Me.rowError.Visible = True
            Me.lstErrors.Visible = True
            Exit Sub
        Else
            Me.lstErrors.Text &= ""
            Me.rowError.Visible = False
            Me.lstErrors.Visible = False
        End If



        'Store the concatenated HTML templates in session variable
        Session("ConfirmationTemplates") = strConfirmationTemplates
        'Redirect to printing  page
        Response.Redirect("../ReservPrintConfirmation.aspx")
    End Sub

    ''' <summary>
    ''' Helper function, used to retrieve email template physically on web server 
    ''' </summary>
    ''' <param name="path"> file location as string</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetrieveEmailTemplate(ByVal path As String) As String
        Dim body As String = ""
        Dim template As String = ""
        'Set the location of the file to read from
        template = IO.Path.Combine(Server.MapPath(""), path)
        'Read the file and store results in string
        Dim sr As New IO.StreamReader(template)
        Do While Not sr.Peek = -1
            body &= sr.ReadLine()
        Loop
        sr.Close()
        Return body
    End Function


    ''' <summary>
    ''' iterates through the repeater, determines which row is checked and return a list with the objects info that the user wants to print
    ''' </summary>
    ''' <param name="printList">List that contains objects with print info</param>
    ''' <remarks></remarks>
    Private Sub FilterRepeater(ByRef printList As List(Of ReservationConfirmation))
        For Each row As RepeaterItem In Me.rptReservations.Items
            'find associated checkbox
            Dim chkPrint As CheckBox = DirectCast(row.FindControl("chkPrint"), CheckBox)
            'if associated textbox is checked...    
            If chkPrint.Checked Then
                'find lblConfirmation
                Dim lblConfirmation As Label = DirectCast(row.FindControl("lblConfirmationNumber"), Label)
                'retrieve object that matches the row index from the list in Session("lstFilteredConfirmations"); then, add it to print list from repeater's datasource
                Dim confirmationToPrint As ReservationConfirmation = CType(Session("lstFilteredConfirmations"), List(Of ReservationConfirmation)).Item(row.ItemIndex)
                printList.Add(confirmationToPrint)
            End If

            'if the confirmation has not been selected for printing, eliminate it from the lookup update file on the AS400
            'For the whole resultset is automatically set for update after the search results have become available(existing BORS functionality)
            If Not chkPrint.Checked Then
                'find lblConfirmation
                Dim lblConfirmation As Label = DirectCast(row.FindControl("lblConfirmationNumber"), Label)
                ReservationConfirmations.DeleteUnselectedConfirmations(lblConfirmation.Text)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Compares the checkindate to the current date. Return value > 0 if checkindate is greater than current date
    ''' </summary>
    ''' <param name="_checkindate">date</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function validateCheckinDate(ByVal _checkindate As String) As Integer
        Dim checkindate As Date = Date.Parse(_checkindate)
        Return checkindate.CompareTo(Date.Now())
    End Function


    Private Function TaxRate(ByVal reservation As ReservationConfirmation, ByVal resort As ResortInfo) As String

        Dim tax As Double = Nothing
        Dim total As Double = Nothing
        Dim totalAmount As String = Nothing
        Dim NumberOfNights As Integer = 0

        If reservation.NumberOfNights = "1" Then
            NumberOfNights = 2
        Else
            NumberOfNights = reservation.NumberOfNights
        End If

            If reservation.ResAmount > 0 And reservation.Restype = "B" Then
                tax = CDbl(resort.TaxRate) / 100
            tax = tax * CDbl(reservation.ResAmount / NumberOfNights)
                tax = Math.Round(tax, 2, MidpointRounding.AwayFromZero)
            total = CDbl(reservation.ResAmount) + (tax * NumberOfNights)
                totalAmount = total.ToString("F", CultureInfo.InvariantCulture)

            End If
            Return totalAmount
    End Function

    Private Function GetOwnerInfo(ByVal reservation As ReservationConfirmation) As ReservationConfirmation
        Try
            Dim ds As New DataSet
            ds = OwnerData.searchOwners(reservation.OwnerID, "", "", "", "")
            reservation.OwnerAddress1 = ds.Tables(0).Rows(0).Item("address1").ToString.Trim & "  " & ds.Tables(0).Rows(0).Item("address2").ToString.Trim
            reservation.OwnerAddress2 = ds.Tables(0).Rows(0).Item("city").ToString.Trim & ",  " & ds.Tables(0).Rows(0).Item("state").ToString.Trim & ",  " & ds.Tables(0).Rows(0).Item("postalcode").ToString().Replace("0000", "")
        Catch ex As Exception

        End Try



        Return reservation
    End Function


#End Region

    Private Sub btnSubmit_Load(sender As Object, e As EventArgs) Handles btnSubmit.Load

    End Sub
End Class
