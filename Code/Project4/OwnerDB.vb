Imports System.Data
Imports System.Web.SessionState.HttpSessionState
Imports System.Data.SqlClient
Imports LoggingProcess
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Net
Imports System.Collections.Generic
Imports System.ComponentModel
Imports BxgCorp.Mortgage
Imports VSSA.IVRSearchSvc
Imports IBM.Data.DB2.iSeries
Imports System.Globalization
Imports VSSA.fetchresp
Imports VSSA.OwnerServices
Imports VSSA.ptsBucketRes
Imports VSSA.ResortServices
Imports VSSA.WaiversResp
Imports VSSA.OwnerAcctResp

Public Module OwnerDB
    Private audit As New AuditorLog()

    Public bxgOwner As New OwnerFetchResponse


    Public Function ClubServiceMapOptionTable(ByVal MapId As String) As String

        Dim response As String = ""

        Try
            Dim table As New DataTable("ClubServiceMap")
            table.Columns.Add(New DataColumn("ID", GetType(String)))
            table.Columns.Add(New DataColumn("Menu", GetType(String)))

            table.Rows.Add("A", "Reservations")
            table.Rows.Add("B", "New Reservation")
            table.Rows.Add("C", "Existing Reservation")
            table.Rows.Add("D", "Mortgage")
            table.Rows.Add("E", "Mortgage Payment")
            table.Rows.Add("F", "Mortgage Other")
            table.Rows.Add("G", "Maintenance Fees")
            table.Rows.Add("H", "Club Dues")
            table.Rows.Add("I", "Points")
            table.Rows.Add("J", "Points Purchase")
            table.Rows.Add("K", "Points Other")
            table.Rows.Add("L", "Encore Dividends")
            table.Rows.Add("M", "Traveler Plus")
            table.Rows.Add("N", "Something Else")
            table.Rows.Add("O", "Unknown")
            table.Rows.Add("P", "Confirmation")
            table.Rows.Add("Q", "Customer Service")
            table.Rows.Add("O", "Unknown")
            table.Rows.Add("R", "Open Nights")

            Dim result As DataRow() = table.[Select]("ID = '" & MapId & "'")

            If result.Length > 0 Then
                response = result(0)(1)
            Else
                response = " "
            End If



        Catch ex As Exception
            response = ""
        End Try


        Return response

    End Function

    Public Function ClubServiceTransferTextTable(ByVal Id As String) As String
        Dim response As String = ""


        Dim table As New DataTable("ClubServiceTransferText")
        table.Columns.Add(New DataColumn("TransferID", GetType(String)))
        table.Columns.Add(New DataColumn("Text", GetType(String)))

        table.Rows.Add("01", "International Caller")
        table.Rows.Add("02", "International Caller")
        table.Rows.Add("03", "ACH Mortgage - International Caller ")
        table.Rows.Add("04", "Club Services")
        table.Rows.Add("05", "Mortgage Delinquency")
        table.Rows.Add("06", "ACH Mortgage Caller")
        table.Rows.Add("07", "Club Dues/Maintenance Fee Delinquency")
        table.Rows.Add("08", "FF - Maintenance Fee Delinquency")
        table.Rows.Add("09", "Owner Type Unknown")
        table.Rows.Add("10", "Point Owner - Level Unknown")
        table.Rows.Add("17", "Fixed Flex")
        table.Rows.Add("18", "Fixed Flex")
        table.Rows.Add("73", "Unknown")
        table.Rows.Add("78", "Payment")
        table.Rows.Add("77", "Fixed Flex")
        table.Rows.Add("12", "Fixed Flex")
        table.Rows.Add("15", "Fixed Flex")
        table.Rows.Add("11", "Fixed Flex")
        table.Rows.Add("14", "Fixed Flex")
        table.Rows.Add("13", "Fixed Flex")
        table.Rows.Add("16", "Fixed Flex")
        table.Rows.Add("19", "First Year")
        table.Rows.Add("73", "Unknown")
        table.Rows.Add("74", "First Year")
        table.Rows.Add("21", "First Year")
        table.Rows.Add("22", "First Year")
        table.Rows.Add("26", "First Year - Points Unknown")
        table.Rows.Add("27", "First Year")
        table.Rows.Add("28", "First Year")
        table.Rows.Add("23", "First Year")
        table.Rows.Add("24", "First Year")
        table.Rows.Add("25", "First Year")
        table.Rows.Add("20", "First Year")
        table.Rows.Add("35", "Premier")
        table.Rows.Add("73", "Unknown")
        table.Rows.Add("75", "Premier")
        table.Rows.Add("37", "Premier")
        table.Rows.Add("39", "Premier")
        table.Rows.Add("42", "Premier - Unknown")
        table.Rows.Add("43", "Premier")
        table.Rows.Add("44", "Premier")
        table.Rows.Add("41", "Premier")
        table.Rows.Add("40", "Premier")
        table.Rows.Add("38", "Premier")
        table.Rows.Add("36", "Premier")
        table.Rows.Add("46", "Vacation Club")
        table.Rows.Add("73", "Unknown")
        table.Rows.Add("76", "Vacation Club")
        table.Rows.Add("47", "Vacation Club")
        table.Rows.Add("48", "Vacation Club")
        table.Rows.Add("54", "Vacation Club - Unknown")
        table.Rows.Add("52", "Vacation Club")
        table.Rows.Add("53", "Vacation Club")
        table.Rows.Add("49", "Vacation Club")
        table.Rows.Add("50", "Vacation Club")
        table.Rows.Add("51", "Vacation Club")
        table.Rows.Add("45", "Vacation Club")
        table.Rows.Add("31", "Sampler")
        table.Rows.Add("32", "Sampler - Unknown")
        table.Rows.Add("33", "Sampler")
        table.Rows.Add("34", "Sampler")
        table.Rows.Add("29", "Sampler")
        table.Rows.Add("30", "Sampler")
        table.Rows.Add("63", "N/A")
        table.Rows.Add("65", "Unknown")
        table.Rows.Add("66", "N/A")
        table.Rows.Add("64", "N/A")
        table.Rows.Add("62", "N/A")
        table.Rows.Add("61", "N/A")

        table.Rows.Add("67", "N/A")
        table.Rows.Add("68", "N/A")
        table.Rows.Add("69", "N/A")
        table.Rows.Add("70", "Explorer")
        table.Rows.Add("71", "N/A")
        table.Rows.Add("72", "N/A")
        table.Rows.Add("79", "ACH Mortgage Caller")


        Dim result As DataRow() = table.[Select]("TransferID = '" & Id & "'")

        If result.Length > 0 Then
            response = result(0)(1)
        Else
            response = "  "
        End If




        Return response

    End Function

    Public Function GetLoansByArvact(ByVal arvact As String) As List(Of Loan)

        Dim loans As New List(Of Loan)
        Dim MO As New MortgageOwner
        loans = MortgageOwner.GetLoansByArvact(arvact)
        Return loans

    End Function

    Public Function GetLoanSummary(ByVal arvact As String) As List(Of LoanSummary)
        Dim loans As New List(Of LoanSummary)
        Dim MO As New MortgageOwner

        loans = MortgageOwner.GetLoansSummaryByArvact(arvact)

        Return loans
    End Function

    Public Function GetOwnerPurchaseDate(ByVal arvact As String) As Owner

        Dim owner As New Owner
        Dim MO As New MortgageOwner
        owner = MortgageOwner.GetOwnerInfoByArvact(arvact)
        Return owner

    End Function



    Public Function GetDispositionDesc(ByVal dsp As String) As String
        Dim disposition As String = ""


        Select Case dsp
            Case "NS"
                disposition = "No Search"
            Case "NR"
                disposition = "No Record Found"
            Case "MR"
                disposition = "Multiple Records Found"
            Case "MZ"
                disposition = "MisMatched Zip Code"
            Case "NZ"
                disposition = "No Zip Code"
            Case "OO"
                disposition = "Opt Out"
            Case "DL"
                disposition = "Delinquency"
            Case "LH"
                disposition = "Legal Hold"
            Case "SU"
                disposition = "Service Unavailable"
            Case "RF"
                disposition = "Record Found"
            Case Else
                disposition = "Disposition Unavailable"

        End Select
        Return disposition
    End Function

    Public Function GetConciergeCustomerInfo(ByVal customerNumber As String) As Popup_Customers_ResponseCustomer()

        Dim clientSVC As IVRSearchSvc.IVRSearchSvc = New IVRSearchSvc.IVRSearchSvc()

        Dim req As IVRSearchSvc.Popup_Customer_Request = New Popup_Customer_Request()
        req.CustomerNumber = customerNumber
        Dim resp As Popup_Customers_ResponseCustomer() = clientSVC.PopupCustomerInfo(req)

        Return resp
    End Function

    Public Function GetConciergeCustomersByPhone(ByVal phoneNumber As String) As Popup_Customers_ResponseCustomer()
        Dim clientSVC As IVRSearchSvc.IVRSearchSvc = New IVRSearchSvc.IVRSearchSvc()

        Dim req As IVRSearchSvc.Popup_Customers_By_Phone_Request = New Popup_Customers_By_Phone_Request()
        req.PhoneNumber = phoneNumber
        Dim resp As Popup_Customers_ResponseCustomer() = clientSVC.PopupCustomersByPhone(req)

        Return resp
    End Function

    Public Function GetConciergeAccountInfoByCustomer(ByVal customerNumber As String) As Popup_Customer_Accounts_ResponseAccount()
        Dim clientSVC As IVRSearchSvc.IVRSearchSvc = New IVRSearchSvc.IVRSearchSvc()
        Dim req As IVRSearchSvc.Popup_Customer_Accounts_Request = New Popup_Customer_Accounts_Request()
        req.CustomerNumber = customerNumber
        Dim resp As Popup_Customer_Accounts_ResponseAccount() = clientSVC.PopupCustomerAccountsInfo(req)
        Return resp
    End Function

    Public Function FetchOwnerBillDate(ByVal _arvact As String) As String

        Dim connection As New SqlConnection(ConfigurationManager.ConnectionStrings("VSSA.bxgwebDBConnection").ConnectionString)
        Dim cmnd As SqlCommand = connection.CreateCommand
        Dim billMonth As Integer = 0
        Dim billDate As String = ""
        Dim currentMonth As Integer = DateTime.Now.Month
        Dim currentYear As Integer = DateTime.Now.Year
        Dim futureYear As Integer = DateTime.Now.AddYears(1).Year

        Dim stpWatchInfo As New System.Diagnostics.Stopwatch
        stpWatchInfo.Start()



        Try

            Using connection
                connection.Open()
                cmnd.CommandTimeout = 300

                Using cmnd
                    cmnd.CommandType = CommandType.StoredProcedure
                    cmnd.CommandText = "uspGetAnniversaryMonth2"
                    cmnd.Parameters.AddWithValue("@ARVACT", CInt(_arvact.Trim()))

                    Using dr As IDataReader = cmnd.ExecuteReader
                        While dr.Read()

                            billMonth = dr("Bill_Month")
                        End While

                        dr.Close()

                    End Using

                End Using

            End Using

            If billMonth <> 99 Then

                If currentMonth > billMonth Then
                    billDate = billMonth.ToString("00") & "/" & futureYear
                Else
                    billDate = billMonth.ToString("00") & "/" & currentYear
                End If

            End If


            stpWatchInfo.Stop()
        Catch ex As Exception

            stpWatchInfo.Stop()
            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, ExceptionHandlingConstants.DBAccessPolicy)
            If (rethrow) Then
                Throw
            End If

        Finally

            If connection.State = ConnectionState.Open Then
                connection.Close() ' closing the connection
            End If

        End Try
        Return IIf(billDate = "", "N/A", billDate)
    End Function



    Public Function FetchOwnerInfo(ByVal arvact As Integer) As OwnerFetchResponse
        Dim OwnerSvc As New OwnerService()
        Dim OwnerSvcResp As New OwnerFetchResponse()
        OwnerSvcResp = OwnerSvc.OwnerFetch(arvact.ToString())
        bxgOwner = OwnerSvcResp
        Return bxgOwner
    End Function

    Public Function FetchOwnerPayBalence(ByVal arvact As Integer) As DataSet
        Dim accountBalence As New DataSet
        Dim OwnerWS As New LegacyOwnerWS.OwnerWS1SoapClient
        Dim Ownerobj = OwnerWS.fetchPayAcctBal(arvact, LegacyOwnerWS.Action.enumDetailsbyLine)

        Dim dtBalance As DataTable = ConvertToDataTable(Ownerobj)
        accountBalence.Tables.Add(dtBalance)

        Return accountBalence

    End Function


    Public Function FetchAcctContractInfo(ByVal arvact As String) As List(Of OwnerAcctResp.AccountInfo)
        Dim accList As New List(Of OwnerAcctResp.AccountInfo)
        Dim Ownersvc As New OwnerService()
        Dim OwnerAccResp As New OwnerAcctResponse()
        OwnerAccResp = Ownersvc.OwnerAccounts(arvact)
        For Each account As OwnerAcctResp.AccountInfo In OwnerAccResp.Account.Memberships.VacationClubMembership.Accounts.AccountInfo
            If account.Legacy.ProjectNumber <> "51" And account.Legacy.ProjectNumber <> "52" Then

                Dim strExpireDate As DateTime
                If account.Legacy.ProjectNumber = "50" Then
                    If account.Legacy.Expiration IsNot Nothing Then
                        If Len(account.Legacy.Expiration) = 6 Then
                            strExpireDate = DateTime.ParseExact(account.Legacy.Expiration, "yyMMdd", CultureInfo.InvariantCulture)
                            account.Legacy.Expiration = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(strExpireDate))
                        Else
                            account.Legacy.Expiration = "N/A"
                        End If
                    End If

                End If

                If account.Legacy.Expiration = "0" Then
                    account.Legacy.Expiration = "N/A"
                End If

                If account.NextEarnAmount = "0" Then
                    account.NextEarnAmount = ""
                End If

                accList.Add(account)

            End If
        Next
        Return accList

    End Function

    Public Function ConvertToDataTable(Of T)(data As IList(Of T)) As DataTable
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim table As New DataTable()
        For Each prop As PropertyDescriptor In properties
            table.Columns.Add(prop.Name, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
        Next
        For Each item As T In data
            Dim row As DataRow = table.NewRow()
            For Each prop As PropertyDescriptor In properties
                row(prop.Name) = If(prop.GetValue(item), DBNull.Value)
            Next
            table.Rows.Add(row)
        Next
        Return table

    End Function
    Public Function FetchAS400SavePointsBucket(ByVal arvact As String) As DataTable
        Dim OwnerServ As New OwnerService()
        Dim as400resp As AS400Resp.AS400PointsResponse = Nothing
        Dim as400req As New AS400Req.AS400PointsRequest()
        as400req.Arvact = arvact
        as400req.PointTypes = "A,B,C,S,H,F,J,K,T,U,X"
        as400req.ListType = "1"
        as400resp = OwnerServ.FetchAS400SavePointsBucket(as400req)
        Dim willbesaved As Boolean = False
        Dim needtosave As Boolean = False
        Dim strExpireDate As String = Nothing
        Dim thisDay As DateTime = DateTime.Today
        Dim pttbl As New DataTable("AS400tblPointsBuckets")
        Dim tbl_row As DataRow

        With pttbl
            .Columns.Add("AcctNum", GetType(String))
            .Columns.Add("ExpireDate", GetType(DateTime))
            .Columns.Add("PointBal", GetType(Integer))
            .Columns.Add("PointTypeDesc", GetType(String))
            .Columns.Add("Action", GetType(String))
        End With

        Dim Ownersvc As New OwnerService()
        Dim OwnerAccResp As New OwnerAcctResponse()
        OwnerAccResp = Ownersvc.OwnerAccounts(arvact)

        If Not IsNothing(as400resp) Then
            For x As Integer = 0 To (as400resp.PointsRecord.Count)
                'gird will show points details for all owners except for Expired Value sampler and sampler 24
                If OwnerAccResp.Account.Memberships.VacationClubMembership.Legacy.HomeProjectNumber = "51" And OwnerAccResp.Account.Memberships.VacationClubMembership.Legacy.HomeProjectNumber = "52" Then

                    tbl_row = pttbl.NewRow
                    strExpireDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(as400resp.PointsRecord(x).ExpireDate))
                    With tbl_row
                        .Item("AcctNum") = as400resp.PointsRecord(x).AcctNum
                        .Item("ExpireDate") = CDate(strExpireDate)
                        .Item("PointBal") = as400resp.PointsRecord(x).PointBal
                        .Item("Action") = as400resp.PointsRecord(x).Action
                        .Item("PointTypeDesc") = as400resp.PointsRecord(x).PointTypeDesc

                    End With
                    If needtosave = False And strExpireDate >= thisDay And as400req.ListType = "0" And as400resp.PointsRecord(x).PointBal > 0 Then
                        needtosave = True
                    ElseIf needtosave = False And willbesaved = False And strExpireDate >= thisDay And as400req.ListType = "1" And as400resp.PointsRecord(x).PointBal > 0 Then
                        willbesaved = True
                    End If
                    pttbl.Rows.Add(tbl_row)
                End If
            Next
        End If
        Return pttbl

    End Function

    Public Function FetchOwnerPointsBuckets(ByVal pttbl As DataTable, ByVal bxgOwner As OwnerFetchResponse) As DataTable
        bxgOwner = HttpContext.Current.Session("BoomiOwner")
        Dim tbl As New DataTable("tblPointsBuckets")
        Dim tbl_row As DataRow
        Dim tblmaster As New DataTable("TBLMASTER")
        Dim ds As New DataSet("dataset")
        Dim pointsdescval As String
        Dim strExpireDate As String = Nothing
        Dim thisDay As DateTime = DateTime.Today

        With tbl
            .Columns.Add("AcctNum", GetType(String))
            .Columns.Add("PointBal", GetType(Integer))
            .Columns.Add("ExpireDate", GetType(String))
            .Columns.Add("PointTypeDesc", GetType(String))
            .Columns.Add("Action", GetType(String))
            .Columns.Add("NextEarnDate", GetType(String))
            .Columns.Add("NextEarnAmount", GetType(String))
            .Columns.Add("SecMkt", GetType(String))
        End With

        Dim Ownerobj As New List(Of fetchresp.VacationClubPoint)
        If Not bxgOwner.Account.Memberships.VacationClubMembership.Points.VacationClubPoint Is Nothing Then
            Ownerobj = bxgOwner.Account.Memberships.VacationClubMembership.Points.VacationClubPoint
        End If

        For count As Integer = 0 To Ownerobj.Count - 1
            pointsdescval = String.Empty
            tbl_row = tbl.NewRow
            strExpireDate = Ownerobj(count).EndDate.ToString()
            With tbl_row

                .Item("AcctNum") = Ownerobj(count).AccountNumber
                .Item("PointBal") = Ownerobj(count).PointBalance
                .Item("ExpireDate") = strExpireDate
                .Item("SecMkt") = Ownerobj(count).SecondaryMarket

                If Not (bxgOwner.Account.Memberships.VacationClubMembership.Sampler Is Nothing) Then
                    If (bxgOwner.Account.Memberships.VacationClubMembership.Sampler.IsSampler) Then 'It’s only apply for Sampler Owners (VS and S24).
                        If (bxgOwner.Account.Memberships.VacationClubMembership.Legacy.HomeProjectNumber = 52) Then 'It’s only apply for Sampler 24 Owners.
                            If Ownerobj(count).PointTypeDescription = "Additional Points" Then
                                Ownerobj(count).PointTypeDescription = "Annual"
                            End If
                        End If
                    End If
                End If
                .Item("PointTypeDesc") = Ownerobj(count).PointTypeDescription
                .Item("NextEarnDate") = Ownerobj(count).NextEarnDate
                .Item("NextEarnAmount") = Ownerobj(count).NextEarnAmount

                If Ownerobj(count).PointTypeDescription = "Annual" Or Ownerobj(count).PointTypeDescription = "Borrowed" Then

                    Dim expression As String = " PointBal = '" & Trim(Ownerobj(count).PointBalance) & "'  and AcctNum = '" & Trim(Ownerobj(count).AccountNumber) & "' and  ExpireDate = '" & CDate(strExpireDate) & "'"
                    Dim foundRows As DataRow()
                   
                    foundRows = pttbl.Select(expression)

                    If foundRows.Count = 0 Then
                        .Item("Action") = "N/A"
                    End If

                    Dim i As Integer
                    ' Print column 0 of each returned row.
                    For i = 0 To foundRows.GetUpperBound(0)

                        If CDate(strExpireDate) >= thisDay And foundRows(i)(4) = "1" And Ownerobj(i).PointBalance <= 0 Then
                            .Item("Action") = "N/A"
                        ElseIf CDate(strExpireDate) >= thisDay And foundRows(i)(4) = "1" And Ownerobj(i).PointBalance > 0 Then
                            .Item("Action") = "Will be Saved"
                        ElseIf CDate(strExpireDate) >= thisDay And foundRows(i)(4) = "0" And Ownerobj(i).PointBalance > 0 Then
                            .Item("Action") = "Need to Save"
                        Else
                            .Item("Action") = "N/A"
                        End If
                    Next i
                Else
                    .Item("Action") = "N/A"
                End If
                If Not (bxgOwner.Account.Memberships.VacationClubMembership.Sampler Is Nothing) Then
                    If bxgOwner.Account.Memberships.VacationClubMembership.Sampler.IsSampler = True Then
                        .Item("Action") = "N/A"
                        .Item("NextEarnDate") = "N/A"
                        .Item("NextEarnAmount") = "N/A"
                    End If
                End If
            End With
            tbl.Rows.Add(tbl_row)
        Next

        Return tbl


    End Function

    Public Function GetRequestParameters(ByVal req As String, ByVal ani As String) As IVRRequest
        Dim reqparams As New IVRRequest

        reqparams.Arvact = req.Substring(0, 6).Replace("_", "").Trim()
        reqparams.ANIPhone = ani
        reqparams.CustomerId = req.Substring(6, 10).Replace("_", "").Trim()
        reqparams.ValidationPhone = req.Substring(16, 10).Replace("_", "").Trim()
        reqparams.MenuId = req.Substring(26, 1).Replace("_", "").Trim()
        reqparams.Disposition = req.Substring(27, 2).Replace("_", "").Trim()
        reqparams.TransaferId = req.Substring(29, 2).Replace("_", "").Trim()


        Return reqparams


    End Function

    Public Function ValidateRequest(ByVal req As String, ByVal ani As String, ByVal popType As String) As String
        Dim reqparams As New IVRRequest
        Dim result As String = ""
        Dim format As String = ""
        result &= ""


        If req = "" Then
            result &= "Request can't be blank. <br>"
        End If

        If req.Length < 31 Then
            result &= "Request length can't be less than 31 characters. <br>"
        End If

        Try
            format = req.Substring(0, 6).Trim()
            reqparams.Arvact = format.Replace("_", "0")
            reqparams.ANIPhone = ani
            reqparams.CustomerId = req.Substring(6, 10).Trim()
            reqparams.ValidationPhone = req.Substring(16, 10).Trim()
            reqparams.MenuId = req.Substring(26, 1).Trim()
            reqparams.Disposition = req.Substring(27, 2).Trim()
            reqparams.TransaferId = req.Substring(29, 2).Trim()
            Dim Ownersvc As New OwnerService()
            Dim OwnerSvcResp As New OwnerFetchResponse()
            OwnerSvcResp = Ownersvc.OwnerFetch(reqparams.Arvact)
            HttpContext.Current.Session("BoomiOwner") = OwnerSvcResp
            If UCase(popType) = "OWNER" And reqparams.Arvact.Length <> 6 Then
                result &= "Invalid ARVACT number. <br>"
            End If

            If UCase(popType) = "CONCIERGE" And reqparams.CustomerId.Length <> 10 Then
                result &= "Invalid CUSTOMER NUMBER. <br>"
            End If

            If reqparams.Disposition = "MR" Or reqparams.Disposition = "NZ" Or reqparams.Disposition = "SU" Then

                If reqparams.ANIPhone.Length <> 10 Or reqparams.ValidationPhone.Length <> 10 Then
                    result &= "Invalid PHONE NUMBER. <br>"
                End If

            End If

        Catch ex As Exception
            result &= "ValidateRequest error - " & ex.Message.ToString()
        End Try

        Return result

    End Function

    Public Function GetRawParameters(ByVal req As String, ByVal ani As String) As IVRRequest

        Dim reqparams As New IVRRequest

        reqparams.Arvact = req.Substring(0, 6)
        reqparams.ANIPhone = ani
        reqparams.CustomerId = req.Substring(6, 10)
        reqparams.ValidationPhone = req.Substring(16, 10)
        reqparams.MenuId = req.Substring(26, 1)
        reqparams.Disposition = req.Substring(27, 2)
        reqparams.TransaferId = req.Substring(29, 2)

        Return reqparams


    End Function

    Public Function GetWaivers(ByVal arvact As WaiversReq.OwnerWaiversRequest)
        Dim waivers As String = ""
        Dim ownWaivers As New ResortService
        Dim waiversResp As OwnerWaiversResponse = Nothing
        waiversResp = ownWaivers.WaiversAvailable(arvact)
        If CInt(waiversResp.OwnerWaivers.WaiversAvailable) > 10 Then
            waivers = "N/A"
        Else
            waivers = waiversResp.OwnerWaivers.WaiversAvailable
        End If

        Return waiversResp.OwnerWaivers.WaiversAvailable

    End Function

    Public Function FormatPhone(ByVal PhoneNumber As String) As String

        If PhoneNumber Is Nothing Then
            Return ""
        End If

        Dim FormattedPhone As String
        Dim reg = New Regex("(\d{3})(\d{3})(\d{4})")

        FormattedPhone = reg.Replace(PhoneNumber.Replace("[^0-9]", ""), "($1) $2-$3")
        Return IIf(FormattedPhone <> "", FormattedPhone, "N/A")

    End Function

    Public Sub fetchIVROwnerAccountsHolders(ByVal _arvact As String)

        Dim connection As New SqlConnection(ConfigurationManager.ConnectionStrings("VSSA.bxgwebDBConnection").ConnectionString)
        Dim cmnd As SqlCommand = connection.CreateCommand
        Dim id As Integer = 0

        Dim stpWatchInfo As New System.Diagnostics.Stopwatch
        stpWatchInfo.Start()

        Dim accounts As New List(Of AccountInfo)
        Dim accountHolder As New List(Of AccountHolder)

        Try

            Using connection
                connection.Open()
                cmnd.CommandTimeout = 300

                Using cmnd
                    cmnd.CommandType = CommandType.StoredProcedure
                    cmnd.CommandText = "uspGetAccountOwnerName"
                    cmnd.Parameters.AddWithValue("@ARVACT", CInt(_arvact.Trim()))

                    Using dr As IDataReader = cmnd.ExecuteReader
                        While dr.Read()

                            Dim accountinfo As New AccountInfo

                            accountinfo.index = dr("id").ToString
                            accountinfo.AcctNum = dr("Acct").ToString
                            accountinfo.proj = dr("Proj").ToString
                            accountinfo.projNM = dr("ProjNM").ToString
                            accounts.Add(accountinfo)
                            accountinfo = Nothing

                        End While

                        dr.NextResult()

                        While (dr.Read())

                            Dim holderName As New AccountHolder

                            holderName.OwnerName = dr("ownername").ToString.Trim
                            holderName.OwnerHolderType = dr("seq")

                            accountHolder.Add(holderName)
                            holderName = Nothing

                        End While

                        dr.Close()

                    End Using

                End Using

            End Using

            System.Web.HttpContext.Current.Session("accountHolder") = accountHolder
            System.Web.HttpContext.Current.Session("AccountInfo") = accounts
            For i As Integer = 0 To accounts.Count - 1
                If accounts(i).proj = "50" Then
                    System.Web.HttpContext.Current.Session("AccountNumber") = accounts(i).AcctNum
                    Exit For
                End If

            Next
            stpWatchInfo.Stop()
        Catch ex As Exception

            stpWatchInfo.Stop()
            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, ExceptionHandlingConstants.DBAccessPolicy)
            If (rethrow) Then
                Throw
            End If

        Finally

            If connection.State = ConnectionState.Open Then
                connection.Close() ' closing the connection
            End If

        End Try

    End Sub

    ''' <summary>
    ''' Gets the mortage maintenance status.
    ''' </summary>
    ''' <returns>Boolean value True if LSAMS is down for maintenance.</returns>
    ''' <remarks></remarks>
    Public Function isMortgageMaintenance() As Boolean
        Dim isMaintenance As Boolean = False
        Dim returnValue As String = String.Empty
        Try
            Using conn As IDbConnection = New iDB2Connection
                ' Create db connection and execute stored proc
                conn.ConnectionString = Utilities.GetConnectionString("bxgwebDBConnectionAS400DB2")
                Dim cmd As iDB2Command = conn.CreateCommand
                cmd.CommandText = "LSAMSMOD.MOSTSCHK"
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@USRSTS", iDB2DbType.iDB2Char, 10)
                cmd.Parameters("@USRSTS").Value = String.Empty ' "          "
                cmd.Parameters("@USRSTS").Direction = ParameterDirection.InputOutput

                conn.Open()
                cmd.ExecuteNonQuery()

                returnValue = cmd.Parameters("@USRSTS").Value

                If Not String.IsNullOrEmpty(returnValue) Then
                    If returnValue.ToUpper.Trim.Equals("*DISABLED") Then
                        isMaintenance = True
                    End If
                End If


                conn.Close()

            End Using

        Catch ex As Exception
            ' take no chances here, if we are not able to reach the AS400 to see if maintenance is happening let's assume the AS400 is down
            'Return True
        End Try
        Return isMaintenance
    End Function

    Public Function GetResorts(ByVal resortText As String) As DataTable


        Dim connection As New SqlConnection(ConfigurationManager.ConnectionStrings("VSSA.bxgwebDBConnection").ConnectionString)
        Dim cmnd As SqlCommand = connection.CreateCommand
        Dim dt = New DataTable()

        ''Using dt As New DataTable()
        Try

            Using connection
                connection.Open()
                cmnd.CommandTimeout = 300

                Using cmnd
                    cmnd.CommandType = CommandType.StoredProcedure
                    cmnd.CommandText = "uspSelectResortList"

                    Using sda As New SqlDataAdapter()
                        sda.SelectCommand = cmnd
                        sda.Fill(dt)

                    End Using

                End Using

            End Using


        Catch ex As Exception
            Dim errorString As String = ex.Message.ToString()

        Finally

            If connection.State = ConnectionState.Open Then
                connection.Close() ' closing the connection
            End If

        End Try

        ''End Using

        Return dt



    End Function

End Module
