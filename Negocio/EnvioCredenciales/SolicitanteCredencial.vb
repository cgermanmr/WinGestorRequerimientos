Imports System.Text.RegularExpressions

Public Class SolicitanteCredencial
    Inherits UsuarioCredencial


    Sub New(ByRef job As Requerimiento)
        Me.Job = job
        ObtenerUsuarios()
    End Sub

    Private _sinExtension As Boolean
    Public Shadows Property SinExtension() As Boolean
        Get
            Return _sinExtension
        End Get
        Set(ByVal value As Boolean)
            _sinExtension = value
            For Each u As UsuarioSistema In Usuarios
                u.SinExtension = value
            Next
        End Set
    End Property


    Property Usuarios As New List(Of UsuarioSistema)

    Public Function ResumenEnvio() As String
        Dim _resumen As New System.Text.StringBuilder

        _resumen.AppendLine("Claves al solicitante: ")
        _resumen.AppendLine()
        _resumen.AppendLine(DireccionCorreo)

        _resumen.AppendLine()
        _resumen.AppendLine("Archivos ZIP a Usuarios: ")
        _resumen.AppendLine()

        For Each us As UsuarioSistema In Usuarios
            If Not String.IsNullOrEmpty(us.DireccionCorreo) Then
                _resumen.AppendLine(us.NombreYApellido & " - " & us.NombreUsuario & " - " & us.DireccionCorreo)
                _resumen.AppendLine()
            End If
        Next
        Return _resumen.ToString
    End Function

    Public Function ResumenEnvioInternos() As String
        Dim _resumen As New System.Text.StringBuilder

        _resumen.AppendLine("Claves al Usuario: ")
        _resumen.AppendLine()

        For Each us As UsuarioSistema In Usuarios
            If Not String.IsNullOrEmpty(us.DireccionCorreo) Then
                _resumen.AppendLine(us.NombreYApellido & " - " & us.NombreUsuario & " - " & us.DireccionCorreo)
                _resumen.AppendLine()
            End If
        Next
        Return _resumen.ToString
    End Function

    Public Sub EnviarUsuarioInterno()
        Try
            
            For Each us As UsuarioSistema In Usuarios
                If Not String.IsNullOrEmpty(us.DireccionCorreo) Then
                    us.EnviarUsuarioInterno()
                End If
            Next

        Catch ex As Exception
            MsgBox("Descripción: " & ex.Message, CType(vbOKOnly + MsgBoxStyle.Information, MsgBoxStyle), "No se envío")
            Logger.WriteLogExeption(ex)
        End Try

    End Sub

    Public Overrides Sub Enviar()
        Try
            GenerarTextoCorreo()

            For Each us As UsuarioSistema In Usuarios
                If Not String.IsNullOrEmpty(us.DireccionCorreo) Then
                    us.Enviar()
                End If
            Next

            Dim strArgumentos As String = "-u ""WF - " & Me.NroRequerimiento & """ -t " & Me.DireccionCorreo & " -s smtpsrv3.sba.com.ar:25 -o reply-to=noresponder@cajval.sba.com.ar -o message-file=""" & TextoCorreo & ".txt"" -o message-charset=UTF8"

            MyBase.Enviar(strArgumentos)

        Catch ex As Exception
            MsgBox("Descripción: " & ex.Message, CType(vbOKOnly + MsgBoxStyle.Information, MsgBoxStyle), "No se envío")
            Logger.WriteLogExeption(ex)
        End Try

    End Sub

    Protected Overrides Sub GenerarTextoCorreo()

        Dim sw As System.IO.StreamWriter = Nothing
        Try
            'check the file
            If Not System.IO.Directory.Exists(Application.StartupPath & "\claves\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\claves\")
            End If

            TextoCorreo = Application.StartupPath & "\claves\Mensaje WF - " & NroRequerimiento.ToString & " - solicitante"
            Dim fic As String = TextoCorreo & ".txt"

            sw = New System.IO.StreamWriter(fic, False)
            sw.WriteLine("Estimado," & vbCrLf)
            sw.WriteLine("Informamos la clave con la cual se podrá desencriptar el archivo ZIP que contiene la identificación (usuario) y contraseña correspondiente a vuestra  solicitud oportunamente enviada y gestionada a través del ticket del asunto." & vbCrLf)
            sw.WriteLine("Por favor, informar la clave:")
            sw.WriteLine()
            For Each us As UsuarioSistema In Usuarios.FindAll(Function(x) String.IsNullOrEmpty(x.DireccionCorreo).Equals(False) And String.IsNullOrEmpty(x.NombreUsuario).Equals(False) And String.IsNullOrEmpty(x.Sistema).Equals(False) And String.IsNullOrEmpty(x.NombreYApellido).Equals(False))
                sw.WriteLine(us.ClaveZip & " al usuario " & us.NombreUsuario & " del sistema " & us.Sistema & " (archivo zip enviado a " & us.DireccionCorreo & " )")
                sw.WriteLine()
            Next

            sw.WriteLine(Me.FirmaCorreo)

        Catch ex As Exception
            MsgBox("Descripción: " & ex.Message, CType(vbOKOnly + MsgBoxStyle.Information, MsgBoxStyle))
            Logger.WriteLogExeption(ex)
        Finally
            ' Close the file and release resources.
            sw.Close()
        End Try

    End Sub

    Private Sub ObtenerUsuarios()
        Dim re As Regex
        Dim mc As MatchCollection
        Dim m As Match

        Dim PATRON_USUARIOS As String = "<td>(&nbsp;)<\/td>|<td class=TextoIzq>(.*?)<\/td>"

        re = New Regex(PATRON_USUARIOS, RegexOptions.IgnoreCase)

        ' obtener la colección de resultados
        mc = re.Matches(Me.Job.JobHtml)

        Dim contador As Integer = 0

        Dim us As UsuarioSistema = Nothing

        For Each m In mc

            If Not String.IsNullOrEmpty(m.Groups(1).Value) Then
                If contador = 1 Then
                    us = New UsuarioSistema
                End If
                contador += 1
            Else
                If us IsNot Nothing And contador > 1 And Not String.IsNullOrEmpty(m.Groups(2).Value) Then
                    Select Case contador
                        Case 2
                            us.NombreYApellido = Trim(m.Groups(2).Value)
                        Case 3
                            us.NombreYApellido = Trim(m.Groups(2).Value) & " " & us.NombreYApellido
                        Case 4
                            us.NombreUsuario = Trim(m.Groups(2).Value)
                            us.ListaCorreos.InsertRange(0, Me.ListaCorreos)
                            us.Job = Me.Job

                            If Not Regex.IsMatch(us.NombreUsuario, "[a-z]{3}\d{3}[a-z]{1,2}") And Me.Usuarios.FindAll(Function(x) x.NombreUsuario = us.NombreUsuario).Count = 0 Then
                                Me.Usuarios.Add(us)
                            End If
                            contador = 0
                    End Select
                End If
            End If
        Next

        For Each m In Regex.Matches(Me.Job.JobHtml, "[a-zA-Z]{3,4}\d{3}[a-zA-Z]{1,2}")
            If ListaUsuarios.FindAll(Function(x) x.Equals(m.Value)).Count = 0 Then
                ListaUsuarios.Add(m.Value)
            End If
        Next


        '**********************************************************************************

        ' obtener la colección de resultados
        Dim re2 As Regex
        re2 = New Regex("<td class=TextoDer>(Sistema:)<\/td>|<td class=TextoIzq>(.*?)<\/td>", RegexOptions.IgnoreCase)
        mc = re2.Matches(Me.Job.JobHtml)

        contador = 0
        For Each m In mc
            If Not String.IsNullOrEmpty(m.Groups(1).Value) Then
                contador += 1
            Else
                If contador = 1 And Not String.IsNullOrEmpty(m.Groups(2).Value) Then
                    Sistema = m.Groups(2).Value
                    For Each u As UsuarioSistema In Usuarios
                        u.Sistema = Sistema
                    Next
                    contador = 0
                End If
            End If
        Next


    End Sub

    Public Sub NuevoUsuario()
        Dim u As New UsuarioSistema
        u.ListaCorreos.InsertRange(0, Me.ListaCorreos)
        Usuarios.Add(u)
    End Sub

End Class


