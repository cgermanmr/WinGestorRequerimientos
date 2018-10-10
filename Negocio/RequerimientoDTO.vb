Imports System.Text.RegularExpressions

<Serializable> Public Class RequerimientoDTO
    Private _jobId As Integer
    Public Property JobId() As Integer
        Get
            Return _jobId
        End Get
        Set(ByVal value As Integer)
            _jobId = value
        End Set
    End Property

    Private _proceso As String
    Public Property Proceso() As String
        Get
            Return _proceso
        End Get
        Set(ByVal value As String)
            _proceso = value
        End Set
    End Property

    Private _esColaPropia As Boolean
    Public Property EsColaPropia() As Boolean
        Get
            Return _esColaPropia
        End Get
        Set(ByVal value As Boolean)
            _esColaPropia = value
        End Set
    End Property

    Private _cola As String = "N/A"
    Public Property Cola() As String
        Get
            Return _cola
        End Get
        Set(ByVal value As String)
            _cola = value
        End Set
    End Property

    Private _descripcion As String
    Public Property Descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    Private _estado As String
    Public Property Estado() As String
        Get
            Return _estado
        End Get
        Set(ByVal value As String)
            _estado = value
        End Set
    End Property

    Private _solicitante As String
    Public Property Solicitante() As String
        Get
            Return _solicitante
        End Get
        Set(ByVal value As String)
            _solicitante = value
        End Set
    End Property

    Property Direccion As String
    Property Empresa As String
    Property CodigoAgente As String
    Property Sistema As String

    Private _ingreso As String
    Public Property Ingreso() As String
        Get
            Return _ingreso
        End Get
        Set(ByVal value As String)
            _ingreso = value
        End Set
    End Property

    Private _fechaUltimaModificacion As String
    Public Property FechaUltimaModificacion() As String
        Get
            Return _fechaUltimaModificacion
        End Get
        Set(ByVal value As String)
            _fechaUltimaModificacion = value
        End Set
    End Property

    Private _fechaIngreso As String
    Public Property FechaIngreso() As String
        Get
            Return _fechaIngreso
        End Get
        Set(ByVal value As String)
            _fechaIngreso = value
        End Set
    End Property

    Private _instancia As String = "N/A"
    Public Property Instancia() As String
        Get
            Return _instancia
        End Get
        Set(ByVal value As String)
            _instancia = value
        End Set
    End Property

    Private _modifico As String
    Public Property Modifico() As String
        Get
            Return _modifico
        End Get
        Set(ByVal value As String)
            _modifico = value
        End Set
    End Property

    Private _otrsVinculado As Integer
    Public Property OtrsVinculado() As Integer
        Get
            Return _otrsVinculado
        End Get
        Set(ByVal value As Integer)
            _otrsVinculado = value
        End Set
    End Property

    Private _jobHtml As String
    Public Property JobHtml() As String
        Get
            Return _jobHtml
        End Get
        Set(ByVal value As String)
            If value.Contains("/imagenes/cruz.jpg") Then
                value = Replace(value, "<td align=right valign=center onClick=""javascript:cerrar_ventana();""><img  valign=middle border=0 class=textLinkVentana alt=""Cerrar"" src=""https://main.cajval.sba.com.ar/wf/imagenes/cruz.jpg""></img></td>", String.Empty)
            End If
            _jobHtml = value
        End Set
    End Property

    Public ReadOnly Property UrlJob() As String
        Get
            Return "https://main.cajval.sba.com.ar/wf/workflow/wf_ampliar_descripcion.asp?job_id=" & Me.JobId
        End Get
    End Property

    Public ReadOnly Property UrlEjecutarInstancia() As String
        Get
            Select Case Me.Instancia
                Case "Tomar tarea"
                    Return "https://main.cajval.sba.com.ar/wf/workflow/wf_tomar_tarea.asp?job_id=" & Me.JobId
                Case "Ejecutar tarea"
                    Return "https://main.cajval.sba.com.ar/wf/workflow/wf_ejecucion_job.asp?job_id=" & Me.JobId
                Case "Aprobacion"
                    Return "https://main.cajval.sba.com.ar/wf/workflow/wf_aprobacion_job.asp?job_id=" & Me.JobId
                Case Else
                    Return Me.UrlJob
            End Select
        End Get
    End Property

End Class
