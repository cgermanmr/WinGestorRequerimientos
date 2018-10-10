<Serializable>
Public Class STreeNode

#Region "Propiedades"
    Private _text As String
    Public Property Text() As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property

    Private _tag As RequerimientoDTO()
    Public Property Tag() As RequerimientoDTO()
        Get
            Return _tag
        End Get
        Set(ByVal value As RequerimientoDTO())
            _tag = value
        End Set
    End Property

#End Region

End Class
