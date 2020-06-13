﻿'===============================================================
' Pila
'---------------------------------------------------------------
'Proposito : Crea el molde para las Pila de cartas que se van a utilizar
'            en el juego de solitario
'
' Autor : Ingrd Fernández 2020
'         Aaron Vargas 2020
'         Daniel Calderon 2020
'
' Notas   : La pila va ordenada de mayor a menor
'           
'---------------------------------------------------------------
' Parametros
'-----------
' elementos : contiene todas las cartas de la pila.
'                 
' cartaMenor: indica cual esla carta de menor valor en toda la pila
' 
' cartaMayor: indica cual esla carta de mayor valor en toda la pila
'
'---------------------------------------------------------------
' Retorno : Un objeto tipo Pila
'          
'---------------------------------------------------------------
'Historial
'---------------------------------------------------------------
' 11 Junio AV  : Version inicial
' 
'===============================================================


Public Class Pila
    Private elementos As List(Of Carta) = New List(Of Carta)
    Private cartaMenor As Carta 'La primer carta de la pila es la menor (la que esta en el top de la pila)
    Private cartaMayor As Carta 'La ultima carta de la pila es mayor (la que esta en el fondo de la pila)
    'En una totalmente llena el K seria el numero mayor y habria sido el primero en entrar y el As '
    Sub New()
        cartaMenor = Nothing
        cartaMayor = Nothing
    End Sub

    Sub New(nuevosElementos As List(Of Carta))
        elementos = nuevosElementos
        If (nuevosElementos.Count > 0) Then
            cartaMayor = nuevosElementos(0)
            cartaMenor = nuevosElementos(nuevosElementos.Count - 1)
        End If
    End Sub

    Sub New(nuevosElementos As Pila)
        elementos = nuevosElementos.elementos
        cartaMayor = nuevosElementos.cartaMayor
        cartaMenor = nuevosElementos.cartaMenor
    End Sub

    Public Function esVacia()
        Return Not (elementos.Count > 0)
    End Function

    '===============================================================
    ' Insert
    '---------------------------------------------------------------
    'Proposito : Agrega una carta en la pila actual
    '           
    '
    ' Autor : Ingrd Fernández 2020
    '         Aaron Vargas 2020
    '         Daniel Calderon 2020
    '
    ' Notas   : La pila va ordenada de mayor a menor
    '           
    '---------------------------------------------------------------
    ' Parametros
    '-----------
    ' carta: objeto que se desea insertar
    '
    '---------------------------------------------------------------
    ' Retorno : Un valor booleano si lo pudo insertar o no
    '          
    '---------------------------------------------------------------
    'Historial
    '---------------------------------------------------------------
    ' 11 Junio AV  : Version inicial
    ' 
    '===============================================================
    Public Function Insert(carta As Carta) As Boolean
        If (Not esVacia() AndAlso cartaMenor.numero <= carta.numero) Then
            'Entra cuando la carta menor de la pila actual es menor a la carta entrante
            Return False
        End If
        If (Me.esVacia) Then
            cartaMayor = carta
        End If
        elementos.Add(carta)
        cartaMenor = carta
        Return True
    End Function

    Public Function Insert(cartas As Pila) As Boolean
        If (Not (Me.esVacia Or cartas.esVacia) AndAlso (cartaMenor.numero <= cartas.cartaMayor.numero Or cartaMenor.numero - cartas.cartaMayor.numero <> 1)) Then
            'Entra cuando la carta menor de la pila actual es menor a la carta mayor de la pila entrante
            Return False
        End If
        If (Me.esVacia) Then
            cartaMayor = cartas.cartaMayor
        End If
        For Each carta In cartas.elementos
            elementos.Add(carta)
        Next
        cartaMenor = cartas.cartaMenor
        Return True
    End Function


    Public Function SacarCartas(carta As Carta) As Pila
        If (Not Me.esVacia) Then
            Dim index = elementos.IndexOf(carta)
            Dim ant = index
            Dim nuevaLista As Pila = New Pila()
            If (index >= 0) Then
                nuevaLista.Insert(elementos(index))
                For i = index + 1 To elementos.Count - 1
                    nuevaLista.Insert(elementos(i))
                    If (Not elementos(ant).familia.Nombre.Equals(elementos(i).familia.Nombre)) Then
                        Return New Pila()
                    End If
                    ant = i
                Next
                elementos.RemoveRange(index, (elementos.Count) - index)
                If (esVacia()) Then
                    cartaMayor = Nothing
                    cartaMenor = Nothing
                Else
                    cartaMenor = elementos(elementos.Count - 1)
                End If
                Return nuevaLista
            End If
        End If
        Return Nothing
    End Function

    Public Function Count()
        Return elementos.Count
    End Function

    Public Function obtenerCarta(indice)
        If (indice <= elementos.Count - 1 And indice > -1) Then
            Return elementos(indice)
        End If
        Return Nothing
    End Function


End Class
