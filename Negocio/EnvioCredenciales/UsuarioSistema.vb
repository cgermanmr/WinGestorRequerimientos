Imports System.IO

Public Class UsuarioSistema
    Inherits UsuarioCredencial

    Property NombreUsuario As String
    Property UsuarioTWS As String
    Property NombreYApellido As String
    Property NombreArchivo As String
    Property Clave As String
    Property ClaveZip As String

    Private _sistema As String
    Public Shadows Property Sistema() As String
        Get
            Return _sistema
        End Get
        Set(ByVal value As String)
            _sistema = value
            If value.Contains("USUARIOS") Then _sistema = "USUARIOS"
            If value.Contains("TP8") Then _sistema = "TP8"

        End Set
    End Property



    Sub New()
        _Clave = UsuarioCredencial.GetPasswd
        _ClaveZip = UsuarioCredencial.GetPasswd
        Me.Sistema = "USUARIOS"
    End Sub


    Public Sub EnviarUsuarioInterno()
        If String.IsNullOrEmpty(NombreYApellido) Or String.IsNullOrEmpty(NombreUsuario) Or String.IsNullOrEmpty(Sistema) Then
            Throw New Exception("Faltan datos del usuario")
            Exit Sub
        End If

        GenerarTextoUsuarioInterno()

        Dim strArgumentos As String = "-u ""WF - " & Me.NroRequerimiento.ToString & """ -t " & Me.DireccionCorreo & " -s smtpsrv3.sba.com.ar:25 -o reply-to=noresponder@cajval.sba.com.ar -o message-file=""" & TextoCorreo & ".txt"" -o message-charset=UTF8"
        'Dim strArgumentos As String = "-f gestiondeclaves@cajval.sba.com.ar -bcc gestiondeclaves@cajval.sba.com.ar -u ""WF - " & Me.NroRequerimiento.ToString & """ -t " & Me.DireccionCorreo & " -a """ & NombreArchivo & ".zip"" -s smtpsrv3.sba.com.ar:25 -o reply-to=mesadeayuda@cau.sba.com.ar -o message-file=""" & TextoCorreo & ".txt"" -o message-charset=UTF8"
        MyBase.Enviar(strArgumentos)
    End Sub

    Public Sub GenerarTextoUsuarioInterno()

        Dim sw As System.IO.StreamWriter = Nothing
        Try
            'check the file
            If Not System.IO.Directory.Exists(Application.StartupPath & "\claves\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\claves\")
            End If

            Me.TextoCorreo = Application.StartupPath & "\claves\Mensaje WF - " & NroRequerimiento.ToString & " - " & NombreUsuario
            Dim fic As String = TextoCorreo & ".txt"

            sw = New System.IO.StreamWriter(fic, False)
            sw.WriteLine("Estimado," & vbCrLf)
            sw.WriteLine("Por la presente, comunicamos a Ud. que en respuesta a vuestra nota de pedido se ha procedido a habilitar al usuario del sistema " & Me.Sistema & ", del que se adjunta su identificación de conexión y clave personal." & vbCrLf)
            sw.WriteLine("NOMBRE Y APELLIDO: " & NombreYApellido)
            sw.WriteLine("USUARIO: " & NombreUsuario)
            sw.WriteLine("CLAVE: " & Clave)
            sw.WriteLine()
            sw.WriteLine("Sin otro particular, saluda a Ud. muy Atte.")
            sw.WriteLine()
            sw.WriteLine("Gestión de Claves")
            sw.WriteLine("Caja de Valores S.A.")
            sw.WriteLine("Por favor no imprimas este e-mail si no es necesario.")

        Catch ex As Exception
            MsgBox("Descripción: " & ex.Message, CType(vbOKOnly + MsgBoxStyle.Information, MsgBoxStyle))
            Logger.WriteLogExeption(ex)
        Finally
            ' Close the file and release resources.
            sw.Close()
        End Try

    End Sub

    Public Overrides Sub Enviar()

        If String.IsNullOrEmpty(NombreYApellido) Or String.IsNullOrEmpty(NombreUsuario) Or String.IsNullOrEmpty(Sistema) Then
            Throw New Exception("Faltan datos del usuario")
            Exit Sub
        End If

        GenerarTextoCorreo()
        GenerarZipConClave()

        If SinExtension Then
            Rename(NombreArchivo & ".zip", NombreArchivo)
        Else
            NombreArchivo = NombreArchivo & ".zip"
        End If

        'Dim strArgumentos As String = "-u ""WF - " & Me.NroRequerimiento.ToString & """ -t " & Me.DireccionCorreo & " -a """ & NombreArchivo & ".zip"" -s smtpsrv3.sba.com.ar:25 -o reply-to=noresponder@cajval.sba.com.ar -o message-file=""" & TextoCorreo & ".txt"" -o message-charset=UTF8"
        Dim strArgumentos As String = "-u ""WF - " & NroRequerimiento.ToString & """ -t " & DireccionCorreo & " -a """ & NombreArchivo & """ -s smtpsrv3.sba.com.ar:25 -o reply-to=noresponder@cajval.sba.com.ar -o message-file=""" & TextoCorreo & ".txt"" -o message-charset=UTF8"

        MyBase.Enviar(strArgumentos)
    End Sub

    Private Sub GenerarZipConClave()

        Dim sw As System.IO.StreamWriter = Nothing
        Try
            'check the file
            If Not System.IO.Directory.Exists(Application.StartupPath & "\claves\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\claves\")
            End If

            NombreArchivo = Application.StartupPath & "\claves\WF - " & NroRequerimiento.ToString & " - " & NombreUsuario
            Dim fic As String = NombreArchivo & ".txt"

            sw = New System.IO.StreamWriter(fic, False)
            sw.WriteLine("NOMBRE Y APELLIDO: " & NombreYApellido)
            sw.WriteLine("USUARIO: " & NombreUsuario)
            sw.WriteLine("CLAVE: " & Clave)

            If String.IsNullOrEmpty(Me.UsuarioTWS).Equals(False) Then
                sw.WriteLine()
                sw.WriteLine("Usuario TWS: " & UsuarioTWS)
                sw.WriteLine("CLAVE: VER instructivo TWS.docx")
                sw.Close()
                MyBase.Ejecutar("c:\Program Files\7-Zip\7z.exe", "a """ & NombreArchivo & ".zip"" -p" & ClaveZip & " """ & NombreArchivo & ".txt"" """ & Application.StartupPath & "\claves\instructivo TWS.docx""")
            Else
                sw.Close()
                MyBase.Ejecutar("c:\Program Files\7-Zip\7z.exe", "a """ & NombreArchivo & ".zip"" -p" & ClaveZip & " """ & NombreArchivo & ".txt""")
            End If


        Catch ex As Exception
            MsgBox("Descripción: " & ex.Message, CType(vbOKOnly + MsgBoxStyle.Information, MsgBoxStyle))
            Logger.WriteLogExeption(ex)
        Finally
            ' Close the file and release resources.
            sw.Close()
        End Try

    End Sub

    Protected Overrides Sub GenerarTextoCorreo()
        
        Dim sw As System.IO.StreamWriter = Nothing
        Try
            'check the file
            If Not System.IO.Directory.Exists(Application.StartupPath & "\claves\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\claves\")
            End If

            Me.TextoCorreo = Application.StartupPath & "\claves\Mensaje WF - " & NroRequerimiento.ToString & " - " & NombreUsuario
            Dim fic As String = TextoCorreo & ".txt"

            sw = New System.IO.StreamWriter(fic, False)
            sw.WriteLine("Estimado," & vbCrLf)
            sw.WriteLine("Adjuntamos al presente mensaje un archivo comprimido y encriptado en formato ZIP, conteniendo identificación (usuario) y contraseña para el sistema " & Me.Sistema & " de acuerdo a lo gestionado en el ticket del asunto." & vbCrLf)
            sw.WriteLine("La clave para desencriptar  dicho archivo ha sido enviada en un correo electrónico con el asunto WF - " & NroRequerimiento.ToString & " a la dirección de correo del solicitante autorizado." & vbCrLf)
            sw.WriteLine(Me.FirmaCorreo)

        Catch ex As Exception
            MsgBox("Descripción: " & ex.Message, CType(vbOKOnly + MsgBoxStyle.Information, MsgBoxStyle))
            Logger.WriteLogExeption(ex)
        Finally
            ' Close the file and release resources.
            sw.Close()
        End Try

    End Sub

  
End Class
