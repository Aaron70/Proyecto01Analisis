Imports Proyecto01_Solitario

Public Class Carta
    Private numeroCarta As Integer
    Private simboloCarta As String
    Private familiaCarta As Familia
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

    Public Property simbolo As String
        Get
            Return simbolo
        End Get
        Set(value As String)
            simboloCarta = value
        End Set
    End Property

    Public Property numero As Integer
        Get
            Return numero
        End Get
        Set(value As Integer)
            numeroCarta = value
        End Set
    End Property

    Public Property familia As Familia
        Get
            Return familiaCarta
        End Get
        Set(value As Familia)
            familiaCarta = value
        End Set
    End Property
End Class
