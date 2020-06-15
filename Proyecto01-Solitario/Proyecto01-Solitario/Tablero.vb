Public Class Tablero
    Private pilas(10) As Pila
    Private botones(10) As List(Of Button)
    Private indicesAnteriores(2) As Integer
    Private cartasSeleccionadas As Pila
    Private botonesSeleccionados As List(Of Button)
    Private mazo As Pila
    Private registro As Stack
    Private random As Random
    Private coordenadas As Point


    Private ANCHOCARTAS As Integer = 105
    Private LARGOCARTAS As Integer = 145
    Private PORCENTAJELARGOVISIBLE As Decimal = 0.3
    Private PORCENTAJELARGONOVISIBLE As Decimal = 0.15

    Private Sub Tablero_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        panel_contenedor.AllowDrop = True
        Me.WindowState = FormWindowState.Maximized

        pilas = {New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila()}
        botones = {New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button),
            New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button), New List(Of Button)}
        registro = New Stack()
        random = New Random(Now.Millisecond)
        coordenadas = New Point()
        botonesSeleccionados = New List(Of Button)
        cartasSeleccionadas = New Pila()

        Dim Mazo1 As Mazo = New Mazo()
        Dim Mazo2 As Mazo = New Mazo()
        Mazo1.barajarCartas()
        Mazo2.barajarCartas()
        UnirMazos(Mazo1, Mazo2)

        RepartirCartas()
        registro.Clear()
    End Sub

    Private Function calcularPosicion(pila, pos) As Point
        Return New Point((25 + ANCHOCARTAS) * pila + 20, (LARGOCARTAS * PORCENTAJELARGONOVISIBLE) * (pos) + 10)
    End Function

    Private Function GenerarNumeroRandom(Min As Integer, Max As Integer) As Integer 'genera un numero random
        Return random.Next(Min, Max + 1)
    End Function

    Private Function ObtenerIndices(boton As Button)
        Dim indices(2) As Integer
        indices(0) = (boton.Location.X - 20) / (ANCHOCARTAS + 25)
        indices(1) = (((boton.Location.Y) - 10) / (LARGOCARTAS * PORCENTAJELARGONOVISIBLE))
        Return indices
    End Function

    Private Sub UnirMazos(Mazo1 As Mazo, Mazo2 As Mazo)
        While Mazo2.ListaCartas.Count > 0 'Insertamos en posiciones aleatorias cartas aleatorias de las parejas formadas anteriormente'
            Dim pos = GenerarNumeroRandom(0, Mazo1.ListaCartas.Count - 1)
            Dim pos2 = GenerarNumeroRandom(0, Mazo2.ListaCartas.Count - 1)
            Mazo1.ListaCartas.Insert(pos, Mazo2.ListaCartas(pos2))
            Mazo2.ListaCartas.Remove(Mazo2.ListaCartas(pos2))
        End While
        mazo = New Pila(Mazo1.ListaCartas)
    End Sub

    Private Function obtenerCarta(pila, posicion) As Object()
        Dim carta As Carta = pilas(pila).obtenerCarta(posicion)
        Dim boton As Button = botones(pila)(posicion)
        Return {carta, boton}
    End Function

    Private Sub deshabilitar(pila As Integer)
        Dim visible = True
        Dim pareja() = obtenerCarta(pila, pilas(pila).Count - 1)
        Dim carta As Carta = pareja(0)
        Dim boton As Button = pareja(1)
        carta.esVisible = True
        boton.Enabled = True
        For i = 2 To pilas(pila).Count
            pareja = obtenerCarta(pila, pilas(pila).Count - i)
            Dim carta2 As Carta = pareja(0)
            Dim boton2 As Button = pareja(1)
            If (Not carta.familia.Nombre.Equals(pilas(pila).obtenerCarta(pilas(pila).Count - i).familia.Nombre) Or Not carta2.numero - carta.numero = 1) Then
                visible = False
            End If
            carta2.esVisible = visible
            boton2.Enabled = visible
            boton2.SendToBack()
            carta = carta2
        Next
    End Sub

    Private Sub CreateCarta(pila As Integer, carta As Carta)

        Dim btn As Button = New Button()
        pilas(pila).InserForce(carta)
        btn.Size = New Size(ANCHOCARTAS, LARGOCARTAS)
        btn.Location = calcularPosicion(pila, pilas(pila).Count - 1)
        btn.Text = carta.numero.ToString() + carta.familia.Nombre
        btn.Cursor = Cursors.Hand

        botones(pila).Add(btn)
        deshabilitar(pila)
        Dim jugada() = {0, pila, carta, btn} '{tipo,destino,carta,boton} //cuando es de tipo 0 es porque viene del mazo y no tiene origen
        registro.Push(jugada)

        AddHandler btn.MouseDown, AddressOf StartDrag
        AddHandler btn.MouseMove, AddressOf Drag
        AddHandler btn.MouseUp, AddressOf EndDrag
        panel_contenedor.Controls.Add(btn)
        btn.BringToFront()
    End Sub


    Private Sub RepartirCartas()
        Dim contador = 44
        While contador > 0
            For i = 0 To pilas.Length - 1
                If (Not contador > 0) Then
                    Exit For
                End If
                Dim carta As Carta = mazo.obtenerCarta(mazo.Count - 1)
                mazo.Remove(mazo.obtenerCarta(mazo.Count - 1))
                CreateCarta(i, carta)
                contador -= 1
            Next
        End While
        For i = 0 To pilas.Length - 1
            ''mazo.obtenerCarta(mazo.Count - 1).esVisible = True
            Dim carta As Carta = mazo.obtenerCarta(mazo.Count - 1)
            mazo.Remove(mazo.obtenerCarta(mazo.Count - 1))
            CreateCarta(i, carta)
            deshabilitar(i)
        Next
    End Sub

    Private Function volver()
        If (registro.Count > 0) Then
            Dim jugada = registro.Pop()

            If (jugada(0) = 0) Then ' {tipo,destino,carta,boton} //Inserccion desde el mazo
                Dim btn As Button = jugada(3)
                Dim carta As Carta = jugada(2)
                pilas(jugada(1)).Remove(carta)
                panel_contenedor.Controls.Remove(btn)
                botones(jugada(1)).Remove(btn)
                btn.Dispose()
                mazo.InserForce(carta)
                btn_Repartir.Enabled = mazo.Count > 0
                deshabilitar(jugada(1))

            ElseIf (jugada(0) = 1) Then ' {tipo,origen,destino,carta,boton,visible} //Inserccion desde el mazo
                Dim carta As Carta = jugada(3)
                Dim btn As Button = jugada(4)
                pilas(jugada(1)).InserForce(carta)
                pilas(jugada(2)).Remove(carta)
                botones(jugada(1)).Add(btn)
                botones(jugada(2)).Remove(btn)
                ''btn.Enabled = jugada(5)
                btn.Location = calcularPosicion(jugada(1), pilas(jugada(1)).Count - 1)
                deshabilitar(jugada(1))
                deshabilitar(jugada(2))
            ElseIf (jugada(0) = 2) Then '{tipo,cantidad de push} //el tipo dos indica un conjunto de instrucciones
                For i = 0 To jugada(1)
                    volver()
                Next


            End If
        End If
        Return True
    End Function



    Private Sub StartDrag(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        Dim b As Button = DirectCast(sender, Button)
        indicesAnteriores = ObtenerIndices(b)
        coordenadas.Y = MousePosition.Y - sender.top
        coordenadas.X = MousePosition.X - sender.left
        Dim origen As Pila = pilas(indicesAnteriores(0))
        Dim pareja() = obtenerCarta(indicesAnteriores(0), indicesAnteriores(1))
        Dim carta As Carta = pareja(0)
        b = pareja(1)
        b.Text = carta.simbolo

        If (IsNothing(carta)) Then
            MessageBox.Show("No retorno cartas")
        End If
        cartasSeleccionadas = origen.SacarCartas(carta)

        For i = indicesAnteriores(1) To botones(indicesAnteriores(0)).Count - 1
            b = botones(indicesAnteriores(0))(i)
            If (IsNothing(cartasSeleccionadas) OrElse cartasSeleccionadas.esVacia()) Then
                b.ForeColor = Color.Red
                b.Location = calcularPosicion(indicesAnteriores(0), indicesAnteriores(1))
                botonesSeleccionados.Clear()
                Return
            Else
                botonesSeleccionados.Add(b)
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
            For Each b In botonesSeleccionados
                b.Location = New Point(MousePosition.X - coordenadas.X, MousePosition.Y - coordenadas.Y + ((LARGOCARTAS * PORCENTAJELARGONOVISIBLE) * y))
                b.BringToFront()
                y += 1
            Next
        End If
    End Sub

    Private Sub EndDrag(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        Dim b As Button = DirectCast(sender, Button)
        Dim indicesActuales = ObtenerIndices(b)
        Dim origen As Pila = pilas(indicesAnteriores(0))
        Dim destino As Pila = pilas(indicesActuales(0))
        Dim y As Integer = destino.Count
        If (destino.Insert(cartasSeleccionadas)) Then
            b.Location = calcularPosicion(indicesActuales(0), y)

            b.BringToFront()
            Dim i = 0
            For Each b In botonesSeleccionados
                Dim jugada = {1, indicesAnteriores(0), indicesActuales(0), cartasSeleccionadas.elementos(i), b, b.Enabled}
                i += 1
                registro.Push(jugada)
                botones(indicesAnteriores(0)).Remove(b)
                botones(indicesActuales(0)).Add(b)
                b.Location = calcularPosicion(indicesActuales(0), y)
                b.BringToFront()
                y += 1
            Next
        Else
            y = origen.Count
            origen.InserForce(cartasSeleccionadas)
            b.Location = calcularPosicion(indicesAnteriores(0), indicesAnteriores(1))
            b.ForeColor = Color.Red
            For Each b In botonesSeleccionados
                b.Location = calcularPosicion(indicesAnteriores(0), y)
                b.ForeColor = Color.Red
                b.BringToFront()
                y += 1
            Next
        End If
        deshabilitar(indicesActuales(0))
        deshabilitar(indicesAnteriores(0))
        botonesSeleccionados.Clear()
    End Sub

    Private Sub panel_contenedor_Paint(sender As Object, e As PaintEventArgs) Handles panel_contenedor.Paint

    End Sub

    Private Sub btn_Repartir_Click(sender As Object, e As EventArgs) Handles btn_Repartir.Click
        For i = 0 To pilas.Length - 1
            If (mazo.Count > 0) Then
                Dim carta As Carta = mazo.obtenerCarta(mazo.Count - 1)
                mazo.Remove(mazo.obtenerCarta(mazo.Count - 1))
                CreateCarta(i, carta)
            Else
                btn_Repartir.Enabled = False
                Return
            End If
        Next
        Dim jugada() = {2, pilas.Length - 1}
        registro.Push(jugada)
        btn_Repartir.Enabled = mazo.Count > 0
    End Sub

    Private Sub btn_atras_Click(sender As Object, e As EventArgs) Handles btn_atras.Click
        volver()
    End Sub
End Class