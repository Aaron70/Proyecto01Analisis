Imports System.ComponentModel

Public Class Barajas

    Public Shared reparticiones As New List(Of Pila)

    Private random As Random

    Private Sub CrearBarajas_Click(sender As Object, e As EventArgs) Handles CrearBarajas.Click
        reparticiones = New List(Of Pila)
        Dim cant = NumeroBarajas.Value
        For i = 1 To cant
            Dim Mazo1 As Mazo = New Mazo(1)
            Dim Mazo2 As Mazo = New Mazo(1)

            Mazo1.barajarCartas()
            Mazo2.barajarCartas()

            UnirMazos(Mazo1, Mazo2)
        Next


        Tablero.Show()
        Me.Hide()
    End Sub

    Public Function GenerarNumeroRandom(Min As Integer, Max As Integer) As Integer 'genera un numero random
        If (IsNothing(random)) Then
            random = New Random(Now.Millisecond)
        End If
        Return random.Next(Min, Max + 1)
    End Function

    Private Sub UnirMazos(Mazo1 As Mazo, Mazo2 As Mazo)
        While Mazo2.ListaCartas.Count > 0 'Insertamos en posiciones aleatorias cartas aleatorias de las parejas formadas anteriormente'

            Dim pos = GenerarNumeroRandom(0, Mazo1.ListaCartas.Count - 1)
            Dim pos2 = GenerarNumeroRandom(0, Mazo2.ListaCartas.Count - 1)

            Mazo1.ListaCartas.Insert(pos, Mazo2.ListaCartas(pos2))
            Mazo2.ListaCartas.Remove(Mazo2.ListaCartas(pos2))

        End While

        reparticiones.Add(New Pila(Mazo1.ListaCartas))
    End Sub

    Private Sub Barajas_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Main.Visible = True
    End Sub
End Class