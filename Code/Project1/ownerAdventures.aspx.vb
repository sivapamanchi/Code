Namespace BluegreenOnline

    Partial Class ownerAdventures
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Session("IsTravelerPlusEmployee") = "TRUE" Then
                'MEMBER_NAME_FULL.Value = Session("EmployeeFirstName") & " " & Session("EmployeelastName")
            Else
                'MEMBER_NAME_FULL.Value = Session("OwnerNameFirst") & " " & Session("OwnerNameLast")
            End If

            UcMenu1.CrTPOwnerMenu()
        End Sub
    End Class

End Namespace
