Imports Proyecto01_Solitario

Public Class Carta
    Private numero As Integer
    Private simbolo As String
    Private familia As Familia
    Private visible As Boolean

    Public Sub New(numero As Integer, simbolo As String, familia As Familia, visible As Boolean)
        Me.numero = numero
        Me.simbolo = simbolo
        Me.familia = familia
        Me.visible = visible
    End Sub

    Public Property esVisible As Boolean
        Get
            Return visible
        End Get
        Set(value As Boolean)
            visible = value
        End Set
    End Property

    Public Property simboloCarta As String
        Get
            Return simbolo
        End Get
        Set(value As String)
            simbolo = value
        End Set
    End Property

    Public Property numeroCarta As Integer
        Get
            Return numero
        End Get
        Set(value As Integer)
            numero = value
        End Set
    End Property
End Class
