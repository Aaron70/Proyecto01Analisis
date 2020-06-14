'===============================================================
' Familia
'---------------------------------------------------------------
'Proposito : Crea el molde para las familias que se van a utilizar
'            en el juego de solitario
'
' Autor : Ingrd Fernández 2020
'         Aaron Vargas 2020
'         Daniel Calderon 2020
'
' Notas   : Cada carta pertenece a una familia
'           
'---------------------------------------------------------------
' Parametros
'-----------
' Nombre : Identifica a cada familia.
'                 
' Color: ayuda a definir el tipo de familia y el color de las
'        cartas que pertenecen a cada familia
' 
'---------------------------------------------------------------
' Retorno : Un objeto tipo Familia
'          
'---------------------------------------------------------------
'Historial
'---------------------------------------------------------------
' 11 Junio IF  : Version inicial
' 
'===============================================================

Public Class Familia
    Private nombreFamilia As String
    Private colorFamilia As String

    Public Sub New(nombreFamilia As String)
        Me.Nombre = nombreFamilia
        If (nombreFamilia.Equals("Diamantes") Or nombreFamilia.Equals("Corazones")) Then
            colorFamilia = "Rojo"
        Else
            colorFamilia = "Negro"
        End If
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
