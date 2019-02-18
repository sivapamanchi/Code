Imports VSSA.fetchresp

Public Class OwnerUtilities
    Public Shared Function GetOmwerType(ByVal bxgOwner As OwnerFetchResponse) As String
        Dim ownerType As String = ""
        bxgOwner = HttpContext.Current.Session("BoomiOwner")
        Dim counter As Integer = 0
        Do Until counter = bxgOwner.Account.Memberships.VacationClubMembership.Legacy.MaintenanceFees.MaintenanceFeeReservation.Count

            With bxgOwner.Account.Memberships.VacationClubMembership.Legacy.MaintenanceFees.MaintenanceFeeReservation(counter)

                If Not (bxgOwner.Account.Memberships.VacationClubMembership.Sampler Is Nothing) Then
                    If bxgOwner.Account.Memberships.VacationClubMembership.Sampler.IsSampler = True Then
                        If bxgOwner.Account.Memberships.VacationClubMembership.Legacy.HomeProjectNumber = "51" Then
                            ownerType = "Sampler"
                        ElseIf bxgOwner.Account.Memberships.VacationClubMembership.Legacy.HomeProjectNumber = "52" Then
                            ownerType = "Sampler 24"
                        End If
                    Else
                        If .ProjectNumber = "50" Then
                            ownerType = "Vacation Club"
                        ElseIf .ProjectNumber = "32" Then
                            ownerType = "Players"
                        ElseIf .ProjectNumber = "79" Then
                            ownerType = "Pono"
                        Else
                            ownerType = .ProjectName
                        End If
                    End If
                End If
            End With

            counter = counter + 1
        Loop

        Return ownerType

    End Function

    Public Shared Function GetCollectionStatus(ByVal payinfo As DataSet) As String
        Dim colStatus As String = ""

        Dim names As List(Of String) = payinfo.Tables(0).AsEnumerable() _
                                               .Select(Function(r) r.Field(Of String)("collectionCode")) _
                                               .Distinct() _
                                               .ToList()

        Dim dt As DataTable = payinfo.Tables(0)
        For i As Integer = 0 To names.Count - 1
            If colStatus.Length > 0 Then
                colStatus &= ", " & names(i)
            Else
                colStatus &= names(i)
            End If

        Next
        Return colStatus
    End Function


End Class
