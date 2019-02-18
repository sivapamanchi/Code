Imports ReservationLibrary
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports LoggingProcess
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


Partial Class ReservationSearch
    Inherits VSSABaseClass

#Region "Fields"
    Private lstReservationConfirmations As List(Of ReservationConfirmation)
    Private reservations As New ReservationConfirmations
    Private Shared lstAuthenticatedUsers As List(Of String)
#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Me.PopulateYearMonthDropDowns()
            'if querystring is coming from the AS400 execute search by reservation number
            If Not Request.QueryString("reservation") Is Nothing Then
                Session("isRequest") = True 'Flag that determines if search is to be executed based on querystring
                txtReservationNumber.Text = Request.QueryString("reservation")
                BuildExecuteSearch(Request.QueryString("reservation"))
                'make sure the search panel is invisible if call comes from the AS400
                Me.pnlSearch.Visible = False
                Me.rowCloseWindow.Visible = True
                Me.rowGoHome.Visible = False
                'if query string is coming from vssa homepage, execute search by owner number
            ElseIf Not Request.QueryString("ownerNumber") Is Nothing Then
                Session("isRequest") = True 'Flag that determines if search is to be executed based on querystring
                txtArvact.Text = Request.QueryString("ownerNumber")
                BuildExecuteSearch(Request.QueryString("ownerNumber"))
                'make sure the search panel is invisible if call comes from vssa home page
                Me.pnlSearch.Visible = False
                Me.rowCloseWindow.Visible = False
                Me.rowGoHome.Visible = True
            Else
                Session("isRequest") = False
            End If
        End If
    End Sub
    'Protected Sub checkinmonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkinmonth.SelectedIndexChanged
    '    Me.PopulateDaysInMonths(sender) 'When the month ddown selected value is changed, populate the day ddown accordinly
    'End Sub
    'Protected Sub checkoutmonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkoutmonth.SelectedIndexChanged
    '    Me.PopulateDaysInMonths(sender)
    'End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSubmit.Click
        BuildExecuteSearch("") 'builds search object; then, execute search
    End Sub



#End Region

#Region "Repeater Binding Events"

    ''' <summary>
    ''' Reservations Repeater ItemCommand Event
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rptReservations_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptReservations.ItemCommand
        Me.rowError.Visible = False
        Me.lstErrors.Text = ""

        Try


            Select Case e.Item.ItemType
                Case ListItemType.AlternatingItem, ListItemType.Item
                    If e.CommandName = "expand" Then 'If user clicks on expand button...
                        Dim lnkExpand As LinkButton = CType(e.CommandSource, LinkButton) 'Retrieve the command object via the commandSource property of the event argument
                        Dim childRow As HtmlTableRow = CType(e.CommandSource.FindControl("rowChild"), HtmlTableRow) 'find the expandable html row associated to the item where the command was fired
                        'hide or show the expandable row, and toggle the linkButton text property accordingly  
                        If lnkExpand.Text = "+" Then
                            childRow.Visible = True
                            lnkExpand.Text = "-"
                            lnkExpand.ToolTip = "Hide"
                            lnkExpand.ForeColor = Drawing.Color.Blue
                            childRow.Focus() 'make sure to set the focus on the expanded row
                        Else
                            childRow.Visible = False
                            lnkExpand.Text = "+"
                            lnkExpand.ToolTip = "Expand"
                            lnkExpand.ForeColor = Drawing.Color.Blue
                            lnkExpand.Focus()
                        End If
                    ElseIf e.CommandName = "updateEmail" Then 'If user wants to update email address...
                        Dim lnkUpdateEmail As LinkButton = CType(e.CommandSource, LinkButton) 'Retrieve control that fired the event
                        Dim txtUpdateEmail As TextBox = CType(lnkUpdateEmail.FindControl("txtUpdateEmail"), TextBox) 'Find associated textbox
                        Dim lblEmailAddress As Label = CType(lnkUpdateEmail.FindControl("lblEmailAddress"), Label) 'Find Email Address control
                        lblEmailAddress.Text = txtUpdateEmail.Text 'Update email address label
                        Dim childRow As HtmlTableRow = CType(lblEmailAddress.FindControl("rowChild"), HtmlTableRow)
                        Dim lnkPrintConfirmation As LinkButton = CType(e.CommandSource.FindControl("lnkPrintEmail"), LinkButton)
                        'Validate Email Address
                        If Not Utilities.IsValidEmail(lblEmailAddress.Text.ToLower().Trim()) Then
                            Me.rowError.Visible = True
                            Me.lstErrors.Text = "<li>Please enter a valid email address. If the owner does not have an email address, click on print to print confirmation</li><br/>"
                            lnkPrintConfirmation.Enabled = True
                            'if search is executed from the query string, check flag and activate proper validation display
                            If Not Session("isRequest") Is Nothing Then
                                If Session("isRequest") Then
                                    Me.rowAS400Validation.Visible = True
                                    Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                                    Me.lblAS400Validate.Font.Size = 9
                                    Me.lblAS400Validate.Text = "<li>Please enter a valid email address. If the owner does not have an email address,<br/>click on print to print confirmation</li><br/>"
                                End If
                            End If
                            Exit Sub
                        Else
                            lnkPrintConfirmation.Enabled = False
                            Me.rowError.Visible = False
                            Me.lstErrors.Text = ""

                            If Session("isRequest") Then
                                Me.rowAS400Validation.Visible = False
                                Me.lblAS400Validate.Text = ""
                            End If
                        End If
                        'disable print function if email address available. Enable print function if email address unavailable
                        If Not Utilities.IsValidEmail(lblEmailAddress.Text.ToLower().Trim()) Then
                            lnkPrintConfirmation.Enabled = True
                        Else
                            lnkPrintConfirmation.Enabled = False
                        End If
                        If childRow.Visible = True Then
                            Dim lnkExpand As LinkButton = CType(e.CommandSource.FindControl("lnkExpand"), LinkButton)
                            lnkExpand.Text = "+"
                            childRow.Visible = False
                        End If
                    ElseIf e.CommandName = "sendEmail" Then 'if user clicks on the send button

                        Dim lnkSendEmail As LinkButton = CType(e.CommandSource, LinkButton) 'Retrieve control that fired the event
                        Dim lnkConfirmationNumber As Label = CType(e.Item.FindControl("lblConfirmationNumber"), Label) 'Fin conf. label control
                        Dim lblEmailAddress As Label = CType(e.Item.FindControl("lblEmailAddress"), Label) 'Find Email Address control
                        Dim lblReservationType As Label = CType(e.Item.FindControl("lblResortType"), Label) 'Find reservationtype label  
                        Dim lnkPrintConfirmation As LinkButton = CType(e.CommandSource.FindControl("lnkPrintEmail"), LinkButton)

                        'Retrieving relevant confirmation instance
                        Dim as400Queue As New ReservationConfirmations
                        'Dim confirmationObject As ReservationConfirmation = as400Queue.getReservationByResNumber(lnkConfirmationNumber.Text.Trim())
                        Dim confirmationObject As ReservationConfirmation = CType(Session("lstReservationConfirmations"), List(Of ReservationConfirmation)).Item(e.Item.ItemIndex) 'as400Queue.getReservationByResNumber(lnkConfirmationNumber.Text.Trim())


                        'setting object to insert data in AS400 database
                        confirmationObject.Delivery = "E"
                        confirmationObject.ReservationNumber = lnkConfirmationNumber.Text
                        confirmationObject.EmailAddress = lblEmailAddress.Text.ToLower().Trim()
                        confirmationObject.proccessType = "0"
                        confirmationObject.delete = "0"
                        confirmationObject.conftype = "R"
                        confirmationObject.ResStatus = "New Email"
                        confirmationObject.NetworkLoginName = "Webuser" 'Environment.UserName

                        'Validate Email Address
                        If Not Utilities.IsValidEmail(lblEmailAddress.Text.ToLower().Trim()) Then
                            Me.rowError.Visible = True
                            Me.lstErrors.Text = "<li>Please enter a valid email address. If the owner does not have an email address, click on print to print confirmation</li><br/>"
                            lnkPrintConfirmation.Enabled = True
                            'if search is executed from the query string, check flag and activate proper validation display
                            If Not Session("isRequest") Is Nothing Then
                                If Session("isRequest") Then
                                    Me.rowAS400Validation.Visible = True
                                    Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                                    Me.lblAS400Validate.Font.Size = 9
                                    Me.lblAS400Validate.Text = "<li>Please enter a valid email address. If the owner does not have an email address,<br/>click on print to print confirmation</li><br/>"
                                End If
                            End If
                            Exit Sub
                        Else
                            lnkPrintConfirmation.Enabled = False
                            Me.rowError.Visible = False
                            Me.lstErrors.Text = ""

                            If Session("isRequest") Then
                                Me.rowAS400Validation.Visible = False
                                Me.lblAS400Validate.Text = ""
                            End If
                        End If
                        'inserting confirmation object to as400 queue. Retrieving insertion status in string variable
                        Dim insertConfirmationStatus As String = as400Queue.insertQueue(confirmationObject)
                        Dim insertCommentsStatus As String = ""
                        insertCommentsStatus = Utilities.inserAs400Comments(confirmationObject, Session("adminName"))

                        If Request("debug") = "True" Then
                            Response.Write("Insert comment into the as400 = " & insertConfirmationStatus.ToLower & "<br /><br />")

                        End If

                        'Response.Write(insertConfirmationStatus.ToLower)
                        'Response.Write(insertCommentsStatus.ToLower)
                        'Response.Write("'" & Session("isRequest") & "'")

                        If insertConfirmationStatus.ToLower = "success" And insertCommentsStatus.ToLower = "success" Then
                            Me.rowError.Visible = True
                            Me.lstErrors.Text = ""
                            Me.lstErrors.ForeColor = Drawing.Color.Green
                            Me.lstErrors.Text &= "<li> Confirmation Email successfully sent to: <br/>&nbsp;&nbsp;&nbsp;&nbsp;" & confirmationObject.EmailAddress & "</li><br/>"

                            'if search is executed from the query string, check flag and activate proper validation display
                            If Not Session("isRequest") Is Nothing Then
                                If Session("isRequest") Then

                                    Me.rowAS400Validation.Visible = True
                                    Me.lblAS400Validate.ForeColor = Drawing.Color.Green
                                    Me.lblAS400Validate.Font.Size = 9
                                    Me.lblAS400Validate.Text = "<li> Confirmation Email successfully sent to: <br/>&nbsp;&nbsp;&nbsp;&nbsp;" & confirmationObject.EmailAddress & "</li><br/>"
                                End If
                            End If

                        Else
                            Me.rowError.Visible = True
                            Me.lstErrors.Text = ""
                            Me.lstErrors.Text &= "<li>There was an error processing your request. <br/>&nbsp;&nbsp;Please, contact the Web Team or create a bits ticket</li><br/>"

                            'if search is executed from the query string, check flag and activate proper validation display
                            If Not Session("isRequest") Is Nothing Then
                                If Session("isRequest") Then
                                    Me.rowAS400Validation.Visible = True
                                    Me.lblAS400Validate.Font.Size = 9
                                    Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                                    Me.lblAS400Validate.Text = "<li>There was an error processing your request. <br/>&nbsp;&nbsp;Please, contact the Web Team or create a bits ticket</li><br/>"
                                End If
                            End If

                        End If

                    ElseIf e.CommandName = "printConfirmation" Then 'if user clicks on print link button
                        Dim lnkConfirmationNumber As Label = CType(e.Item.FindControl("lblConfirmationNumber"), Label) 'Fin conf. label control
                        Dim as400Queue As New ReservationConfirmations
                        'retrieving the confirmation object from the grid initial datasource
                        Dim confirmationObject As ReservationConfirmation = CType(Session("lstReservationConfirmations"), List(Of ReservationConfirmation)).Item(e.Item.ItemIndex) 'as400Queue.getReservationByResNumber(lnkConfirmationNumber.Text.Trim())
                        'setting object addtnl properties to insert in AS400 queue
                        confirmationObject.NetworkLoginName = Environment.UserName

                        confirmationObject.Delivery = "P"
                        confirmationObject.EmailAddress = ""
                        confirmationObject.proccessType = "0"
                        confirmationObject.delete = "0"
                        confirmationObject.conftype = "R"
                        confirmationObject.ResStatus = "New Print Confirmation"
                        'inserting confirmation object to as400 queue. Retrieving insertion status in string variable
                        Dim status As String = as400Queue.insertQueue(confirmationObject)

                        'Response.Write("Insert in Queue")
                        'Response.Write(status.ToLower)

                        If status.ToLower = "success" Then
                            Me.rowError.Visible = True
                            Me.lstErrors.Text = ""
                            Me.lstErrors.ForeColor = Drawing.Color.Green
                            Me.lstErrors.Text &= "<li>Reservation Confirmation was successfully added to the printing queue.</li><br/>"
                            If Not Session("isRequest") Is Nothing Then
                                If Session("isRequest") Then
                                    Me.rowAS400Validation.Visible = True
                                    Me.lblAS400Validate.Font.Size = 9
                                    Me.lblAS400Validate.ForeColor = Drawing.Color.Green
                                    Me.lblAS400Validate.Text = "<li>Reservation Confirmation was successfully added to the printing queue.</li><br/>"
                                End If
                            End If
                        Else
                            Me.rowError.Visible = True
                            Me.lstErrors.Text = ""
                            Me.lstErrors.Text &= "<li>There was an error processing your request. <br/>&nbsp;&nbsp;Please, contact the Web Team or create a bits ticket</li><br/>"
                            'if search is executed from the query string, check flag and activate proper validation display
                            If Not Session("isRequest") Is Nothing Then
                                If Session("isRequest") Then
                                    Me.rowAS400Validation.Visible = True
                                    Me.lblAS400Validate.Font.Size = 9
                                    Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                                    Me.lblAS400Validate.Text = "<li>There was an error processing your request. <br/>&nbsp;&nbsp;Please, contact the Web Team or create a bits ticket</li><br/>"
                                End If
                            End If
                        End If


                    End If
            End Select

        Catch ex As System.InvalidOperationException
            Dim rethrow As Boolean

            rethrow = ExceptionPolicy.HandleException(ex, "UI Policy")

            If (rethrow) Then
                Throw
            End If

        Catch ex As Exception
            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, ExceptionHandlingConstants.DBAccessPolicy)
            If (rethrow) Then
                Throw
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Reservations Repeater Item Databound Event
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
                Dim lblReservationType As Label = CType(e.Item.FindControl("lblResortType"), Label)
                Dim lblEmailAddress As Label = CType(e.Item.FindControl("lblEmailAddress"), Label)
                Dim lnkPrintConfirmation As LinkButton = CType(e.Item.FindControl("lnkPrintEmail"), LinkButton)
                lblEmailAddress.Text = _reservationConfirmations.EmailAddress
                lblConfNumber.Text = _reservationConfirmations.ReservationNumber
                lblResortName.Text = _reservationConfirmations.ResortName
                lblCheckInDate.Text = _reservationConfirmations.CheckInDate
                lblCheckoutDate.Text = _reservationConfirmations.CheckOutDate
                lblReservationType.Text = Utilities.GetReservationDescription(_reservationConfirmations.Restype)

                'Disable for now users want to print and Email at the same time
                'If Not Utilities.IsValidEmail(_reservationConfirmations.EmailAddress.ToLower().Trim()) Then
                '    lnkPrintConfirmation.Enabled = True
                'Else
                '    lnkPrintConfirmation.Enabled = False
                'End If

                If lblReservationType.Text = "" Then
                    lblReservationType.Text = "N/A"
                End If
                If lblEmailAddress.Text = "" Then
                    lblEmailAddress.Text = "N/A"
                End If
                If lblCheckInDate.Text = "" Then
                    lblCheckInDate.Text = "N/A"
                End If
                If lblCheckoutDate.Text = "" Then
                    lblCheckoutDate.Text = "N/A"
                End If

                'Project 69 and 85 can only be printed; therefore make sure the user gets the instructions
                Dim printOnlyConfirmation As ReservationConfirmation = CType(e.Item.DataItem, ReservationConfirmation)
                If printOnlyConfirmation.AS400ProjectNumber = "85" Or printOnlyConfirmation.AS400ProjectNumber = "69" Then
                    Dim lnkSendEmail As LinkButton = CType(e.Item.FindControl("lnkSendEmail"), LinkButton)
                    Dim lnkPrintEmail As LinkButton = CType(e.Item.FindControl("lnkPrintEmail"), LinkButton)
                    lnkSendEmail.Enabled = False
                    lnkPrintEmail.Enabled = False
                    If Session("isRequest") Then 'Error display when the user comes from the AS400 with the F10 key
                        lnkSendEmail.Enabled = False
                        lnkPrintEmail.Enabled = False
                    End If
                Else
                    rptReservations.Visible = True
                End If
        End Select
    End Sub
#End Region
#Region "Functions/Subs"
    ''' <summary>
    ''' Retrieves Email template data and redirect to print confirmation page
    ''' </summary>S
    ''' <param name="obj"> reservation confirmation object</param>
    ''' <remarks></remarks>
    Private Sub PrintConfirmation(ByVal obj As ReservationConfirmation)
        'string that will entail the template body for one object at a time
        Dim strTemplateBody = ""
        'String that will concatenate the html templates to be printed
        Dim strConfirmationTemplates As String = ""
        'Building HTML template with objects info based on user selection
        strTemplateBody = Me.RetrieveConfirmationTemplate(1, obj.Restype, obj.AS400ProjectNumber)
        If strTemplateBody = "" Then
            Exit Sub
        End If
        Dim resort As ResortInfo = Utilities.getResortInfo(obj.AS400ProjectNumber)
        'concatenate each object email template in string separated by pagebreaks
        strConfirmationTemplates &= Utilities.modifyEmailBody(strTemplateBody, resort, obj) & "<hr/>"
        'setting printing flag on AS400 for each reservation
        obj.Delivery = "P"
        If Not Utilities.setAS400PrintingFlag(obj, Session("adminName")) Then
            Me.lstErrors.Text = "<li>Error updating printing queue. Please contact I.T or create a bits ticket</li><br/>"
            Me.rowError.Visible = True
            Me.lstErrors.Visible = True
            'if search is executed from the query string, check flag and activate proper validation display
            If Not Session("isRequest") Is Nothing Then
                If Session("isRequest") Then
                    Me.rowAS400Validation.Visible = True
                    Me.lblAS400Validate.Font.Size = 9
                    Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                    Me.lblAS400Validate.Text = "<li>Error updating printing queue. Please contact I.T or create a bits ticket</li><br/>"
                End If
            End If
            Exit Sub
        Else
            Me.lstErrors.Text = ""
            Me.rowError.Visible = False
            Me.lstErrors.Visible = False
        End If
        'Store the concatenated HTML templates in session variable
        Session("ConfirmationTemplates") = strConfirmationTemplates
        'Redirect to printing  page
        Response.Redirect("ReservPrintConfirmation.aspx")
    End Sub

    ''' <summary>
    ''' Helper Method for PrintConfirmation
    ''' </summary>
    ''' <param name="confTypeId">Confirmation Type ID</param>sendEmail
    ''' <param name="resType">Reservation Type ID</param>
    ''' <param name="projectNumber">AS400 project Number</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetrieveConfirmationTemplate(ByVal confTypeId As Integer, ByVal resType As String, ByVal projectNumber As Integer) As String
        Dim template As String = ""
        Try
            Me.lstErrors.Visible = False
            Me.lstErrors.Text = ""
            Me.rowError.Visible = False

            'retrieve the relevant template from the database
            Dim conn As New SqlConnection(ConfigurationManager.AppSettings("VSSA.bxgwebDBConnection"))
            Dim cmd As New SqlCommand("uspSelectConfirmationByReservation", conn)
            Dim dbDataReader As SqlDataReader
            cmd.CommandType = Data.CommandType.StoredProcedure
            conn.Open()
            SqlCommandBuilder.DeriveParameters(cmd)
            cmd.Parameters("@ConfirmationTypeID").Value = 1
            cmd.Parameters("@ReservationTypeName").Value = resType
            cmd.Parameters("@AS400ProjNo").Value = projectNumber
            dbDataReader = cmd.ExecuteReader()
            If Not dbDataReader.HasRows Then
                Me.lstErrors.Visible = True
                If resType = "P" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Points reservations Email Template available for this resort. <br/>Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Points reservations Email Template available for this resort.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType = "B" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for Bonus Time  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for Bonus Time  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "f" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for Flex  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for Flex  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "q" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for New Owner reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for New Owner reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType = "$" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for Leisure Path reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for Leisure Path reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "e" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for Employee  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for Employee  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "m" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for Marketing reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for Marketing reservations. <br/>Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "r" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for Rental reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for Rental reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "u" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for Resort Use  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for Resort Use  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "v" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for VIP/COMP reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for VIP/COMP reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "w" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for world Inbound  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for world Inbound  reservations. Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "a" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for VC Friend  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for VC Friend  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "g" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for PM Unit Upgrade reservations. <br/>Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for PM Unit Upgrade reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "j" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for Freedom Plus  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for Freedom Plus  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "k" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for PSE  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for PSE  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "L" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for Loganita Lodge  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for Loganita Lodge  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "x" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for Extended Stay  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for Extended Stay  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                ElseIf resType.ToLower() = "y" Then
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>There is no Email Template available for First Year Incentive  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.Font.Size = 9
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There is no Email Template available for First Year Incentive  reservations.<br/> Please use the Rental CMS system to create one</li><br/>"
                        End If
                    End If
                Else
                    Me.rowError.Visible = True
                    Me.lstErrors.Visible = True
                    Me.lstErrors.Text &= "<li>This is an invalid reservation type</li><br/>"
                    If Not Session("isRequest") Is Nothing Then
                        If Session("isRequest") Then
                            Me.rowAS400Validation.Visible = False
                            Me.lblAS400Validate.Text = ""
                        End If
                    End If
                End If
            End If
            While dbDataReader.Read
                template = dbDataReader("ConfirmationEmailBody")
            End While
            dbDataReader.Close()
            cmd.Connection.Close()
            cmd.Connection.Dispose()
            cmd.Dispose()
            Return template
        Catch ex As Exception
            Me.rowError.Visible = True
            Me.lstErrors.Visible = True
            Me.lstErrors.Text &= "<li>There was an error retrieving the confirmation template. Please contact I.T or create a bits ticket</li><br/>"
        End Try
        Return template
    End Function

    ''' <summary>
    ''' Execute the search process when the btnSubmit_Click event is triggered- validate search request
    ''' </summary>
    ''' <remarks></remarks>

    Private Function ValidatesSearchCriteria() As String

        Dim result As String = ""

        If Me.txtReservationNumber.Text.ToUpper().Trim() = "" And Me.txtArvact.Text.Trim = "" And txtstartdate.Text.Trim = "" Then
            result = "Invalid search criteria. Please revise your search."
        End If

        If txtstartdate.Text.Trim <> "" Then
            If txtenddate.Text.Trim = "" Then
                result = "End date can't be empty. Please revise your search."
            End If
        End If

        If result.Length > 0 Then
            Return result
        End If

        Dim fromDate As New Date
        Dim toDate As New Date
        If txtstartdate.Text.Trim <> "" And txtenddate.Text.Trim <> "" Then
            Try
                toDate = Date.ParseExact(txtenddate.Text.ToString, "M/d/yyyy", Nothing)
                fromDate = Date.ParseExact(txtstartdate.Text.ToString, "M/d/yyyy", Nothing)
                If toDate.CompareTo(fromDate) < 0 Then
                    result = "Start date must be greater than End date. Please revise your search."

                ElseIf toDate.CompareTo(fromDate) = 0 Then
                    result = "Start and End dates cannot be the same. Please revise your search."
                End If
            Catch ex As Exception
                result = " Invalid date format. Please revise your search. "
            End Try
        End If





        Return result



    End Function

    ''' <summary>
    ''' Execute the search process when the btnSubmit_Click event is triggered
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BuildExecuteSearch(ByVal as400Query As String)
        Me.lstErrors.Text = ""

        Try

            Dim result As String = ValidatesSearchCriteria()

            If result.Length > 1 Then
                Me.rowError.Visible = True
                Me.lstErrors.Text = result
                Exit Sub
            End If



            'instantiate a search object
            Dim searchObject As New ReservationLibrary.ReservationSearchCriteria
            'set the search object properties
            searchObject.OwnerID = Me.txtArvact.Text
            searchObject.ProcessType = "E"
            'if query is coming from the AS400 set the flag to true to skip internal object validation
            If as400Query.Length > 0 Then
                searchObject.IsAS400Query = True
            End If
            searchObject.ReservationNumber = Me.txtReservationNumber.Text.ToUpper().Trim()
            'If user searches by date, make sure to include date parameters 
            'If Not checkinmonth.SelectedItem.Text.ToLower() = "month" And Not checkinyear.SelectedItem.Text.ToLower() = "year" And Not checkoutmonth.SelectedItem.Text.ToLower() = "month" And Not checkoutyear.SelectedItem.Text.ToLower = "year" Then
            '    searchObject.FromDate = checkinmonth.SelectedValue & "/" & Me.checkinday.SelectedValue & "/" & checkinyear.SelectedValue
            '    searchObject.ToDate = checkoutmonth.SelectedValue & "/" & Me.checkoutday.SelectedValue & "/" & checkoutyear.SelectedValue
            'Else 'Otherwise, set the date search parameters to null
            'searchObject.FromDate = ""
            'searchObject.ToDate = ""
            'End If
            searchObject.FromDate = txtstartdate.Text.ToString
            searchObject.ToDate = txtenddate.Text.ToString

            If txtLastName.Text.Length > 0 And txtFirstName.Text.Length > 0 Then
                searchObject.Name = Me.txtLastName.Text & ", " & Me.txtFirstName.Text
            Else
                searchObject.Name = ""
            End If

            'Data is validated by the object, and errors are stored in Errors property which returns a list of errormessages
            'Only proceed with the search if user enters at least one seach parameter
            If searchObject.Errors.Count = 0 Then
                Me.rowError.Visible = False
                Me.lstErrors.Text = ""
                Dim rsConfirmationsDBAccess As New ReservationConfirmations

                'Retrieve VSSA reservations using the getVSSAOwnerReservations() function call. Store result in collection 
                'Dim lstTmpReservationConfirmations As List(Of ReservationConfirmation) = 


                'Add the last item of the returned collection as it reflects the most recent change
                Dim lstReservationConfirmations As List(Of ReservationConfirmation) = reservations.getVSSAOwnerReservations(searchObject, " ", 2)
                Dim allowedResTypes As String = System.Configuration.ConfigurationManager.AppSettings("ReservationTypes")
                Dim ds As New DataSet

                If allowedResTypes.ToLower <> "all" And lstReservationConfirmations.Count > 0 Then
                    Dim lstAllowedResTypes As New List(Of String)
                    lstAllowedResTypes = allowedResTypes.Split(",").ToList()
                    lstReservationConfirmations = lstReservationConfirmations.FindAll(Function(x) lstAllowedResTypes.Contains(x.Restype))
                End If

                'make sure that the original datasource is not empty

                'Awkward loop populating the missing properties using a different stored procedure since it's dangerous to open Two simultaneous connections the AS400 database
                For Each obj As ReservationConfirmation In lstReservationConfirmations
                    'At times the AS400 database is unavailable, make sure to trap that error in try catch block
                    Try

                        obj.OwnerAccount = rsConfirmationsDBAccess.getReservationByResNumber(obj.ReservationNumber).OwnerAccount
                        obj.People = rsConfirmationsDBAccess.getReservationByResNumber(obj.ReservationNumber).People
                        obj.UnitDetail = ReservationConfirmations.UnitType(rsConfirmationsDBAccess.getReservationByResNumber(obj.ReservationNumber).UnitDetail)
                        obj.OwnerProject = rsConfirmationsDBAccess.getReservationByResNumber(obj.ReservationNumber).OwnerProject
                        Dim ownernumber = Request.QueryString("ownerNumber")
                        If String.IsNullOrEmpty(ownernumber) Then
                            ownernumber = String.Empty
                        End If
                        ds = OwnerData.searchOwners(ownernumber, "", "", "", "")
                        If Not (Session("OwnerEmail") = Nothing) Then
                            If (obj.EmailAddress.Trim = "") Then
                                obj.EmailAddress = Session("OwnerEmail").ToString()
                            End If
                        Else
                            If (obj.EmailAddress.Trim = "") Then
                                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                        obj.EmailAddress = ds.Tables(0).Rows(i)(7).ToString()
                                    Next
                                End If

                            End If

                        End If
                    Catch ex As Exception
                        Me.rowError.Visible = True
                        Me.lstErrors.Text = "<li>There was an issue processing your request. The application server may be temporarily unavailable. Please try again.</li>"
                        If Session("isRequest") Then 'AS400 Error display
                            Me.rowAS400Validation.Visible = True
                            Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                            Me.lblAS400Validate.Text = "<li>There was an issue processing your request. The application server may be temporarily unavailable. Please try again.</li>"
                        End If
                        Exit Sub
                    End Try
                Next


                'make sure the datasource has records
                If lstReservationConfirmations Is Nothing Or lstReservationConfirmations.Count = 0 Then
                    'Regular search error display
                    Me.rowError.Visible = True
                    Me.lstErrors.Text = "<li>No records matched your search</li><br/>"
                    If Session("isRequest") Then 'AS400 Error display
                        Me.rowAS400Validation.Visible = True
                        Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                        Me.lblAS400Validate.Text = "<li>No records matched your search</li><br/>"
                    End If
                    Exit Sub
                    'If there are results, make sure there were no internal errors during data retrieval    
                ElseIf lstReservationConfirmations IsNot Nothing Or lstReservationConfirmations.Count > 0 Then
                    For Each reservation As ReservationConfirmation In lstReservationConfirmations
                        If reservation.LstErrors.Count > 0 Then
                            For Each errorMessage As String In reservation.LstErrors
                                'Regular search error display
                                Me.rowError.Visible = True
                                Me.lstErrors.Text = "<li>" & errorMessage & "</li><br/>"

                                'AS400 error display
                                If Session("isRequest") Then
                                    Me.rowAS400Validation.Visible = True
                                    Me.lblAS400Validate.ForeColor = Drawing.Color.Red
                                    Me.lblAS400Validate.Text = "<li>" & errorMessage & "</li><br/>"
                                End If
                                Exit Sub
                            Next
                        End If
                    Next
                Else
                    'no errors hide all error panels
                    Me.rowError.Visible = False
                    Me.lstErrors.Text = ""
                    Me.rowAS400Validation.Visible = False
                    Me.lblAS400Validate.Visible = False
                End If

                Dim utilities As New Utilities
                utilities.SortReservationsByDate(lstReservationConfirmations, ReservationLibrary.Utilities.SortingOption.Descending)

                'store datasource in session before databind because session is being used in repeater item_databound event.
                Session("lstReservationConfirmations") = lstReservationConfirmations
                'Set the repeater's datasource, provided that there were no errors
                Me.rptReservations.DataSource = lstReservationConfirmations
                rptReservations.DataBind()

            Else
                Me.rowError.Visible = True
                Me.lstErrors.Text = ""
                For Each errorMessage As String In searchObject.Errors
                    Me.lstErrors.Text &= "<li>" & errorMessage & "</li><br/>"
                Next
            End If
        Catch ex As Exception
            Me.rowError.Visible = True
            Me.lstErrors.Text = "<li>No records matched your search</li><br/>"
        End Try



    End Sub

    ''' <summary>
    ''' Populates The year and the month dropdowns
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopulateYearMonthDropDowns()
        ' Dim x As Integer
        'populate Years dropdown
        'For x = Year(Now) - 1 To Year(Now) + 3
        '    Dim li As New ListItem
        '    Dim li2 As New ListItem
        '    li.Text = x
        '    li.Value = x
        '    li2.Text = x
        '    li2.Value = x
        '    checkinyear.Items.Add(li)
        '    checkoutyear.Items.Add(li2)
        'Next
        'Populate Month DropDown
        'For x = 1 To 12
        '    Dim li As New ListItem
        '    Dim li2 As New ListItem
        '    li.Text = MonthName(x, True)
        '    li.Value = x
        '    li2.Text = MonthName(x, True)
        '    li2.Value = x
        '    checkinmonth.Items.Add(li)
        '    checkoutmonth.Items.Add(li2)
        'Next
    End Sub

    ''' <summary>
    ''' 'populates the days dropdown relatively to the selected month in the month dropdown
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <remarks></remarks>
    Private Sub PopulateDaysInMonths(ByVal Sender As Object)
        Dim ddlTrigger As DropDownList = CType(Sender, DropDownList)

        'Dim x As Integer
        'if a dropdown calls this method determine which one it is 
        'If ddlTrigger.ID = "checkinmonth" Or ddlTrigger.ID = "checkinyear" Then 'veryfing which controls are calling the method to avoid synchronized changes
        '    'Make sure the year selected index is valid
        '    If checkinyear.SelectedValue = "Year" Then
        '        checkinyear.SelectedIndex = 1
        '    End If
        '    'if Month is selected, reset everything
        '    If checkinmonth.SelectedValue = "Month" Then
        '        Dim liDay As New ListItem
        '        checkinday.Items.Clear()
        '        checkinyear.SelectedIndex = 0
        '        liDay.Text = "Day"
        '        liDay.Selected = True
        '        checkinday.Items.Add(liDay)
        '        Exit Sub
        '    End If
        '    'days ddown needs to be repopulated each time a new month is selected
        '    'Dates: make sure we retrieve the appropriate number of days for each month, repopulate day dropdown
        '    checkinday.Items.Clear() 'clear before repopulating to avoid redundancy
        '    For x = 1 To Date.DaysInMonth(checkinyear.SelectedValue, CType(checkinmonth.SelectedValue, Integer))
        '        Dim li As New ListItem
        '        li.Text = x
        '        li.Value = x
        '        checkinday.Items.Add(li)
        '    Next

        'ElseIf ddlTrigger.ID = "checkoutmonth" Or ddlTrigger.ID = "checkoutyear" Then 'veryfing which controls are calling the method to avoid synchronized changes
        '    'Make sure the year selected index is valid
        '    If checkoutyear.SelectedValue = "Year" Then
        '        checkoutyear.SelectedIndex = 1
        '    End If
        '    'if  value: Month is selected, reset everything
        '    If checkoutmonth.SelectedValue = "Month" Then
        '        Dim liDay As New ListItem
        '        checkoutday.Items.Clear()
        '        checkoutyear.SelectedIndex = 0
        '        liDay.Text = "Day"
        '        liDay.Selected = True
        '        checkoutday.Items.Add(liDay)
        '        Exit Sub
        '    End If
        '    'days ddown needs to be repopulated each time a new month is selected
        '    'Dates: make sure we retrieve the appropriate number of days for each month, repopulate day dropdown
        '    checkoutday.Items.Clear() 'clear before repopulating to avoid redundancy


        '    For x = 1 To Date.DaysInMonth(checkoutyear.SelectedValue, CType(checkoutmonth.SelectedValue, Integer))
        '        Dim li As New ListItem
        '        li.Text = x
        '        li.Value = x
        '        checkoutday.Items.Add(li)

        '    Next

        '    'If Date.IsLeapYear(checkoutyear.SelectedValue) Then
        '    '    If checkoutmonth.SelectedValue = 2 Then
        '    '        li.Text = "29"
        '    '        li.Value = "29"
        '    '        checkoutday.Items.Add(li)
        '    '    End If
        '    'End If



        'End If
    End Sub
#End Region



    Protected Sub bntBackTop_Click(sender As Object, e As EventArgs) Handles bntBackTop.Click
        Response.Redirect("Default.aspx?arvact=" & Request.QueryString("ownerNumber"))
    End Sub

End Class
