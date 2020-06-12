Public Class Form1
    Private Arreglo(10) As Pila
    Private APilas(10) As List(Of Button)
    Dim coordenadas As Point
    Dim posAnterior As Point
    Dim indiceAnterior(2) As Integer
    Dim AnchoCartas As Integer = 80
    Dim altoCartas As Integer = 130
    Dim cartas As Pila
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Arreglo = {New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila()}
        APilas = {New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button),
            New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button)}
        Panel1.AllowDrop = True


        CreateCarta(0, 3)
        CreateCarta(0, 2)
        CreateCarta(0, 1)

        CreateCarta(1, 6)
        CreateCarta(1, 5)
        CreateCarta(1, 4)
        CreateCarta(1, 3)
        CreateCarta(1, 2)

        CreateCarta(2, 6)
        CreateCarta(2, 5)
        CreateCarta(2, 4)
    End Sub

    Private Sub CreateCarta(pila, carta)
        Dim familia As Familia = New Familia("Diamantes", "Rojo")
        Dim car As Carta = New Carta(carta, "A", familia, True)
        Dim btn As Button = New Button()
        If (Arreglo(pila).Insert(car)) Then
            btn.Size = New Size(AnchoCartas, altoCartas)
            btn.Location = New Point((19 + AnchoCartas) * pila + 13, (altoCartas * 0.3) * (Arreglo(pila).Count - 1) + 10)
            btn.Text = carta.ToString()
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

    End Sub

    Private Sub Drag(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            sender.top = MousePosition.Y - coordenadas.Y
            sender.left = MousePosition.X - coordenadas.X
            Dim b As Button = DirectCast(sender, Button)
            Dim p As Point = New Point(MousePosition.X - coordenadas.X, MousePosition.Y - coordenadas.Y)
            b.BringToFront()
            Dim btn As Button
            ''APilas(0).Controls.Item(1).Location = New Point(MousePosition.X - coordenadas.X, MousePosition.Y - coordenadas.Y + 20)
            For i = indiceAnterior(1) + 1 To APilas(indiceAnterior(0)).Count - 1
                btn = APilas(indiceAnterior(0))(i)
                btn.Location = New Point(MousePosition.X - coordenadas.X, MousePosition.Y - coordenadas.Y + 20)
                btn.BringToFront()
            Next
        End If
    End Sub

    Private Sub EndDrag(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        Dim b As Button = DirectCast(sender, Button)
        Dim indicesActuales = ObtenerIndices(b)
        Dim origen As Pila = Arreglo(indiceAnterior(0))
        Dim destino As Pila = Arreglo(indicesActuales(0))
        If (destino.Insert(cartas)) Then
            b.Location = New Point((19 + AnchoCartas) * indicesActuales(0) + 13, (altoCartas * 0.3) * (destino.Count - 1) + 10)
            b.BringToFront()
        Else
            origen.Insert(cartas)
            b.Location = posAnterior
            b.ForeColor = Color.Red
        End If
        ''b.Text = ObtenerIndices(b)(0).ToString() + "," + ObtenerIndices(b)(1).ToString()

    End Sub


End Class
