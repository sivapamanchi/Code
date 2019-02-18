Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration
Imports System.Globalization
Imports VSSA.SPDocumentUpload


Public Class AddressUpload
    Inherits System.Web.UI.Page

    Public ownerToBeModified As New BXG_Owner
    Public Shared lstStates As List(Of States)
    Public Shared lstCountries As List(Of Countries)
    '' Create a list to store Error Data .
    Dim errorList As DataTable = GetTable()
    Dim docLibraryURL As String = ConfigurationManager.AppSettings("VSSA.GetAuditDocumentLibraryURL")

    Dim totalRecords As Integer = 0
    Dim validRecords As Integer = 0
    Dim invalidRecords As Integer = 0
    Dim SPAuditTrailString As New StringBuilder()
    Dim fileContentAsByteArray() As Byte
    Dim destinationUrl As String() = {}
    Dim docURL As String = String.Empty
    Dim FileName As String = String.Empty
    Dim FilePath As String = String.Empty
    Dim fileExtensionsAllowed As String() = {".XLS", ".XLSX"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("DisplayAddressUpload")) OrElse Not Session("DisplayAddressUpload").Equals("True") Then
            Response.Redirect("../Default.aspx")
        End If
        If Not Session("SearchParam") Is Nothing Then
            Session("SearchParam") = Nothing
        End If
        If Not Session("SearchData") Is Nothing Then
            Session("SearchData") = Nothing
        End If
        If Environment.UserName.Length > 8 Then
            Session("adminName") = Environment.UserName.Substring(1, 8)
        Else
            Session("adminName") = Environment.UserName
        End If
        If lstStates Is Nothing OrElse lstStates.Count = 0 Then
            lstStates = New List(Of States)
            Dim arrStates As ArrayList = CommonData.getStates
            For x = 0 To arrStates.Count - 1
                Dim st As States = TryCast(arrStates(x), States)
                lstStates.Add(st)
            Next
        End If
        If lstCountries Is Nothing OrElse lstStates.Count = 0 Then
            lstCountries = New List(Of Countries)
            Dim arrCountries As ArrayList = CommonData.getCountries
            For x = 0 To arrCountries.Count - 1
                Dim c As Countries = TryCast(arrCountries(x), Countries)
                lstCountries.Add(c)
            Next
        End If
        InitiateVisibility()

        If Not (IsPostBack) Then
            Session("errorListRecords") = ""
        End If
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If excelFileUpload.HasFile Then

            Dim dateTimeFormat As String = Date.Now().ToString("dd-MM-yyyy", CultureInfo.InvariantCulture) + "_" + Date.Now().ToString("hh-mm-ss", CultureInfo.InvariantCulture)
            FileName = Session("adminName") + "_" + dateTimeFormat + "_" + Path.GetFileName(excelFileUpload.PostedFile.FileName)
            hdn_FileName.Value = FileName

            Dim Extension As String = Path.GetExtension(excelFileUpload.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("VSSA.GetExcelSheetFolderPath")
            If (fileExtensionsAllowed.Contains(Extension.ToUpper())) Then
                FilePath = Server.MapPath(FolderPath + FileName)

                docURL = docLibraryURL + "/" + FileName
                destinationUrl = {docLibraryURL & Convert.ToString("/" + FileName)}



                excelFileUpload.SaveAs(FilePath)
                Import_To_Datatable(FilePath, Extension, "Yes")
            Else
                SetClassForDiv(0, 0, 0, " Please select File with Excel Extension.")
                'LabelErrorMsg.Visible = True
                'LabelErrorMsg.Text = "Please select File with Excel Extension."
            End If
            
        Else
            SetClassForDiv(0, 0, 0, " Please Select Valid Excel Template before Uploading.")
            'LabelErrorMsg.Visible = True
            'LabelErrorMsg.Text = "Please Select Valid Excel Template before Uploading."
        End If
    End Sub

    Private Sub Import_To_Datatable(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String)
        Dim conStr As String = ""

        Dim columnHeaders As New ArrayList(ConfigurationManager.AppSettings("VSSA.GetExcelSheetColumnValues").ToString().Split(";"))

        Select Case Extension
            Case ".xls"
                'Excel 97-03 
                conStr = ConfigurationManager.ConnectionStrings("VSSA.GetExcelConnectionStrigForXLSX").ConnectionString
                Exit Select
            Case ".xlsx"
                'Excel 07 
                conStr = ConfigurationManager.ConnectionStrings("VSSA.GetExcelConnectionStrigForXLSX").ConnectionString
                Exit Select
        End Select
        conStr = String.Format(conStr, FilePath, isHDR)

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet  // IF error for XLSX extensions install http://www.microsoft.com/en-us/download/details.aspx?id=23734
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        totalRecords = dt.Rows.Count
        Dim hasError As Boolean = False

        If (dt.Rows.Count > 0) Then

            For index As Integer = 0 To columnHeaders.Count() - 1

                If Not (columnHeaders.Contains(dt.Columns(index).ColumnName.ToUpper)) Then
                    ''errorList.Rows.Add(dt.Columns(index).ColumnName, "Header is not part of Valid Template.")
                    SetClassForDiv(0, 0, 0, "Uploaded Excel has a header " + dt.Columns(index).ColumnName + " which is not a part of Valid Template.")
                    'LabelErrorMsg.Visible = True
                    'LabelErrorMsg.Text = "Uploaded Excel has a header " + dt.Columns(index).ColumnName + " which is not a part of Valid Template."
                    hasError = True
                    Exit Sub
                End If
                If (hasError) Then
                    invalidRecords += 1
                End If
            Next
        Else
            SetClassForDiv(0, 0, 0, "No Records Found!! Please upload Valid Excel with Records.")
            'LabelErrorMsg.Visible = True
            'LabelErrorMsg.Text = ""
        End If

        If (dt.Rows.Count > 1000) Then
            SetClassForDiv(0, 0, 0, "Record limit of 1,000 exceeded, no records have been updated.")
            Exit Sub
        End If


        Try
            ''Loop to get Data is proper to send to SQL and log Error if any
            CheckForExcelSheetValidData(dt)
            'Bind Data to GridView 
            SetClassForDiv(totalRecords, validRecords, invalidRecords)

            'lblInformation.Visible = True
            'lblInformation.Text = "Out of " + totalRecords.ToString() + " records ," + validRecords.ToString() + " were updated and " + invalidRecords.ToString() + " were invalid and not updated (Details below)."

            If (errorList.Rows.Count > 0) Then
                Session("errorListRecords") = errorList
                grdErrorList.Visible = True
                grdErrorList.DataSource = errorList
                grdErrorList.DataBind()
            End If
        Catch ex As Exception
            Dim messageException As String = ex.Message.ToString()
        End Try

        'Delete the Saved File 
        If System.IO.File.Exists(FilePath) = True Then
            System.IO.File.Delete(FilePath)
        End If

    End Sub
    Function SetClassForDiv(ByVal totalRecords As Integer, ByVal validRecords As Integer, ByVal invalidRecords As Integer, Optional message As String = "")

        If (message = "") Then


            If (totalRecords = validRecords) Then
                Dim strTotalRecordsWasWere As String = String.Empty
                Dim strTotalRecordsEntryEntries As String = String.Empty
                If totalRecords = 1 Then
                    strTotalRecordsWasWere = "was"
                    strTotalRecordsEntryEntries = "entry"
                Else
                    strTotalRecordsWasWere = "were"
                    strTotalRecordsEntryEntries = "entries"
                End If
                divInformation.Attributes.Add("class", "isa_success")
                divList.Attributes.Add("class", "fa fa-check")
                divList.InnerHtml = " Upload Successful. " + validRecords.ToString() + " " & strTotalRecordsEntryEntries & " " & strTotalRecordsWasWere & " updated."
                grdErrorList.Visible = False
                'ElseIf (invalidRecords = totalRecords) Then
                '    divInformation.Attributes.Add("class", "isa_error")
                '    divList.Attributes.Add("class", "fa fa-times-circle")
                '    divList.InnerHtml = " All records Failed to update."
                '    grdErrorList.Visible = True
            ElseIf (invalidRecords > 0) Then
                Dim strValidRecordsWasWere As String = String.Empty
                Dim strInvalidRecordsWasWere As String = String.Empty
                Dim strTotalRecordsRecord As String = String.Empty
                divInformation.Attributes.Add("class", "isa_warning")
                divList.Attributes.Add("class", "fa fa-warning")
                grdErrorList.Visible = True
                If totalRecords = 1 Then
                    strTotalRecordsRecord = "record"
                ElseIf (totalRecords = 0 OrElse totalRecords > 1) Then
                    strTotalRecordsRecord = "records"
                End If
                If validRecords = 1 Then
                    strValidRecordsWasWere = "was"
                ElseIf (validRecords = 0 OrElse validRecords > 1) Then
                    strValidRecordsWasWere = "were"
                End If
                If invalidRecords = 1 Then
                    strInvalidRecordsWasWere = "was"
                ElseIf (invalidRecords = 0 OrElse invalidRecords > 1) Then
                    strInvalidRecordsWasWere = "were"
                End If

                divList.InnerHtml = " Out of " + totalRecords.ToString() + " " & strTotalRecordsRecord & ", " + validRecords.ToString() + " " & strValidRecordsWasWere & " updated and " + invalidRecords.ToString() + " " & strInvalidRecordsWasWere & " invalid and not updated (Details below)."
                'divList.InnerHtml = "Upload has failed for the following ARVACT Numbers. Please click on " & Chr(34) & _
                '    "History" & Chr(34) & " link or " & Chr(34) & "Last Updated File" & Chr(34) & _
                '    " link to access failed records. If issue persists, please email the Help Desk."
            End If
        Else
            divInformation.Attributes.Add("class", "isa_error")
            divList.Attributes.Add("class", "fa fa-times-circle")
            divList.InnerHtml = message
        End If

        divInformation.Visible = True
        Return 0
    End Function

    Function GetTable() As DataTable

        Dim table As New DataTable

        'Dim AutoNumberColumn = New DataColumn()
        'AutoNumberColumn.ColumnName = "SL.#"
        'AutoNumberColumn.DataType = GetType(Integer)
        'AutoNumberColumn.AutoIncrement = True
        'AutoNumberColumn.AutoIncrementSeed = 1
        'AutoNumberColumn.AutoIncrementStep = 1

        'table.Columns.Add(AutoNumberColumn)
        table.Columns.Add("Arvact", GetType(String))
        table.Columns.Add("ErrorMessage", GetType(String))

        Return table
    End Function

    Private Function prepareDataofBxgownerAndUpdate(ByVal dataRowFromDatatable As DataRow, ByVal ownerToBeModified As BXG_Owner) As Boolean

        ''Assign all the values from excel to object
        If Not dataRowFromDatatable("Address1") Is DBNull.Value Then
            ownerToBeModified.Address1 = Trim(CType(dataRowFromDatatable("Address1"), String))
        Else
            ownerToBeModified.Address1 = String.Empty
        End If

        If Not dataRowFromDatatable("Address2") Is DBNull.Value Then
            ownerToBeModified.Address2 = Trim(CType(dataRowFromDatatable("Address2"), String))
        Else
            ownerToBeModified.Address2 = String.Empty
        End If

        If Not dataRowFromDatatable("City") Is DBNull.Value Then
            ownerToBeModified.City = Trim(CType(dataRowFromDatatable("City"), String))
        Else
            ownerToBeModified.City = String.Empty
        End If

        If Not dataRowFromDatatable("State") Is DBNull.Value Then
            Dim strState As String = Trim(CType(dataRowFromDatatable("State"), String)).ToUpper()
            If strState.Length = 2 Then
                ownerToBeModified.State = (From st As States In lstStates Where st.StateID.ToUpper.Equals(strState) Select st.StateDescription).FirstOrDefault
                ownerToBeModified.Subdivision = (From st As States In lstStates Where st.StateID.ToUpper.Equals(strState) Select st.StateDescription).FirstOrDefault
            Else
                ownerToBeModified.State = (From st As States In lstStates Where st.StateDescription.ToUpper.Equals(strState) Select st.StateDescription).FirstOrDefault
                ownerToBeModified.Subdivision = (From st As States In lstStates Where st.StateDescription.ToUpper.Equals(strState) Select st.StateDescription).FirstOrDefault
            End If
        Else
            ownerToBeModified.State = String.Empty
            ownerToBeModified.Subdivision = String.Empty
        End If

        If Not dataRowFromDatatable("Country") Is DBNull.Value Then
            Dim strCountry As String = Trim(CType(dataRowFromDatatable("Country"), String)).ToUpper()
            If strCountry.Length = 2 Then
                ownerToBeModified.CountryCode = (From c As Countries In lstCountries Where c.CountryCode.ToUpper.Equals(strCountry) Select c.CountryCode).FirstOrDefault
            Else
                ownerToBeModified.CountryCode = (From c As Countries In lstCountries Where c.CountryFullName.ToUpper.Equals(strCountry) Select c.CountryCode).FirstOrDefault
            End If
        Else
            ownerToBeModified.CountryCode = String.Empty
        End If

        If Not dataRowFromDatatable("ZipCode") Is DBNull.Value Then
            ownerToBeModified.PostalCode = Trim(CType(dataRowFromDatatable("ZipCode"), String))
        Else
            ownerToBeModified.PostalCode = String.Empty
        End If

        '' This code would have replaced valid data on owner records!!!! By getting the owner info first, you don't have to attempt to set default values
        '' Default remaining object properties to defaults as ModifyOwnerInfo Function expects object to be defaulted
        'ownerToBeModified.Address3 = String.Empty
        'ownerToBeModified.PhoneHome = String.Empty
        'ownerToBeModified.PhoneCell = String.Empty

        Return OwnerData.ModifyOwnerInfo(ownerToBeModified, "MassUploadExcelSheetOwners")

    End Function

    Private Sub CheckForExcelSheetValidData(ByVal dTable As DataTable)
        SPAuditTrailString = New StringBuilder()
        Dim lstSPAuditTrail As New List(Of String)
        Dim changesMadeFrom As String = ConfigurationManager.AppSettings("VSSA.GetEnvironment").ToString()
        Dim currentUserId As String = "-1;#" + Environment.UserDomainName + "\" + Environment.UserName
        Dim recordStatus As Boolean = False
        Dim updateResult As Boolean = False
        Dim oldOwnerData As New BXG_Owner
        Dim ARVACT As String = String.Empty
        Dim hasError As Boolean = False
        Dim errorMessage As String = String.Empty
        Dim emptyRow As Boolean = False
        Dim recordCount As Integer = 0

        For Each row In dTable.Rows
            recordCount += 1
            ownerToBeModified = Nothing
            oldOwnerData = New BXG_Owner
            errorMessage = String.Empty
            hasError = False
            ARVACT = String.Empty
            emptyRow = False
            If Not row(0).ToString() Is DBNull.Value Then
                ARVACT = row(0).ToString()
            End If
            ' Get owner from DB and only replace specific fields, we don't want to accidentally delete set some important values

            If Not String.IsNullOrEmpty(ARVACT) Then
                If IsNumeric(ARVACT) Then
                    ownerToBeModified = searchOwners(ARVACT.ToString)
                End If
            End If
            ' before we modify the owner, let's make a copy to be used for historical data
            If ownerToBeModified IsNot Nothing Then
                With oldOwnerData
                    .Address1 = ownerToBeModified.Address1
                    .Address2 = ownerToBeModified.Address2
                    .City = ownerToBeModified.City
                    .State = ownerToBeModified.State
                    .PostalCode = ownerToBeModified.PostalCode
                    .CountryCode = ownerToBeModified.CountryCode
                End With
            Else
                If row(0).ToString.Length = 0 AndAlso _
                        row(1).ToString.Length = 0 AndAlso _
                        row(2).ToString.Length = 0 AndAlso _
                        row(3).ToString.Length = 0 AndAlso _
                        row(4).ToString.Length = 0 AndAlso _
                        row(5).ToString.Length = 0 AndAlso _
                        row(6).ToString.Length = 0 Then
                    emptyRow = True
                    ' this is an empty row, we need to remove it from the count of total records
                    totalRecords -= 1
                End If
            End If

            ' invalid owner number will return a base object with ARVACT value of 0
            If ownerToBeModified Is Nothing OrElse ownerToBeModified.ARVACT.Equals(0) Then
                ''errorList.Rows.Add(row(0), "Given Arvact Number doesnot Exist.")
                errorMessage = IIf(String.IsNullOrEmpty(errorMessage), "Given Arvact Number does not Exist.", errorMessage + " ; " + "Given Arvact Number does not Exist.")
                hasError = True
            End If

            If (row(1).ToString.Length > 255 OrElse row(1).ToString.Length = 0) Then
                '' errorList.Rows.Add(row(0), "Address 1 Cannot Exceed 255 Characters.")
                errorMessage = IIf(String.IsNullOrEmpty(errorMessage), "Address 1 Cannot be blank or exceed 255 Characters.", errorMessage + " ; " + "Address 1 Cannot be blank or exceed 255 Characters.")
                hasError = True
            End If
            If (row(2).ToString.Length > 255) Then
                '' errorList.Rows.Add(row(0), "First Name Cannot Exceed 255 Characters.")
                errorMessage = IIf(String.IsNullOrEmpty(errorMessage), "Address 2 Cannot Exceed 255 Characters.", errorMessage + " ; " + "Address 2 Cannot Exceed 255 Characters.")
                hasError = True
            End If
            If (row(3).ToString.Length > 50 OrElse row(3).ToString.Length = 0) Then
                '' errorList.Rows.Add(row(0), "First Name Cannot Exceed 255 Characters.")
                errorMessage = IIf(String.IsNullOrEmpty(errorMessage), "City Name cannot be blank or more than 50 Characters.", errorMessage + " ; " + "City Name cannot be blank or more than 50 Characters.")
                hasError = True
            End If
            If (row(4).ToString.Length > 0) Then
                Dim strState As String = row(4).ToString().ToUpper()
                Dim strStateFromDB As String = String.Empty
                If strState.Length = 2 Then
                    strStateFromDB = (From st As States In lstStates Where st.StateID.ToUpper.Equals(strState) Select st.StateDescription).FirstOrDefault
                Else
                    strStateFromDB = (From st As States In lstStates Where st.StateDescription.ToUpper.Equals(strState) Select st.StateDescription).FirstOrDefault
                End If
                If String.IsNullOrEmpty(strStateFromDB) Then
                    errorMessage = IIf(String.IsNullOrEmpty(errorMessage), "Invalid state or state code.", errorMessage + " ; " + "Invalid state or state code.")
                    hasError = True
                End If
            Else
                errorMessage = IIf(String.IsNullOrEmpty(errorMessage), "State is mandatory field.", errorMessage + " ; " + "State is mandatory field.")
                hasError = True
            End If

            If (row(5).ToString.Length > 0) Then
                Dim strCountry As String = row(5).ToString().ToUpper
                Dim strCountryFromDB As String = String.Empty
                If strCountry.Length = 2 Then
                    strCountryFromDB = (From c As Countries In lstCountries Where c.CountryCode.ToUpper.Equals(strCountry) Select c.CountryCode).FirstOrDefault
                Else
                    strCountryFromDB = (From c As Countries In lstCountries Where c.CountryFullName.ToUpper.Equals(strCountry) Select c.CountryCode).FirstOrDefault
                End If
                If String.IsNullOrEmpty(strCountryFromDB) Then
                    errorMessage = IIf(String.IsNullOrEmpty(errorMessage), "Invalid country or country code.", errorMessage + " ; " + "Invalid country or country code.")
                    hasError = True
                End If
            Else
                errorMessage = IIf(String.IsNullOrEmpty(errorMessage), "Country Code is mandatory field.", errorMessage + " ; " + "Country Code is mandatory field.")
                hasError = True
            End If
            If (row(6).ToString.Length > 10 OrElse row(6).ToString.Length = 0) Then
                '' errorList.Rows.Add(row(0), "First Name Cannot Exceed 255 Characters.")
                errorMessage = IIf(String.IsNullOrEmpty(errorMessage), "Postal Code Cannot be blank or exceed 10 Characters.", errorMessage + " ; " + "Postal Code Cannot be blank or exceed 10 Characters.")
                hasError = True
            End If
            If (Not hasError AndAlso OwnerBlackList.isOwnerIntheBlackList(ARVACT)) Then
                errorMessage = IIf(String.IsNullOrEmpty(errorMessage), "ARVACT is blacklisted and cannot be updated.", errorMessage + " ; " + "ARVACT is blacklisted and cannot be updated.")
                hasError = True
            End If


            If (hasError) Then

                If Not String.IsNullOrEmpty(ARVACT) Then
                    errorList.Rows.Add(ARVACT, errorMessage)
                    invalidRecords += 1
                ElseIf emptyRow Then
                    ' do nothing
                Else
                    errorList.Rows.Add(-1, errorMessage)
                    invalidRecords += 1
                End If
                recordStatus = False
                updateResult = False
            Else
                ' only update the record if no errors found
                validRecords += 1
                recordStatus = True
                updateResult = prepareDataofBxgownerAndUpdate(row, ownerToBeModified)
            End If

            If (updateResult) Then
                Session("arvact") = ownerToBeModified.ARVACT
                fetchOwnerAccountsHolders(ownerToBeModified.ARVACT)
                ownerToBeModified.AccountHolders = Session("accountHolder")
                ownerToBeModified.Accounts = Session("AccountInfo")
                Session("Owner") = ownerToBeModified
                If ownerToBeModified.Accounts.Count > 10 Then
                    Dim BS As Boolean = True
                End If
                insertAs400Comments("Owner address information has been changed from VSSA address upload")
            End If

            If Not emptyRow Then
                ' for empty rows let's not log anything
                SPAuditTrailString.Append(String.Format("<Method ID='{0}' Cmd='New'>", dTable.Rows.IndexOf(row)))
                SPAuditTrailString.Append(String.Format("<Field Name='Title'>{0}</Field>", row(0).ToString()))

                SPAuditTrailString.Append(String.Format("<Field Name='Address1'>{0}</Field>", row(1).ToString()))
                SPAuditTrailString.Append(String.Format("<Field Name='Address2'>{0}</Field>", row(2).ToString()))
                SPAuditTrailString.Append(String.Format("<Field Name='City'>{0}</Field>", row(3).ToString()))
                SPAuditTrailString.Append(String.Format("<Field Name='State'>{0}</Field>", row(4).ToString()))
                SPAuditTrailString.Append(String.Format("<Field Name='Country'>{0}</Field>", row(5).ToString()))
                SPAuditTrailString.Append(String.Format("<Field Name='Zip'>{0}</Field>", row(6).ToString()))

                If String.IsNullOrEmpty(errorMessage) Then

                    SPAuditTrailString.Append(String.Format("<Field Name='OldAddress1'>{0}</Field>", oldOwnerData.Address1))
                    SPAuditTrailString.Append(String.Format("<Field Name='OldAddress2'>{0}</Field>", oldOwnerData.Address2))
                    SPAuditTrailString.Append(String.Format("<Field Name='OldCity'>{0}</Field>", oldOwnerData.City))
                    SPAuditTrailString.Append(String.Format("<Field Name='OldState'>{0}</Field>", oldOwnerData.State))
                    SPAuditTrailString.Append(String.Format("<Field Name='OldCountry'>{0}</Field>", oldOwnerData.CountryCode))
                    SPAuditTrailString.Append(String.Format("<Field Name='OldZip'>{0}</Field>", oldOwnerData.PostalCode))
                Else
                    SPAuditTrailString.Append(String.Format("<Field Name='ErrorMessage'>{0}</Field>", errorMessage))
                End If



                SPAuditTrailString.Append(String.Format("<Field Name='Status'>{0}</Field>", recordStatus))

                SPAuditTrailString.Append(String.Format("<Field Name='Environment'>{0}</Field>", changesMadeFrom))
                SPAuditTrailString.Append(String.Format("<Field Name='AuditDocument'>{0}, {1}</Field>", docURL, FileName))
                SPAuditTrailString.Append(String.Format("<Field Name='UploadedBy'>{0}</Field>", currentUserId))

                SPAuditTrailString.Append("</Method>")
            End If

            If recordCount = 30 Then
                ' create a new batch which will be less than the size limit for the lists.asmx service to handle at once
                lstSPAuditTrail.Add(SPAuditTrailString.ToString)
                SPAuditTrailString.Clear()
                recordCount = 0
            End If
        Next
        ''Call the Sharepoint to store File and also store records which are updated in BGO Db
        If (SPAuditTrailString.Length > 0) Then
            lstSPAuditTrail.Add(SPAuditTrailString.ToString)
        End If
        If lstSPAuditTrail.Count > 0 Then
            insertSharepointListItems(FilePath, destinationUrl, lstSPAuditTrail)
        End If
    End Sub

    Protected Sub PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        'Dim FolderPath As String = ConfigurationManager.AppSettings("VSSA.GetExcelSheetFolderPath")
        ' '' Dim FileName As String = FileName
        'Dim Extension As String = Path.GetExtension(hdn_FileName.Value)
        'Dim pageIndexFilePath As String = Server.MapPath(FolderPath + FileName)

        'Import_To_Datatable(pageIndexFilePath, Extension, "Yes")
        grdErrorList.Visible = True

        grdErrorList.PageIndex = e.NewPageIndex
        grdErrorList.DataSource = CType(Session("errorListRecords"), DataTable)
        grdErrorList.DataBind()
    End Sub

    Private Sub insertSharepointListItems(ByVal sourceUrl As String, ByVal destinationUrl As String(), ByVal lstItemsToBeAudited As List(Of String))
        Try
            Dim fileContentsData As Byte() = File.ReadAllBytes(sourceUrl)

            Dim spDoc As New SPDocumentUpload.CopySoapClient
            spDoc.ClientCredentials.Windows.ClientCredential = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("VSSA.GetSharePointAdminId"), ConfigurationManager.AppSettings("VSSA.GetSharePointAdminPwd"), "BXGCORP")

            ''Address Upload 
            Dim sp As New SPAddressUpload.ListsSoapClient
            sp.ClientCredentials.Windows.ClientCredential = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("VSSA.GetSharePointAdminId"), ConfigurationManager.AppSettings("VSSA.GetSharePointAdminPwd"), "BXGCORP")
            'Dim result As CopyResult()

            Dim cResult1 As New SPDocumentUpload.CopyResult()
            Dim cResultArray As SPDocumentUpload.CopyResult() = {cResult1}
            Dim fFiledInfo As New SPDocumentUpload.FieldInformation()
            fFiledInfo.DisplayName = "Uploaded By"
            fFiledInfo.InternalName = "UploadedBy"
            fFiledInfo.Type = FieldType.User
            fFiledInfo.Value = "-1;#" + Environment.UserDomainName + "\" + Environment.UserName

            Dim fFiledInfoArray As SPDocumentUpload.FieldInformation() = {fFiledInfo}



            Dim copyresult As UInteger = spDoc.CopyIntoItems(sourceUrl, destinationUrl, fFiledInfoArray, fileContentsData, cResultArray)

            If cResultArray IsNot Nothing AndAlso cResultArray.Length > 0 AndAlso cResultArray(0).ErrorCode = 0 Then
                Dim listHistory = ConfigurationManager.AppSettings("VSSA.GetHistoryListName")
                For Each spBatch As String In lstItemsToBeAudited
                    Dim listWS = sp.GetList(listHistory)
                    Dim ndListView = sp.GetListAndView(listHistory, "")
                    Dim xReader As System.Xml.XmlReader = ndListView.CreateReader
                    Dim doc As New System.Xml.XmlDocument
                    doc.Load(xReader)

                    Dim batchElement As System.Xml.XmlElement = doc.CreateElement("Batch")
                    batchElement.SetAttribute("OnError", "Continue")
                    batchElement.SetAttribute("ListVersion", "1")

                    batchElement.InnerXml = spBatch.ToString()
                    sp.UpdateListItems(listHistory, XElement.Parse(batchElement.OuterXml))

                Next
                spDoc.Close()
                sp.Close()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbNewSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNewSearch.Click
        Response.Redirect("../default.aspx")
    End Sub

    Protected Sub lbAddressUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbAddressUpload.Click
        Server.Transfer("AddressUpload.aspx")
    End Sub

    Private Sub InitiateVisibility()
        grdErrorList.Visible = False
        LabelErrorMsg.Visible = False
        LabelErrorMsg.Text = ""
        divInformation.Visible = False


        'lblInformation.Visible = False
        'lblInformation.Text = ""


    End Sub

End Class

