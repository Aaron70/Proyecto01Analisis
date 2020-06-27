Imports System.ComponentModel
Imports System.IO

Public Class Tablero
    Private pilas(9) As Pila
    Private botones(9) As List(Of PictureBox)
    Private indicesAnteriores(2) As Integer
    Private cartasSeleccionadas As Pila
    Private botonesSeleccionados As List(Of PictureBox)
    Private mazo As Pila
    Private mazoSeleccionado As Integer = 1
    Private mazoAnt As Integer = 0
    Private registro As Stack
    Private random As Random
    Private coordenadas As Point
    Private imagenVolteada As String = Path.Combine(Environment.CurrentDirectory, "..\..\Cartas\volteada.jpg")
    Private faltan As Integer = 8
    Private jugando As Boolean = False
    Private puntos As Integer = 500
    Private botonesRepartir As List(Of PictureBox) = New List(Of PictureBox)

    Private ANCHOCARTAS As Integer = 105
    Private LARGOCARTAS As Integer = 145
    Private PORCENTAJELARGOVISIBLE As Decimal = 0.17
    Private PORCENTAJELARGONOVISIBLE As Decimal = 0.15
    Private OFFSETX As Integer = 70
    Private OFFSETY As Integer = 160

    Private Sub Tablero_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        panel_contenedor.AllowDrop = True
        Me.WindowState = FormWindowState.Maximized

        'Inicializa las variables'
        random = New Random(Now.Millisecond)
        panel_contenedor.Controls.Clear()
        pilas = {New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila()}
        botones = {New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox),
            New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox)}
        registro = New Stack()
        coordenadas = New Point()
        botonesSeleccionados = New List(Of PictureBox)
        cartasSeleccionadas = New Pila()

        'Hace que se repartan las cartas en funcion del mazo elegido'
        CambiarMazo()
    End Sub



    Private Function copiarListas(ByVal original As List(Of Carta), ByVal copia As List(Of Carta))
        Dim cant = original.Count - 1
        Dim orgCopy = original.ToArray().Clone()
        Dim copy(cant) As Carta

        For ind = 0 To cant
            copy(ind) = orgCopy(ind)
        Next
        Return copy.ToList()
    End Function

    Private Sub CambiarMazo()
        If (Not IsNothing(pilas) And Not IsNothing(pilas(0))) Then
            panel_contenedor.Controls.Clear()
            colocarCartasRepartir()


            pilas = {New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila(), New Pila()}
            botones = {New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox),
            New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox), New List(Of PictureBox)}
            mazo = New Pila()

            'Se selecciona el mazo y se reparen las cartas'
            mazo.getElementos = copiarListas(Barajas.reparticiones(mazoSeleccionado - 1).getElementos, mazo.getElementos)
            RepartirCartas()
            registro.Clear()
            controlJuego(False)
            controlIndicadores(True)
        End If
    End Sub

    Private Sub colocarCartasRepartir()
        For i = 0 To 4
            crearBotonRepartir(i)
        Next
    End Sub

    Private Sub crearBotonRepartir(i)
        Dim btn As PictureBox
        btn = New PictureBox()
        btn.Image = Image.FromFile(imagenVolteada)
        btn.Size = New Size(ANCHOCARTAS, LARGOCARTAS)
        btn.SizeMode = PictureBoxSizeMode.StretchImage
        btn.Location = New Point((5) * i + 5, 5)
        AddHandler btn.Click, AddressOf btn_Repartir_Click
        botonesRepartir.Add(btn)
        btn.BringToFront()
        panel_contenedor.Controls.Add(btn)
        btn.BringToFront()
    End Sub

    Private Sub controlJuego(flag)
        btn_atras.Enabled = flag
        btn_atras.Visible = flag
        panel_contenedor.Controls.Add(btn_atras)

        If (flag) Then panel_contenedor.Controls.Add(Puntaje)

        For i = 0 To botones.Length - 1
            For j = 0 To botones(i).Count - 1
                botones(i)(j).Enabled = flag
            Next
        Next

        For i = 0 To botonesRepartir.Count - 1
            botonesRepartir(i).Enabled = flag
        Next

    End Sub

    Private Function voltearCarta(pos)
        Dim pila As Pila = pilas(pos)
        Dim carta As Carta = pila.obtenerCarta(pila.Count - 1)

        If (pila.Count > 0 AndAlso Not carta.esVisible) Then
            Dim boton As PictureBox = botones(pos)(botones(pos).Count - 1)
            carta.esVisible = True
            carta.esMobilbe = True
            boton.Enabled = True
            boton.Image = Image.FromFile(carta.imagen)
            boton.SizeMode = PictureBoxSizeMode.StretchImage
            Return False
        End If

        Return True
    End Function

    Private Sub AgarrarCartas(posPila As Integer, Optional posFila As Integer = -1)
        Dim origen As Pila = pilas(posPila)
        If (posFila = -1) Then posFila = origen.Count - 1
        Dim cartaABuscar = origen.obtenerCarta(posFila)
        Dim listaCartas As Pila = origen.SacarCartas(cartaABuscar)
        Dim listaBotones As List(Of PictureBox) = New List(Of PictureBox)
        'Selecciona y saca de la pila las cartas y botones'
        For i = posFila To listaCartas.Count - 1
            origen.Remove(listaCartas.obtenerCarta(i))
            listaBotones.Add(botones(posPila)(i))
            botones(posPila).Remove(botones(posPila)(i))
        Next

        cartasSeleccionadas = listaCartas
        botonesSeleccionados = listaBotones
    End Sub

    Private Function ColocarCartas(pos, Optional force = False, Optional pila = True)
        Dim destinoCartas = pilas(pos)
        Dim destinoBotones = botones(pos)
        Dim posK As Integer = destinoCartas.validarComlumnaKAS(cartasSeleccionadas) 'Valida si ya se completo una columna'
        Dim visibleOrigen = True
        Dim visibleDestino = True
        Dim carta As Carta
        Dim boton As PictureBox
        Dim y = destinoCartas.Count

        If (posK <> -1) Then 'Validar si al insertar estoy completando una columna'
            Dim backupCartas As Pila = destinoCartas.SacarCartas(destinoCartas.obtenerCarta(posK))
            Dim backupBotones As List(Of PictureBox) = New List(Of PictureBox)
            Dim backupCartasOrigen As Pila = New Pila()
            Dim backupBotonesOrigen As List(Of PictureBox) = New List(Of PictureBox)
            Dim btn As PictureBox

            'Guarda, quita y oculta los botones del destino que ayudan a completar la fila'
            For i = 0 To (backupCartas.Count) - 1
                btn = destinoBotones(posK)
                btn.Visible = False
                btn.Enabled = False
                destinoBotones.Remove(btn)
                backupBotones.Add(btn)
            Next
            'Oculta los botones que ayudan a completar la fila en el destino'
            For i = 0 To botonesSeleccionados.Count - 1
                botonesSeleccionados(i).Visible = False
                botonesSeleccionados(i).Enabled = False
                backupBotonesOrigen.Add(botonesSeleccionados(i))
                backupCartasOrigen.Insert(cartasSeleccionadas.obtenerCarta(i))
            Next

            visibleDestino = voltearCarta(pos)
            visibleOrigen = voltearCarta(indicesAnteriores(0))
            faltan -= 1
            If (faltan = 0) Then
                MessageBox.Show("Felicidades has ganado!")
                Return False
            End If
            sumarPuntos(100)
            Dim jugada = {4, pos, backupCartas, backupBotones, indicesAnteriores(0), backupCartasOrigen, backupBotonesOrigen, visibleDestino, visibleOrigen}
            If (pila) Then registro.Push(jugada)
        ElseIf (indicesAnteriores(0) <> pos) AndAlso destinoCartas.Insert(cartasSeleccionadas) OrElse force AndAlso destinoCartas.InserForce(cartasSeleccionadas) Then
            Dim activada = voltearCarta(indicesAnteriores(0))
            Dim i = 0
            Dim backupCartas = New Pila()
            Dim backupBotones = New List(Of PictureBox)

            For bt = 0 To botonesSeleccionados.Count - 1
                backupBotones.Add(botonesSeleccionados(bt))
                backupCartas.Insert(cartasSeleccionadas.obtenerCarta(i))
                destinoBotones.Add(botonesSeleccionados(bt))
                botonesSeleccionados(bt).Location = calcularPosicion(pos, y, True)
                botonesSeleccionados(bt).BringToFront()
                botonesSeleccionados(bt).Enabled = True
                botonesSeleccionados(bt).Visible = True
                cartasSeleccionadas.obtenerCarta(i).esMobilbe = True
                cartasSeleccionadas.obtenerCarta(i).esVisible = True
                y += 1
                i += 1
            Next
            Dim jugada = {3, indicesAnteriores(0), backupCartas, backupBotones, activada, pos}
            If (pila) Then registro.Push(jugada)
            restarPuntos()
        Else
            Dim origen = pilas(indicesAnteriores(0))
            y = origen.Count
            origen.InserForce(cartasSeleccionadas)

            For Each b In botonesSeleccionados
                botones(indicesAnteriores(0)).Add(b)
                b.Location = calcularPosicion(indicesAnteriores(0), y)
                ''  b.ForeColor = Color.Red
                b.BringToFront()
                y += 1
            Next

        End If
        botonesSeleccionados.Clear()
        cartasSeleccionadas = New Pila()
        If (botones(pos).Count > 0) Then
            indicesAnteriores = ObtenerIndices(botones(pos)(0))
        Else
            indicesAnteriores = {pos, 0}
        End If
        Return True
    End Function

    Private Sub controlIndicadores(flag)
        btnAnterior.Visible = flag
        btnAnterior.Enabled = flag
        panel_contenedor.Controls.Add(btnAnterior)

        btnSiguiente.Visible = flag
        btnSiguiente.Enabled = flag
        panel_contenedor.Controls.Add(btnSiguiente)

        btnJugar.Visible = flag
        btnJugar.Enabled = flag
        panel_contenedor.Controls.Add(btnJugar)

        lbNumeroTablero.Visible = flag
        panel_contenedor.Controls.Add(lbNumeroTablero)

        nudNumeroTablero.Visible = flag
        panel_contenedor.Controls.Add(nudNumeroTablero)

    End Sub

    Private Function calcularPosicion(pila, pos, Optional flag = False) As Point
        Dim Porcentaje = PORCENTAJELARGONOVISIBLE
        If (pos > 0 AndAlso (pilas(pila).obtenerCarta(pos - 1).esVisible Or Not flag)) Then
            Porcentaje = PORCENTAJELARGOVISIBLE
        End If
        Return New Point((25 + ANCHOCARTAS) * pila + OFFSETX, (LARGOCARTAS * Porcentaje) * (pos) + OFFSETY)
    End Function



    Public Function GenerarNumeroRandom(Min As Integer, Max As Integer) As Integer 'genera un numero random
        If (IsNothing(random)) Then
            random = New Random(Now.Millisecond)
        End If
        Return random.Next(Min, Max + 1)
    End Function

    Private Function ObtenerIndices(boton As PictureBox)
        Dim indices(2) As Integer
        indices(0) = (boton.Location.X - OFFSETX) / (ANCHOCARTAS + 25)
        indices(1) = (((boton.Location.Y) - OFFSETY) / (LARGOCARTAS * PORCENTAJELARGOVISIBLE))
        If (indices(0) >= pilas.Length) Then
            indices(0) = pilas.Length - 1
        End If
        If (indices(0) <= 0) Then
            indices(0) = 0
        End If

        If (indices(1) <= 0) Then
            indices(1) = 0
        End If
        Return indices
    End Function



    Private Function obtenerCarta(pila, posicion) As Object()
        If (pila < 0) Then
            pila = 0
        ElseIf (pila > pilas.Length - 1) Then
            pila = pilas.Length - 1
        End If
        If (posicion < 0) Then
            posicion = 0
        ElseIf (posicion > pilas(pila).Count - 1) Then
            posicion = pilas(pila).Count - 1
        End If
        Dim carta As Carta = pilas(pila).obtenerCarta(posicion)
        Dim boton As PictureBox = botones(pila)(posicion)
        Return {carta, boton}
    End Function

    Private Sub restarPuntos(Optional pts = 1)
        puntos -= pts
        Puntaje.Text = "Puntos: " + puntos.ToString()
        If (puntos <= 0) Then
            MessageBox.Show("Has perdido no te quedan puntos!")
            controlJuego(False)
        End If
    End Sub

    Private Sub sumarPuntos(Optional pts = 1)
        puntos += pts
        Puntaje.Text = "Puntos: " + puntos.ToString()
    End Sub

    Private Function obtenerUltimaCarta(pila) As Object()
        Dim carta As Carta = pilas(pila).obtenerCarta(pilas(pila).Count - 1)
        Dim boton As PictureBox = botones(pila)(botones(pila).Count - 1)
        Return {carta, boton}
    End Function

    Private Sub deshabilitar(pila As Integer)
        Dim visible = True
        Dim pareja() = obtenerUltimaCarta(pila)
        Dim carta As Carta = pareja(0)
        Dim boton As PictureBox = pareja(1)
        carta.esVisible = visible
        carta.esMobilbe = visible
        boton.Enabled = visible

        For i = 2 To pilas(pila).Count 'i inicia en 2 para iniciar desde la penultima carta'
            pareja = obtenerCarta(pila, pilas(pila).Count - i)
            Dim carta2 As Carta = pareja(0)
            Dim boton2 As PictureBox = pareja(1)
            If (Not carta.familia.Nombre.Equals(carta2.familia.Nombre) Or Not carta2.numero - carta.numero = 1) Then
                visible = False
            End If
            carta2.esVisible = visible
            carta2.esMobilbe = visible
            boton2.Enabled = visible
            If carta2.esVisible Then
                boton2.Image = Image.FromFile(carta2.imagen)
                boton2.SizeMode = PictureBoxSizeMode.StretchImage
            Else
                boton2.Image = Image.FromFile(imagenVolteada)
                boton2.SizeMode = PictureBoxSizeMode.StretchImage
            End If
            boton2.SendToBack()
            carta = carta2
        Next
    End Sub

    Private Sub CreateCarta(pila As Integer, carta As Carta)
        Dim btn As PictureBox = New PictureBox()
        pilas(pila).InserForce(carta)
        btn.Size = New Size(ANCHOCARTAS, LARGOCARTAS)
        btn.Location = calcularPosicion(pila, pilas(pila).Count - 1)
        btn.Text = carta.numero.ToString() + carta.familia.Nombre
        btn.Cursor = Cursors.Hand

        'colocando imagenes por carta
        If carta.esVisible Then
            btn.Image = Image.FromFile(carta.imagen)
            btn.SizeMode = PictureBoxSizeMode.StretchImage
        Else
            btn.Image = Image.FromFile(imagenVolteada)
            btn.SizeMode = PictureBoxSizeMode.StretchImage

        End If
        btn.Enabled = carta.esVisible
        botones(pila).Add(btn)
        ''deshabilitar(pila)
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
                carta.esVisible = False
                mazo.Remove(mazo.obtenerCarta(mazo.Count - 1))
                CreateCarta(i, carta)
                contador -= 1
            Next
        End While
        For i = 0 To pilas.Length - 1
            ''mazo.obtenerCarta(mazo.Count - 1).esVisible = True
            Dim carta As Carta = mazo.obtenerCarta(mazo.Count - 1)
            carta.esVisible = True
            carta.esMobilbe = True
            mazo.Remove(mazo.obtenerCarta(mazo.Count - 1))
            CreateCarta(i, carta)
            ''deshabilitar(i)
        Next
    End Sub

    Private Function volver()
        If (registro.Count > 0) Then
            Dim jugada = registro.Pop()
            If (jugada(0) = 0) Then ' {tipo,destino,carta,boton} //Inserccion desde el mazo
                Dim btn As PictureBox = jugada(3)
                Dim carta As Carta = jugada(2)

                pilas(jugada(1)).Remove(carta)
                mazo.InserForce(carta)

                panel_contenedor.Controls.Remove(btn)
                botones(jugada(1)).Remove(btn)
                btn.Dispose()
                ''deshabilitar(jugada(1))

            ElseIf (jugada(0) = 1) Then ' {tipo,origen,destino,carta,boton,visible anterior} //Inserccion desde el mazo
                Dim carta As Carta = jugada(3)
                Dim btn As PictureBox = jugada(4)

                If (Not IsNothing(jugada(5))) Then
                    Dim cartaAnterior = pilas(jugada(1)).obtenerCarta(pilas(jugada(1)).Count - 1)
                    Dim botonAnterior = botones(jugada(1))(botones(jugada(1)).Count - 1)
                    cartaAnterior.esVisible = jugada(5)
                    botonAnterior.Enabled = jugada(5)
                    If pilas(jugada(1)).obtenerCarta(pilas(jugada(1)).Count - 1).esVisible Then
                        botonAnterior.Image = Image.FromFile(cartaAnterior.imagen)
                    Else
                        botonAnterior.Image = Image.FromFile(imagenVolteada)
                        botonAnterior.Enabled = False
                        carta.esMobilbe = False
                    End If
                    botonAnterior.SizeMode = PictureBoxSizeMode.StretchImage
                End If


                pilas(jugada(1)).InserForce(carta)
                pilas(jugada(2)).Remove(carta)
                botones(jugada(1)).Add(btn)
                botones(jugada(2)).Remove(btn)
                btn.BringToFront()
                btn.Location = calcularPosicion(jugada(1), pilas(jugada(1)).Count - 1)

                restarPuntos()
            ElseIf (jugada(0) = 2) Then '{tipo,cantidad de push} //el tipo dos indica un conjunto de instrucciones
                For i = 0 To jugada(1)
                    volver()
                Next
                crearBotonRepartir(botonesRepartir.Count)
                restarPuntos()
            ElseIf (jugada(0) = 3) Then ''{tipo,origen,cartas,botones,acitvado,destino}
                faltan += 1
                Dim destinoCartas = pilas(jugada(5))
                Dim destinoBotones = botones(jugada(5))
                Dim origenCartas = pilas(jugada(1))
                Dim origenBotones = botones(jugada(1))
                cartasSeleccionadas = jugada(2)
                botonesSeleccionados = jugada(3)
                For i = 0 To cartasSeleccionadas.Count - 1
                    destinoCartas.Remove(cartasSeleccionadas.obtenerCarta(i))
                    destinoBotones.Remove(botonesSeleccionados(i))
                Next
                If (origenCartas.Count > 0) Then
                    origenCartas.obtenerCarta(origenCartas.Count - 1).esVisible = jugada(4)
                    origenCartas.obtenerCarta(origenCartas.Count - 1).esMobilbe = jugada(4)
                    origenBotones(origenBotones.Count - 1).Enabled = jugada(4)
                    If (jugada(4)) Then
                        origenBotones(origenBotones.Count - 1).Image = Image.FromFile(origenCartas.obtenerCarta(origenCartas.Count - 1).imagen)
                    Else
                        origenBotones(origenBotones.Count - 1).Image = Image.FromFile(imagenVolteada)

                    End If
                    origenBotones(origenBotones.Count - 1).SizeMode = PictureBoxSizeMode.StretchImage
                End If
                origenCartas.InserForce(jugada(2))
                For Each btn As PictureBox In jugada(3)
                    btn.Location = calcularPosicion(jugada(1), origenBotones.Count)
                    btn.BringToFront()
                    origenBotones.Add(btn)
                Next
                cartasSeleccionadas = New Pila()
                botonesSeleccionados = New List(Of PictureBox)
            ElseIf (jugada(0) = 4) Then ''{4, destino, backupCartas, backupBotones, origen, cartasSeleccionadas, botonesSeleccionados, visibleDestino, visibleOrigen}
                Dim origen As Pila = pilas(jugada(4))
                Dim destino As Pila = pilas(jugada(1))

                If (origen.Count > 0) Then origen.obtenerCarta(origen.Count - 1).esVisible = jugada(8)
                If (destino.Count > 0) Then destino.obtenerCarta(destino.Count - 1).esVisible = jugada(7)

                cartasSeleccionadas = jugada(2)
                botonesSeleccionados = jugada(3)
                ColocarCartas(jugada(1), True, False)
                'Devolviendo las cartas y botones del destino'
                ''For i = 0 To listaCartas.Count - 1
                ''destino.Insert(listaCartas.obtenerCarta(i))
                ''listaBotones(i).Visible = True
                ''listaBotones(i).Enabled = True
                ''botones(jugada(1)).Add(listaBotones(i))
                ''listaBotones(i).Location = calcularPosicion(jugada(1), botones(jugada(1)).Count - 1)
                ''Next

                cartasSeleccionadas = jugada(5)
                botonesSeleccionados = jugada(6)
                ColocarCartas(jugada(4), True, False)
                'Devolviendo las cartas y botones del origen'
                ''For i = 0 To listaCartas.Count - 1
                ''origen.Insert(listaCartas.obtenerCarta(i))
                ''listaBotones(i).Visible = True
                ''listaBotones(i).Enabled = True
                ''listaBotones(i).Location = calcularPosicion(jugada(4), botones(jugada(4)).Count - 1)
                ''botones(jugada(4)).Add(listaBotones(i))
                ''Next
                restarPuntos(99)
            End If
        End If
        Return True
    End Function



    Private Sub StartDrag(sender As Object, e As System.Windows.Forms.MouseEventArgs)

        Dim b As PictureBox = DirectCast(sender, PictureBox)
        If (Not IsNothing(b)) Then
            indicesAnteriores = ObtenerIndices(b)
            coordenadas.Y = MousePosition.Y - sender.top
            coordenadas.X = MousePosition.X - sender.left
            Dim origen As Pila = pilas(indicesAnteriores(0))
            Dim pareja() = obtenerCarta(indicesAnteriores(0), indicesAnteriores(1))
            Dim carta As Carta = pareja(0)
            b = pareja(1)

            If (IsNothing(carta)) Then
                MessageBox.Show("No retorno cartas")
            End If
            cartasSeleccionadas = origen.SacarCartas(carta)
            Dim newPila(botones(indicesAnteriores(0)).Count - 1) As PictureBox
            botones(indicesAnteriores(0)).CopyTo(newPila)
            Dim temp As List(Of PictureBox) = newPila.ToList()
            If (Not cartasSeleccionadas.esVacia) Then
                For i = indicesAnteriores(1) To botones(indicesAnteriores(0)).Count - 1
                    b = botones(indicesAnteriores(0))(i)
                    If (IsNothing(cartasSeleccionadas) OrElse cartasSeleccionadas.esVacia()) Then
                        b.ForeColor = Color.Red
                        b.Location = calcularPosicion(indicesAnteriores(0), indicesAnteriores(1))
                        botonesSeleccionados.Clear()
                        Return
                    Else
                        temp.Remove(b)
                        botonesSeleccionados.Add(b)
                    End If
                Next
                botones(indicesAnteriores(0)) = temp
            End If
        End If
    End Sub

    Private Sub Drag(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left And botonesSeleccionados.Count > 0 Then
            sender.top = MousePosition.Y - coordenadas.Y
            sender.left = MousePosition.X - coordenadas.X
            Dim b As PictureBox = DirectCast(sender, PictureBox)
            Dim y As Integer = 0
            Label1.Text = ObtenerIndices(b)(0).ToString()
            panel_contenedor.Controls.Add(Label1)
            Label2.Text = ObtenerIndices(b)(1).ToString()
            panel_contenedor.Controls.Add(Label2)

            b.BringToFront()
            For Each b In botonesSeleccionados
                b.Location = New Point(MousePosition.X - coordenadas.X, MousePosition.Y - coordenadas.Y + ((LARGOCARTAS * PORCENTAJELARGOVISIBLE) * y))
                b.BringToFront()
                y += 1
            Next
        End If
    End Sub

    Private Sub EndDrag(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        If (botonesSeleccionados.Count > 0) Then
            Dim b As PictureBox = DirectCast(sender, PictureBox)
            Dim indicesActuales = ObtenerIndices(b)
            ColocarCartas(indicesActuales(0))
        Else

        End If
    End Sub

    Private Sub panel_contenedor_Paint(sender As Object, e As PaintEventArgs) Handles panel_contenedor.Paint

    End Sub

    Private Sub btn_Repartir_Click(sender As Object, e As EventArgs)
        Dim b As PictureBox = DirectCast(sender, PictureBox)
        b.Dispose()
        botonesRepartir.Remove(b)
        panel_contenedor.Controls.Remove(b)
        For i = 0 To pilas.Length - 1
            If (mazo.Count > 0) Then
                Dim carta As Carta = mazo.obtenerCarta(mazo.Count - 1)
                carta.esMobilbe = True
                mazo.Remove(mazo.obtenerCarta(mazo.Count - 1))
                carta.esVisible = True
                CreateCarta(i, carta)
            End If
        Next
        Dim jugada() = {2, pilas.Length - 1}
        registro.Push(jugada)
        restarPuntos()
    End Sub

    Private Sub btn_atras_Click(sender As Object, e As EventArgs) Handles btn_atras.Click
        volver()
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        nudNumeroTablero.Value += 1
    End Sub

    Private Sub nudNumeroTablero_ValueChanged(sender As Object, e As EventArgs) Handles nudNumeroTablero.ValueChanged
        If (nudNumeroTablero.Value > Barajas.reparticiones.Count) Then
            nudNumeroTablero.Value = 1
        End If
        If (nudNumeroTablero.Value < 1) Then
            nudNumeroTablero.Value = Barajas.reparticiones.Count
        End If
        mazoSeleccionado = nudNumeroTablero.Value
        CambiarMazo()
    End Sub

    Private Sub btnJugar_Click(sender As Object, e As EventArgs) Handles btnJugar.Click
        For i = 0 To pilas.Length - 1
            For j = 0 To pilas(i).Count - 1
                If (pilas(i).obtenerCarta(j).esVisible) Then
                    botones(i)(j).Enabled = True
                End If
            Next
        Next
        controlJuego(True)
        controlIndicadores(False)
    End Sub

    Private Sub Tablero_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Main.Visible = True
        Barajas.reparticiones = Nothing
        Barajas.NumeroBarajas.Value = 1
        Barajas.RBunaFamilia.Checked = True
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        nudNumeroTablero.Value -= 1
    End Sub
End Class