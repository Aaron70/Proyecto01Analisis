Imports Proyecto01_Solitario

'===============================================================
' Mazo
'---------------------------------------------------------------
'Proposito : Crea el molde para el mazo que se van a utilizar
'            en el juego de solitario
'
' Autor : Ingrd Fernández 2020
'         Aaron Vargas 2020
'         Daniel Calderon 2020
'
' Notas   : Un mazo es un grupo de cartas de distintas  familias
'           
'---------------------------------------------------------------
' Parametros
'-----------
' ListaCartas : grupo de cartas de distintos tipos 
'                 
' Vacio: ayuda a definir si la lista de cartas contiene
'        alguna carta
' 
'---------------------------------------------------------------
' Retorno : Un objeto tipo Mazo
'          
'---------------------------------------------------------------
'Historial
'---------------------------------------------------------------
' 11 Junio IF  : Version inicial
' 
'===============================================================

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
