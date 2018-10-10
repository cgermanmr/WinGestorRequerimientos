Public Class DatosFiltro
    'indice_inf=0&indice_sup=30&ordenarpor=2&orden=1&ver_campo1=on&
    'solicitante_id=idawidowski&ver_campo3=on&
    'proceso_id=F0203-AB.SUBUSUARIO.LOGN&ver_campo4=on&
    'estado_job=abierto&ver_campo5=on&
    'instancia_id=Ejecutar+tarea&ver_campo6=on&
    'usuario=gmedina&
    'otrs_id=12345&
    'buscar_texto=prueba-texto

    Private _indiceInf As Integer = 0
    Public Property IndiceInf() As Integer
        Get
            Return _indiceInf
        End Get
        Set(ByVal value As Integer)
            _indiceInf = value
        End Set
    End Property

    Private _indiceSup As Integer = 30
    Public Property IndiceSup() As Integer
        Get
            Return _indiceSup
        End Get
        Set(ByVal value As Integer)
            _indiceSup = value
        End Set
    End Property

    Private _solicitanteId As String
    Public Property SolicitanteId() As String
        Get
            Return _solicitanteId
        End Get
        Set(ByVal value As String)
            _solicitanteId = value
        End Set
    End Property

    Private _procesoId As String
    Public Property ProcesoId() As String
        Get
            Return _procesoId
        End Get
        Set(ByVal value As String)
            _procesoId = value
        End Set
    End Property

    Private _estadoJob As String = "abierto"
    Public Property EstadoJob() As String
        Get
            Return _estadoJob
        End Get
        Set(ByVal value As String)
            _estadoJob = value
        End Set
    End Property

    Private _instanciaId As String
    Public Property InstanciaId() As String
        Get
            Return _instanciaId
        End Get
        Set(ByVal value As String)
            _instanciaId = value
        End Set
    End Property
    Private _usuario As String
    Public Property Usuario() As String
        Get
            If String.IsNullOrWhiteSpace(_usuario) Then
                Return SesionActualWindows.SesionActual.ObtenerUsuarioActual.Nombre
            Else
                Return _usuario
            End If

        End Get
        Set(ByVal value As String)
            _usuario = value
        End Set
    End Property

    Private _otrsId As String
    Public Property OtrsId() As String
        Get
            Return _otrsId
        End Get
        Set(ByVal value As String)
            _otrsId = value
        End Set
    End Property

    Private _buscarTexto As String
    Public Property BuscarTexto() As String
        Get
            Return _buscarTexto
        End Get
        Set(ByVal value As String)
            _buscarTexto = value
        End Set
    End Property


    'indice_inf=0&indice_sup=30&ordenarpor=6&orden=1&ver_campo1=on&solicitante_id=&ver_campo3=on&proceso_id=&ver_campo4=on&estado_job=abierto&ver_campo5=on&instancia_id=&ver_campo6=on&usuario=gmedina&otrs_id=&buscar_texto=

    Public Overrides Function ToString() As String
        Return String.Format("indice_inf={7}&indice_sup={8}&ordenarpor=6&orden=1&ver_campo1=on&solicitante_id={0}&ver_campo3=on&proceso_id={1}&ver_campo4=on&estado_job={2}&ver_campo5=on&instancia_id={3}&ver_campo6=on&usuario={4}&otrs_id={5}&buscar_texto={6}", _
                          Me.SolicitanteId, Me.ProcesoId, Me.EstadoJob, Replace(Me.InstanciaId, " ", "+"), Me.Usuario, Me.OtrsId, Me.BuscarTexto, Me.IndiceInf.ToString, Me.IndiceSup.ToString)
    End Function

    Public Sub Avanzar()
        _indiceInf += 30
        _indiceSup += 30
    End Sub

End Class
