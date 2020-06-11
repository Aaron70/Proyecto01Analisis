Public Class Familia
    Private nombreFamilia As String
    Private colorFamilia As String

    Public Sub New(nombreFamilia As String, colorFamilia As String)
        Me.Nombre = nombreFamilia
        Me.Color = colorFamilia
    End Sub

    Public Property Nombre As String
        Get
            Return nombreFamilia
        End Get
        Set(value As String)
            nombreFamilia = value
        End Set
    End Property

    Public Property Color As String
        Get
            Return colorFamilia
        End Get
        Set(value As String)
            colorFamilia = value
        End Set
    End Property
End Class
