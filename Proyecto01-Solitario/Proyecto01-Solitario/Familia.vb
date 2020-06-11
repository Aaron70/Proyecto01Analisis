Public Class Familia
    Private nombre As String
    Private color As String

    Public Sub New(nombre As String, color As String)
        Me.nombre = nombre
        Me.color = color
    End Sub

    Public Property nombreFamilia As String
        Get
            Return nombre
        End Get
        Set(value As String)
            nombre = value
        End Set
    End Property

    Public Property ColorFamilia As String
        Get
            Return color
        End Get
        Set(value As String)
            color = value
        End Set
    End Property

End Class
