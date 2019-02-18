Namespace SendPoints
    Public Enum ProductType
        BLUGRNBAS
        BLUGRNBON
        BLUGRNREV
        BLUGRNEXG
        BLUGRNDMS
    End Enum
    Public Class Order
        Public Property Points As Integer
        Public Property ProductType As String
    End Class
    Public Class Points
        Public Property ChoicePrivilegeID As String
        Public Property Person As List(Of Person)
        Public Property Order As List(Of Order)
    End Class
    Public Class Person
        Public Property LastName As String
    End Class
End Namespace
