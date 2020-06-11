Public Class Pila
    Private vacia As Boolean
    Private elementos As List(Of Carta)
    Private primerElemento As Carta 'El primer numero de la pila es el menor'
    Private ultimoElemento As Carta 'El ultimo numero de la pila es el mayor'
    'En una totalmente llena el K seria el numero mayor y habria sido el primero en entrar y el As '
    Sub New()
        elementos = New List(Of Carta) 'Inicializamos el arreglo'
        vacia = True 'La pila inicia vacia'
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
        If (ultimoElemento.numero > cartas.ultimoElemento.numero) Then 'Validamos que el numero mayor de la pila de entrada sea menor al menor de la pila en la que se desa insertar '
            elementos.Concat(cartas.elementos)
            vacia = False
        End If
        Return False
    End Function

End Class
