Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Http
Imports System.Text
Imports System.IO
Imports System.Threading.Tasks


Public Class ClienteWeb

    'http://blog.cleverswine.net/2013/08/06/using-cookies-with-httpclient/
    Private Shared _cookieContainer As CookieContainer
    Private Shared _handler As HttpClientHandler

    Private Shared ReadOnly Property Handler() As HttpClientHandler
        Get
            If IsNothing(_handler) Then
                _cookieContainer = New CookieContainer()
                _handler = New HttpClientHandler()
                _handler.CookieContainer = _cookieContainer
                _handler.UseCookies = True
                _handler.AllowAutoRedirect = True
            End If

            Return _handler
        End Get
    End Property

    Public Shared Async Function LogOn() As Threading.Tasks.Task(Of String)

        Dim result As String = String.Empty
        'Where we are posting to:
        Dim _uri As New Uri("https://main.cajval.sba.com.ar/wf/rg_logon_submit.asp")

        Dim _cliente As HttpClient = GetClient()

        'Create an Http client and set the headers we want
        _cliente.DefaultRequestHeaders.Add("Referer", "https://main.cajval.sba.com.ar/wf/rg_logon_form.asp")

        Dim datosUsuario As String = String.Format("vc_usuario_id={0}&vc_clave_id={1}&vc_aplicacion_id=SERVICIOS", _
                                                  SesionActualWindows.SesionActual.ObtenerUsuarioActual.Nombre, _
                                                  SesionActualWindows.SesionActual.ObtenerUsuarioActual.Clave)

        Dim _content As New StringContent(datosUsuario, Encoding.UTF8, "application/x-www-form-urlencoded")

        'Post the data
        Dim aResponse As HttpResponseMessage = Await _cliente.PostAsync(_uri, _content)

        If (aResponse.IsSuccessStatusCode) Then
            aResponse.Content.LoadIntoBufferAsync().Wait()
            Dim receiveStream As Stream = aResponse.Content.ReadAsStreamAsync().Result
            Dim readStream As StreamReader = New StreamReader(receiveStream, Encoding.GetEncoding("Windows-1252"))
            result = readStream.ReadToEnd()
        Else
            'show the response status code
            result = "HTTP Status: " + aResponse.StatusCode.ToString() + " - Reason: " + aResponse.ReasonPhrase
        End If

        'AgregarHostEnHTML(result)

        Return result

    End Function

    Public Shared Function GetClient() As HttpClient

        'the second param prevents the Handler from being disposed with the client
        Dim client As New HttpClient(Handler, False)
        client.DefaultRequestHeaders.Add("Accept", "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*")
        client.DefaultRequestHeaders.Add("Accept-Language", "es-AR")
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)")
        client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate")
        client.DefaultRequestHeaders.Add("Host", "main.cajval.sba.com.ar")
        client.DefaultRequestHeaders.Add("Connection", "Keep-Alive")
        client.DefaultRequestHeaders.Add("Cache-Control", "no-cache")

        'client.Timeout = New TimeSpan(0, 0, 5)

        Return client

    End Function

    'Private Shared Sub VerificarSesion()
    '    If Handler.CookieContainer.Count = 0 Then
    '        LogOn().Wait()
    '    End If

    '    For Each ck As Cookie In Handler.CookieContainer.GetCookies(New Uri("https://main.cajval.sba.com.ar"))
    '        If ck.Expired Then
    '            LogOn().Wait()
    '        End If
    '    Next

    'End Sub

    Public Async Function GetJobHtml(JobId As Integer) As Threading.Tasks.Task(Of String)

        'Await LogOn()

        Dim result As String = String.Empty
        'Where we are posting to:
        Dim _uri As New Uri("https://main.cajval.sba.com.ar/wf/workflow/wf_ampliar_descripcion.asp?job_id=" & JobId)
        'Create an Http client and set the headers we want
        Dim _cliente As HttpClient = GetClient()

        Dim _content As New StringContent(String.Empty, Encoding.UTF8, "application/x-www-form-urlencoded")

        Try
            'Post the data
            Dim aResponse As HttpResponseMessage = Await _cliente.PostAsync(_uri, _content)

            If (aResponse.IsSuccessStatusCode) Then
                aResponse.Content.LoadIntoBufferAsync().Wait()
                Dim receiveStream As Stream = aResponse.Content.ReadAsStreamAsync().Result
                Dim readStream As StreamReader = New StreamReader(receiveStream, Encoding.GetEncoding("Windows-1252"))
                result = readStream.ReadToEnd()

                If result.Contains("Error N°") Then
                    Await LogOn()
                    result = GetJobHtml(JobId).Result
                End If

            Else
                'show the response status code
                result = "HTTP Status: " + aResponse.StatusCode.ToString() + " - Reason: " + aResponse.ReasonPhrase
            End If

        Catch exRequest As HttpRequestException
            Throw exRequest
        Catch ae As AggregateException
            Throw ae
        Catch ex As Exception
            Throw ex
        End Try

        AgregarHostEnHTML(result)

        Return result

    End Function

    'POST /wf/workflow/wf_listado_solicitudes_servicio.asp HTTP/1.1
    'Accept: image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*
    'Referer: https://main.cajval.sba.com.ar/wf/workflow/wf_listado_solicitudes_servicio.asp
    'Accept-Language: es-AR
    'User-Agent: Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)
    'Content-Type: application/x-www-form-urlencoded
    'Accept-Encoding: gzip, deflate
    'Host: main.webapp.sba.com.ar
    'Content-Length: 175
    'Connection: Keep-Alive
    'Cache-Control: no-cache
    'Cookie: registro=log%2Daccesos=27&ultimo%5Facceso=13%2F10%2F2015+14%3A13%3A57&llave=6yqhNdc1oGdhVR6v&log%2Dsesiones=1&vista=usuarios%2Dinformatica&usuario=gmedina; ASPSESSIONIDSCCASSTT=HJPDDNHCBFPFBECBFKHBMNAO; servicios=cod3%2Derror=&cod2%2Derror=&cod1%2Derror=

    'indice_inf=0&indice_sup=30&ordenarpor=2&orden=1&solicitante_id=&proceso_id=&ver_campo4=on&estado_job=abierto&instancia_id=&ver_campo6=on&usuario=gmedina&otrs_id=&buscar_texto=
    'indice_inf=0&indice_sup=30&ordenarpor=2&orden=1&ver_campo1=on&solicitante_id=idawidowski&ver_campo3=on&proceso_id=F0203-AB.SUBUSUARIO.LOGN&ver_campo4=on&estado_job=abierto&ver_campo5=on&instancia_id=Ejecutar+tarea&ver_campo6=on&usuario=gmedina&otrs_id=12345&buscar_texto=prueba-texto

    Public Async Function GetListadoJobs(Optional df As DatosFiltro = Nothing) As Threading.Tasks.Task(Of String)

        'Await LogOn()

        Dim result As String = String.Empty

        'Where we are posting to:
        Dim _uri As New Uri("https://main.cajval.sba.com.ar/wf/workflow/wf_listado_solicitudes_servicio.asp")

        'Create an Http client and set the headers we want
        Dim _cliente As HttpClient = GetClient()

        _cliente.DefaultRequestHeaders.Add("Referer", "https://main.cajval.sba.com.ar/wf/workflow/wf_listado_solicitudes_servicio.asp")

        If df Is Nothing Then
            df = New DatosFiltro
        End If

        Dim _content As New StringContent(df.ToString, Encoding.UTF8, "application/x-www-form-urlencoded")

        'Post the data
        Dim aResponse As HttpResponseMessage = Await _cliente.PostAsync(_uri, _content)

        If (aResponse.IsSuccessStatusCode) Then
            aResponse.Content.LoadIntoBufferAsync().Wait()
            Dim receiveStream As Stream = aResponse.Content.ReadAsStreamAsync().Result
            Dim readStream As StreamReader = New StreamReader(receiveStream, Encoding.GetEncoding("Windows-1252"))
            result = readStream.ReadToEnd()

            If result.Contains("Error N°") Then
                Await LogOn()
                result = Await GetListadoJobs(df)
            End If

        Else
            'show the response status code
            result = "HTTP Status: " + aResponse.StatusCode.ToString() + " - Reason: " + aResponse.ReasonPhrase
        End If

        AgregarHostEnHTML(result)

        Return result

    End Function

    Public Async Function EjecutarJob(JobId As Integer, comentario As String) As Task

        Await LogOn()

        Dim result As String = String.Empty
        'Where we are posting to:
        Dim _uri As New Uri("https://main.cajval.sba.com.ar/wf/workflow/wf_ejecucion_job_submit.asp")
        'Create an Http client and set the headers we want
        Dim _cliente As HttpClient = GetClient()

        'job_id=49916&accion=confirmo&descripcion=

        PrepararComentario(comentario)

        Dim _content As New StringContent("job_id=" & JobId.ToString & "&accion=confirmo&descripcion=" & comentario, Encoding.UTF8, "application/x-www-form-urlencoded")

        Try
            'Post the data
            Dim aResponse As HttpResponseMessage = Await _cliente.PostAsync(_uri, _content)

            If (aResponse.IsSuccessStatusCode) Then
                aResponse.Content.LoadIntoBufferAsync().Wait()
                Dim receiveStream As Stream = aResponse.Content.ReadAsStreamAsync().Result
                Dim readStream As StreamReader = New StreamReader(receiveStream, Encoding.GetEncoding("Windows-1252"))
                result = readStream.ReadToEnd()

                If result.Contains("Error N°") Then
                    Await LogOn()
                    Await EjecutarJob(JobId, comentario)
                End If
            Else
                'show the response status code
                result = "HTTP Status: " + aResponse.StatusCode.ToString() + " - Reason: " + aResponse.ReasonPhrase
            End If

        Catch exRequest As HttpRequestException
            Throw exRequest
        Catch ae As AggregateException
            Throw ae
        Catch ex As Exception
            Throw ex
        End Try

        'AgregarHostEnHTML(result)

        'Return result

    End Function

    'Public Async Function GetJobHtmlTomar(JobId As Integer) As Threading.Tasks.Task(Of String)

    '    Dim result As String = String.Empty
    '    'Where we are posting to:
    '    Dim _uri As New Uri("https://main.cajval.sba.com.ar/wf/workflow/wf_tomar_tarea.asp?job_id=" & JobId)
    '    'Create an Http client and set the headers we want
    '    Dim _cliente As HttpClient = GetClient()

    '    Dim _content As New StringContent(String.Empty, Encoding.UTF8, "application/x-www-form-urlencoded")

    '    Try
    '        'Post the data
    '        Dim aResponse As HttpResponseMessage = Await _cliente.PostAsync(_uri, _content)

    '        If (aResponse.IsSuccessStatusCode) Then
    '            aResponse.Content.LoadIntoBufferAsync().Wait()
    '            Dim receiveStream As Stream = aResponse.Content.ReadAsStreamAsync().Result
    '            Dim readStream As StreamReader = New StreamReader(receiveStream, Encoding.GetEncoding("Windows-1252"))
    '            result = readStream.ReadToEnd()

    '            If result.Contains("Error N°") Then
    '                Await LogOn()
    '                Await GetJobHtmlTomar(JobId)
    '            End If
    '        Else
    '            'show the response status code
    '            result = "HTTP Status: " + aResponse.StatusCode.ToString() + " - Reason: " + aResponse.ReasonPhrase
    '        End If

    '    Catch exRequest As HttpRequestException
    '        Throw exRequest
    '    Catch ae As AggregateException
    '        Throw ae
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    '    AgregarHostEnHTML(result)

    '    Return result

    'End Function

    Public Async Function TomarJob(JobId As Integer, comentario As String) As Threading.Tasks.Task

        Await LogOn()

        Dim result As String = String.Empty
        'Where we are posting to:
        Dim _uri As New Uri("https://main.cajval.sba.com.ar/wf/workflow/wf_tomar_tarea_submit.asp")
        'Create an Http client and set the headers we want
        Dim _cliente As HttpClient = GetClient()

        PrepararComentario(comentario)

        Dim _content As New StringContent("job_id=" & JobId.ToString & "&accion=aceptar&descripcion=" & comentario, Encoding.UTF8, "application/x-www-form-urlencoded")

        Try
            'Post the data
            Dim aResponse As HttpResponseMessage = Await _cliente.PostAsync(_uri, _content)

            If (aResponse.IsSuccessStatusCode) Then
                aResponse.Content.LoadIntoBufferAsync().Wait()
                Dim receiveStream As Stream = aResponse.Content.ReadAsStreamAsync().Result
                Dim readStream As StreamReader = New StreamReader(receiveStream, Encoding.GetEncoding("Windows-1252"))
                result = readStream.ReadToEnd()

                If result.Contains("Error N°") Then
                    Await LogOn()
                    Await TomarJob(JobId, comentario)
                End If
            Else
                'show the response status code
                result = "HTTP Status: " + aResponse.StatusCode.ToString() + " - Reason: " + aResponse.ReasonPhrase
            End If

        Catch exRequest As HttpRequestException
            Throw exRequest
        Catch ae As AggregateException
            Throw ae
        Catch ex As Exception
            Throw ex
        End Try

        AgregarHostEnHTML(result)

        'Return result

    End Function

    Public Async Function AprobarJob(JobId As Integer, comentario As String) As Threading.Tasks.Task

        Await LogOn()

        Dim result As String = String.Empty
        'Where we are posting to:
        Dim _uri As New Uri("https://main.cajval.sba.com.ar/wf/workflow/wf_aprobacion_job_submit.asp")
        'Create an Http client and set the headers we want
        Dim _cliente As HttpClient = GetClient()

        'job_id=49916&accion=aceptar&descripcion=

        PrepararComentario(comentario)

        Dim _content As New StringContent("job_id=" & JobId.ToString & "&accion=aprobar&descripcion=" & comentario, Encoding.UTF8, "application/x-www-form-urlencoded")

        Try
            'Post the data
            Dim aResponse As HttpResponseMessage = Await _cliente.PostAsync(_uri, _content)

            If (aResponse.IsSuccessStatusCode) Then
                aResponse.Content.LoadIntoBufferAsync().Wait()
                Dim receiveStream As Stream = aResponse.Content.ReadAsStreamAsync().Result
                Dim readStream As StreamReader = New StreamReader(receiveStream, Encoding.GetEncoding("Windows-1252"))
                result = readStream.ReadToEnd()

                If result.Contains("Error N°") Then
                    Await LogOn()
                    Await AprobarJob(JobId, comentario)
                End If
            Else
                'show the response status code
                result = "HTTP Status: " + aResponse.StatusCode.ToString() + " - Reason: " + aResponse.ReasonPhrase
            End If

        Catch exRequest As HttpRequestException
            Throw exRequest
        Catch ae As AggregateException
            Throw ae
        Catch ex As Exception
            Throw ex
        End Try

        AgregarHostEnHTML(result)

        'Return result

    End Function

    'https://main.cajval.sba.com.ar/wf/workflow/wf_listado_solicitudes_servicio.asp

    Public Shared Async Function GetJobHtmlMenuFiltro() As Threading.Tasks.Task(Of String)

        Await LogOn()

        Dim result As String = String.Empty
        'Where we are posting to:
        Dim _uri As New Uri("https://main.cajval.sba.com.ar/wf/workflow/wf_listado_solicitudes_servicio.asp")
        'Create an Http client and set the headers we want
        Dim _cliente As HttpClient = GetClient()

        Dim _content As New StringContent(String.Empty, Encoding.UTF8, "application/x-www-form-urlencoded")

        Try
            'Post the data
            Dim aResponse As HttpResponseMessage = Await _cliente.PostAsync(_uri, _content)

            If (aResponse.IsSuccessStatusCode) Then
                aResponse.Content.LoadIntoBufferAsync().Wait()
                Dim receiveStream As Stream = aResponse.Content.ReadAsStreamAsync().Result
                Dim readStream As StreamReader = New StreamReader(receiveStream, Encoding.GetEncoding("Windows-1252"))
                result = readStream.ReadToEnd()

                If result.Contains("Error N°") Then
                    Await LogOn()
                    result = Await GetJobHtmlMenuFiltro()
                End If
            Else
                'show the response status code
                result = "HTTP Status: " + aResponse.StatusCode.ToString() + " - Reason: " + aResponse.ReasonPhrase
            End If

        Catch exRequest As HttpRequestException
            Throw exRequest
        Catch ae As AggregateException
            Throw ae
        Catch ex As Exception
            Throw ex
        End Try

        AgregarHostEnHTML(result)

        SoloFormulario(result)

        Return result

    End Function

    Private Shared Sub AgregarHostEnHTML(ByRef result As String)
        If result.Contains("../") Then
            result = Replace(result, "../", "https://main.cajval.sba.com.ar/wf/")
        End If

        'https://main.cajval.sba.com.ar/wf//upload/wf_l_archivo.asp?id=407
        '/wf//upload/
        If result.Contains("<a href=/wf//upload/") Then
            result = Replace(result, "<a href=/wf//upload/", "<a href=https://main.cajval.sba.com.ar/wf//upload/")
        End If

    End Sub

    Private Shared Sub PrepararComentario(ByRef coment As String)
        If coment.Trim.Contains(" ") Then
            coment = Replace(coment, " ", "+")
        End If
    End Sub

    Private Shared Sub SoloFormulario(ByRef htmlMenu As String)
        htmlMenu = htmlMenu.Remove(htmlMenu.IndexOf("<!-- Cargar menu -->"), 4534)
        htmlMenu = htmlMenu.Remove(htmlMenu.IndexOf("<INPUT onclick=javascript:aplicar_filtro() class=botonFormVentana type=button value=Aplicar id=button1 name=button1>"), 116)
    End Sub


End Class
