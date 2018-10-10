Imports System.Net.Http
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Timers


Public Class Requerimiento
    Inherits ThreadWrapperBase
    Implements IComparable, IDisposable

#Region "Eventos"
    Public Event JobNoCargadoEvent(sender As Object, e As EventArgs)
    Public Event JobCargadoEvent(sender As Object, e As EventArgs)
    Public Event JobRefrescadoEvent(sender As Object, e As EventArgs)
#End Region

#Region "Variables Privadas"
    Private Const PATRON_JOBID_NO_CARGADO As String = "Requerimiento de Servicio N°&nbsp;0"
    Private _clienteWeb As New ClienteWeb
    Private _time As Date
#End Region
    
#Region "Constructor"

    Sub New()
        _time = Date.Now
        InicializarTimer()
    End Sub

    Private Sub HiloAnalizarHtml(htmlObj As Object)
        AnalizarHtmlWF(DirectCast(htmlObj, String))
    End Sub

    Sub New(JobDto As RequerimientoDTO)
        _jobDto = JobDto
    End Sub

    Sub New(ByVal JobId As Integer)
        _jobDto.JobId = JobId
        Me.Start()
    End Sub

#End Region

#Region "Operacion Asiscronica"

    Protected Overrides Async Sub OnStart()
        Try
            _jobDto.JobHtml = Await _clienteWeb.GetJobHtml(Me.JobId)
        Catch exProcesoCancelado As System.Threading.Tasks.TaskCanceledException
            System.Threading.Thread.Sleep(Convert.ToInt32(3000 * Rnd()))
            OnStart()
        Catch exHttpRequest As HttpRequestException
            System.Threading.Thread.Sleep(Convert.ToInt32(3000 * Rnd()))
            OnStart()
        Catch exAE As System.AggregateException
            System.Threading.Thread.Sleep(Convert.ToInt32(3000 * Rnd()))
            OnStart()
        Catch ex As Exception
            MsgBox(String.Format("Error en Requerimiento {1}: {0}", ex.Message, Me.JobId.ToString))
        End Try
        If String.IsNullOrEmpty(_jobDto.JobHtml).Equals(False) Then
            AnalizarHtmlWF(_jobDto.JobHtml)
        End If
    End Sub

#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Esta propiedad indica si se a realizado el analisis del html y se han cargado los valores
    ''' </summary>
    ''' <remarks></remarks>
    Private _cargaCompleta As Boolean
    Public ReadOnly Property CargaCompleta() As Boolean
        Get
            Return _cargaCompleta
        End Get
    End Property

    Public Property JobHtml() As String
        Get
            Return _jobDto.JobHtml
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(value).Equals(False) Then
                _jobDto.JobHtml = value
                'Dim tsk As New Thread(AddressOf HiloAnalizarHtml)
                'tsk.Start(_jobDto.JobHtml)
                'AnalizarHtmlWF(value)
            End If
        End Set
    End Property

    Private _jobDto As New RequerimientoDTO()
    Public Property JobDto() As RequerimientoDTO
        Get
            Return _jobDto
        End Get
        Set(ByVal value As RequerimientoDTO)
            _jobDto = value

            _cargaCompleta = True
            RaiseEvent JobCargadoEvent(Me, New EventArgs)

        End Set
    End Property

    Public ReadOnly Property UrlJob() As String
        Get
            Return _jobDto.UrlJob
        End Get
    End Property

    Public Property JobId() As Integer
        Get
            Return _jobDto.JobId
        End Get
        Set(ByVal value As Integer)
            _jobDto.JobId = value
        End Set
    End Property

    Public ReadOnly Property OtrsVinculado() As Integer
        Get
            If String.IsNullOrEmpty(_jobDto.OtrsVinculado.ToString) Then
                AnalizarHtmlWF(_jobDto.JobHtml)
            End If

            Return _jobDto.OtrsVinculado
        End Get
       
    End Property

    Public ReadOnly Property Estado() As String
        Get
            If String.IsNullOrEmpty(_jobDto.Estado) Then
                AnalizarHtmlWF(_jobDto.JobHtml)
            End If

            Return _jobDto.Estado
        End Get
    End Property

    Public ReadOnly Property FechaIngreso() As String
        Get
            If String.IsNullOrEmpty(_jobDto.FechaIngreso) Then
                AnalizarHtmlWF(_jobDto.JobHtml)
            End If

            Return _jobDto.FechaIngreso
        End Get
    End Property

    Private ReadOnly Property FechaUltimaModificacion() As String
        Get
            If String.IsNullOrEmpty(_jobDto.FechaUltimaModificacion) Then
                AnalizarHtmlWF(_jobDto.JobHtml)
            End If

            Return _jobDto.FechaUltimaModificacion
        End Get
    End Property

    Public ReadOnly Property Proceso() As String
        Get
            If String.IsNullOrEmpty(_jobDto.Proceso) Then
                AnalizarHtmlWF(_jobDto.JobHtml)
            End If

            Return _jobDto.Proceso
        End Get
    End Property

    Public Property Instancia() As String
        Get
            If Me.Estado <> "abierto" Then
                Return "N/A"
            Else
                Return _jobDto.Instancia
            End If
        End Get
        Set(ByVal value As String)
            _jobDto.Instancia = value
        End Set
    End Property

    Public ReadOnly Property Solicitante() As String
        Get
            If String.IsNullOrEmpty(_jobDto.Solicitante) Then
                _jobDto.Solicitante = _jobDto.Ingreso
            End If

            Return _jobDto.Solicitante
        End Get
    End Property

    Public ReadOnly Property Ingreso() As String
        Get
            If String.IsNullOrEmpty(_jobDto.Ingreso) Then
                AnalizarHtmlWF(_jobDto.JobHtml)
            End If

            Return _jobDto.Ingreso
        End Get
    End Property

    Public ReadOnly Property Modifico() As String
        Get
            Return _jobDto.Modifico
        End Get
    End Property

    Public ReadOnly Property EsColaPropia() As Boolean
        Get
            Return _jobDto.EsColaPropia
        End Get
    End Property

    Public Property Cola() As String
        Get
            If Me.Estado <> "abierto" Then
                Return "N/A"
            Else
                Return _jobDto.Cola
            End If

        End Get
        Set(ByVal value As String)
            If value.Contains("&nbsp;") Then
                _jobDto.EsColaPropia = True
                _jobDto.Cola = Regex.Matches(value, "</img>&nbsp;(.*)").Item(0).Groups(1).Value()
            Else
                _jobDto.EsColaPropia = False
                _jobDto.Cola = value
            End If
        End Set
    End Property

    Public ReadOnly Property Descripcion() As String
        Get
            If String.IsNullOrEmpty(_jobDto.Descripcion) Then
                AnalizarHtmlWF(_jobDto.JobHtml)
            End If

            Return _jobDto.Descripcion
        End Get
    End Property

    'Private _listaCorreos As New List(Of String)
    'Public Property ListaCorreos() As List(Of String)
    '    Get
    '        Return _listaCorreos
    '    End Get
    '    Set(ByVal value As List(Of String))
    '        _listaCorreos = value
    '    End Set
    'End Property


    Private Function DiasHabiles(ByVal date1 As Date, ByVal date2 As Date) As Integer
        Dim _diasHabiles As Integer
        Dim _diaContador As Date = date2

        For i As Integer = 1 To (date1 - date2).Days
            If _diaContador.DayOfWeek <> DayOfWeek.Saturday And _diaContador.DayOfWeek <> DayOfWeek.Sunday Then
                _diasHabiles += 1
            End If
            _diaContador = _diaContador.AddDays(1)
        Next

        Return _diasHabiles
    End Function

    Dim formato As String = "{0}.{1}"

    Private Function ObtenerTiempoTotal() As String
        Dim _tiempoTotal As String = String.Empty

        Dim f_ingreso As Date = DateTime.ParseExact(Me.FechaIngreso, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) 'Date.Parse(Me.FechaIngreso)

        Select Case Me.Estado
            Case "abierto"
                Dim _dias As Integer = DiasHabiles(Date.Now, f_ingreso)
                Dim _hora As String = Regex.Match((Date.Now - f_ingreso).ToString, "(\d\d:\d\d:\d\d)").Groups(1).Value
                _tiempoTotal = String.Format(formato, _dias, _hora)
            Case Else
                Dim f_modificacion As Date = DateTime.ParseExact(Me.FechaUltimaModificacion, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) 'Date.Parse(Me.FechaUltimaModificacion)
                Dim _dias As Integer = DiasHabiles(f_modificacion, f_ingreso)
                Dim _hora As String = Regex.Match((f_modificacion - f_ingreso).ToString, "(\d\d:\d\d:\d\d)").Groups(1).Value
                _tiempoTotal = String.Format(formato, _dias, _hora)
        End Select

        Return _tiempoTotal

    End Function

    Public ReadOnly Property TiempoTotal() As String
        Get
            Return ObtenerTiempoTotal()
        End Get
    End Property

    Private Function ObtenerTiempoInstancia() As String
        Dim _tiempoInstancia As String = String.Empty

        Select Case Me.Estado
            Case "abierto"
                Dim f_modificacion As Date = DateTime.ParseExact(Me.FechaUltimaModificacion, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) 'Date.Parse(Me.FechaUltimaModificacion) 'DateTime.ParseExact("18-12-2007", "dd-MM-yyyy", CultureInfo.InvariantCulture)
                Dim _dias As Integer = DiasHabiles(Date.Now, f_modificacion)
                Dim _hora As String = Regex.Match((Date.Now - f_modificacion).ToString, "(\d\d:\d\d:\d\d)").Groups(1).Value
                _tiempoInstancia = String.Format(formato, _dias, _hora)

            Case Else
                _tiempoInstancia = "N/A"
        End Select

        Return _tiempoInstancia

    End Function

    Public ReadOnly Property TiempoInstancia() As String
        Get
            Return ObtenerTiempoInstancia()
        End Get
    End Property

    Public Property AutoRefresh() As Boolean
        Get
            Return _timerRefreshJob.Enabled
        End Get
        Set(ByVal value As Boolean)
            _timerRefreshJob.Enabled = value
        End Set
    End Property

#End Region

#Region "Funciones"

    Private Function GetRnd() As Double
        Dim ran As New Random
        Return ran.Next(180000, 240000)
    End Function

    Shared Function GetPasswd() As String
        Dim obj As New Random()
        Dim posibles As String = "*-+#$%1234567890ABCDEFGHJKLMNPQRSTUVWXYZ1234567890*-+#$%" '"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
        Dim longitud As Integer = posibles.Length
        Dim letra As Char
        Dim longitudnuevacadena As Integer = 8
        Dim nuevacadena As String = ""
        For i As Integer = 0 To longitudnuevacadena - 1
            letra = posibles(obj.[Next](longitud))
            nuevacadena += letra.ToString()
        Next
        Threading.Thread.Sleep(50)
        Return nuevacadena

    End Function

    Private _timerRefreshJob As System.Timers.Timer
    Private Sub InicializarTimer()
        _timerRefreshJob = New System.Timers.Timer(GetRnd())
        AddHandler _timerRefreshjob.Elapsed, AddressOf OnTimedEvent
        _timerRefreshJob.Enabled = False
        _timerRefreshJob.AutoReset = True
    End Sub

    Private Sub OnTimedEvent(source As Object, e As ElapsedEventArgs)

        If Me.Estado IsNot Nothing Then
            If Me.Estado.Contains("cerrado") Then
                _timerRefreshJob.Enabled = False
                Exit Sub
            End If
        End If

        If Me.EsColaPropia Then
            Me.Refresh()
            _timerRefreshJob.Interval = GetRnd()
        End If

    End Sub

    Public Async Sub ModificarTarea(ByVal comentario As String)
        If Me.Instancia.Contains("Tomar") Then
            Await _clienteWeb.TomarJob(Me.JobId, comentario)
        End If

        If Me.Instancia.Contains("Ejecutar") Then
            Await _clienteWeb.EjecutarJob(Me.JobId, comentario)
        End If

        Me.Refresh()

    End Sub

    Public Async Sub Refresh()
        Dim _grActualizador As New GestorRequerimiento

        Me.JobDto = DirectCast(Await _grActualizador.ObtenerJobsActualizado(Me), Requerimiento).JobDto

        _time = Now

        RaiseEvent JobRefrescadoEvent(Me, New EventArgs)

    End Sub

    Private Function RemoveHtmlTags(ByVal datoCampo As String) As String

        datoCampo = datoCampo.Replace("&nbsp;", String.Empty)
        datoCampo = datoCampo.Replace("<br>", vbCrLf)
        datoCampo = datoCampo.Replace("<i>", String.Empty)
        datoCampo = datoCampo.Replace("</i>", String.Empty)
        datoCampo = datoCampo.Replace("<b>", String.Empty)
        datoCampo = datoCampo.Replace("</b>", String.Empty)

        Return datoCampo

    End Function

    Private Sub AnalizarHtmlWF(ByVal DocumentText As String)
        SyncLock Me

            If String.IsNullOrEmpty(DocumentText) Then
                Me.OnStart()
                Exit Sub
            End If

            ' Buscar coincidencias
            Dim re As Regex
            Const PATRON As String = "<td class=TextoDer>(.*?)<\/td>|<td class=TextoIzq>(.*?)<\/td>"

            If Regex.IsMatch(DocumentText, PATRON_JOBID_NO_CARGADO) Then
                RaiseEvent JobNoCargadoEvent(Me, New EventArgs())
                Exit Sub
            End If

            If Not DocumentText.Contains("Requerimiento de Servicio") Then
                Me.OnStart()
                Exit Sub
            End If

            ' Intentar crear el objeto RegEx
            re = New Regex(PATRON, RegexOptions.IgnoreCase)

            Dim mc As MatchCollection

            ' obtener la colección de resultados
            mc = re.Matches(DocumentText)

            Dim campoWf As New Campo

            Dim nombreCampo As String = String.Empty

            Dim m As Match
            For Each m In mc

                If Not String.IsNullOrEmpty(m.Groups(1).Value) Then
                    nombreCampo = m.Groups(1).Value
                Else
                    Dim datoCampo As String = RemoveHtmlTags(m.Groups(2).Value)
                    Select Case nombreCampo
                        Case "Ticket OTRS vinculado:"
                            _jobDto.OtrsVinculado = Integer.Parse(datoCampo)
                        Case "Estado del requerimiento:"
                            _jobDto.Estado = datoCampo
                        Case "Solicitó:"
                            _jobDto.Ingreso = Trim(Regex.Matches(datoCampo, "(.*)\(").Item(0).Groups(1).Value)
                            _jobDto.FechaIngreso = Regex.Matches(datoCampo, "\d\d[- /.]\d\d[- /.]\d{4}\s\d+.\d+.\d+").Item(0).Groups(0).Value
                        Case "Modificó:"
                            If datoCampo.Contains("null") Then
                                _jobDto.Modifico = _jobDto.Ingreso
                                _jobDto.FechaUltimaModificacion = _jobDto.FechaIngreso
                            Else
                                _jobDto.Modifico = Trim(Regex.Matches(datoCampo, "(.*)\(").Item(0).Groups(1).Value)
                                _jobDto.FechaUltimaModificacion = Regex.Matches(datoCampo, "\d\d[- /.]\d\d[- /.]\d{4}\s\d+.\d+.\d+").Item(0).Groups(0).Value

                            End If

                        Case "Agente o Depositante:"
                            _jobDto.CodigoAgente = datoCampo
                        Case "Sistema:"
                            _jobDto.Sistema = datoCampo
                        Case "Proceso que lo rige:"
                            _jobDto.Proceso = datoCampo
                        Case "Descripción del requerimiento:"
                            _jobDto.Descripcion = datoCampo

                            If _jobDto.Descripcion.Contains("Solicitó:").Equals(True) Then
                                If Regex.IsMatch(_jobDto.Descripcion, "Solicitó:(.*)\b") Then
                                    _jobDto.Solicitante = Regex.Matches(_jobDto.Descripcion, "Solicitó:(.*)\b", RegexOptions.IgnoreCase).Item(0).Groups(1).Value.Trim
                                End If
                            End If

                            If _jobDto.Descripcion.Contains("Sol:").Equals(True) Then
                                If Regex.IsMatch(_jobDto.Descripcion, "Sol:(.*)\b") Then
                                    _jobDto.Solicitante = Regex.Matches(_jobDto.Descripcion, "Sol:(.*)\b", RegexOptions.IgnoreCase).Item(0).Groups(1).Value.Trim
                                End If
                            End If

                            If _jobDto.Descripcion.Contains("Solicitante:").Equals(True) Then
                                If Regex.IsMatch(_jobDto.Descripcion, "Solicitante:(.*)\b") Then
                                    _jobDto.Solicitante = Regex.Matches(_jobDto.Descripcion, "Solicitante:(.*)\b", RegexOptions.IgnoreCase).Item(0).Groups(1).Value.Trim
                                End If
                            End If

                            '******************************************************************************************

                            If _jobDto.Descripcion.Contains("Dirección:").Equals(True) Then
                                If Regex.IsMatch(_jobDto.Descripcion, "Dirección:(.*)\b") Then
                                    _jobDto.Direccion = Regex.Matches(_jobDto.Descripcion, "Dirección:(.*)\b", RegexOptions.IgnoreCase).Item(0).Groups(1).Value.Trim
                                End If
                            End If

                            If _jobDto.Descripcion.Contains("Dir:").Equals(True) Then
                                If Regex.IsMatch(_jobDto.Descripcion, "Dir:(.*)\b") Then
                                    _jobDto.Direccion = Regex.Matches(_jobDto.Descripcion, "Dir:(.*)\b", RegexOptions.IgnoreCase).Item(0).Groups(1).Value.Trim
                                End If
                            End If

                            If _jobDto.Descripcion.Contains("Empresa:").Equals(True) Then
                                If Regex.IsMatch(_jobDto.Descripcion, "Empresa:(.*)\b") Then
                                    _jobDto.Empresa = Regex.Matches(_jobDto.Descripcion, "Empresa:(.*)\b", RegexOptions.IgnoreCase).Item(0).Groups(1).Value.Trim
                                End If
                            End If

                            If _jobDto.Descripcion.Contains("Emp:").Equals(True) Then
                                If Regex.IsMatch(_jobDto.Descripcion, "Emp:(.*)\b") Then
                                    _jobDto.Empresa = Regex.Matches(_jobDto.Descripcion, "Emp:(.*)\b", RegexOptions.IgnoreCase).Item(0).Groups(1).Value.Trim
                                End If
                            End If

                    End Select

                End If

            Next

            '********************************************************************************************************

            Dim PATRON_INSTANCIAS As String = "(TextoDer><b>(.*)<\/b><\/td>)|(TextoCen>(.*)<\/td>)|(TextoIzq>(.*)<\/td>)|(\b(.*)\s<\/b><\/td>)|(([^\d\s\/:].*)\s<\/td>)|(\d+\/\d+\/\d+\s\d+:\d+\:\d+)"

            SyncLock Me
                re = New Regex(PATRON_INSTANCIAS, RegexOptions.IgnoreCase)

                ' obtener la colección de resultados
                mc = re.Matches(DocumentText)

                Dim _listaInstancias As New List(Of InstanciaProceso)
                Dim _instancia As InstanciaProceso = Nothing

                Dim contadorCap As Integer = 0

                For Each m In mc

                    If Not String.IsNullOrEmpty(m.Groups(2).Value) Then
                        _instancia = New InstanciaProceso
                        _instancia.Numero = Trim(m.Groups(2).Value)
                        contadorCap = 0
                    Else
                        If _instancia IsNot Nothing Then
                            Select Case contadorCap
                                Case 1
                                    _instancia.Accion = Trim(m.Groups(4).Value)
                                Case 2
                                    _instancia.Descripcion = Trim(m.Groups(6).Value)
                                Case 3
                                    _instancia.Usuario = Convert.ToString(IIf(String.IsNullOrEmpty(m.Groups(8).Value), String.Empty, Trim(m.Groups(8).Value)))
                                    If _instancia.Usuario.Contains("&nbsp;") Then
                                        _instancia.Comentario = Regex.Matches(_instancia.Usuario, "(Comentario:\s.*).><\/img>").Item(0).Groups(0).Value
                                        _instancia.Usuario = _instancia.Usuario.Split(New String() {"&nbsp;"}, StringSplitOptions.RemoveEmptyEntries)(0)
                                    End If
                                Case 4
                                    _instancia.AccionTomada = Convert.ToString(IIf(String.IsNullOrEmpty(m.Groups(10).Value), String.Empty, Trim(m.Groups(10).Value)))
                                Case 5
                                    _instancia.Hora = Convert.ToString(IIf(String.IsNullOrEmpty(m.Groups(11).Value), String.Empty, Trim(m.Groups(11).Value)))
                                    _listaInstancias.Add(_instancia)
                            End Select
                        End If
                    End If
                    contadorCap += 1
                Next

            End SyncLock

            '*******************************************************************************************************************

            'Dim PATRON_CORREOS As String = "[a-z\.\-_]+@[a-z\.\-_]+"

            're = New Regex(PATRON_CORREOS, RegexOptions.IgnoreCase)

            '' obtener la colección de resultados
            'mc = re.Matches(DocumentText)

            'For Each m In mc
            '    If Not String.IsNullOrEmpty(m.Groups(1).Value) Then
            '        Me.ListaCorreos.Add(Trim(m.Groups(1).Value))
            '    End If
            'Next

            '********************************************************

            _cargaCompleta = True
            RaiseEvent JobCargadoEvent(Me, New EventArgs())

        End SyncLock

    End Sub

#End Region

    Public Overrides Function ToString() As String
        Return "Job " & Me.JobId
    End Function

#Region "Comparadores"

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo

        ' Comprobamos si el tipo a comparar es de este tipo
        Dim j As Requerimiento = TryCast(obj, Requerimiento)
        ' Si no es de ese tipo o es nulo, indicar que son iguales
        If j Is Nothing Then
            Return 0
        End If

        'Return Me.JobId.CompareTo(j.JobId)
        Return j.EsColaPropia.CompareTo(Me.EsColaPropia)

    End Function

    'Nro de WF
    Public Shared ReadOnly Property JobIdComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _JobIdComparer(sortOrder)
        End Get
    End Property

    Private Class _JobIdComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim CompareResult As Integer = x.JobId.CompareTo(y.JobId)

            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'OTRSVinculado
    Public Shared ReadOnly Property OtrsVinculadoComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _OtrsVinculadoComparer(sortOrder)
        End Get
    End Property

    Private Class _OtrsVinculadoComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim CompareResult As Integer = x.OtrsVinculado.CompareTo(y.OtrsVinculado)

            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'Fecha de Ingreso
    Public Shared ReadOnly Property FechaIngresoComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _FechaIngresoComparer(sortOrder)
        End Get
    End Property

    Private Class _FechaIngresoComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare

            Dim fecha1 As Date = CDate(x.FechaIngreso)
            Dim fecha2 As Date = CDate(y.FechaIngreso)

            Dim CompareResult As Integer = System.DateTime.Compare(fecha1, fecha2)
            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'Proceso
    Public Shared ReadOnly Property ProcesoComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _ProcesoComparer(sortOrder)
        End Get
    End Property

    Private Class _ProcesoComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim CompareResult As Integer = System.String.Compare(x.Proceso.ToString(), y.Proceso.ToString())

            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'Estado
    Public Shared ReadOnly Property EstadoComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _EstadoComparer(sortOrder)
        End Get
    End Property

    Private Class _EstadoComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim CompareResult As Integer = System.String.Compare(x.Estado.ToString(), y.Estado.ToString())

            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'Por Instancia
    Public Shared ReadOnly Property InstanciaComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _InstanciaComparer(sortOrder)
        End Get
    End Property

    Private Class _InstanciaComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim CompareResult As Integer = System.String.Compare(x.Instancia.ToString(), y.Instancia.ToString())

            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'Por ultimo en modificar
    Public Shared ReadOnly Property ModificoComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _ModificoComparer(sortOrder)
        End Get
    End Property

    Private Class _ModificoComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim CompareResult As Integer = System.String.Compare(x.Modifico.ToString(), y.Modifico.ToString())

            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'Solicitante
    Public Shared ReadOnly Property SolicitanteComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _SolicitanteComparer(sortOrder)
        End Get
    End Property

    Private Class _SolicitanteComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim CompareResult As Integer = System.String.Compare(x.Solicitante.ToString, y.Solicitante.ToString)

            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'Usuario que ingreso
    Public Shared ReadOnly Property IngresoComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _IngresoComparer(sortOrder)
        End Get
    End Property

    Private Class _IngresoComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then
                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim CompareResult As Integer = System.String.Compare(x.Ingreso.ToString(), y.Ingreso.ToString())
            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'Tiempo en Cola
    Public Shared ReadOnly Property TiempoEnColaComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _TiempoEnColaComparer(sortOrder)
        End Get
    End Property

    Private Class _TiempoEnColaComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1
        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim xTiempo, yTiempo As TimeSpan

            If x.Estado = "abierto" Then
                xTiempo = Date.Now - Date.Parse(x.FechaUltimaModificacion)
            Else
                xTiempo = New TimeSpan
            End If

            If y.Estado = "abierto" Then
                yTiempo = Date.Now - Date.Parse(y.FechaUltimaModificacion)
            Else
                yTiempo = New TimeSpan
            End If

            Dim CompareResult As Integer = System.TimeSpan.Compare(xTiempo, yTiempo)
            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'Tiempo en total
    Public Shared ReadOnly Property TiempoTotalComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _TiempoTotalComparer(sortOrder)
        End Get
    End Property

    Private Class _TiempoTotalComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1
        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim xTiempo, yTiempo As TimeSpan

            If x.Estado = "abierto" Then
                xTiempo = Date.Now - Date.Parse(x.FechaIngreso)
            Else
                xTiempo = Date.Parse(x.FechaUltimaModificacion) - Date.Parse(x.FechaIngreso)
            End If

            If y.Estado = "abierto" Then
                yTiempo = Date.Now - Date.Parse(y.FechaIngreso)
            Else
                yTiempo = Date.Parse(y.FechaUltimaModificacion) - Date.Parse(y.FechaIngreso)
            End If

            Dim CompareResult As Integer = System.TimeSpan.Compare(xTiempo, yTiempo)

            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'Ordenamiento por Cola
    Public Shared ReadOnly Property ColaComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _ColaComparer(sortOrder)
        End Get
    End Property

    Private Class _ColaComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim CompareResult As Integer = System.String.Compare(x.Cola.ToString(), y.Cola.ToString())

            Return CompareResult * sortOrderModifier
        End Function

    End Class

    'Ordenamiento por Cola Propia
    Public Shared ReadOnly Property ColaPropiaComparer(ByVal sortOrder As SortOrder) As IComparer(Of Requerimiento)
        Get
            Return New _ColaPropiaComparer(sortOrder)
        End Get
    End Property

    Private Class _ColaPropiaComparer
        Implements IComparer(Of Requerimiento)

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(x As Requerimiento, y As Requerimiento) As Integer Implements IComparer(Of Requerimiento).Compare
            Dim CompareResult As Integer = x.EsColaPropia.CompareTo(y.EsColaPropia) 'System.String.Compare(x.Cola.ToString(), y.Cola.ToString())

            Return CompareResult * sortOrderModifier
        End Function

    End Class

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
                '_campos = Nothing
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region


End Class



