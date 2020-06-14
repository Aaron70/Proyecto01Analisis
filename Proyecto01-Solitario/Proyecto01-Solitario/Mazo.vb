﻿Imports Proyecto01_Solitario

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
    Private listaCartasMazo As List(Of Carta) = New List(Of Carta)
    Private esVacio As Boolean
    Private r As Random = New Random(Now.Millisecond)
    Private cartasPorFamilia() As List(Of Carta) = {New List(Of Carta), New List(Of Carta), New List(Of Carta), New List(Of Carta)}

    Public Sub New()
        Me.Vacio = True
        inicializarCartas()
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

    Private Sub inicializarCartas()
        Dim familias() As Familia = {New Familia("Diamentes"), New Familia("Corazones"), New Familia("Picas"), New Familia("Treboles")}
        Dim familiaIndex = 0
        For Each familia In familias
            For i = 1 To 13
                Dim carta = New Carta(i, familia, False)
                ListaCartas.Add(carta)
                cartasPorFamilia(familiaIndex).Add(carta)
            Next
            familiaIndex += 1
        Next
        esVacio = False
    End Sub

    Private Function GenerarNumeroRandom(Min As Integer, Max As Integer) As Integer 'genera un numero random
        Return r.Next(Min, Max + 1)
    End Function

    Public Function barajarCartas()
        Dim cartas As List(Of Carta) = New List(Of Carta)
        Dim cartasPorFamiliaBarajadas() As List(Of Carta) = {New List(Of Carta), New List(Of Carta), New List(Of Carta), New List(Of Carta)}
        For index = 0 To cartasPorFamilia.Count - 1 'Barajamos cada familia dentro del mazo
            While cartasPorFamilia(index).Count > 0
                Dim pos = GenerarNumeroRandom(0, cartasPorFamilia(index).Count - 1)
                cartasPorFamiliaBarajadas(index).Add(cartasPorFamilia(index)(pos))
                cartasPorFamilia(index).Remove(cartasPorFamilia(index)(pos))
            End While
        Next
        cartasPorFamiliaBarajadas(0).Concat(cartasPorFamiliaBarajadas(2))
        While cartasPorFamiliaBarajadas(3).Count > 0 'Insertamos en posiciones aleatorias cartas aleatorias por pareja de familias'
            Dim pos = GenerarNumeroRandom(0, cartasPorFamiliaBarajadas(1).Count - 1) 'Primera pareja'
            Dim pos2 = GenerarNumeroRandom(0, cartasPorFamiliaBarajadas(3).Count - 1)
            cartasPorFamiliaBarajadas(1).Insert(pos, cartasPorFamiliaBarajadas(3)(pos2))
            cartasPorFamiliaBarajadas(3).Remove(cartasPorFamiliaBarajadas(3)(pos2))

            pos = GenerarNumeroRandom(0, cartasPorFamiliaBarajadas(0).Count - 1) 'Segunda Pareja'
            pos2 = GenerarNumeroRandom(0, cartasPorFamiliaBarajadas(2).Count - 1)
            cartasPorFamiliaBarajadas(0).Insert(pos, cartasPorFamiliaBarajadas(2)(pos2))
            cartasPorFamiliaBarajadas(2).Remove(cartasPorFamiliaBarajadas(2)(pos2))
        End While

        While cartasPorFamiliaBarajadas(1).Count > 0 'Insertamos en posiciones aleatorias cartas aleatorias de las parejas formadas anteriormente'
            Dim pos = GenerarNumeroRandom(0, cartasPorFamiliaBarajadas(0).Count - 1)
            Dim pos2 = GenerarNumeroRandom(0, cartasPorFamiliaBarajadas(1).Count - 1)
            cartasPorFamiliaBarajadas(0).Insert(pos, cartasPorFamiliaBarajadas(1)(pos2))
            cartasPorFamiliaBarajadas(1).Remove(cartasPorFamiliaBarajadas(1)(pos2))
        End While

        ListaCartas = cartasPorFamiliaBarajadas(0)
        Return ListaCartas
    End Function






End Class
