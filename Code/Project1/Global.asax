
<%@ Application Language="VB" Codebehind="Global.asax.vb" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        BXG.BGO.Choice.PrivilegesAccounts.Infrastructure.AccountMappings.Configure()
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
        'URN 09/21/2009  
        'Dim ex As System.Exception
        'ex = Server.GetLastError()
        'Dim exHandler As New BluegreenOnline.ExceptionHandler
       
        'Dim x As String
        'x = Server.MachineName
                
        'exHandler.HandleException(ex)
               
        'Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("ExceptionServer") + "/errorGeneric.aspx", False)
                    
        'Server.ClearError()--%>
        
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>