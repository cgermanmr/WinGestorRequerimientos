Imports System.Text.RegularExpressions
Imports System.ComponentModel
Imports System.Linq
Imports System.IO
Imports System.Net.Http
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization.Formatters.Soap
Imports System.Globalization
Imports System.Timers

<Serializable> Public Class SesionActualWindows

#Region "Variables de Sesion y Eventos"


    Public Event JobsListadoCompletoSesionEvent()
    Public Event JobsPorcentajeModificadoSesionEvent()
    Public Event JobRefrescadoSesionEvent()
    Public Event NuevoJobSesionEvent(sender As Object, e As EventArgs)

    Public Event UsuarioValidadoEvent()
    Public Event IniciarSesionEvent()

#Region "Constantes"
    Private Const URL_LOG_ON_INV As String = "https://main.cajval.sba.com.ar/IN/rg_logon_form.asp"
    Public Const URL_INV_DEFAULT_LOGUEADO As String = "https://main.cajval.sba.com.ar/IN/default_logueado.asp"
    Public Const URL_LOG_ON As String = "https://main.cajval.sba.com.ar/wf/rg_logon_form.asp"
    Private Const URL_LOG_OUT As String = "https://main.cajval.sba.com.ar/wf/rg_logout.asp"
    Private Const URL_DEFAULT As String = "https://main.cajval.sba.com.ar/wf/default.asp"
    Private Const URL_LISTADO As String = "https://main.cajval.sba.com.ar/wf/workflow/wf_listado_solicitudes_servicio.asp?usuario="
    Private Const URL_DEFAULT_LOGUEADO As String = "https://main.cajval.sba.com.ar/wf/default_logueado.asp"
    Public Shared HTML_FILTRO As String

    ''' <summary>
    '''Error N°: 1010
    '''Descripción: Usuario inválido. No se pudo verificar con éxito su estado de sesión. Si le vuelve a ocurrir, por favor verifique con el administrador del sistema. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Const URL_ERROR_1010 As String = "https://main.cajval.sba.com.ar/wf/default.asp?msg=1010"
    ''' <summary>
    '''Error N°: 515
    '''Descripción: No existe sesion, o su browser no soporta el uso de cookies.(REGISTRO)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const URL_ERROR_515 As String = "https://main.cajval.sba.com.ar/IN/default.asp?msg=515"
    ''' <summary>
    '''Error N°: 1011
    '''Descripción: Sesión terminada. El tiempo de expiración para la sesión terminó. Por favor vuelva a logonearse.   
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URL_ERROR_1011 As String = "https://main.cajval.sba.com.ar/wf/default.asp?msg=1011"
    ''' <summary>
    '''  Error N°: 1024
    '''Descripción: El nombre de usuario gsdfsdf no existe. Por favor registrese o contáctese con el administrador del sistema.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URL_ERROR_1024 As String = "https://main.cajval.sba.com.ar/wf/rg_logon_form.asp?msg=1024&usuario="
    ''' <summary>
    '''Error N°: 1023
    '''Descripción: La clave del usuario gmedina está vencida. Por favor cambie su clave ingresando en la opción 'Cambio de clave'.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URL_ERROR_1023 As String = "https://main.cajval.sba.com.ar/wf/rg_logon_form.asp?msg=1023&usuario="
    ''' <summary>
    '''Error N°: 1022
    '''Descripción: La clave suministrada para el nombre de usuario gmedina no es la correcta. Por favor vuelva a intentarlo, o recurra a la opción 'Olvido de clave'.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URL_ERROR_1022 As String = "https://main.cajval.sba.com.ar/wf/rg_logon_form.asp?msg=1022&usuario="


#End Region

#End Region

#Region "Constructores"
    'Implementacion del singleton, solo una instancia en memoria
    Private Shared ReadOnly _instancia As SesionActualWindows = New SesionActualWindows()
    Public Shared Function SesionActual() As SesionActualWindows
        Return _instancia
    End Function

    Private _timeLife As Date
    Private Sub New()
        _timeLife = Now
        InicializarTimer()
        InicializarGestores()
    End Sub

    Private Sub InicializarGestores()
        _gestores.Add("Sesion", New GestorRequerimiento())
        _gestores.Add("Abiertos", New GestorRequerimiento())
        _gestores.Add("Actualizacion", New GestorRequerimiento())
    End Sub


#End Region

#Region "Propiedades"

    Private _horaUltimaActualizacion As Date
    Public ReadOnly Property HoraUltimaActualizacion() As String
        Get
            Return String.Format("Última actualización {0} hs - {1} elementos", _horaUltimaActualizacion.ToString("dd\/MM\/yy HH:mm:ss"), SesionActual.GestorRequerimiento.JobsTotal)
        End Get
    End Property

    Private _recargaEnCurso As Boolean
    Public ReadOnly Property RecargaEnCurso() As Boolean
        Get
            Return _recargaEnCurso
        End Get
        
    End Property

    Private _habilitarCarta As Boolean
    Public ReadOnly Property CartaHabilitada() As Boolean
        Get
            Return _habilitarCarta
        End Get
       
    End Property


    Public Property AutoRefresh() As Boolean
        Get
            Return _timerVerificarJobs.Enabled
        End Get
        Set(ByVal value As Boolean)
            _timerVerificarJobs.Enabled = value

            For Each k As String In _gestores.Keys
                _gestores(k).AutoRefresh = value
            Next

        End Set
    End Property

    ''' <summary>
    ''' Indica si esta aplicado el filtro mis colas
    ''' </summary>
    ''' <remarks></remarks>
    Private _filtroMisColas As Boolean
    Public Property FiltroMisColas() As Boolean
        Get
            Return _filtroMisColas
        End Get
        Set(ByVal value As Boolean)
            _filtroMisColas = value
            SesionActual.Orden = Requerimiento.ColaPropiaComparer(SortOrder.Descending)
            GestorRequerimiento.Listar.Sort(SesionActual.Orden)
        End Set
    End Property

    Private _orden As System.Collections.Generic.IComparer(Of Requerimiento) = Requerimiento.ColaPropiaComparer(SortOrder.Descending)
    Public Property Orden() As System.Collections.Generic.IComparer(Of Requerimiento)
        Get
            Return _orden
        End Get
        Set(ByVal value As System.Collections.Generic.IComparer(Of Requerimiento))
            _orden = value
        End Set
    End Property

    Private _jobSeleccionado As Requerimiento
    Public Property JobSeleccionado() As Requerimiento
        Get
            Return _jobSeleccionado
        End Get
        Set(ByVal value As Requerimiento)
            _jobSeleccionado = value
        End Set
    End Property

    Public ReadOnly Property GestorRequerimiento() As GestorRequerimiento
        Get
            Return _gestores("Sesion")

        End Get
    End Property

    Private Sub EstablecerGestorRequerimientoSesion(ByVal gr As GestorRequerimiento)

        _gestores("Sesion") = gr

        'Asociar eventos
        AddHandler _gestores("Sesion").JobsListadoCompletoEvent, AddressOf JobsListadoCompletoManejador
        AddHandler _gestores("Sesion").NuevoJobsCargadoEvent, AddressOf JobsPorcentajeModificadoManejador
        AddHandler _gestores("Sesion").JobsRefrescadoEvent, AddressOf JobsRefrescadoEventManejador

        If _gestores("Sesion").Done.Equals(False) Then
            _gestores("Sesion").Start()
        End If

        If _gestores("Sesion").JobsTotal = _gestores("Sesion").JobsProcesados And _gestores("Sesion").JobsProcesados > 0 Then
            'Se lanzan los eventos para actualizar interfaz grafica cuando se vuelve a gestor de jobs abiertos
            RaiseEvent JobsPorcentajeModificadoSesionEvent()
            RaiseEvent JobsListadoCompletoSesionEvent()
        End If

    End Sub

    Private _estadoAutenticacion As Integer
    Public ReadOnly Property EstadoAutenticacion() As Integer
        Get
            Return _estadoAutenticacion
        End Get
    End Property

    Public ReadOnly Property JobsProcesados() As Integer
        Get
            If _gestores("Actualizacion").Listar.Count = 0 Then ' Is Nothing Then
                Return GestorRequerimiento.JobsProcesados
            Else
                Return _gestores("Actualizacion").JobsProcesados
            End If

        End Get
    End Property

    Public ReadOnly Property JobsTotal() As Integer
        Get
            If _gestores("Actualizacion").Listar.Count = 0 Then 'Is Nothing Then
                Return GestorRequerimiento.JobsTotal
            Else
                Return _gestores("Actualizacion").JobsTotal
            End If
        End Get
    End Property

    Public ReadOnly Property UrlListadoSolicitudes() As String
        Get
            If Not _usuario Is Nothing Then
                Return URL_LISTADO & Me._usuario.Nombre & "&estado=abierto"
            Else
                Return URL_LISTADO
            End If

        End Get
    End Property

    Private _gestores As New Dictionary(Of String, GestorRequerimiento)()

    Public Property GestoresJobs() As Dictionary(Of String, GestorRequerimiento)
        Get
            Return _gestores
        End Get
        Set(ByVal value As Dictionary(Of String, GestorRequerimiento))
            _gestores = value
        End Set
    End Property


#End Region

#Region "Funciones y Procedimientos"

    Private Function ObtenerTiempoEstimadoCarga() As String
        Dim _seg As Double

        _seg = (Me.JobsTotal - Me.JobsProcesados) / 4

        Dim _time As New TimeSpan(0, 0, Convert.ToInt32(_seg))

        Return _time.ToString
    End Function

    ''' <summary>
    ''' Devuelve un string con el progreso de la carga
    ''' </summary>
    ''' <returns>Texto con descripción del progreso</returns>
    ''' <remarks></remarks>
    Public Function ObtenerProgresoCarga() As String
        Dim _progreso As String
        Dim _porcentaje As Double

        If SesionActualWindows.SesionActual.JobsTotal > 0 Then
            _porcentaje = (SesionActualWindows.SesionActual.JobsProcesados / SesionActualWindows.SesionActual.JobsTotal)
        End If

        _progreso = String.Format("Cargando {2} Job {0}/{1} - Tiempo estimado {3}", _
                                  SesionActualWindows.SesionActual.JobsProcesados, _
                                  SesionActualWindows.SesionActual.JobsTotal, _
                                  _porcentaje.ToString("00.00%"), _
                                  ObtenerTiempoEstimadoCarga)
        Return _progreso

    End Function

    Public Sub ReCarga()
        If SesionActual.RecargaEnCurso Then
            Exit Sub
        End If

        If AutoRefresh Then
            _timerVerificarJobs.Interval = GetRnd() * 2
        End If

        _gestores("Actualizacion") = New GestorRequerimiento()
        AddHandler _gestores("Actualizacion").JobsListadoCompletoEvent, AddressOf JobsListadoCompletoManejador
        AddHandler _gestores("Actualizacion").NuevoJobsCargadoEvent, AddressOf JobsPorcentajeModificadoManejador
        _gestores("Actualizacion").Start()

        Me._recargaEnCurso = True

        _contadorRecarga = 0

        GC.Collect()

    End Sub

    ''' <summary>
    ''' Establece el gestor de requerimientos con los jobs abiertos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GestionarJobsAbiertos()
        EstablecerGestorRequerimientoSesion(_gestores("Abiertos"))
    End Sub

    Public Function getGestorFiltro(datosFiltro As DatosFiltro) As GestorRequerimiento
        Return AsociarEventosGestorFiltro(New GestorRequerimiento(datosFiltro))
    End Function

    Private Function FiltrarPorColaPropia() As List(Of Requerimiento)

        Dim listaReq As List(Of Requerimiento) = (From wf In Me.ListarJobsAbiertos _
                                  Select wf Where wf.EsColaPropia.Equals(True)).ToList

        If listaReq IsNot Nothing Then
            Return listaReq
        Else
            Return Nothing
        End If

    End Function

    Private _txtFiltro As String
    Private _filtroPorTexto As Boolean
    Public Function FiltrarPorTexto(txt As String) As List(Of Requerimiento)
        If String.IsNullOrEmpty(txt) Then
            _filtroPorTexto = False
        Else
            _filtroPorTexto = True
        End If

        _txtFiltro = txt

        If FiltroMisColas Then Return GestorRequerimiento.FiltrarPorTexto(txt).FindAll(Function(x) x.EsColaPropia.Equals(True))

        Return GestorRequerimiento.FiltrarPorTexto(txt)

    End Function

#Region "Control de Eventos"
    Private Sub JobsRefrescadoEventManejador()
        RaiseEvent JobRefrescadoSesionEvent()
    End Sub

    Private Sub JobsListadoCompletoManejador(sender As Object, e As EventArgs)

        SyncLock Me
            If _gestores("Actualizacion").Listar.Count > 0 Then
                _horaUltimaActualizacion = Now
                EstablecerGestorRequerimientoSesion(_gestores("Actualizacion"))
                _gestores("Abiertos") = _gestores("Actualizacion")
                _gestores("Actualizacion") = New GestorRequerimiento
            End If

            If GestorRequerimiento.Listar.Exists(Function(x) x.EsColaPropia.Equals(True) And x.Cola.Contains("OP Seguridad Operativa").Equals(True)) Then
                _habilitarCarta = True
            End If

            SesionActual.Serializar()

            Me._recargaEnCurso = False

            RaiseEvent JobsListadoCompletoSesionEvent()

        End SyncLock
    End Sub

    Private Sub JobsPorcentajeModificadoManejador()
        SyncLock Me
            RaiseEvent JobsPorcentajeModificadoSesionEvent()
        End SyncLock
    End Sub

    Private Sub NuevoJobEventManejador(sender As Object, e As EventArgs)
        SyncLock Me
            RaiseEvent NuevoJobSesionEvent(sender, e)
        End SyncLock
    End Sub

#End Region

    Public Function Listar() As List(Of Requerimiento)

        If _filtroMisColas Then
            Return FiltrarPorColaPropia()
        End If

        If _filtroPorTexto Then
            Return FiltrarPorTexto(_txtFiltro)
        End If

        Return GestorRequerimiento.Listar()

    End Function

    Public Function ListarJobsAbiertos() As List(Of Requerimiento)
        Return GestorRequerimiento.Listar()
    End Function

    Private Function AsociarEventosGestorFiltro(gestor As GestorRequerimiento) As GestorRequerimiento
        AddHandler gestor.JobsRefrescadoEvent, AddressOf JobsRefrescadoEventManejador
        Return gestor
    End Function

    '*************************************************************
    Public Function getGestorJobsPorListadoNros(listadoJobs As Integer()) As GestorRequerimiento
        Return AsociarEventosGestorFiltro(New GestorRequerimiento(listadoJobs))
    End Function

    '*************************************************************
    Public Function getGestorJobsPorIntervalo(ByVal desde As Integer, ByVal hasta As Integer) As GestorRequerimiento
        Return AsociarEventosGestorFiltro(New GestorRequerimiento(desde, hasta))
    End Function

    Private _timerVerificarJobs As System.Timers.Timer
    Private Sub InicializarTimer()
        _timerVerificarJobs = New System.Timers.Timer(GetRnd)
        AddHandler _timerVerificarJobs.Elapsed, AddressOf VerificarOnTimedEvent
        _timerVerificarJobs.Enabled = False
        _timerVerificarJobs.AutoReset = True
    End Sub


    Private Shared _contadorRecarga As Integer
    Private Async Sub VerificarOnTimedEvent(source As Object, e As ElapsedEventArgs)

        Dim grVerificador As New GestorRequerimiento()
        AddHandler grVerificador.JobsListadoCompletoEvent, AddressOf JobsListadoCompletoManejador
        AddHandler grVerificador.NuevoJobEvent, AddressOf NuevoJobEventManejador

        If Await grVerificador.ExistenJobsNuevos(Me.GestorRequerimiento) Then
            SesionActual.ReCarga()
        Else
            _timerVerificarJobs.Interval = GetRnd()

        End If

        grVerificador.Dispose()

        _contadorRecarga += 1

        If _contadorRecarga = 10 Then
            SesionActual.ReCarga()
        End If

        _horaUltimaActualizacion = Now

        'SesionActual.Serializar()

        GC.Collect()

    End Sub

    Private Function GetRnd() As Double
        Dim ran As New Random

        Return ran.Next(180000, 300000)

    End Function

#End Region

#Region "LogOn"

    Public Sub Cerrar()
        Me.EstablecerUsuarioActual(Nothing)
        Me._estadoAutenticacion = ResultadoAutenticacion.UsuarioInvalido

        If GestorRequerimiento IsNot Nothing Then
            Me.GestorRequerimiento.Dispose()
        End If

    End Sub

    Private _usuario As Usuario
    Public Sub EstablecerUsuarioActual(usuario As Usuario)
        If Not usuario Is Nothing Then
            Me._usuario = usuario
            _estadoAutenticacion = ResultadoAutenticacion.UsuarioInvalido
        End If
    End Sub

    Public Function ObtenerUsuarioActual() As Usuario
        Return Me._usuario
    End Function

    Public Async Function LogOn() As Threading.Tasks.Task
        Try
            Dim resultLogOn As String = Await ClienteWeb.LogOn

            If resultLogOn.Contains("Error N°").Equals(False) Then
                Me._estadoAutenticacion = ResultadoAutenticacion.UsuarioValido

                HTML_FILTRO = Await ClienteWeb.GetJobHtmlMenuFiltro

                Exit Function
            Else
                Select Case Regex.Matches(resultLogOn, "Error N°:&nbsp;<\/b>(\d+)<br>").Item(0).Groups(1).Value
                    Case "1010"
                        Me._estadoAutenticacion = ResultadoAutenticacion.UsuarioInvalido
                    Case "1024"
                        Me._estadoAutenticacion = ResultadoAutenticacion.UsuarioInvalido
                    Case "1023"
                        Me._estadoAutenticacion = ResultadoAutenticacion.ClaveVencida
                    Case "1022"
                        Me._estadoAutenticacion = ResultadoAutenticacion.ClaveIncorrecta
                End Select
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            Logger.WriteLogExeption(ex)
        End Try


    End Function

    'Public Sub LogOnWebBrowser()
    '    LogOnWebBrowser(New WebBrowser)
    'End Sub

    Public Sub LogOnWebBrowser(wb As WebBrowser)

        If wb.Url Is Nothing Then
            AddHandler wb.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf NavegadorLogOn_DocumentCompleted)
            wb.Url = New Uri(URL_LOG_ON)
            Exit Sub
        End If

        If wb.Url.AbsoluteUri <> URL_LOG_ON Then
            AddHandler wb.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf NavegadorLogOn_DocumentCompleted)
            wb.Url = New Uri(URL_LOG_ON)
            Exit Sub
        End If

        Dim btnIngresar As HtmlElement = Nothing

        For Each elemento As HtmlElement In wb.Document.All
            If elemento.GetAttribute("name") = "vc_usuario_id" Then
                elemento.SetAttribute("value", SesionActualWindows.SesionActual.ObtenerUsuarioActual.Nombre)
            End If
            If elemento.GetAttribute("name") = "vc_clave_id" Then
                elemento.SetAttribute("value", SesionActualWindows.SesionActual.ObtenerUsuarioActual.Clave)
            End If

            If elemento.GetAttribute("value") = "Ingresar" Then
                btnIngresar = elemento
            End If
        Next
        If btnIngresar IsNot Nothing Then
            btnIngresar.InvokeMember("click")
        End If

    End Sub

    Public Sub LogOnInventario(wb As WebBrowser)

        If wb.IsDisposed Then
            Exit Sub
        End If

        If wb.Url Is Nothing Then
            AddHandler wb.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf NavegadorLogOn_DocumentCompleted)
            wb.Url = New Uri(URL_LOG_ON_INV)
            Exit Sub
        End If

        If wb.Url.AbsoluteUri <> URL_LOG_ON_INV Then
            AddHandler wb.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf NavegadorLogOn_DocumentCompleted)
            wb.Url = New Uri(URL_LOG_ON_INV)
            Exit Sub
        End If

        Dim btnIngresar As HtmlElement = Nothing

        For Each elemento As HtmlElement In wb.Document.All
            If elemento.GetAttribute("name") = "vc_usuario_id" Then
                elemento.SetAttribute("value", SesionActualWindows.SesionActual.ObtenerUsuarioActual.Nombre)
            End If
            If elemento.GetAttribute("name") = "vc_clave_id" Then
                elemento.SetAttribute("value", SesionActualWindows.SesionActual.ObtenerUsuarioActual.Clave)
            End If

            If elemento.GetAttribute("value") = "Ingresar" Then
                btnIngresar = elemento
            End If
        Next
        If btnIngresar IsNot Nothing Then
            btnIngresar.InvokeMember("click")
        End If

    End Sub

    Private Sub NavegadorLogOn_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs)

        Select Case DirectCast(sender, WebBrowser).Url.AbsoluteUri
            Case URL_DEFAULT, URL_ERROR_1010, URL_ERROR_1011, URL_LOG_OUT
                DirectCast(sender, WebBrowser).Url = New Uri(URL_LOG_ON)
            Case URL_LOG_ON_INV
                LogOnInventario(DirectCast(sender, WebBrowser))
            Case URL_LOG_ON
                LogOnWebBrowser(DirectCast(sender, WebBrowser))
        End Select

    End Sub

#End Region

#Region "Serializar"

    Public Sub Serializar(wfs As List(Of Requerimiento))
        Dim fs As FileStream = Nothing
        Try
            'check the file
            If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp\")
            End If

            fs = New FileStream(Application.StartupPath & "\tmp\Historico.bin", FileMode.Append, FileAccess.Write)

            'Get a Binary Formatter instance
            Dim bf As New BinaryFormatter()

            Dim ListaJobDto As New List(Of RequerimientoDTO)

            For Each r As Requerimiento In wfs
                ListaJobDto.Add(r.JobDto)
            Next

            ' Serialize the instance to the file.
            bf.Serialize(fs, ListaJobDto.ToArray)

        Catch ex As Exception
            MsgBox("No se pudo guardar la sesión. " & vbCr & "Descripción: " & ex.Message, CType(vbOKOnly + MsgBoxStyle.Information, MsgBoxStyle))
            Logger.WriteLogExeption(ex)
        Finally
            ' Close the file and release resources.
            fs.Close()
        End Try

    End Sub

    Public Sub Serializar()
        Dim fs As FileStream = Nothing
        Try
            'check the file
            If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp\")
            End If

            fs = New FileStream(Application.StartupPath & "\tmp\requerimientos.bin", FileMode.Create, FileAccess.ReadWrite)

            'Get a Binary Formatter instance
            Dim bf As New BinaryFormatter()

            Dim ListaJobDto As New List(Of RequerimientoDTO)

            For Each r As Requerimiento In Me.GestorRequerimiento.Listar
                ListaJobDto.Add(r.JobDto)
            Next

            ' Serialize the instance to the file.
            bf.Serialize(fs, ListaJobDto.ToArray)

        Catch ex As Exception
            MsgBox("No se pudo guardar la sesión. " & vbCr & "Descripción: " & ex.Message, CType(vbOKOnly + MsgBoxStyle.Information, MsgBoxStyle))
            Logger.WriteLogExeption(ex)
        Finally
            ' Close the file and release resources.
            fs.Close()
        End Try

    End Sub

    Public Sub Deserializar()
        Dim fs As FileStream = Nothing
        Try

            fs = New FileStream(Application.StartupPath & "\tmp\requerimientos.bin", FileMode.Open)

            'Get a Binary Formatter instance
            Dim bf As New BinaryFormatter()

            ' Deserialize from the file, creating an instance of SerializableClass.
            ' The deserialized object must be cast to the proper type.
            _gestores("Abiertos") = New GestorRequerimiento(CType(bf.Deserialize(fs), RequerimientoDTO()))
            EstablecerGestorRequerimientoSesion(_gestores("Abiertos"))

        Catch ex As System.IO.FileNotFoundException
            MsgBox(ex.Message, CType(vbOKOnly + MsgBoxStyle.Information, MsgBoxStyle))
            Logger.WriteLogExeption(ex)
        Finally
            ' Close the file and release resources.
            fs.Close()
        End Try

    End Sub

    Public Function ExisteArchivoUltimaSesion() As Boolean
        If File.Exists(Application.StartupPath & "\tmp\requerimientos.bin") Then
            'Dim fileCreatedDate As DateTime = File.GetCreationTime(Application.StartupPath & "\tmp\requerimientos.bin")
            Dim fileUpdateDate As DateTime = File.GetLastWriteTime(Application.StartupPath & "\tmp\requerimientos.bin")

            _horaUltimaActualizacion = fileUpdateDate
            If (Date.Now - fileUpdateDate).Hours > 1 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

#End Region

End Class
