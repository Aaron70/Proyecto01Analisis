Imports Proyecto01_Solitario

Public Class Mazo
    Private listaCartasMazo As List(Of Carta)
    Private esVacio As Boolean

    Public Sub New(listaCartasMazo As List(Of Carta), esVacio As Boolean)
        Me.ListaCartas = listaCartasMazo
        Me.Vacio = esVacio
    End Sub

    Public Property Vacio As Boolean
        Get
            Return esVacio
        End Get
        Set(value As Boolean)
            esVacio = value
        End Set
    End Property

    Public Property ListaCartas As List(Of Carta)
        Get
            Return listaCartasMazo
        End Get
        Set(value As List(Of Carta))
            listaCartasMazo = value
        End Set
    End Property

End Class
