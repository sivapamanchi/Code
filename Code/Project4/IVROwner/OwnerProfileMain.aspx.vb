Imports VSSA.ResortsService
Imports ReservationLibrary
Imports System.Collections.Generic
Imports System.ComponentModel
Imports BxgCorp.Mortgage
Imports VSSA.IVRSearchSvc
Imports System.Threading
Imports System.Runtime.Remoting.Messaging
Imports VSSA.fetchresp
Imports VSSA.WaiversReq
Imports VSSA.ResortServices
Imports System.Globalization
Imports VSSA.OwnerServices
Public Class OwnerProfileMain
    Inherits VSSABaseClass
    Public bxgowner As New OwnerFetchResponse
    Public raisedRedFlag As Boolean = False
    Public OwnerArvact As String
    Public total As Double
    Public _PolicyEligible As Boolean
    Public _PolicyStatus As String
    Public _Status As String
    Public _PromoCodeActived As Boolean = Nothing
    Public ivrReqParams As New IVRRequest
    Public reqParamsStr As String
    Public ANI As String
    Public owner As New BXG_Owner
    Delegate Function WorkDelegate(arg As String) As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        reqParamsStr = Request.QueryString("UUI")
        ANI = Request.QueryString("ani")

        Session("UUI") = reqParamsStr
        Session("ani") = ANI

        If Not IsNothing(reqParamsStr) Then
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo.Start()
            Dim results As String = ValidateRequest(reqParamsStr, ANI, "OWNER")

            If results <> "" Then
                'lblError.Text = results
                Exit Sub
            End If


            PopulateSearchResults()

            If OwnerArvact = "" Or ivrReqParams.Disposition = "MR" Or ivrReqParams.Disposition = "NZ" Or ivrReqParams.Disposition = "SU" Then
                Response.Redirect("SearchOwner.aspx?ani=" & ANI & "&UUI=" & reqParamsStr)
            End If


            Dim d1 As DateTime = DateTime.Now



            Dim s As TimeSpan = DateTime.Now - d1


            LoadAccountHolderNames()
            LoadAcctContractInfo()
            LoadPaymentInfo()
            LoadOwnerPoints(OwnerArvact)
            LoadReservationHistory()
            OwnerWaivers()
            LoadOwnerPurchaseDate()
            LoadMortgageAccountSummary()

            stpWatchInfo.Stop()
            ProcessTime.Text &= "Total Time:" & stpWatchInfo.Elapsed.ToString & "<br>"
        Else
            'lblError.Text = "Request Parameters can't be empty."
        End If


        If ConfigurationManager.AppSettings("DisplayProcessTime") = "1" Then
            ProcessTime.Visible = True
        End If


    End Sub


    Public Sub PopulateSearchResults()

        'Owner is in blacklist
        Try
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo = New Stopwatch
            stpWatchInfo.Start()

            ivrReqParams = GetRequestParameters(reqParamsStr, ANI)

            Select Case ivrReqParams.Disposition
                Case "LH"
                    lblAcctStatus.Text = "Legal Hold"
                Case "DL"
                    lblAcctStatus.Text = "Delinquent"
                Case Else
                    lblAcctStatus.Text = "N/A"
            End Select


            If ivrReqParams.ANIPhone IsNot Nothing Then
                lblCaptured.Text = FormatPhone(ivrReqParams.ANIPhone)
            End If

            If ivrReqParams.ValidationPhone IsNot Nothing Then
                lblSearched.Text = FormatPhone(ivrReqParams.ValidationPhone)
            End If

            If ClubServiceMapOptionTable(ivrReqParams.MenuId).Trim() = "" Then
                pnlMenu.Visible = False
            Else
                lblMenuOption.Text = ClubServiceMapOptionTable(ivrReqParams.MenuId)
            End If

            lblDisposition.Text = GetDispositionDesc(ivrReqParams.Disposition)
            lblTransferTxt.Text = ClubServiceTransferTextTable(ivrReqParams.TransaferId)
            OwnerArvact = ivrReqParams.Arvact
            Session("arvact") = ivrReqParams.Arvact
            If Not String.IsNullOrEmpty(OwnerArvact) Then

                raisedRedFlag = OwnerBlackList.isOwnerIntheBlackList(CType(OwnerArvact, Integer))
                bxgowner = Session("BoomiOwner")
                PopulateOwnerInfo(bxgowner)

            End If
            If IsNothing(bxgOwner) Then
                lblError.Text = "No Record Found."
            End If
            PopulateOwnerInfo(bxgOwner)

            stpWatchInfo.Stop()
            ProcessTime.Text &= "PopulateSearchResults:" & stpWatchInfo.Elapsed.ToString & "<br>"

        Catch ex As Exception
            lblError.Text = "Process error at  (PopulateSearchResults)-" & ex.Message.ToString()
        End Try


    End Sub

    Public Sub LoadAccountHolderNames()

        Try
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo = New Stopwatch
            stpWatchInfo.Start()

            fetchIVROwnerAccountsHolders(OwnerArvact)
            Dim accountHolder As New List(Of AccountHolder)
            accountHolder = DirectCast(Session("accountHolder"), List(Of AccountHolder))

            If Not IsNothing(accountHolder) Then


                Dim primaryNames = From name In accountHolder
                                   Where name.OwnerHolderType = "0"
                                   Select name

                Dim secNames = From name In accountHolder
                               Where name.OwnerHolderType = "1"
                               Select name

                Dim thirdNames = From name In accountHolder
                                 Where name.OwnerHolderType = "2"
                                 Select name


                rptprimaryNames.DataSource = primaryNames
                rptprimaryNames.DataBind()

                rptsecNames.DataSource = secNames
                rptsecNames.DataBind()

                rptthirdNames.DataSource = thirdNames
                rptthirdNames.DataBind()

            End If

            stpWatchInfo.Stop()
            ProcessTime.Text &= "LoadAccountHolderNames:" & stpWatchInfo.Elapsed.ToString & "<br>"

        Catch ex As Exception
            lblError.Text = " Process error at (LoadAccountHolderNames)-" & ex.Message.ToString()
        End Try


    End Sub

    Public Sub PopulateOwnerInfo(ByVal bxgOwner As OwnerFetchResponse)
        bxgOwner = Session("BoomiOwner")
        Try
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo = New Stopwatch
            stpWatchInfo.Start()
            Try
                Dim svc As New SitecoreProfileSvc.ProfileSoapClient
                Dim req As New SitecoreProfileSvc.IsUserRegisterRequest
                Dim reqBody As New SitecoreProfileSvc.IsUserRegisterRequestBody

                reqBody.ARVACT = bxgOwner.Identifier
                If Not bxgOwner.People(0).EmailAddresses Is Nothing Then
                    reqBody.email = bxgOwner.People(0).EmailAddresses(0).Email
                Else
                    reqBody.email = "N/A"
                End If
                req.Body = reqBody

                Dim resp As SitecoreProfileSvc.IsUserRegisterResponse = svc.SitecoreProfileSvc_ProfileSoap_IsUserRegister(req)
                If resp.Body.IsUserRegisterResult.Equals(True) Then
                    Session("Registered") = "Yes"
                Else
                    Session("Registered") = "No"
                End If
            Catch ex As Exception

                Session("Registered") = "No"
            End Try
            With owner
                lblRegister.Text = Session("Registered").ToString()
            End With
            lblArvact.Text = bxgOwner.Identifier


            rpOwnerTypes.DataSource = bxgOwner.Account.Memberships.VacationClubMembership.Accounts.AccountInfo.ToList()
            rpOwnerTypes.DataBind()

            For Each account As fetchresp.AccountInfo In bxgOwner.Account.Memberships.VacationClubMembership.Accounts.AccountInfo
                If Not (bxgOwner.Account.Memberships.VacationClubMembership.Sampler Is Nothing) Then
                    If Not (bxgOwner.Account.Memberships.VacationClubMembership.Sampler.IsSampler = True) Then
                        If (account.Legacy.ProjectNumber = "50") Then
                            lblOwnerLevel.Text = bxgOwner.Account.Memberships.VacationClubMembership.Membership.MembershipLevelDescription
                        End If
                    End If
                End If
            Next
            lblAddress.Text = bxgOwner.People(0).Addresses(0).AddressLine1

            If bxgOwner.People(0).Addresses(0).CountryCode = "US" Then

                If bxgOwner.People(0).Addresses(0).ProvinceCode = "AA" Or bxgOwner.People(0).Addresses(0).ProvinceCode = "" Then  'tuned based on the state setup on Owner service fetch owner 
                    bxgOwner.People(0).Addresses(0).ProvinceCode = bxgOwner.People(0).Addresses(0).Subdivison
                End If

                lblCity.Text = bxgOwner.People(0).Addresses(0).City & ", " & bxgOwner.People(0).Addresses(0).ProvinceCode & " " & bxgOwner.People(0).Addresses(0).PostalCode

            Else

                lblCity.Text = bxgOwner.People(0).Addresses(0).AddressLine1 & " " & bxgOwner.People(0).Addresses(0).AddressLine2

            End If




            If bxgOwner.People(0).PhoneNumbers.Count = 1 Then
                If bxgOwner.People(0).PhoneNumbers(0).PhoneNumber IsNot Nothing Then
                    lblHomePhone.Text = FormatPhone(bxgOwner.People(0).PhoneNumbers(0).PhoneNumber)
                    lblBusinessPhone.Text = "N/A"
                End If
            Else
                If bxgOwner.People(0).PhoneNumbers(0).PhoneNumber IsNot Nothing Then
                    lblHomePhone.Text = FormatPhone(bxgOwner.People(0).PhoneNumbers(0).PhoneNumber)
                    lblBusinessPhone.Text = FormatPhone(bxgOwner.People(0).PhoneNumbers(1).PhoneNumber)
                End If
            End If


            If Not bxgOwner.People(0).EmailAddresses Is Nothing Then
                lblEmail.Text = bxgOwner.People(0).EmailAddresses(0).Email
            Else
                lblEmail.Text = "N/A"
            End If

            lblPaperless.Text = UCase(CType(bxgOwner.Account.Memberships.VacationClubMembership.Paperless.PaperlessDelivered, String))

            If Not bxgOwner.Account.Memberships.TravelerPlusMembership.Membership Is Nothing Then
                lblTpLevel.Text = bxgOwner.Account.Memberships.TravelerPlusMembership.Membership.MembershipLevel
                lblTpRenewal.Text = CType(bxgOwner.Account.Memberships.TravelerPlusMembership.Renewal.AutoRenewal, String)
                lblTpExpDate.Text = bxgOwner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate
            End If

            lblEncDividend.Text = bxgOwner.Account.Memberships.BluegreenRewardsMembership.Rewards.RewardsBalance.ToString()

            If Not bxgOwner.Account.Memberships.VacationClubMembership.Legacy.BonusTime Is Nothing Then
                If bxgOwner.Account.Memberships.VacationClubMembership.Legacy.BonusTime.BlueWhiteCertificateEligible = "NotEligible" Then
                    lblBHStays.Text = "0"
                Else
                    lblBHStays.Text = "1"
                End If
            End If
            If bxgOwner.Financial.InstallmentPlanEligible = "1" Then
                lblInstallPlan.Text = "Yes"
            End If
            If Not (bxgOwner.Financial.InstallmentPlans Is Nothing) Then
                If bxgOwner.Financial.InstallmentPlans.InstallmentPlan(0).InstallmentPlanStatus = "IS" Or bxgOwner.Financial.InstallmentPlans.InstallmentPlan(0).InstallmentPlanStatus = "ID" Then
                    lblInstallPlan.Text = "Yes"
                Else
                    lblInstallPlan.Text = "N/A"
                End If
                lblPmtPlan.Text = "N/A"
            End If
            lblTotalBillablePts.Text = bxgOwner.Account.Memberships.VacationClubMembership.Points.TotalAnnualPoints.ToString()
            lblTotalAvailPts.Text = bxgOwner.Account.Memberships.VacationClubMembership.Points.TotalPoints.ToString()
            lblBillDate.Text = FetchOwnerBillDate(Session("arvact"))

            stpWatchInfo.Stop()
            ProcessTime.Text &= "PopulateOwnerInfo:" & stpWatchInfo.Elapsed.ToString & "<br>"

        Catch ex As Exception
            lblError.Text = " Process error at  (PopulateOwnerInfo)-" & ex.Message.ToString()
        End Try


    End Sub

    Protected Sub rpBilling_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)

        If (e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem) Then
            total += Convert.ToDouble(CType(e.Item.FindControl("lblAmtDue"), Label).Text)
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            CType(e.Item.FindControl("lblTotalDue"), Label).Text = total.ToString("N")
        End If

    End Sub

    Public Sub LoadPaymentInfo()
        Try
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo = New Stopwatch
            stpWatchInfo.Start()


            Dim result As DataSet = FetchOwnerPayBalence(CType(OwnerArvact, Integer))

            Dim dt As New DataTable()

            dt = result.Tables(0)


            For Each dr As DataRow In dt.Rows
                If dr("CollectionCode").ToString() = "PP" Then
                    lblPmtPlan.Text = "Yes"
                End If
            Next


            rpBilling.DataSource = result
            rpBilling.DataBind()

            stpWatchInfo.Stop()
            ProcessTime.Text &= "LoadPaymentInfo:" & stpWatchInfo.Elapsed.ToString & "<br>"

        Catch ex As Exception
            lblError.Text = "Process error at  (LoadPaymentInfo)-" & ex.Message.ToString()
        End Try

    End Sub

    Private Sub LoadReservationHistory()
        Try
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo = New Stopwatch
            stpWatchInfo.Start()


            Dim history As New ResortsService.ReservationHistory
            Dim historyResultFuture As New ResortsService.ReservationHistoryList
            Dim historyResultPast As New ResortsService.ReservationHistoryList
            Dim reservationAS400 As New ResortsService.ResortsServiceClient
            Dim owner As New ResortsService.OwnerID

            history.OwnerID = Nothing
            owner.OwnerVacationNumber = OwnerArvact
            history.OwnerID = owner
            history.SiteName = Sites.OnlinePoints
            history.EffectiveDate = Date.Today
            history.SearchHistoryBy = ReservationHistoryType.Future
            historyResultFuture = reservationAS400.GetReservationsHistory(history)
            history.SearchHistoryBy = ReservationHistoryType.Past
            historyResultPast = reservationAS400.GetReservationsHistory(history)
            Dim countFutureReservations As Integer = GetReservationsCount(historyResultFuture.ReservationHistoryItem)
            Dim countPastReservations As Integer = GetReservationsCount(historyResultPast.ReservationHistoryItem)

            lblTotalReservations.Text = (countFutureReservations + countPastReservations).ToString()

            If historyResultFuture.ReservationHistoryItem.Length = 0 Then


                ReDim Preserve historyResultFuture.ReservationHistoryItem(0)

                historyResultFuture.ReservationHistoryItem(0) = New VSSA.ResortsService.ReservationHistoryItem()
                historyResultFuture.ReservationHistoryItem(0)._ReservationNumber = "N/A"
                historyResultFuture.ReservationHistoryItem(0)._ReservationType = "N/A"
                historyResultFuture.ReservationHistoryItem(0)._ExchangeCode = "N/A"
                historyResultFuture.ReservationHistoryItem(0)._GuestFullName = "N/A"
                historyResultFuture.ReservationHistoryItem(0)._TakenBy = "N/A"
                historyResultFuture.ReservationHistoryItem(0)._ProjectStay = "0"
                historyResultFuture.ReservationHistoryItem(0)._CheckInDate = ""
                historyResultFuture.ReservationHistoryItem(0)._AS400UnitType = "N/A"
                historyResultFuture.ReservationHistoryItem(0)._NumberOfNights = "N/A"
                historyResultFuture.ReservationHistoryItem(0)._Points = "N/A"
                historyResultFuture.ReservationHistoryItem(0)._AmountDue = "N/A"
                historyResultFuture.ReservationHistoryItem(0)._PolicyStatus = "N/A"
                historyResultFuture.ReservationHistoryItem(0)._EligibleDate = "0"

            End If

            Me.rptPendingReservations.DataSource = historyResultFuture.ReservationHistoryItem
            Me.rptPendingReservations.DataBind()


            stpWatchInfo.Stop()
            ProcessTime.Text &= "LoadReservationHistory:" & stpWatchInfo.Elapsed.ToString & "<br>"

        Catch ex As Exception
            lblError.Text = "Process error at   (LoadReservationHistory)-" & ex.Message.ToString()
        End Try


    End Sub


    Public Sub LoadOwnerPoints(ByVal arvact As String)
        Try
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo = New Stopwatch
            stpWatchInfo.Start()

            Dim AS400SavePointsBuckets As DataTable = FetchAS400SavePointsBucket(arvact)
            Dim OwnerPointsBuckets As DataTable = FetchOwnerPointsBuckets(AS400SavePointsBuckets, bxgowner)

            If OwnerPointsBuckets.Rows.Count = 0 Then
                OwnerPointsBuckets.Rows.Add("N/A", -1, "N/A", "N/A", "N/A", "N/A", "N/A", "N/A")
            End If

            grdOwnerPoints.DataSource = OwnerPointsBuckets
            grdOwnerPoints.DataBind()


            stpWatchInfo.Stop()
            ProcessTime.Text &= "LoadOwnerPoints:" & stpWatchInfo.Elapsed.ToString & "<br>"


        Catch ex As Exception
            lblError.Text = " Process error at  (LoadOwnerPoints)-" & ex.Message.ToString()
        End Try



    End Sub

    Private Sub LoadAcctContractInfo()

        Try
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo = New Stopwatch
            stpWatchInfo.Start()

            Dim accList As New List(Of OwnerAcctResp.AccountInfo)
            accList = FetchAcctContractInfo(OwnerArvact)

            For Each account As OwnerAcctResp.AccountInfo In accList
                If IsNumeric(account.NextEarnAmount) Then
                    account.NextEarnAmount = String.Format("{0:N0}", Convert.ToInt32(account.NextEarnAmount))
                End If
            Next

            If accList.Count = 0 Then
                For Each account As OwnerAcctResp.AccountInfo In accList
                    account.Legacy.ProjectName = "N/A"
                    account.Legacy.Expiration = "N/A"
                    account.AccountNumber = "N/A"
                    account.NextEarnAmount = "N/A"
                Next
            End If

            grdContracts.DataSource = accList
            grdContracts.DataBind()
            stpWatchInfo.Stop()
            ProcessTime.Text &= "LoadAcctContractInfo:" & stpWatchInfo.Elapsed.ToString & "<br>"

        Catch ex As Exception
            lblError.Text = "Process error at  (LoadAcctContractInfo)-" & ex.Message.ToString()
        End Try

    End Sub

    Function getResortName(ByVal _projNum As Integer, ByVal _reservationType As String) As String
        Dim resort As New ResortInfo

        Try
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo = New Stopwatch
            stpWatchInfo.Start()

            If _projNum = 0 Then
                Return "N/A"
            End If

            resort = ReservationLibrary.Utilities.getResortInfo(_projNum)
            If Trim(_reservationType) = "K" Or Trim(_reservationType) = "L" Then
                resort.ResortName = "Preferred Status Exchange Resorts"
            End If
        Catch ex As Exception
            lblError.Text = "Process error at   (getResortName)-" & ex.Message.ToString()
        End Try



        Return resort.ResortName

    End Function



    Public Function NumericToDate(ByVal source As String, ByVal format As String) As String
        Dim temp As String = ""

        If IsNothing(source) Then
            Return "00/00/0000"
        End If

        If source.Length = 0 Then
            Return "00/00/0000"
        ElseIf source.Length = 3 Then
            source = "000" & source
        ElseIf source.Length = 4 Then
            source = "00" & source
        End If

        If source.Length >= 5 And IsNumeric(source) Then

            If source.Length = 5 Then
                source = "0" & source
            End If

            If format = "yyMMdd" Then
                temp = source.Substring(2, 2) & "/" & source.Substring(4, 2) & "/" & source.Substring(0, 2)
            ElseIf format = "MMddyy" Then
                temp = source.Substring(0, 2) & "/" & source.Substring(2, 2) & "/" & source.Substring(4, 2)
            End If

            Return CDate(temp).ToString("MM/dd/yyyy")

        Else

            Return "00/00/0000"

        End If

    End Function

    Public Function CalcCheckOutDate(ByVal days As Integer, ByVal checkIn As String) As String

        Try
            Dim dtm As DateTime = Convert.ToDateTime(NumericToDate(checkIn, "yyMMdd"))

            dtm = dtm.AddDays(days)

            Return dtm.ToString("d")

        Catch ex As Exception
            lblError.Text = "We are unable to process your request. CalcCheckOutDate-" & ex.Message.ToString()
            Return "00/00/00"

        End Try

    End Function

    Function getVillaDescription(ByVal pn As Integer, ByVal ut As String) As String

        Dim xx As String = Nothing

        Try

            xx = ReservationLibrary.Utilities.getUnitDescription(pn, ut)

            If String.IsNullOrEmpty(xx) Then
                xx = "N/A"

            End If

        Catch ex As Exception

            lblError.Text = "We are unable to process your request. getVillaDescription-" & ex.Message.ToString()

        End Try

        Return xx

    End Function

    Shared Function ReservationType(ByVal rt As String, ByVal exch As String) As String

        rt = ReservationLibrary.Utilities.GetReservationDescription(Trim(rt))

        'Determine if this is an exchange reservation.  If it is, then exch will be populated.
        'if it is, then rt needs to be "exch*"  
        'This was added after a column labelled Exchange was removed from the reservation
        'repeater as a way to display an exchange reservation type.

        If Trim(exch) = "#" Then
            rt = "Exch*"
        ElseIf Trim(exch) = "@" Then
            rt = "Exch*"

        ElseIf Trim(exch) = "*" Then
            rt = "Exch*"

        ElseIf Trim(exch) = "&" Then
            rt = "Exch*"
        End If

        Return rt


    End Function
    Function convertPoints(ByVal s As String, ByVal t As String) As String

        If t = "P" Then

            s = Format(CInt(s), "##,##0")
            Return s

        Else
            Return "N/A"

        End If


    End Function
    Public Function PolicyStatus(ByVal Policy As String, ByVal EligibilityDate As String, ByVal ReservationCondition As String, ByVal ReservationNbr As String, ByVal ExchangeType As String, ByVal ReservationType As String) As String

        Dim _PolicyISODate As Date = Nothing
        Dim _dateTimeInfo As Date = DateTime.Now    'Left(DateTime.Now.ToString("s"), 10)
        Dim RowNo As Integer = Nothing
        Dim _lnkClientID As String = Nothing
        Dim _ClientID As String = Nothing

        _PolicyEligible = Nothing
        _PolicyStatus = Nothing
        _Status = Nothing


        If ReservationCondition = "Future" Then

            If _PromoCodeActived Then

                Select Case Policy
                    Case "A"
                        _PolicyStatus = "Yes" 'Active
                    Case Else
                        _PolicyStatus = "No"
                End Select


                If Trim(ReservationType) <> "P" Then
                    _PolicyStatus = "N/A"
                ElseIf Trim(ExchangeType) = "#" Or Trim(ExchangeType) = "@" Or Trim(ExchangeType) = "*" Or Trim(ExchangeType) = "&" Or Trim(ExchangeType) = "G" Or Trim(ExchangeType) = "Q" Then
                    _PolicyStatus = "N/A"
                End If

            Else


                If EligibilityDate <> "0" Then

                    '_PolicyISODate = EligibilityDate.Substring(4, 2) & "/" & EligibilityDate.Substring(6, 2) & "/" & EligibilityDate.Substring(0, 4)

                    _PolicyISODate = Convert.ToDateTime(EligibilityDate.Substring(4, 2) & "/" & EligibilityDate.Substring(6, 2) & "/" & EligibilityDate.Substring(0, 4))
                    'CDate(_PolicyISODate).ToString("d")

                    If _dateTimeInfo <= _PolicyISODate Then
                        _PolicyEligible = True
                    Else
                        _PolicyEligible = False
                    End If

                    Select Case Policy
                        Case "A"
                            _PolicyStatus = "Yes" 'Active
                        Case "D"
                            If _PolicyEligible And Trim(ReservationType) = "P" Then
                                _PolicyStatus = "No"
                            Else
                                _PolicyStatus = "No"
                            End If
                        Case Else
                            _PolicyStatus = "N/A"
                    End Select

                    If Trim(ExchangeType) = "#" Or Trim(ExchangeType) = "@" Or Trim(ExchangeType) = "*" Or Trim(ExchangeType) = "&" Or Trim(ExchangeType) = "G" Or Trim(ExchangeType) = "Q" Then
                        _PolicyStatus = "N/A"
                    End If



                Else
                    If Trim(ReservationType) <> "P" Then
                        _PolicyStatus = "N/A"
                    ElseIf Trim(ExchangeType) = "#" Or Trim(ExchangeType) = "@" Or Trim(ExchangeType) = "*" Or Trim(ExchangeType) = "&" Or Trim(ExchangeType) = "G" Or Trim(ExchangeType) = "Q" Then
                        _PolicyStatus = "N/A"
                    End If
                End If

            End If


        Else '<---- Past Reservations

            Select Case Policy
                Case "A"
                    _PolicyStatus = "Protected"
                Case "S"
                    _PolicyStatus = "Consumed"
                Case "D", "", " "
                    _PolicyStatus = "Not Protected"
                Case "E"
                    _PolicyStatus = "Expired"
                Case "T"
                    _PolicyStatus = "Transfered"
                Case "X"
                    _PolicyStatus = "Refunded"
                Case Else
                    _PolicyStatus = "-"
            End Select

        End If

        Return _PolicyStatus

    End Function

    Public Function GetReservationsCount(ByVal reservationList As Object) As Integer
        Dim currentMonth As DateTime = DateTime.Parse("01/" & DateTime.Now.Month & "/" & DateTime.Now.Year)
        Dim oneYearBack As DateTime = currentMonth.AddYears(-1)

        Dim resCount As Integer = 0
        For Each reservationItem As ResortsService.ReservationHistoryItem In reservationList
            Dim dateCreated As DateTime = Convert.ToDateTime(NumericToDate(reservationItem._DateConfirmed, "yyMMdd"))

            If dateCreated >= oneYearBack Then
                resCount = resCount + 1

            End If

        Next
        Return resCount

    End Function


    Public Sub LoadMortgageAccount()
        Dim loans As New List(Of Loan)

        Try
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo = New Stopwatch
            stpWatchInfo.Start()

            loans = GetLoansByArvact(bxgOwner.Identifier)

            stpWatchInfo.Stop()
            ProcessTime.Text &= "LoadMortgageAccount:" & stpWatchInfo.Elapsed.ToString & "<br>"

        Catch ex As Exception
            lblError.Text = "We are unable to process your request. LoadMortgageAccount-" & ex.Message.ToString()
        End Try


    End Sub

    Public Sub LoadMortgageAccountSummary()
        Dim loans As New List(Of LoanSummary)
        lblError.Visible = True

        Try
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo = New Stopwatch
            stpWatchInfo.Start()

            Dim MOLUp As Boolean = isMortgageMaintenance()

            If Not MOLUp Then


                loans = GetLoanSummary(bxgOwner.Identifier)

                For Each Loan As LoanSummary In loans
                    If Loan.DelinquentDays <> "" Then
                        Loan.DelinquentDays = Convert.ToInt32(Loan.DelinquentDays).ToString("N0")
                    End If
                Next

                If loans IsNot Nothing And loans.Count = 0 Then
                    loans.Add(New LoanSummary() With {.DelinquentDays = "N/A", .LoanStatus = "N/A", .LoanNumber = -1, .DelinquentAmount = -1})

                End If

                grdMortgage.DataSource = loans
                grdMortgage.DataBind()
            Else
                lblError.Text &= "The Mortgage section information of our site is currently down for maintenance.<br>"
            End If



            stpWatchInfo.Stop()
            ProcessTime.Text &= "LoadMortgageAccountSummary:" & stpWatchInfo.Elapsed.ToString & "<br>"

        Catch ex As Exception
            lblError.Text = "Process error at  (LoadMortgageAccountSummary)-" & ex.Message.ToString()
        End Try


    End Sub

    Protected Sub grdMortgage_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)


        If (e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem) Then

            Dim lblLoan As Label = CType(e.Item.FindControl("lblLoan"), Label)

            If lblLoan.Text = "-1" Then
                lblLoan.Text = "N/A"
            End If

            Dim lblDelqAmt As Label = CType(e.Item.FindControl("lblDelqAmt"), Label)

            If lblDelqAmt.Text = "-1.00" Then
                lblDelqAmt.Text = "N/A"
            End If

        End If

    End Sub
    Public Sub LoadOwnerPurchaseDate()
        Dim owner As New Owner

        Try
            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo.Start()

            Dim MOLUp As Boolean = isMortgageMaintenance()

            If Not MOLUp Then
                owner = GetOwnerPurchaseDate(bxgOwner.Identifier)
                If UCase(owner.UpgradeStatus) = "Y" Or UCase(owner.ReloadStatus) = "Y" Then
                    lblUpgrade.Text = "Yes"
                End If

                If owner.LastSaleDate IsNot Nothing Then
                    If owner.LastSaleDate <> "" And Len(owner.LastSaleDate) = 6 And owner.LastSaleDate.Substring(0, 2) <> "98" Then

                        lblLastPurchase.Text = owner.LastSaleDate.Substring(2, 2) & "/" & owner.LastSaleDate.Substring(4, 2) & "/20" & owner.LastSaleDate.Substring(0, 2)

                    End If
                End If

                If owner.UpgradeStatus = "" Then
                    lblUpgrade.Text = "N/A"
                End If
            Else
                lblError.Text &= "The OwnerSaleDate information of our site is currently down for maintenance.<br>"
            End If


            stpWatchInfo.Stop()
            ProcessTime.Text &= "LoadOwnerPurchaseDate:" & stpWatchInfo.Elapsed.ToString & "<br>"
        Catch ex As Exception
            lblError.Text = "Process error at  (LoadOwnerSaleDate)-" & ex.Message.ToString()
        End Try
    End Sub

    Public Sub OwnerWaivers()
        Try

            Dim stpWatchInfo As New Stopwatch
            stpWatchInfo.Start()

            Dim WaivArvact As New OwnerWaiversRequest()
            WaivArvact.OwnerID = Session("arvact")
            lblWaivers.Text = GetWaivers(WaivArvact)


            stpWatchInfo.Stop()
            ProcessTime.Text &= "OwnerWaivers:" & stpWatchInfo.Elapsed.ToString & "<br>"

        Catch ex As Exception
            lblError.Text = "Process error at  (OwnerWaivers) -" & ex.Message.ToString()
        End Try

    End Sub

End Class