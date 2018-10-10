Imports System.Text.RegularExpressions

Public MustInherit Class UsuarioCredencial

    Private _job As Requerimiento
    Public Property Job() As Requerimiento
        Get
            Return _job
        End Get
        Set(ByVal value As Requerimiento)
            _job = value
            ObtenerDatos(Job.JobHtml)
        End Set
    End Property

    Property DireccionCorreo As String
    Protected Property TextoCorreo As String
    Property NroRequerimiento As String = "0"
    Protected Property FirmaCorreo As String
    Property ListaCorreos As New List(Of String)
    Property ListaUsuarios As New List(Of String)
    Property SinExtension As Boolean
    Property Sistema As String

    Sub New()
        FirmaCorreo = "Saludos cordiales." & vbCrLf & _
        "Gestión de Claves" & vbCrLf & _
        "Caja de Valores S.A." & vbCrLf & _
        "Por favor no imprimas este e-mail si no es necesario." & vbCrLf & _
        "Ante cualquier duda o consulta relacionada, puede comunicarse con nuestro Centro de Atención de Usuarios al 4317 8998, o por correo electrónico a mesadeayuda@cau.sba.com.ar."
    End Sub

    'https://micsharp.wordpress.com/
    Protected Sub Ejecutar(strEXE As String, strArgumentos As String)
        'Armar el proceso a ejecutar
        Dim startInfo As ProcessStartInfo = New ProcessStartInfo(strEXE, strArgumentos)

        'Para poder manupular la salida indicamos que no se ejecute el shell
        startInfo.UseShellExecute = False

        '(...)UseShellExecute debe ser true si se desea establecer ErrorDialog en true(...)
        startInfo.ErrorDialog = False

        'Sin ventana...
        startInfo.CreateNoWindow = True

        'Deseamos manipular la salida del proceso, para ello debemos establecer que se redirija la salida
        startInfo.RedirectStandardOutput = True

        Try
            Dim p As Diagnostics.Process = System.Diagnostics.Process.Start(startInfo)

            'Leemos la salida (objeto StreamReader)
            Dim sr As System.IO.StreamReader = p.StandardOutput
            Dim cadenaSalida As String = sr.ReadToEnd()
            sr.Close()

            'La visualizamos en el textbox. Un ejemplo basico ;)...
            'MsgBox(cadenaSalida)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Enviar(strArgumentos As String)
        Ejecutar(Application.StartupPath & "\sendEmail-v156\sendEmail.exe", "-f gestiondeclaves@cajval.sba.com.ar -bcc gestiondeclaves@cajval.sba.com.ar " & strArgumentos)
        'Ejecutar(Application.StartupPath & "\sendEmail-v156\sendEmail.exe", "-f gmedina@cajval.sba.com.ar -bcc gmedina@cajval.sba.com.ar " & strArgumentos)
    End Sub

    MustOverride Sub Enviar()
    Protected MustOverride Sub GenerarTextoCorreo()

    Shared Function GetPasswd() As String
        Dim letra As Char
        Dim longitudnuevacadena As Integer = 8
        Dim nuevacadena As String = ""
        Dim obj As New Random()

        Dim posibles As String = "ABCDEFGHJKLMNPQRSTUVWXYZ"
        Dim longitud As Integer = posibles.Length
        For i As Integer = 0 To 2
            letra = posibles(obj.[Next](longitud))
            nuevacadena += letra.ToString()
        Next

        posibles = "*-+#$%_"
        longitud = posibles.Length
        letra = posibles(obj.[Next](longitud))
        nuevacadena += letra.ToString()

        posibles = "123456789"
        longitud = posibles.Length
        For i As Integer = 0 To 1
            letra = posibles(obj.[Next](longitud))
            nuevacadena += letra.ToString()
        Next

        posibles = "*-+#$%_123456789ABCDEFGHJKLMNPQRSTUVWXYZ123456789*-+#$%_"
        longitud = posibles.Length
        For i As Integer = 0 To 1
            letra = posibles(obj.[Next](longitud))
            nuevacadena += letra.ToString()
        Next

        Threading.Thread.Sleep(50)
        Return nuevacadena

    End Function

    Private Sub ObtenerDatos(ByVal DocumentText As String)
        Dim re As Regex
        Dim mc As MatchCollection
        Dim m As Match

        Dim PATRON_CORREOS As String = "[a-z\.\-_]+@[a-z\.\-_]+"

        re = New Regex(PATRON_CORREOS, RegexOptions.IgnoreCase)

        ' obtener la colección de resultados
        mc = re.Matches(DocumentText)

        For Each m In mc
            If Not String.IsNullOrEmpty(m.Captures(0).Value) Then
                If Me.ListaCorreos.Find(Function(x) x.Equals(m.Captures(0).Value)) Is Nothing Then
                    Me.ListaCorreos.Add(m.Captures(0).Value)
                End If
            End If
        Next

        Me.NroRequerimiento = Job.JobId.ToString

    End Sub

End Class
