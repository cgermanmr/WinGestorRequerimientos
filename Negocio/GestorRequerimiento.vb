Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.Threading
Imports System.Threading.Tasks


Public Class GestorRequerimiento : Inherits ThreadWrapperBase : Implements IDisposable

    Public Event NuevoJobsCargadoEvent()
    Public Event NuevoJobEvent(sender As Object, e As EventArgs)
    Public Event JobsRefrescadoEvent()
    Public Event JobsListadoCompletoEvent(sender As Object, e As EventArgs)
    Public Event RequerimientoNoCargadoEvent(sender As Object, e As EventArgs)

#Region "Propiedades y variables"

    Private Enum TipoAnalisis As Integer
        HTML = 0
        ListaJobs = 1
        DesdeHastaJobs = 2
        ListaJobsDTO = 3
    End Enum

    Protected _wfs As New List(Of Requerimiento)
    Private _desdeJob, _hastaJob As Integer
    Private _tipoAnalisis As TipoAnalisis
    Private _listaJobsDto As RequerimientoDTO()
    Private _listaJobs As Integer()
    Private _clienteWeb As New ClienteWeb()

    Protected _jobsProcesados As Integer
    Public ReadOnly Property JobsProcesados() As Integer
        Get
            Return _jobsProcesados
        End Get
    End Property

    Protected _jobsTotal As Integer
    Public Property JobsTotal() As Integer
        Get
            Return _jobsTotal
        End Get
        Set(value As Integer)
            _jobsTotal = value
        End Set
    End Property

    Public WriteOnly Property AutoRefresh() As Boolean
        Set(ByVal value As Boolean)
            For Each job As Requerimiento In _wfs
                job.AutoRefresh = value
            Next
        End Set
    End Property



#End Region

#Region "Constructores"

    Dim df As New DatosFiltro
    Sub New(datosFiltro As DatosFiltro)
        df = datosFiltro
    End Sub

    Sub New(listaJobs As List(Of Requerimiento))
        _wfs = listaJobs
    End Sub

    Sub New(listadoJobs As Integer())
        _tipoAnalisis = TipoAnalisis.ListaJobs
        _listaJobs = listadoJobs
    End Sub

    Sub New(ByVal desde As Integer, ByVal hasta As Integer)
        _tipoAnalisis = TipoAnalisis.DesdeHastaJobs
        _desdeJob = desde
        _hastaJob = hasta
    End Sub

    Private _generarHistorial As Boolean = False
    Private _wfsHistorial As List(Of Requerimiento)
    Public Sub New(Optional GenerarHistorial As Boolean = False)
        If GenerarHistorial Then
            _generarHistorial = True
            _wfsHistorial = New List(Of Requerimiento)
            df.EstadoJob = String.Empty
        End If
        _tipoAnalisis = TipoAnalisis.HTML
    End Sub

    Sub New(listaJobsDto As RequerimientoDTO())
        _tipoAnalisis = TipoAnalisis.ListaJobsDTO
        _listaJobsDto = listaJobsDto
    End Sub

#End Region

    Private Sub EnlazarEventos(job As Requerimiento)
        AddHandler job.JobCargadoEvent, AddressOf NuevoJobsCargadoManejador
        AddHandler job.JobNoCargadoEvent, AddressOf JobNoCargadoManejador
        AddHandler job.JobRefrescadoEvent, AddressOf JobRefrescadoManejador
    End Sub

    Private Sub AnalizarListadoJobsPorNros(listadoJobs As Integer())
        Dim wf As Requerimiento = Nothing

        For Each nroj As String In listadoJobs
            wf = New Requerimiento()
            EnlazarEventos(wf)
            wf.JobId = Int32.Parse(nroj)
            _wfs.Add(wf)
            wf.Start()
        Next

        Dim _jobCompletos As Boolean = False
        Dim contador As Integer

        Do Until _jobCompletos
            contador = 0
            For Each r As Requerimiento In _wfs
                If r.CargaCompleta Then contador += 1
            Next
            If contador = _wfs.Count Then _jobCompletos = True
        Loop

    End Sub

    Public Async Function ExistenJobsNuevos(JobsSesion As GestorRequerimiento) As Task(Of Boolean)

        Dim result As Boolean = False
        Dim _listadoHtml As String = Await _clienteWeb.GetListadoJobs()
        Dim _cantidadVerificada As Integer = ObtenerJobsTotalListado(_listadoHtml)
        JobsSesion.JobsTotal = _cantidadVerificada


        Dim tsk As Task = Task.Factory.StartNew(Sub() AnalizarHtmlListadoNoCargaJobs(_listadoHtml))
        Await Task.Factory.ContinueWhenAll({tsk}, Sub()

                                                      Dim jobsCompletos As Boolean = False
                                                      Dim contador As Integer
                                                      'Dim listaNuevosJobs As New List(Of Requerimiento)

                                                      For Each job As Requerimiento In _wfs.FindAll(Function(x) x.EsColaPropia.Equals(True))
                                                          job.Start()
                                                      Next

                                                      Do Until jobsCompletos
                                                          contador = 0
                                                          For Each r As Requerimiento In _wfs.FindAll(Function(x) x.EsColaPropia.Equals(True))
                                                              If r.CargaCompleta Then contador += 1
                                                          Next
                                                          If contador = _wfs.FindAll(Function(x) x.EsColaPropia.Equals(True)).Count Then jobsCompletos = True
                                                      Loop

                                                      'For Each job As Requerimiento In _wfs.FindAll(Function(x) x.EsColaPropia.Equals(True))
                                                      '    If JobsSesion.Listar.Exists(Function(x) x.JobId.Equals(job.JobId)).Equals(False) Then
                                                      '        job.Start()
                                                      '        listaNuevosJobs.Add(job)
                                                      '    End If
                                                      'Next

                                                      'If listaNuevosJobs.Count > 0 Then
                                                      '    Do Until jobsCompletos
                                                      '        contador = 0
                                                      '        For Each r As Requerimiento In listaNuevosJobs
                                                      '            If r.CargaCompleta Then contador += 1
                                                      '        Next
                                                      '        If contador = listaNuevosJobs.Count Then jobsCompletos = True
                                                      '    Loop

                                                      '    For Each job As Requerimiento In listaNuevosJobs
                                                      '        JobsSesion.AddJob(job)
                                                      '        RaiseEvent NuevoJobEvent(job, New EventArgs())
                                                      '    Next

                                                      'End If

                                                  End Sub)

        'verifica diferencia en cantidad total > 5
        If JobsSesion.Listar.FindAll(Function(x) x.Estado.Equals("abierto")).Count > _cantidadVerificada Then
            If (JobsSesion.Listar.FindAll(Function(x) x.Estado.Equals("abierto")).Count - _cantidadVerificada) > 5 Then
                result = True
            End If
        Else
            If (_cantidadVerificada - JobsSesion.Listar.FindAll(Function(x) x.Estado.Equals("abierto")).Count) > 5 Then
                result = True
            End If
        End If

        'agrega los jobs nuevos encontrados y lanza evento de aviso
        For Each job As Requerimiento In _wfs.FindAll(Function(x) x.EsColaPropia.Equals(True))
            If JobsSesion.Listar.Exists(Function(x) x.JobId.Equals(job.JobId)) Then
                JobsSesion.Listar.Find(Function(x) x.JobId.Equals(job.JobId)).JobDto = job.JobDto
            Else
                JobsSesion.AddJob(job)
                RaiseEvent NuevoJobEvent(job, New EventArgs())
                result = True
            End If
        Next

        RaiseEvent JobsListadoCompletoEvent(Me, New EventArgs)

        Return result

    End Function

   
    'Public Async Function ExistenJobsNuevos(JobsSesion As GestorRequerimiento) As Task(Of Boolean)

    '    Dim result As Boolean = False
    '    Dim _listadoHtml As String = Await _clienteWeb.GetListadoJobs()
    '    Dim _cantidadVerificada As Integer = ObtenerJobsTotalListado(_listadoHtml)
    '    JobsSesion.JobsTotal = _cantidadVerificada


    '    Dim tsk As Task = Task.Factory.StartNew(Sub() AnalizarHtmlListado(_listadoHtml))
    '    Await Task.Factory.ContinueWhenAll({tsk}, Sub()
    '                                                  If _wfs.Count > 0 Then
    '                                                      Dim _jobCompletos As Boolean = False
    '                                                      Dim contador As Integer

    '                                                      Do Until _jobCompletos
    '                                                          contador = 0
    '                                                          For Each r As Requerimiento In _wfs
    '                                                              If r.CargaCompleta Then contador += 1
    '                                                          Next
    '                                                          If contador = _wfs.Count Then _jobCompletos = True
    '                                                      Loop
    '                                                  End If
    '                                              End Sub)

    '    'verifica diferencia en cantidad total > 5
    '    If JobsSesion.Listar.FindAll(Function(x) x.Estado.Equals("abierto")).Count > _cantidadVerificada Then
    '        If (JobsSesion.Listar.FindAll(Function(x) x.Estado.Equals("abierto")).Count - _cantidadVerificada) > 5 Then
    '            result = True
    '        End If
    '    Else
    '        If (_cantidadVerificada - JobsSesion.Listar.FindAll(Function(x) x.Estado.Equals("abierto")).Count) > 5 Then
    '            result = True
    '        End If
    '    End If

    '    'agrega los jobs nuevos encontrados y lanza evento de aviso
    '    For Each job As Requerimiento In _wfs.FindAll(Function(x) x.EsColaPropia.Equals(True))
    '        If JobsSesion.Listar.Exists(Function(x) x.JobId.Equals(job.JobId)) Then
    '            JobsSesion.Listar.Find(Function(x) x.JobId.Equals(job.JobId)).JobDto = job.JobDto
    '        Else
    '            JobsSesion.AddJob(job)
    '            RaiseEvent NuevoJobEvent(job, New EventArgs())
    '        End If
    '    Next

    '    RaiseEvent JobsListadoCompletoEvent(Me, New EventArgs)

    '    Return result

    'End Function

    Public Async Function ObtenerJobsActualizado(Job As Requerimiento) As Task(Of Requerimiento)

        Dim wfActulizado As Requerimiento = Nothing
        Dim _df As New DatosFiltro
        _df.OtrsId = Job.OtrsVinculado.ToString
        _df.EstadoJob = String.Empty
        Dim _listadoHtml As String = Await _clienteWeb.GetListadoJobs(_df)
        Dim tsk As Task = Task.Factory.StartNew(Sub() AnalizarHtmlListado(_listadoHtml))
        Await Task.Factory.ContinueWhenAll({tsk}, Sub()
                                                      If _wfs.Count > 0 Then
                                                          Dim _jobCompletos As Boolean = False
                                                          Dim contador As Integer

                                                          Do Until _jobCompletos
                                                              contador = 0
                                                              For Each r As Requerimiento In _wfs
                                                                  If r.CargaCompleta Then contador += 1
                                                              Next
                                                              If contador = _wfs.Count Then _jobCompletos = True
                                                          Loop

                                                          wfActulizado = (From wf In _wfs Select wf Where wf.JobId = Job.JobId).Single
                                                      End If
                                                  End Sub)

        Return wfActulizado

    End Function

    Private Sub JobNoCargadoManejador(sender As Object, e As EventArgs)
        Me._jobsTotal -= 1
        _wfs.Remove(DirectCast(sender, Requerimiento))
        RaiseEvent RequerimientoNoCargadoEvent(sender, e)
        If _wfs.Count = Me._jobsTotal Then
            RaiseEvent JobsListadoCompletoEvent(Me, e)
        End If
        DirectCast(sender, Requerimiento).Dispose()
        SyncLock Me
            Logger.WriteLog(String.Format("No se pudo cargar el job {0}", DirectCast(sender, Requerimiento).JobId.ToString))
        End SyncLock
    End Sub

    Protected Sub NuevoJobsCargadoManejador(sender As Object, e As EventArgs)
        SyncLock Me
            If _jobsProcesados < _jobsTotal Then
                _jobsProcesados += 1

                If _generarHistorial Then
                    _wfsHistorial.Add(DirectCast(sender, Requerimiento))
                    If _wfsHistorial.Count = 60 Then
                        SesionActualWindows.SesionActual.Serializar(_wfsHistorial)
                        _wfsHistorial = New List(Of Requerimiento)
                    End If
                End If

                RaiseEvent NuevoJobsCargadoEvent()
                If _jobsProcesados = _jobsTotal Then
                    RaiseEvent JobsListadoCompletoEvent(Me, e)
                End If
            End If
        End SyncLock
    End Sub

    Private Sub JobRefrescadoManejador(sender As Object, e As EventArgs)
        RaiseEvent JobsRefrescadoEvent()
    End Sub

    Private Sub AnalizarHtmlListadoNoCargaJobs(html As String) '(desde As Integer, hasta As Integer)
        Try
            _jobsTotal = ObtenerJobsTotalListado(html)
            Dim wf As Requerimiento = Nothing
            ' Buscar coincidencias
            Dim re As New Regex("<p>(\d+)<\/p></td>|align=left>(.*)<\/td>", CType(RegexOptions.IgnoreCase, RegexOptions))
            ' obtener la colección de resultados
            Dim mc As MatchCollection = re.Matches(html)
            Dim contadorCap As Integer = 0

            For Each m As Match In mc

                If Not String.IsNullOrEmpty(m.Groups(1).Value) Then
                    wf = New Requerimiento()
                    EnlazarEventos(wf)
                    wf.JobId = Int32.Parse(m.Groups(1).Value)
                    'wf.Start()
                    _wfs.Add(wf)
                    contadorCap = 0
                End If

                Select Case contadorCap
                    Case 5
                        wf.Instancia = m.Groups(2).Value
                    Case 6
                        wf.Cola = m.Groups(2).Value
                End Select
                contadorCap += 1
            Next

            If _wfs.Count = 12000 Then
                _wfs = New List(Of Requerimiento)
            End If

        Catch ex As Exception
            'Logger.WriteLogExeption(ex)
            MsgBox(String.Format("Error AnalizadorHtml: {0}", ex.Message))
        End Try

    End Sub

    Private Sub AnalizarHtmlListado(html As String) '(desde As Integer, hasta As Integer)
        Try
            _jobsTotal = ObtenerJobsTotalListado(html)
            Dim wf As Requerimiento = Nothing
            ' Buscar coincidencias
            Dim re As New Regex("<p>(\d+)<\/p></td>|align=left>(.*)<\/td>", CType(RegexOptions.IgnoreCase, RegexOptions))
            ' obtener la colección de resultados
            Dim mc As MatchCollection = re.Matches(html)
            Dim contadorCap As Integer = 0

            For Each m As Match In mc

                If Not String.IsNullOrEmpty(m.Groups(1).Value) Then
                    wf = New Requerimiento()
                    EnlazarEventos(wf)
                    wf.JobId = Int32.Parse(m.Groups(1).Value)
                    wf.Start()
                    _wfs.Add(wf)
                    contadorCap = 0
                End If

                Select Case contadorCap
                    Case 5
                        wf.Instancia = m.Groups(2).Value
                    Case 6
                        wf.Cola = m.Groups(2).Value
                End Select
                contadorCap += 1
            Next

            If _wfs.Count = 12000 Then
                _wfs = New List(Of Requerimiento)
            End If

        Catch ex As Exception
            'Logger.WriteLogExeption(ex)
            MsgBox(String.Format("Error AnalizadorHtml: {0}", ex.Message))
        End Try

    End Sub

    Private Async Sub Paginador(Optional html As String = Nothing)
        Try
            If html Is Nothing Then
                html = Await _clienteWeb.GetListadoJobs(df)
            End If

            Dim mc1 As MatchCollection = Regex.Matches(html, "<td align=right>(.*?)<\/td>")
            Dim mc2 As MatchCollection = Regex.Matches(html, "<td align=left>(.*?)<\/td>")

            _jobsTotal = Integer.Parse(Regex.Matches(mc1.Item(1).Groups(1).Value, "\d+").Item(0).Groups(0).Value)

            While df.IndiceInf < _jobsTotal

                AnalizarHtmlListado(html)
                df.Avanzar()
                html = Await _clienteWeb.GetListadoJobs(df)
            End While

        Catch exProcesoCancelado As System.Threading.Tasks.TaskCanceledException
            System.Threading.Thread.Sleep(Convert.ToInt32(3000 * Rnd()))
            Paginador(html)
        Catch exHttpRequest As System.Net.Http.HttpRequestException
            System.Threading.Thread.Sleep(Convert.ToInt32(3000 * Rnd()))
            Paginador(html)
        Catch exAE As System.AggregateException
            System.Threading.Thread.Sleep(Convert.ToInt32(3000 * Rnd()))
            Paginador(html)
        Catch ex As Exception
            System.Threading.Thread.Sleep(Convert.ToInt32(3000 * Rnd()))
            Paginador(html)
            'MsgBox(String.Format("Error Paginador: {0}", ex.Message))
        End Try
    End Sub

    Public Function Listar() As List(Of Requerimiento)
        Return _wfs
    End Function

    Function FiltrarPorTexto(txt As String) As List(Of Requerimiento)

        Dim result As List(Of Requerimiento) = Nothing

        If String.IsNullOrWhiteSpace(txt) Then
            result = _wfs
        Else
            txt = txt.ToLower.Trim
            result = (From wf In _wfs Select wf Where _
                   wf.JobId.ToString.ToLower.Contains(txt) Or _
                   wf.Cola.ToLower.Contains(txt) Or _
                   wf.Descripcion.ToLower.Contains(txt) Or _
                   wf.Estado.ToLower.Contains(txt) Or _
                   wf.FechaIngreso.ToLower.Contains(txt) Or _
                   wf.Ingreso.ToLower.Contains(txt) Or _
                   wf.Instancia.ToLower.Contains(txt) Or _
                   wf.Modifico.ToLower.Contains(txt) Or _
                   wf.OtrsVinculado.ToString.ToLower.Contains(txt) Or _
                   wf.Proceso.ToLower.Contains(txt) Or _
                   wf.Solicitante.ToLower.Contains(txt) Or _
                   wf.TiempoInstancia.ToLower.Contains(txt) Or _
                   wf.TiempoTotal.ToLower.Contains(txt) Or _
                   wf.JobHtml.ToLower.Contains(txt)).ToList()
        End If

        Return result

    End Function

    Public Sub AddJob(value As Requerimiento)
        If _wfs.Find(Function(x) x.JobId.Equals(value.JobId)) Is Nothing Then
            _wfs.Add(value)
            EnlazarEventos(value)
        End If

    End Sub

    Public Sub RemoveJob(value As Requerimiento)
        Dim _jobs As New List(Of Requerimiento)

        For Each job As Requerimiento In _wfs
            If job.Equals(value).Equals(False) Then
                _jobs.Add(job)
            End If
        Next

        _wfs = _jobs

    End Sub

    Public Function ObtenerProgreso() As String
        Dim _progreso As String
        Dim _porcentaje As Double

        If Me.JobsTotal > 0 Then
            _porcentaje = (Me.JobsProcesados / Me.JobsTotal)
        End If

        _progreso = String.Format("Cargando {2} Job {0}/{1}", _
                                  Me.JobsProcesados, _
                                  Me.JobsTotal, _
                                  _porcentaje.ToString("00.00%"))
        Return _progreso

    End Function

#Region "Asincronico"

    Protected Overrides Async Sub OnStart()

        Select Case _tipoAnalisis
            Case TipoAnalisis.HTML
                Dim html As String = Await _clienteWeb.GetListadoJobs()
                _jobsTotal = ObtenerJobsTotalListado(html)
                Paginador()

            Case TipoAnalisis.ListaJobs

                If _listaJobs.Length > 0 Then Me._jobsTotal = _listaJobs.Length
                AnalizarListadoJobsPorNros(_listaJobs)

            Case TipoAnalisis.DesdeHastaJobs

                Dim listadoJobs As New List(Of Integer)
                For index As Integer = _desdeJob To _hastaJob
                    listadoJobs.Add(index)
                Next
                Me._jobsTotal = listadoJobs.Count
                AnalizarListadoJobsPorNros(listadoJobs.ToArray)

            Case TipoAnalisis.ListaJobsDTO
                _jobsTotal = _listaJobsDto.Length

                'si son menos de 1000 wfs en archivo cache espero 1 seg
                If _jobsTotal < 1000 Then
                    System.Threading.Thread.Sleep(500)
                End If

                For index As Integer = 0 To _listaJobsDto.Length - 1
                    Dim r As New Requerimiento()
                    _wfs.Add(r)
                    'AddHandler r.JobCargadoEvent, AddressOf NuevoJobsCargadoManejador
                    EnlazarEventos(r)
                    r.JobDto = _listaJobsDto(index)
                Next

        End Select

    End Sub

    Private Function ObtenerJobsTotalListado(html As String) As Integer
        Dim result As Integer
        Dim mc1 As MatchCollection = Regex.Matches(html, "<td align=right>(.*?)<\/td>")
        If mc1.Count > 0 Then
            result = Integer.Parse(Regex.Matches(mc1.Item(1).Groups(1).Value, "\d+").Item(0).Groups(0).Value)
        End If

        Return result
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                _jobsTotal = df.IndiceInf

                ' TODO: eliminar estado administrado (objetos administrados).
                For Each r As Requerimiento In _wfs
                    'r = Nothing
                    r.Dispose()
                Next
            End If
            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region


End Class
