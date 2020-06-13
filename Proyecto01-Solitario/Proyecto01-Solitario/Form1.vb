Public Class Form1
    Private Arreglo(10) As Pila
    Private APilas(10) As List(Of Button)
    Dim coordenadas As Point
    Dim posAnterior As Point
    Dim indiceAnterior(2) As Integer
    Dim AnchoCartas As Integer = 80
    Dim altoCartas As Integer = 130
    Dim cartas As Pila
    Dim botones As List(Of Button) = New List(Of Button)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Arreglo = {New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila()}
        APilas = {New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button),
            New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button)}
        Panel1.AllowDrop = True
        CreateCarta(0, 6)
        CreateCarta(0, 5)
        CreateCarta(0, 4)
        CreateCarta(0, 3)
        CreateCarta(0, 2)


        CreateCarta(1, 5)
        CreateCarta(1, 4)
        CreateCarta(1, 3)
        CreateCarta(1, 2)
        CreateCarta(1, 1)

        CreateCarta(2, 9)
        CreateCarta(2, 8)
        CreateCarta(2, 7)
        CreateCarta(2, 6)
        CreateCarta(2, 5)
        ''RepartirCartas()
    End Sub

    Private Sub RepartirCartas()
        Dim cartasRepartidas = 0
        Dim familia As Familia = New Familia("Diamante", "Rojo")
        Dim Carta As Carta = New Carta(1, "A", familia, False)
        Dim creadas = 1
        While cartasRepartidas < 15

            For index = 0 To Arreglo.Count - 1
                If (creadas < 13) Then
                    Carta = New Carta(Carta.numero + 1, "A", familia, False)
                ElseIf (creadas < 26) Then
                    familia = New Familia("Treboles", "Negro")
                    Carta = New Carta(Carta.numero + 1, "A", familia, False)
                ElseIf (creadas < 39) Then
                    familia = New Familia("Bastos", "Negro")
                    Carta = New Carta(Carta.numero + 1, "A", familia, False)
                ElseIf (creadas < 54) Then
                    familia = New Familia("Corazones", "Rojo")
                    Carta = New Carta(Carta.numero + 1, "A", familia, False)
                End If
                cartasRepartidas += 1
                CreateCarta(index, 0)
            Next
            System.Console.WriteLine("Prueba")
        End While
    End Sub

    Private Sub CreateCarta(pila As Integer, carta As Integer)
        Dim familia As Familia = New Familia("asd", "adf")
        Dim car As Carta = New Carta(carta, "a", familia, True)
        Dim btn As Button = New Button()
        If (Arreglo(pila).InserForce(car)) Then
            btn.Size = New Size(AnchoCartas, altoCartas)
            btn.Location = New Point((19 + AnchoCartas) * pila + 13, (altoCartas * 0.3) * (Arreglo(pila).Count - 1) + 10)
            btn.Text = car.numero.ToString() + car.familia.Nombre
            btn.Cursor = Cursors.Hand

            AddHandler btn.MouseDown, AddressOf StartDrag
            AddHandler btn.MouseMove, AddressOf Drag
            AddHandler btn.MouseUp, AddressOf EndDrag
            Panel1.Controls.Add(btn)
            btn.BringToFront()
            APilas(pila).Add(btn)
        Else
            MessageBox.Show("No se pudo insertar la carta", carta.ToString())
        End If
    End Sub

    Private Function ObtenerIndices(boton As Button)
        Dim indices(2) As Integer
        indices(0) = (boton.Location.X - 13) / (AnchoCartas + 19)
        indices(1) = (((boton.Location.Y) - 10) / (altoCartas * 0.3))
        Return indices
    End Function

    Private Sub StartDrag(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        Dim b As Button = DirectCast(sender, Button)
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
            Dim b As Button = DirectCast(sender, Button)
            b.BringToFront()
            Dim y As Integer = 0
            For Each b In botones
                b.Location = New Point(MousePosition.X - coordenadas.X, MousePosition.Y - coordenadas.Y + ((altoCartas * 0.3) * y))
                b.BringToFront()
                y += 1
            Next
        End If
    End Sub

    Private Sub EndDrag(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        Dim b As Button = DirectCast(sender, Button)
        Dim indicesActuales = ObtenerIndices(b)
        Dim origen As Pila = Arreglo(indiceAnterior(0))
        Dim destino As Pila = Arreglo(indicesActuales(0))
        Dim y As Integer = destino.Count
        If (destino.Insert(cartas)) Then
            b.Location = New Point((19 + AnchoCartas) * indicesActuales(0) + 13, (altoCartas * 0.3) * (y) + 10)
            b.BringToFront()
            For Each b In botones
                APilas(indiceAnterior(0)).Remove(b)
                APilas(indicesActuales(0)).Add(b)
                b.Location = New Point((19 + AnchoCartas) * indicesActuales(0) + 13, (altoCartas * 0.3) * (y) + 10)
                b.BringToFront()
                y += 1
            Next
        Else
            y = origen.Count
            origen.Insert(cartas)
            b.Location = posAnterior
            b.ForeColor = Color.Red
            For Each b In botones
                b.Location = New Point(posAnterior.X, (altoCartas * 0.3) * (y) + 10)
                b.ForeColor = Color.Red
                b.BringToFront()
                y += 1
            Next
        End If

        botones.Clear()
        indiceAnterior = indicesActuales
        ''b.Text = ObtenerIndices(b)(0).ToString() + "," + ObtenerIndices(b)(1).ToString()

    End Sub


End Class
