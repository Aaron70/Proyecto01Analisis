Imports System.IO

Public Class Form

    Private Arreglo(10) As Pila
    Private APilas(10) As List(Of PictureBox)
    Dim coordenadas As Point
    Dim posAnterior As Point
    Dim indiceAnterior(2) As Integer
    Dim AnchoCartas As Integer = 105
    Dim altoCartas As Integer = 145
    Dim porcentajeAltoNoVisible As Decimal = 0.15
    Dim porcentajeAltoVisible As Decimal = 0.3
    Dim cartas As Pila
    Dim botones As List(Of PictureBox) = New List(Of PictureBox)
    Dim mazo As Pila = New Pila()
    Dim jugadas As Stack
    Private r As Random = New Random(Now.Millisecond)
    Private imagenVolteada As String = Path.Combine(Environment.CurrentDirectory, "..\..\Cartas\volteada.jpg")
    Private Function GenerarNumeroRandom(Min As Integer, Max As Integer) As Integer 'genera un numero random
        Return r.Next(Min, Max + 1)
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Arreglo = {New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila()}
        APilas = {New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox),
            New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox)}
        Panel1.AllowDrop = True
        Me.WindowState = FormWindowState.Maximized
        jugadas = New Stack()
        Dim Mazo1 As Mazo = New Mazo()
        Dim Mazo2 As Mazo = New Mazo()
        Mazo1.barajarCartas()
        Mazo2.barajarCartas()

        While Mazo2.ListaCartas.Count > 0 'Insertamos en posiciones aleatorias cartas aleatorias de las parejas formadas anteriormente'
            Dim pos = GenerarNumeroRandom(0, Mazo1.ListaCartas.Count - 1)
            Dim pos2 = GenerarNumeroRandom(0, Mazo2.ListaCartas.Count - 1)
            Mazo1.ListaCartas.Insert(pos, Mazo2.ListaCartas(pos2))
            Mazo2.ListaCartas.Remove(Mazo2.ListaCartas(pos2))
        End While
        mazo = New Pila(Mazo1.ListaCartas)
        RepartirCartas()
        jugadas.Clear()
    End Sub

    Private Sub RepartirCartas()
        Dim contador = 44
        While contador > 0
            For i = 0 To APilas.Length - 1
                If (Not contador > 0) Then
                    Exit For
                End If
                Dim carta As Carta = mazo.obtenerCarta(mazo.Count - 1)
                mazo.Remove(mazo.obtenerCarta(mazo.Count - 1))
                CreateCarta(i, carta)
                contador -= 1
            Next
        End While
        For i = 0 To APilas.Length - 1
            mazo.obtenerCarta(mazo.Count - 1).esVisible = True
            Dim carta As Carta = mazo.obtenerCarta(mazo.Count - 1)
            mazo.Remove(mazo.obtenerCarta(mazo.Count - 1))
            CreateCarta(i, carta)
        Next
    End Sub

    Private Function calcPos(pila) As Point
        Return New Point((25 + AnchoCartas) * pila + 20, (altoCartas * porcentajeAltoNoVisible) * (Arreglo(pila).Count - 1) + 10)
    End Function

    Private Sub CreateCarta(pila As Integer, carta As Carta)

        Dim btn As PictureBox = New PictureBox()
        Dim visible = Arreglo(pila).InserForce(carta)
        Dim pic As PictureBox = New PictureBox()
        btn.Size = New Size(AnchoCartas, altoCartas)
        btn.Location = calcPos(pila)
        btn.Text = carta.numero.ToString() + carta.familia.Nombre
        btn.Cursor = Cursors.Hand
        btn.Enabled = carta.esVisible

        'colocando imagenes por carta
        If carta.esVisible Then
            btn.Image = Image.FromFile(carta.imagen)
            btn.SizeMode = PictureBoxSizeMode.StretchImage
        Else
            btn.Image = Image.FromFile(imagenVolteada)
            btn.SizeMode = PictureBoxSizeMode.StretchImage
        End If

        AddHandler btn.MouseDown, AddressOf StartDrag
        AddHandler btn.MouseMove, AddressOf Drag
        AddHandler btn.MouseUp, AddressOf EndDrag
        Panel1.Controls.Add(btn)
        btn.BringToFront()
        APilas(pila).Add(btn)
        Dim jugada() = {0, pila, carta, btn} '{tipo,destino,carta,boton} //cuando es de tipo 0 es porque viene del mazo y no tiene origen
        jugadas.Push(jugada)

    End Sub

    Private Sub volver()
        If (jugadas.Count > 0) Then
            Dim jugada = jugadas.Pop()

            If (jugada(0) = 0) Then ' {tipo,destino,carta,boton} //Inserccion desde el mazo
                Dim btn As PictureBox = jugada(3)
                Dim carta As Carta = jugada(2)
                Arreglo(jugada(1)).Remove(carta)
                Panel1.Controls.Remove(btn)
                APilas(jugada(1)).Remove(btn)
                btn.Dispose()
                mazo.InserForce(carta)
                btn_Repartir.Enabled = mazo.Count > 0

            ElseIf (jugada(0) = 1) Then ' {tipo,origen,destino,carta,boton,visible} //Inserccion desde el mazo
                Dim carta As Carta = jugada(3)
                Dim btn As PictureBox = jugada(4)
                Arreglo(jugada(1)).InserForce(carta)
                Arreglo(jugada(2)).Remove(carta)
                APilas(jugada(1)).Add(btn)
                APilas(jugada(2)).Remove(btn)
                ''btn.Enabled = jugada(5)
                btn.Location = calcPos(jugada(1))
                deshabilitar(jugada(1))
                deshabilitar(jugada(2))

            End If
        End If
    End Sub

    Private Sub deshabilitar(pila As Integer, carta As Carta)
        Dim visible = True
        For i = 2 To Arreglo(pila).Count
            If Not (carta.familia.Nombre.Equals(Arreglo(pila).obtenerCarta(Arreglo(pila).Count - i))) Then
                visible = False
            End If
            Arreglo(pila).obtenerCarta(Arreglo(pila).Count - i).esVisible = visible
            APilas(pila)(APilas(pila).Count - i).Enabled = visible
        Next
    End Sub

    Private Sub deshabilitar(pila As Integer)
        Dim visible = True
        Dim carta As Pila = Arreglo(pila)
        Arreglo(pila).obtenerCarta(Arreglo(pila).Count - 1).esVisible = True
        APilas(pila)(APilas(pila).Count - 1).Enabled = True
        For i = 2 To Arreglo(pila).Count
            If Not (carta.obtenerCarta(carta.Count - 1).familia.Nombre.Equals(Arreglo(pila).obtenerCarta(Arreglo(pila).Count - i).familia.Nombre)) Then
                visible = False
            End If
            Arreglo(pila).obtenerCarta(Arreglo(pila).Count - i).esVisible = visible
            APilas(pila)(APilas(pila).Count - i).Enabled = visible
        Next
    End Sub



    Private Function ObtenerIndices(boton As PictureBox)
        Dim indices(2) As Integer
        indices(0) = (boton.Location.X - 20) / (AnchoCartas + 25)
        indices(1) = (((boton.Location.Y) - 10) / (altoCartas * porcentajeAltoNoVisible))
        Return indices
    End Function

    Private Sub StartDrag(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        Dim b As PictureBox = DirectCast(sender, PictureBox)
        posAnterior = b.Location
        indiceAnterior = ObtenerIndices(b)
        coordenadas.Y = MousePosition.Y - sender.top
        coordenadas.X = MousePosition.X - sender.left
        Dim origen As Pila = Arreglo(indiceAnterior(0))
        Dim carta As Carta = origen.obtenerCarta(indiceAnterior(1))
        If (IsNothing(carta)) Then
            MessageBox.Show("No retorno cartas")
        End If
        cartas = origen.SacarCartas(carta)

        For i = indiceAnterior(1) To APilas(indiceAnterior(0)).Count - 1
            b = APilas(indiceAnterior(0))(i)
            If (IsNothing(cartas) OrElse cartas.esVacia()) Then
                b.ForeColor = Color.Red
                b.Location = posAnterior
                botones.Clear()
                Return
            Else
                botones.Add(b)
                ''APilas(indiceAnterior(0)).Remove(b)
            End If
        Next
    End Sub

    Private Sub Drag(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            sender.top = MousePosition.Y - coordenadas.Y
            sender.left = MousePosition.X - coordenadas.X
            Dim b As PictureBox = DirectCast(sender, PictureBox)
            b.BringToFront()
            Dim y As Integer = 0
            For Each b In botones
                b.Location = New Point(MousePosition.X - coordenadas.X, MousePosition.Y - coordenadas.Y + ((altoCartas * porcentajeAltoNoVisible) * y))
                b.BringToFront()
                y += 1
            Next
        End If
    End Sub

    Private Sub EndDrag(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        Dim b As PictureBox = DirectCast(sender, PictureBox)
        Dim indicesActuales = ObtenerIndices(b)
        Dim origen As Pila = Arreglo(indiceAnterior(0))
        Dim destino As Pila = Arreglo(indicesActuales(0))
        Dim y As Integer = destino.Count
        If (destino.Insert(cartas)) Then
            origen.obtenerCarta(indiceAnterior(1) - 1).esVisible = True
            APilas(indiceAnterior(0))(indiceAnterior(1) - 1).Enabled = True
            b.Location = New Point((25 + AnchoCartas) * indicesActuales(0) + 20, (altoCartas * porcentajeAltoNoVisible) * (y) + 10)
            b.BringToFront()
            Dim i = 0
            For Each b In botones
                Dim jugada = {1, indiceAnterior(0), indicesActuales(0), cartas.elementos(i), b, b.Enabled}
                i += 1
                jugadas.Push(jugada)
                APilas(indiceAnterior(0)).Remove(b)
                APilas(indicesActuales(0)).Add(b)
                b.Location = New Point((25 + AnchoCartas) * indicesActuales(0) + 20, (altoCartas * porcentajeAltoNoVisible) * (y) + 10)
                b.BringToFront()
                y += 1
            Next
        Else
            y = origen.Count
            origen.InserForce(cartas)
            b.Location = posAnterior
            b.ForeColor = Color.Red
            For Each b In botones
                b.Location = New Point(posAnterior.X, (altoCartas * porcentajeAltoNoVisible) * (y) + 10)
                b.ForeColor = Color.Red
                b.BringToFront()
                y += 1
            Next
        End If
        deshabilitar(indicesActuales(0))
        botones.Clear()
        indiceAnterior = indicesActuales
        ''b.Text = ObtenerIndices(b)(0).ToString() + "," + ObtenerIndices(b)(1).ToString()

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_Repartir.Click
        For i = 0 To APilas.Length - 1
            If (mazo.Count > 0) Then
                mazo.obtenerCarta(mazo.Count - 1).esVisible = True
                Dim carta As Carta = mazo.obtenerCarta(mazo.Count - 1)
                mazo.Remove(mazo.obtenerCarta(mazo.Count - 1))
                CreateCarta(i, carta)
            Else
                btn_Repartir.Enabled = False
                Return
            End If
        Next
        btn_Repartir.Enabled = mazo.Count > 0
    End Sub

    Private Sub btn_volver_Click(sender As Object, e As EventArgs) Handles btn_volver.Click
        volver()
    End Sub

End Class
