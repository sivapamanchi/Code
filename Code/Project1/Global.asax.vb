Imports System.Web
Imports System.Web.SessionState
Imports BXGDiagnostics
Imports System.Diagnostics
Imports System.Web.Configuration


Namespace BluegreenOnline


    Public Class [Global]
        Inherits System.Web.HttpApplication

#Region " Component Designer Generated Code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Component Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call

        End Sub

        'Required by the Component Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Component Designer
        'It can be modified using the Component Designer.
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
        End Sub

#End Region

        Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires when the application is started
        End Sub

        Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires when the session is started
            If Request.Cookies("ASP.NET_SessionId") IsNot Nothing Then
                Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)
            End If
        End Sub

        Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires at the beginning of each request
        End Sub

        Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires upon attempting to authenticate the use
        End Sub

        Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)

            ' Fires when an error occurs
            'Dim ex As Exception = Server.GetLastError.GetBaseException
            'Dim rethrow As Boolean = Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, ExceptionHandlingConstants.UIUnhandledExceptionPolicy)
            'If (rethrow) Then
            '    Throw ex
            'End If

            'Filter out errors on the omission list
            'Dim sErrorHolder As String
            'Dim bReportError As Boolean = True
            'sErrorHolder = (sender.request.servervariables("PATH_INFO") & " " & Context.Error.InnerException.Source & " " & Context.Error.InnerException.Message & " " & Context.Error.InnerException.StackTrace).ToLower()
            'Try

            'sErrorHolder = (sender.request.servervariables("PATH_INFO") & " " & Context.Error.InnerException.Source & " " & Context.Error.InnerException.Message & " " & Context.Error.InnerException.StackTrace).ToLower()

            'Filter out .NET 1.1 build related event always
            'If (sErrorHolder.IndexOf("get_aspx_ver.aspx") > -1) Then bReportError = False

            'Loop through configured error ignore list
            'Dim sOmitItem() As String
            'sOmitItem = System.Configuration.ConfigurationManager.AppSettings("DiagnosticEmailOmissionList").Split("|")

            'For x As Integer = 0 To UBound(sOmitItem)
            'If (sErrorHolder.IndexOf(sOmitItem(x).ToLower()) > -1) Then bReportError = False
            'Next

            'Catch ex As Exception

            'End Try

            'If bReportError Then
            'Filter out page not found errors from logging/emailing
            'If Not Context.Error.InnerException.ToString().StartsWith("System.IO.FileNotFoundException") Then
            '    Dim objLogging As BXGDiagnostics.EventLogging
            '    Dim sPageData As String

            '    'Capture Page and Querystring data to the error log and email notification
            '    Try
            '        sPageData = sender.request.servervariables("PATH_INFO")
            '        If sender.request.servervariables("QUERY_STRING").length > 0 Then
            '            sPageData &= "?" & sender.request.servervariables("QUERY_STRING")
            '        End If
            '        sPageData = " : " & sPageData & "  "
            '        sPageData = sPageData & "; " & Request.ServerVariables("HTTP_USER_AGENT") & ".  "
            '    Catch
            '        sPageData = ""
            '    End Try

            '    objLogging = New EventLogging("BlueGreenOnline", Context.Request.FilePath)
            '    objLogging.LogEvent(sPageData & Context.Error.InnerException.Source & vbCrLf & Context.Error.InnerException.Message & vbCrLf & Context.Error.InnerException.StackTrace, EventLogEntryType.Error)
            'End If
            'End If
        End Sub

        Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires when the session ends
        End Sub

        Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires when the application ends
        End Sub

    End Class

End Namespace
