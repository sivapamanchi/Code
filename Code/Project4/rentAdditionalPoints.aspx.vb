Imports System.IO
Imports System.Net
Imports System.Data.SqlClient
Imports xAuthorize
Imports System.Collections.Generic
Imports BXGDiagnostics
Imports System.Diagnostics
Imports Bluegreenonline
Imports System.Globalization
Imports VSSA.fetchresp

'Namespace TravelerPlus.Owner
Partial Class TravelerPlus_owner_rentAdditionalPoints
    Inherits VSSABaseClass

    Public BXGowner As New OwnerFetchResponse()
    Public owner As New BXG_Owner


#Region "CLASS FIELDS"
#End Region
#Region "Page EVENTS"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("SearchParam") Is Nothing Then
            Session("SearchParam") = Nothing
        End If
        If Not Session("SearchData") Is Nothing Then
            Session("SearchData") = Nothing
        End If
        If Session("arvact") Is Nothing Then
            Response.Redirect("default.aspx")
            Exit Sub
        End If

        Session("OwnerNumber") = Session("arvact").ToString()
        'making sure owner number and payment texbox are not editable e 
        Me.txtBxBVC.ReadOnly = True
        Me.txtBxPayment.ReadOnly = True
        'Me.txtBxPayment.BackColor = Color.LightGray
        'Me.txtBxBVC.BackColor = Color.LightGray
        'Me.txtBxEmail.ReadOnly = True
        'Me.txtBxEmail.BackColor = Color.LightGray
        'Me.txtBxEmail.ForeColor = Color.Black
        'Check if user is logged in 
        If Session("OwnerNumber") = "" Then
            Session("_path_info") = Request.RawUrl
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "default.aspx?sess=timeout")
        End If
        'retrieve owner info
        Session("HistoryCount") = Session("HistoryCount") - 1
        'populate cc dropdowns 
        If Not IsPostBack Then
            Session("HistoryCount") = -1
            If Not Session("BoomiOwner") Is Nothing Then
                BXGowner = Session("BoomiOwner")
            End If
            Session("BXGOwner") = BXGowner

            Me.populateStateDropDown()
            getOwnerInfo(Session("OwnerNumber"))
            Me.populateCCDropDowns()
        End If
    End Sub
    Protected Sub ddlPtsValues_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPtsValues.SelectedIndexChanged
        If ddlPtsValues.SelectedValue = "none" Then
            'replace everything after the colon by empty string to avoid repeated concatenation on page reload
            Me.lblFirstName.Text = Me.lblFirstName.Text.Replace(Me.lblFirstName.Text.Substring(lblFirstName.Text.IndexOf(":") + 1), "")
            Me.lblLastName.Text = Me.lblLastName.Text.Replace(Me.lblLastName.Text.Substring(lblLastName.Text.IndexOf(":") + 1), "")
            Me.lblCity.Text = Me.lblCity.Text.Replace(Me.lblCity.Text.Substring(lblCity.Text.IndexOf(":") + 1), "")
            Me.lblState.Text = Me.lblState.Text.Replace(Me.lblState.Text.Substring(lblState.Text.IndexOf(":") + 1), "")
            Me.lblZip.Text = Me.lblZip.Text.Replace(Me.lblZip.Text.Substring(lblZip.Text.IndexOf(":") + 1), "")
            Me.lblPhone.Text = Me.lblPhone.Text.Replace(Me.lblPhone.Text.Substring(lblPhone.Text.IndexOf(":") + 1), "")
            Me.lblEmail.Text = Me.lblEmail.Text.Replace(Me.lblEmail.Text.Substring(lblEmail.Text.IndexOf(":") + 1), "")
            Me.lblOwnerNumber.Text = Me.lblOwnerNumber.Text.Replace(Me.lblOwnerNumber.Text.Substring(lblOwnerNumber.Text.IndexOf(":") + 1), "")
            If lblCCtype.Text <> "" Then
                Me.lblCCtype.Text = Me.lblCCtype.Text.Replace(Me.lblCCtype.Text.Substring(lblCCtype.Text.IndexOf(":") + 1), "")
            End If
            If lblCardNumber.Text <> "" Then
                Me.lblCardNumber.Text = Me.lblCardNumber.Text.Replace(Me.lblCardNumber.Text.Substring(lblCardNumber.Text.IndexOf(":") + 1), "")
            End If
            Me.txtBxPayment.Text = ""
            Me.lblExpirationDate.Text = Me.lblExpirationDate.Text.Replace(Me.lblExpirationDate.Text.Substring(lblExpirationDate.Text.IndexOf(":") + 1), "")
            Me.pnlConfirm.Visible = False
            Me.credit.Visible = True
            Me.billing.Visible = True
            Me.nav.Style.Item("display") = "block;"
            'Me.outOfPoints.Visible = True
            Me.imgAlert.Visible = False
            Me.lblError.Visible = False
            Exit Sub
        End If
        'clear error display when points package is selected
        Me.imgAlert.Visible = False
        Me.lblError.Text = ""
        'populate payment textbox 
        Me.txtBxPayment.Text = Me.ddlPtsValues.SelectedValue
        'Populate payment confirmation label each time user selects a purchase option
        If Not Me.lblPayment.Text.Substring(lblPayment.Text.IndexOf(":") + 1) = "" Then
            'cleanup the label each time the event is fired to avoid multiple concatenation
            Me.lblPayment.Text = Me.lblPayment.Text.Replace(Me.lblPayment.Text.Substring(lblPayment.Text.IndexOf(":") + 1), "&nbsp;&nbsp; $" & Me.ddlPtsValues.SelectedValue)
        Else
            Me.lblPayment.Text &= "&nbsp;&nbsp;" & Me.ddlPtsValues.SelectedValue
        End If
        'retrieve the amount of points purchased
        Session("pointsPurchased") = ddlPtsValues.SelectedItem.Text.Substring(0, ddlPtsValues.SelectedItem.Text.IndexOf(" "))

    End Sub
    'Confirmation Screen click    
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try


            Dim objOwner As New BluegreenOnline.Bluegreenowner
            objOwner.OwnerFirstName = Me.txtBxFirstName.Text
            objOwner.OwnerLastName = Me.txtBxLastName.Text
            objOwner.OwnerVCNumber = Session("OwnerNumber")
            objOwner.OwnerCity = Me.txtBxCity.Text
            objOwner.OwnerStateAbr = Me.ddlState.SelectedValue
            objOwner.OwnerPostalCode = Me.txtBxZip.Text
            objOwner.OwnerHomePhone = Me.txtBxPhone.Text
            objOwner.OwnerEmailAddress = Me.txtBxEmail.Text

            '  Dim expDate As String = Me.ddlExpYear.SelectedValue & Me.ddlExpMonth.SelectedValue
            Dim expDate As String = Me.ddlExpMonth.SelectedValue & Me.ddlExpYear.SelectedValue


            Dim objPayment As New paymentdetail(ddlCCtype.SelectedItem.Text, txtBxCCNumber.Text, expDate, Me.ddlPtsValues.SelectedValue, "", Me.txtBxFirstName.Text & "  " & Me.txtBxLastName.Text)

            Dim ccConfirm As paymentdetail = SubmitPayment(objOwner, objPayment)

            'Dim authRespons As String() = Split(processPayment, "|")
            If Trim(ccConfirm.CONFNUMBER) = "" Then
                Me.imgAlert.Visible = True
                Me.lblError.Visible = True
                lblError.Text = "<font color=red>Error processing your payment, please contact customer service at 800.459.1597</font>"
                Exit Sub
            ElseIf ccConfirm.CONFNUMBER.Length > 3 Then
                'hide points, billing and credit panels


                Try
                    If addPoints() Then 'add point if purchase is confirmed
                        lblOwnerId.Text = objOwner.OwnerVCNumber
                        lblOwnerName.Text = objOwner.OwnerFirstName + " " + objOwner.OwnerLastName
                        Dim dblPointsValue As Double
                        dblPointsValue = Convert.ToDouble(Me.ddlPtsValues.SelectedValue)
                        lblPaymentAmount.Text = dblPointsValue.ToString("0.00", CultureInfo.InvariantCulture) ' Me.ddlPtsValues.SelectedValue.ToString("0.00")
                        lblAuthorization.Text = ccConfirm.CONFNUMBER
                        Me.imgAlert.Visible = False
                        Me.lblError.Visible = False
                        Me.pnlSuccess.Visible = True
                        Me.pnlConfirm.Visible = False
                        SendEmail(Me.txtBxEmail.Text, Me.txtBxFirstName.Text, Me.txtBxLastName.Text, Me.txtBxPayment.Text, Session("pointsPurchased"))
                        Session("PalettReload") = True
                        'Insert As400 Comments

                        insertAs400Comments("TP Rent PTS for $" + Me.ddlPtsValues.SelectedValue + " @ " + Session("pointspurchased").ToString() + " PTS Auth: " + ccConfirm.CONFNUMBER)

                    Else
                        Me.imgAlert.Visible = True
                        lblError.Visible = True
                        Me.lblError.Text = "<font color=red>Your credit card has been successfully charged, but there was a problem adding points to your account; please, contact customer service at 800.459.1597 </font>"
                        Session("PalettReload") = True
                        Exit Sub
                    End If
                Catch ex As Exception
                    Me.imgAlert.Visible = True
                    lblError.Visible = True
                    Me.lblError.Text = "<font color=red>" & ex.Message & "</font>"
                    'send error to developers
                    'Me.senderror(ex.Message, lblError.Text)
                End Try



            Else
                Me.imgAlert.Visible = True
                Me.lblError.Visible = True
                lblError.Text = "<font color=red>Error processing your payment, please contact customer service at 800.459.1597</font>"
                Exit Sub
            End If
        Catch ex As Exception
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            lblError.Text = "<font color=red>Error processing your payment, please contact customer service at 800.459.1597</font>"
            Exit Sub
        End Try
    End Sub
    Protected Sub btnCloseWindow_Click(sender As Object, e As System.EventArgs) Handles btnCloseWindow.Click
        ModalPopupExtender1.Hide()
        Response.Redirect("rentAdditionalPoints.aspx")
    End Sub

    Private Function SubmitPayment(ByVal owner As BluegreenOnline.Bluegreenowner, ByVal paydetail As paymentdetail) As paymentdetail

        Dim CVC As New clsDBConnectivityVC

        Try


            Dim MerchantID As String = ""

            'NEED TO SELECT THE MERCHANT ID HERE

            Try


                Dim reader As SqlClient.SqlDataReader
                CVC.dbCmnd.CommandText = "uspSelectMerchantID"
                CVC.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                CVC.dbCmnd.Parameters.Clear()
                CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("action", System.Data.SqlDbType.VarChar, 20)).Value = System.Configuration.ConfigurationManager.AppSettings("RentPointsCCProjectNumber")
                reader = CVC.dbCmnd.ExecuteReader()
                While reader.Read
                    MerchantID = reader("MerchantID")
                End While

                reader.Close()

            Catch Ex As Exception

                'SendConfirmation(owner, Amount, "failure", "11" & Ex.Message.ToString)
                paydetail.ERRDETAIL = Ex.Message.ToString
                paydetail.CONFNUMBER = " "
                CVC.Close()
                CVC = Nothing
                Return paydetail

            Finally

                CVC.Close()

            End Try

            'validate merchant id
            If IsNothing(MerchantID) Then
                paydetail.ERRORCODE = "Invalid merchani Id"
                paydetail.CONFNUMBER = " "
                Return paydetail

            ElseIf MerchantID.Length < 5 Then
                paydetail.ERRORCODE = "Invalid merchani Id"
                paydetail.CONFNUMBER = " "
                Return paydetail

            Else

                paydetail.MERCHANTID = MerchantID

            End If

            'process credit card

            Try
                Dim ccprocessobj As New BluegreenOnline.CCProcess
                Dim objpayresponse As paymentdetail = ccprocessobj.processPayment(owner, paydetail)
                paydetail = objpayresponse
            Catch ex As Exception
                paydetail.ERRORCODE = ex.Message.ToString()
                paydetail.CONFNUMBER = " "
                Return paydetail
            End Try


        Catch ex As Exception

            paydetail.ERRORCODE = ex.Message.ToString()
            paydetail.CONFNUMBER = " "
            Return paydetail

        End Try

        Return paydetail

    End Function


    'cancel confirmation button click
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Me.ddlPtsValues.SelectedIndex = 0
        Me.points.Visible = True
        Me.ddlPtsValues_SelectedIndexChanged(sender, New System.EventArgs)
    End Sub
    'credit card submit button click
    Protected Sub btnCCsubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCCsubmit.Click
        Me.imgAlert.Visible = False
        lblError.Text = ""
        If validateCC() And validateForm() Then
            'populate confirmation labels
            Me.lblFirstName.Text &= "  " & Me.txtBxFirstName.Text
            Me.lblLastName.Text &= "&nbsp&nbsp" & Me.txtBxLastName.Text
            Me.lblOwnerNumber.Text &= "&nbsp&nbsp" & Me.txtBxBVC.Text
            Me.lblCity.Text &= "&nbsp&nbsp" & Me.txtBxCity.Text
            Me.lblState.Text &= "&nbsp&nbsp" & ddlState.SelectedItem.Text
            Me.lblZip.Text &= "&nbsp&nbsp" & Me.txtBxZip.Text
            Me.lblEmail.Text &= "&nbsp&nbsp" & Me.txtBxEmail.Text
            Me.lblPhone.Text &= "&nbsp&nbsp" & Me.txtBxPhone.Text
            points.Visible = False
            billing.Visible = False
            credit.Visible = False
            Me.imgAlert.Visible = False
            Me.lblError.Visible = False
            'Me.outOfPoints.Visible = False
            Me.pnlConfirm.Visible = False
            Me.lblCCtype.Text &= "&nbsp&nbsp" & Me.ddlCCtype.SelectedItem.Text
            Me.lblCardNumber.Text &= "&nbsp&nbsp" & Me.txtBxCCNumber.Text
            Me.lblExpirationDate.Text &= "&nbsp&nbsp" & Me.ddlExpMonth.SelectedItem.Text & " " & ddlExpYear.SelectedItem.Text
            Me.nav.Style.Item("display") = "none;"

            '  Review page is removed and functionality is moved here 

            Try


                Dim objOwner As New BluegreenOnline.Bluegreenowner
                objOwner.OwnerFirstName = Me.txtBxFirstName.Text
                objOwner.OwnerLastName = Me.txtBxLastName.Text
                objOwner.OwnerVCNumber = Session("OwnerNumber")
                objOwner.OwnerCity = Me.txtBxCity.Text
                objOwner.OwnerStateAbr = Me.ddlState.SelectedValue
                objOwner.OwnerPostalCode = Me.txtBxZip.Text
                objOwner.OwnerHomePhone = Me.txtBxPhone.Text
                objOwner.OwnerEmailAddress = Me.txtBxEmail.Text

                Dim expDate As String = Me.ddlExpMonth.SelectedValue & Me.ddlExpYear.SelectedValue

                Dim objPayment As New paymentdetail(ddlCCtype.SelectedItem.Text, txtBxCCNumber.Text, expDate, Me.ddlPtsValues.SelectedValue, "", Me.txtBxFirstName.Text & "  " & Me.txtBxLastName.Text)

                Dim ccConfirm As paymentdetail = SubmitPayment(objOwner, objPayment)

                'Dim authRespons As String() = Split(processPayment, "|")
                If Trim(ccConfirm.CONFNUMBER) = "" Then
                    Me.imgAlert.Visible = True
                    Me.lblError.Visible = True
                    lblError.Text = "<font color=red>Error processing your payment, please contact customer service at 800.459.1597</font>"
                    Exit Sub
                ElseIf ccConfirm.CONFNUMBER.Length > 3 Then
                    'hide points, billing and credit panels


                    Try
                        If addPoints() Then 'add point if purchase is confirmed
                            lblOwnerId.Text = objOwner.OwnerVCNumber
                            lblOwnerName.Text = objOwner.OwnerFirstName + " " + objOwner.OwnerLastName
                            Dim dblPointsValue As Double
                            dblPointsValue = Convert.ToDouble(Me.ddlPtsValues.SelectedValue)
                            lblPaymentAmount.Text = dblPointsValue.ToString("0.00", CultureInfo.InvariantCulture) ' Me.ddlPtsValues.SelectedValue.ToString("0.00")
                            lblAuthorization.Text = ccConfirm.CONFNUMBER
                            Me.imgAlert.Visible = False
                            Me.lblError.Visible = False
                            Me.pnlSuccess.Visible = True
                            Me.pnlConfirm.Visible = False
                            SendEmail(Me.txtBxEmail.Text, Me.txtBxFirstName.Text, Me.txtBxLastName.Text, Me.txtBxPayment.Text, Session("pointsPurchased"))
                            Session("PalettReload") = True
                            'Insert As400 Comments

                            insertAs400Comments("TP Rent PTS for $" + Me.ddlPtsValues.SelectedValue + " @ " + Session("pointspurchased").ToString() + " PTS Auth: " + ccConfirm.CONFNUMBER)

                        Else
                            Me.imgAlert.Visible = True
                            lblError.Visible = True
                            Me.lblError.Text = "<font color=red>Your credit card has been successfully charged, but there was a problem adding points to your account; please, contact customer service at 800.459.1597 </font>"
                            Session("PalettReload") = True
                            Exit Sub
                        End If
                    Catch ex As Exception
                        Me.imgAlert.Visible = True
                        lblError.Visible = True
                        Me.lblError.Text = "<font color=red>" & ex.Message & "</font>"
                        'send error to developers
                        'Me.senderror(ex.Message, lblError.Text)
                    End Try



                Else
                    Me.imgAlert.Visible = True
                    Me.lblError.Visible = True
                    lblError.Text = "<font color=red>Error processing your payment, please contact customer service at 800.459.1597</font>"
                    Exit Sub
                End If
            Catch ex As Exception
                Me.imgAlert.Visible = True
                Me.lblError.Visible = True
                lblError.Text = "<font color=red>Error processing your payment, please contact customer service at 800.459.1597</font>"
                Exit Sub
            End Try

        Else
            Exit Sub
        End If
        ModalPopupExtender1.Show()
    End Sub
    'credit card cancel button click
    Protected Sub btnCCCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCCCancel.Click
        Me.ddlPtsValues.SelectedIndex = 0
        Me.points.Visible = True
        'Me.outOfPoints.Visible = True
        Me.ddlPtsValues_SelectedIndexChanged(sender, New System.EventArgs)
    End Sub
#End Region
#Region "OWNER TRANSACTIONS"
    Private Sub getOwnerInfo(ByVal OwnerNumber As String)

        If Session("BXGOwner") Is Nothing Then
            Response.Redirect("http://" & Request.ServerVariables("HTTP_HOST") & "/default.aspx?sess=timeout", False)
        Else
            bxgOwner = Session("BXGOwner")
        End If

        'check if user is logged in

        Try
            Me.txtBxFirstName.Text = bxgOwner.People(0).FirstName
            Me.txtBxLastName.Text = bxgOwner.People(0).LastName
            Me.txtBxBVC.Text = Session("arvact").ToString()
            Me.txtBxCity.Text = bxgOwner.People(0).Addresses(0).City

            Dim DBState As String = ""
            DBState = "US-" + bxgOwner.People(0).Addresses(0).ProvinceCode
            For Each lstitem As ListItem In ddlState.Items
                If lstitem.Value = DBState Then
                    ddlState.ClearSelection()
                    lstitem.Selected = True
                End If
            Next
            Me.txtBxZip.Text = bxgOwner.People(0).Addresses(0).PostalCode
            Me.txtBxEmail.Text = bxgOwner.People(0).EmailAddresses(0).Email
            Me.txtBxPhone.Text = bxgOwner.People(0).PhoneNumbers(0).PhoneNumber
            Session("Address") = bxgOwner.People(0).Addresses(0).AddressLine1
        Catch ex As Exception
        Finally

        End Try
    End Sub

#End Region
#Region "PAYMENT TRANSACTIONS"
    'validates the user input in all the required fields
    Private Function validateForm() As Boolean
        'Form input boolean check list
        Dim inputList As New List(Of Boolean)
        'checking user info
        If Me.ddlPtsValues.SelectedValue = "none" Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><br/><font color=red>Please select a points package</font>"
                inputList.Add(False)
            Else
                lblError.Text = "<font color=red>Please select a points package</font>"
                inputList.Add(False)
            End If
        Else
            inputList.Add(True)
        End If
        If Me.txtBxFirstName.Text.Length = 0 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><br/><font color=red>Please enter your first name</font>"
                inputList.Add(False)
            Else
                lblError.Text = "<font color=red>Please enter your first name</font>"
                inputList.Add(False)
            End If
        Else
            inputList.Add(True)
        End If
        If Me.txtBxLastName.Text.Length = 0 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><br/><font color=red>Please enter your last name</font>"
                inputList.Add(False)
            Else
                lblError.Text = "<font color=red>Please enter your last name</font>"
                inputList.Add(False)
            End If
        Else
            inputList.Add(True)
        End If
        If Me.txtBxCity.Text.Length = 0 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><br/><font color=red>Please enter your city</font>"
                inputList.Add(False)
            Else
                lblError.Text = "<font color=red>Please enter your city</font>"
                inputList.Add(False)
            End If
        Else
            inputList.Add(True)
        End If
        If Me.txtBxZip.Text.Length = 0 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><br/><font color=red>Please enter your zip code</font>"
                inputList.Add(False)
            Else
                lblError.Text = "<font color=red>Please enter your zip code</font>"
                inputList.Add(False)
            End If
        Else
            inputList.Add(True)
        End If
        If Me.txtBxPhone.Text.Length = 0 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><br/><font color=red>Please enter your phone number</font>"
                inputList.Add(False)
            Else
                lblError.Text = "<font color=red>Please enter your phone number</font>"
                inputList.Add(False)
            End If
        Else
            inputList.Add(True)
        End If
        If Me.txtBxEmail.Text.Length = 0 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><br/><font color=red>Please enter your Email address</font>"
                inputList.Add(False)
            Else
                lblError.Text = "<font color=red>Please enter your Email address</font>"
                inputList.Add(False)
            End If
        Else
            inputList.Add(True)
        End If
        'after collecting all the validation results, loop through the list. If one of the values is false return false    
        For Each check As Boolean In inputList
            If check = False Then
                Return check
            End If
        Next
        'at this point, all the values in the list are true, so return true
        Return True
    End Function
    'main cc validation method, which combines all helper method to validate the owner's ccard
    Private Function validateCC() As Boolean
        Dim cardNumIsValid As Boolean = True
        Dim cardDateIsValid As Boolean = True
        'checking credit card number and credit card type 
        If Not Me.CheckCC(Me.txtBxCCNumber.Text) And Not Me.IsValidForCardType(Me.txtBxCCNumber.Text, Me.ddlCCtype.SelectedValue) Then
            cardNumIsValid = False
        End If
        If Not Me.ValidateCCDate() Then
            cardDateIsValid = False
        End If
        If Not cardNumIsValid Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            lblError.Text = "<font color=red>The credit card you entered is not valid. Please check the number again or try another card.</font>"
            If Not cardDateIsValid Then
                Me.lblError.Text &= "<br/><br/><font color=red>Your credit card has expired. Please check the date again or try another card.</font>"
            End If
            Return False
        ElseIf Not cardDateIsValid Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            lblError.Text = "<font color=red>Your credit card has expired. Please check the date again or try another card.</font>"
            Return False
        Else
            Me.imgAlert.Visible = False
            lblError.Visible = False
            lblError.Text = ""
        End If
        Return True
    End Function

    'Helper method adds points to owner account on the AS400 after user purchases points
    Private Function addPoints() As Boolean
        'Add the points to the members account in the AS400
        'set up connectino object    
        Dim myConnection As SqlConnection = New SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings("bxgwebDBConnectionTPPOINTS"))
        'create datareader reference
        Dim dbDataReader As SqlDataReader
        'create command object reference
        Dim dbCmd As SqlCommand
        'validate phone number input
        Dim tmpphone As String = Nothing
        Dim pointsAdded As Boolean = False
        Dim parameters As New Dictionary(Of String, String)

        If IsNumeric(Trim(txtBxPhone.Text.ToString)) Then
            tmpphone = Trim(txtBxPhone.Text.ToString)
        Else
            tmpphone = "0"
        End If
        Try
            'open connection
            myConnection.Open()
            dbCmd = New SqlCommand("uspTravelerPlusAS400PointsSearch", myConnection) 'Initialize command object with stored procedure name and connection object parameters
            dbCmd.CommandType = System.Data.CommandType.StoredProcedure ' 
            'validate and set command object parameter values

            dbCmd.Parameters.Add(New SqlParameter("@vacclub", System.Data.SqlDbType.VarChar, 15)).Value = Session("OwnerNumber")
            dbCmd.Parameters.Add(New SqlParameter("@action", System.Data.SqlDbType.VarChar, 1)).Value = "R"
            dbCmd.Parameters.Add(New SqlParameter("@errorcode", System.Data.SqlDbType.VarChar, 1)).Value = " "
            dbCmd.Parameters.Add(New SqlParameter("@points", System.Data.SqlDbType.VarChar, 6)).Value = 0
            dbCmd.Parameters.Add(New SqlParameter("@deductpoints", System.Data.SqlDbType.VarChar, 15)).Value = Session("pointsPurchased")
            dbCmd.Parameters.Add(New SqlParameter("@name", System.Data.SqlDbType.VarChar, 30)).Value = " "
            dbCmd.Parameters.Add(New SqlParameter("@address", System.Data.SqlDbType.VarChar, 30)).Value = " "
            dbCmd.Parameters.Add(New SqlParameter("@citystatezip", System.Data.SqlDbType.VarChar, 30)).Value = " "
            dbCmd.Parameters.Add(New SqlParameter("@description", System.Data.SqlDbType.VarChar, 10)).Value = "Purchase Additional Points"
            dbCmd.Parameters.Add(New SqlParameter("@confirmation", System.Data.SqlDbType.VarChar, 10)).Value = " "
            dbCmd.Parameters("@points").Direction = ParameterDirection.InputOutput
            dbCmd.Parameters("@errorcode").Direction = ParameterDirection.InputOutput
            dbCmd.Parameters("@name").Direction = ParameterDirection.InputOutput
            dbCmd.Parameters("@address").Direction = ParameterDirection.InputOutput
            dbCmd.Parameters("@citystatezip").Direction = ParameterDirection.InputOutput

            For index = 0 To dbCmd.Parameters.Count - 1

                parameters.Add(dbCmd.Parameters(index).ParameterName, dbCmd.Parameters(index).Value)

            Next

            dbDataReader = dbCmd.ExecuteReader()

            'if output parameter @errorcode="A" display error message    
            If dbCmd.Parameters("@errorcode").Value = "A" Then
                pointsAdded = False

                'Send email notification to all parties, since it's failed!
                EmailNotification.Email.ToTrace("Rent Additional Points Failed..", parameters, dbCmd.Parameters("@errorcode").Value)

            Else
                pointsAdded = True

            End If

            myConnection.Close() ' when done with process close connection

        Catch ex As Exception
            pointsAdded = False

            'Send email notification to all parties, since it's failed!
            EmailNotification.Email.ToTrace("Rent Additional Points, exception occured.", parameters, ex.Message)

        End Try

        Return pointsAdded

    End Function

    'Helper method that returns a boolean value related to the cctype validity
    Private Function IsValidForCardType(ByVal cardnumber As String, ByVal cardtyp As String) As Boolean
        Dim validType As Boolean = False
        Select Case cardtyp
            Case "V"
                If (Regex.IsMatch(cardnumber, "^(4)")) And (cardnumber.Length = 16 Or cardnumber.Length = 13) Then
                    validType = True
                End If
            Case "M"
                If (Regex.IsMatch(cardnumber, "^5[1-5]\d{14}$|^2(?:2(?:2[1-9]|[3-9]\d)|[3-6]\d\d|7(?:[01]\d|20))\d{12}$")) And cardnumber.Length = 16 Then
                    validType = True
                End If
            Case "A"
                If (Regex.IsMatch(cardnumber, "^3(4|7)")) And cardnumber.Length = 15 Then
                    validType = True
                End If
            Case "D"
                If (Regex.IsMatch(cardnumber, "^(6011)")) And cardnumber.Length = 16 Then
                    validType = True
                End If
            Case Else
                validType = False
        End Select
        Return validType
    End Function
    'Helper method that validates credit card number
    Private Function CheckCC(ByVal strCCNo As String) As Boolean
        Try
            Dim i, w, x, y As Integer
            Dim CCNo As String

            y = 0
            CCNo = Replace(Replace(Replace(CStr(strCCNo), "-", ""), " ", ""), ".", "") 'Ensure proper format of the input
            If strCCNo <> CCNo Then
                'Whoah there, spaces and dashes and periods oh my!
                CheckCC = False
            Else
                'Process digits from right to left, drop
                '     last digit if total length is even
                w = 2 * (Len(CCNo) Mod 2)
                For i = Len(CCNo) - 1 To 1 Step -1
                    x = Mid(CCNo, i, 1)
                    If IsNumeric(x) Then
                        Select Case (i Mod 2) + w
                            Case 0, 3 'Even Digit - Odd where total length is odd (eg. Visa vs. Amx)
                                y = y + CInt(x)
                            Case 1, 2 'Odd Digit - Even where total length is odd (eg. Visa vs. Amx)
                                x = CInt(x) * 2
                                If x > 9 Then
                                    'Break the digits (eg. 19 becomes 1 + 9)
                                    '     
                                    y = y + (x \ 10) + (x - 10)
                                Else
                                    y = y + x
                                End If
                        End Select
                    End If
                Next
                'Return the 10's complement of the total
                '     
                y = 10 - (y Mod 10)
                If y > 9 Then y = 0
                CheckCC = (CStr(y) = Right(CCNo, 1))
            End If
        Catch ex As Exception
            'Whoops, that wasn't a number!
            CheckCC = False
        End Try
    End Function
    'Validates credit card expiration date    
    Private Function ValidateCCDate() As Boolean
        Dim bValid As Boolean = True
        Dim dtCreditCardDate As DateTime
        dtCreditCardDate = DateTime.Parse(Me.ddlExpMonth.SelectedValue & "/1/" & Me.ddlExpYear.SelectedItem.Text)
        dtCreditCardDate = dtCreditCardDate.AddMonths(1)
        'checking credit card expiration date
        If dtCreditCardDate.CompareTo(Now) < 0 Then
            bValid = False
        End If
        Return bValid
    End Function
    'populates creditcard dropdowns
    Private Sub populateCCDropDowns()
        'Load CheckIn and CheckOut drop downs
        Dim x As Integer
        Dim CCExpireYear As Integer = System.Configuration.ConfigurationManager.AppSettings("MaxCCExpireYear")
        For x = Year(Now) To Year(Now) + CCExpireYear
            Dim li2 As New ListItem
            li2.Text = x
            '   li2.Value = x
            li2.Value = (x - 2000) 'CC accept only YY format, so modified to YY
            If x = Today.Year Then
                li2.Selected = True
            End If
            Me.ddlExpYear.Items.Add(li2)
        Next
    End Sub
    'populates State dropdown
    Private Sub populateStateDropDown()
        'creating conncection instance
        Dim connection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("VSSA.bxgwebDBConnection"))
        Dim regionsDataReader As SqlDataReader
        Dim cmd As SqlCommand
        Try
            connection.Open() 'opening connection
            'creating command object
            cmd = New SqlCommand("uspCmsSelectRegions", connection)
            regionsDataReader = cmd.ExecuteReader()
            While regionsDataReader.Read()
                Dim li As New ListItem
                li.Text = Trim(regionsDataReader("Subdivision1").ToString())
                'some states are retrieved from db with unecessary info, make sure this info is removed
                If li.Text.Trim = Trim("Guam (see also separate entry under GU)") Then
                    li.Text = "Guam"
                ElseIf li.Text.Trim = "Northern Mariana Islands (see also separ".Trim Then
                    li.Text = "Northern Mariana Islands"
                ElseIf li.Text.Trim = "Puerto Rico (see also separate entry und".Trim Then
                    li.Text = "Puerto Rico"
                ElseIf li.Text.Trim = "United States Minor Outlying Islands (se".Trim Then
                    li.Text = "United States Minor Outlying Islands"
                ElseIf li.Text.Trim = "Virgin Islands, U.S. (see also separate".Trim Then
                    li.Text = "Virgin Islands"
                ElseIf li.Text.Trim = "American Samoa (see also separate entry ".Trim Then
                    li.Text = "American Samoa"
                End If
                li.Value = regionsDataReader("RegionCode").ToString()
                If li.Text.ToLower() = "florida" Then
                    li.Selected = True
                End If
                Me.ddlState.Items.Add(li)
            End While
        Catch ex As Exception
        Finally
            connection.Close()
            cmd.Dispose()
            connection.Dispose()
        End Try
    End Sub
    'processes credit card payment
    'sends confirmation email    
    Private Function SendEmail(ByVal email As String, ByVal first As String, ByVal last As String, ByVal amount As String, ByVal points As String) As String

        Dim sTemplate As String
        Dim sEmailBody As String = ""
        Dim EM As New BluegreenOnline.clsEmailer

        sTemplate = IO.Path.Combine(Server.MapPath(""), "EmailTemplates\PointsPurchase.html")
        Dim sr As New IO.StreamReader(sTemplate)
        Do While Not sr.Peek = -1
            sEmailBody &= sr.ReadLine()
        Loop
        sr.Close()

        'Replace template with data
        sEmailBody = sEmailBody.Replace("|IMAGEPATH|", System.Configuration.ConfigurationManager.AppSettings("BonusTimewebImgPath"))
        sEmailBody = sEmailBody.Replace("|DATE|", Now.ToShortDateString())
        sEmailBody = sEmailBody.Replace("|OWNERID|", Session("OwnerNumber"))
        sEmailBody = sEmailBody.Replace("|OWNERNAME|", StrConv(first & " " & last, VbStrConv.ProperCase))
        sEmailBody = sEmailBody.Replace("|ARVACT|", Session("OwnerNumber"))
        sEmailBody = sEmailBody.Replace("|LASTNAME|", Trim(last))
        sEmailBody = sEmailBody.Replace("|EMAIL|", Trim(email))
        sEmailBody = sEmailBody.Replace("|POINTS|", points)
        sEmailBody = sEmailBody.Replace("|AMOUNT|", amount)
        sEmailBody = sEmailBody.Replace("|COPYRIGHTYEAR|", System.DateTime.Now.Year)
        'Send the email to Owner
        EM.ToEmail = email
        If Not Request.Url().ToString().StartsWith("http://pro-leadstg.bxgcorp.com") Or Not Request.Url().ToString().StartsWith("http://stgweb01") Then

            EM.BCCEmail = "007@bluegreencorp.com;orpoints@bluegreencorp.com" 'BCC email to 007 inbox
        Else
            EM.BCCEmail = "orpoints@bluegreencorp.com"
        End If
        EM.FromEmail = System.Configuration.ConfigurationManager.AppSettings("ReservationRequestEmail")

        EM.Subject = "You have rented additional points."
        EM.BodyText = sEmailBody.Replace("™", "&trade;")
        EM.emailBodyFormat = System.Web.Mail.MailFormat.Html
        EM.sendEmail("Bluegreen Resorts")
        Return "Success"
    End Function
    'send technical error if authorize payment process fails
    Sub senderror(ByVal ex As String, ByVal message As String)
        Dim str As String = ex
        Dim errormessage As String = ""
        Dim EM As New BluegreenOnline.clsEmailer
        EM.ToEmail = "olpsupport@bluegreencorp.com"
        EM.FromEmail = System.Configuration.ConfigurationManager.AppSettings("FromEmailTP")
        EM.Subject = "TP Renewal Error"
        EM.BodyText = "<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01 Transitional//EN""><!-- ""http://www.w3.org/TR/html4/loose.dtd"" -->" &
                      "<html><head><title>Bluegreen Traveler Plus Renewals</title></head><body topmargin=0 leftmargin=0 marginheight=0 marginwidth=0>" &
                      "<table width=490 cellpadding=0 cellspacing=0 border=0 class=text>" &
                      "<tr>" &
                      "<td valign=top>Renewal Exception:<br> " & message & " </td></tr>" &
                      "<tr>" &
                      "<td valign=top>Renewal Exception:<br>" & Session("OwnerNumber") &
                      "<tr>" &
                      "<td valign=top>Renewal Exception:<br>" & str & "</td></tr></table></body></html>"

        EM.BodyText &= "Account number:" & Session("AccountNumber") & " <br>"
        EM.BodyText &= "Owner has already paid money. Failed at second process. <br>"
        'EM.emailBodyFormat = Mail.MailFormat.Html
        EM.sendEmail()
    End Sub
#End Region
End Class
'End Namespace



