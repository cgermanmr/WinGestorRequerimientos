<Serializable> Public Class Campo

#Region "Constructores"

    Sub New()

    End Sub

    Sub New(ByVal nombre As String, ByVal dato As String)
        Me.NombreCampo = nombre
        Me.DatoCampo = dato
    End Sub
#End Region

#Region "Propiedades"
    Private _nombre As String
    Public Property NombreCampo() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _dato As String
    Public Property DatoCampo() As String
        Get
            Return _dato
        End Get
        Set(ByVal value As String)
            _dato = value
        End Set
    End Property
#End Region


End Class