Public Class Usuario

    Sub New()

    End Sub

    Sub New(ByVal nombre As String, ByVal clave As String)
        Me.Nombre = nombre
        Me.Clave = clave
    End Sub

    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _clave As String
    Public Property Clave() As String
        Get
            Return _clave
        End Get
        Set(ByVal value As String)
            _clave = value
        End Set
    End Property


End Class
