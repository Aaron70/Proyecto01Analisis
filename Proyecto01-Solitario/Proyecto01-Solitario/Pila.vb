'===============================================================
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

    Public Property getElementos As List(Of Carta)
        Get
            Return elementos
        End Get
        Set(value As List(Of Carta))
            elementos = value
        End Set
    End Property

    Public Property getCartaMenor As Carta
        Get
            Return cartaMenor
        End Get
        Set(value As Carta)
            cartaMenor = value
        End Set
    End Property

    Public Property getCartaMayor As Carta
        Get
            Return cartaMayor
        End Get
        Set(value As Carta)
            cartaMayor = value
        End Set
    End Property

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
        If ((Not (Me.esVacia Or cartas.esVacia) AndAlso (cartaMenor.numero <= cartas.cartaMayor.numero Or cartaMenor.numero - cartas.cartaMayor.numero <> 1))) Then
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

    Public Function InserForce(carta As Carta)
        Dim inserto = True
        If (Not esVacia() AndAlso cartaMenor.numero <= carta.numero) Then
            'Entra cuando la carta menor de la pila actual es menor a la carta entrante
            inserto = False
        End If
        If (Me.esVacia) Then
            cartaMayor = carta
        End If
        '' carta.esVisible = inserto
        elementos.Add(carta)
        cartaMenor = carta

        Return inserto
    End Function

    Public Function InserForce(cartas As Pila)
        Dim inserto = True
        If (Not esVacia() AndAlso cartaMenor.numero <= cartas.cartaMayor.numero) Then
            'Entra cuando la carta menor de la pila actual es menor a la carta entrante
            inserto = False
        End If
        If (Me.esVacia) Then
            cartaMayor = cartas.cartaMayor
        End If
        For Each carta In cartas.elementos
            elementos.Add(carta)
        Next
        cartaMenor = cartas.cartaMenor
        Return True
        Return inserto
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
                    If (Not elementos(ant).familia.Nombre.Equals(elementos(i).familia.Nombre) Or elementos(ant).numero - elementos(i).numero <> 1) Then
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

    Public Function obtenerCarta(indice) As Carta
        If (indice <= elementos.Count - 1 And indice > -1) Then
            Return elementos(indice)
        End If
        Return Nothing
    End Function


    Public Sub MostrarCartas()
        Console.WriteLine("Cartas: ")
        For Each carta As Carta In elementos
            Console.WriteLine(" Numero: " + carta.numero.ToString() + "  Familia:")
        Next
    End Sub

    Public Sub Remove(carta As Carta)
        elementos.Remove(carta)
        If (esVacia()) Then
            cartaMenor = Nothing
            cartaMayor = Nothing
        Else
            cartaMenor = elementos(elementos.Count - 1)
            cartaMayor = elementos(0)
        End If
    End Sub

    Public Function validarComlumnaKAS(cartas As Pila)
        If (Me.Count + cartas.Count >= 13) Then
            If (cartas.obtenerCarta(cartas.Count - 1).numero = 1) Then
                Dim temp = cartaMenor
                Dim ant = cartas.cartaMayor
                For i = 1 To Count()
                    If (Not temp.familia.Nombre.Equals(ant.familia.Nombre) Or temp.numero - ant.numero <> 1) Then
                        Return -1
                    ElseIf (temp.numero = 13) Then
                        Return Count() - i
                    Else
                        ant = temp
                        temp = elementos(elementos.Count - (i + 1))
                    End If
                Next
                If (temp.numero = 13) Then
                    Return 0
                End If
            End If
        End If
        Return -1
    End Function

End Class
