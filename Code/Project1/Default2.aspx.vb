Imports IBM.Data.DB2.iSeries

Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        response.write("entering IBM zone<br>")
        ' A simple iDB2Connection example
        Dim cn As New iDB2Connection("userid=BGONLIN1;password=BLUE44;DataSource=BLUGRN;LibraryList=*USRLIBL;CheckConnectionOnOpen=True")
        cn.Open()
        RESPONSE.WRITE(cn.JobName)
        cn.Close()


        System.Web.HttpContext.Current.Response.Write("<br>The above should show job name:<br>")

        Try

            Using conn As IDbConnection = New iDB2Connection

                Try

                    conn.ConnectionString = ConfigurationManager.AppSettings("bxgwebDBConnectionAS400DB2")

                    System.Web.HttpContext.Current.Response.Write("<br>Passes connection string setup<br>")
                    Dim cmd As iDB2Command = New iDB2Command '= conn.CreateCommand
                    cmd.Connection = conn
                    cmd.CommandTimeout = 300
                    cmd.CommandText = "ZODBC.GETINDT"
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add("iOwnerNum", iDB2DbType.iDB2Numeric)
                    cmd.Parameters("iOwnerNum").Value = CInt(430142)
                    cmd.Parameters("iOwnerNum").Direction = ParameterDirection.Input

                    conn.Open()

                    Dim dataReader As iDB2DataReader
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                    ' Process each record in the result set
                    While dataReader.Read
                        If Not IsDBNull(dataReader("rBCTYPE")) Then
                            Response.Write(Convert.ToString(CStr(dataReader("rBCTYPE"))))
                        End If

                        If Not IsDBNull(dataReader("rBCELIG")) Then
                            Response.Write(Convert.ToString(dataReader("rBCELIG")))
                        End If

                        If Not IsDBNull(dataReader("rBCSTAT")) Then
                            Response.Write(Convert.ToString(dataReader("rBCSTAT")))
                        End If
                        If Not IsDBNull(dataReader("rLEADACCT")) Then
                            Response.Write(Convert.ToString(dataReader("rLEADACCT")))
                        End If

                        If Not IsDBNull(dataReader("rBCTAMT")) Then
                            Response.Write(Convert.ToString(dataReader("rBCTAMT")))
                        End If

                        If Not IsDBNull(dataReader("rBC1INS")) Then
                            Response.Write(Convert.ToString(dataReader("rBC1INS")))
                        End If

                        If Not IsDBNull(dataReader("rBC1PD")) Then
                            Response.Write(Convert.ToString(dataReader("rBC1PD")))
                        End If

                    End While



                Catch ex As Exception

                    Dim xx As String = ex.Message
                    Response.Write(xx)

                Finally
                    conn.Close()

                End Try

            End Using

        Catch ex As Exception

            System.Web.HttpContext.Current.Response.Write("<br>Error: " & ex.Message & "<br>comment out conn.close.")

        End Try
    End Sub
End Class
