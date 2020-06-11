Imports Proyecto01_Solitario

'===============================================================
' Carta
'---------------------------------------------------------------
'Proposito : Crea el molde para las cartas que se van a utilizar
'            en el juego de solitario
'
' Autor : Ingrd Fernández 2020
'         Aaron Vargas 2020
'         Daniel Calderon 2020
'
' Notas   : Las cartas pertenecen a una familia
'           
'---------------------------------------------------------------
' Parametros
'-----------
' Numero : indica el valor de cada carta.
'                 
' Simbolo: ayuda a definir el tipo de grupo al que pertence la carta
' 
' Familia: grupo al que pertenece un carta
'
' Visible: indica si una carta esta hacia arriba o hacia abajo 
'          en el juego
'---------------------------------------------------------------
' Retorno : Un objeto tipo Carta
'          
'---------------------------------------------------------------
'Historial
'---------------------------------------------------------------
' 11 Junio IF  : Version inicial
' 
'===============================================================

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
