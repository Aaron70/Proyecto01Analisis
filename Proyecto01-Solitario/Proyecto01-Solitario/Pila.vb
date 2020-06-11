Public Class Pila
    Private vacia As Boolean
    Private elementos As List(Of Carta) = New List(Of Carta)
    Private cartaMenor As Carta 'La primer carta de la pila es la menor (la que esta en el top de la pila)
    Private cartaMayor As Carta 'La ultima carta de la pila es mayor (la que esta en el fondo de la pila)
    'En una totalmente llena el K seria el numero mayor y habria sido el primero en entrar y el As '
    Sub New()
        vacia = True
        cartaMenor = Nothing
        cartaMayor = Nothing
    End Sub

    Sub New(nuevosElementos As List(Of Carta))
        elementos = nuevosElementos
        cartaMayor = nuevosElementos(0)
        cartaMenor = nuevosElementos(nuevosElementos.Count - 1)
        vacia = False 'La pila no inicia vacia'
    End Sub

    Public Property esVacia As Boolean
        Get
            Return vacia
        End Get
        Set(value As Boolean)
            vacia = value
        End Set
    End Property


    Public Function Insert(cartas As Pila) As Boolean
        If ((Not Me.esVacia And Not cartas.esVacia) AndAlso (cartaMenor.numero < +cartas.cartaMayor.numero)) Then
            'Entra cuando la carta menor de la pila actual es menor a la carta mayor de la pila entrante
            Return False
        End If
        If (Me.esVacia) Then
            cartaMayor = cartas.cartaMayor
        End If
        elementos.Concat(cartas.elementos)
        cartaMenor = cartas.cartaMenor
        vacia = False
        Return True
    End Function

    Public Function SacarCartas(carta As Carta) As Pila
        If (Not Me.esVacia) Then
            Dim index = elementos.IndexOf(carta)
            Dim nuevaLista As List(Of Carta) = New List(Of Carta)
            If (index > 0) Then
                For i = index To elementos.Count - 1
                    nuevaLista.Add(elementos(i))
                    elementos.Remove(elementos(i))
                Next

            End If
        End If
        Return Nothing
    End Function

End Class
