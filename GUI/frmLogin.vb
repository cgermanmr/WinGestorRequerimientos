Public Class frmLogin

    ' TODO: inserte el código para realizar autenticación personalizada usando el nombre de usuario y la contraseña proporcionada 
    ' (Consulte http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' El objeto principal personalizado se puede adjuntar al objeto principal del subproceso actual como se indica a continuación: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' donde CustomPrincipal es la implementación de IPrincipal utilizada para realizar la autenticación. 
    ' Posteriormente, My.User devolverá la información de identidad encapsulada en el objeto CustomPrincipal
    ' como el nombre de usuario, nombre para mostrar, etc.

    Private Sub RespuestaValidacionUsuario() '(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            If SesionActualWindows.SesionActual.EstadoAutenticacion = ResultadoAutenticacion.UsuarioValido Then
                Me.DialogResult = Windows.Forms.DialogResult.OK

                RemoveHandler SesionActualWindows.SesionActual.UsuarioValidadoEvent, AddressOf RespuestaValidacionUsuario

                Me.Close()

            Else
                Select Case SesionActualWindows.SesionActual.EstadoAutenticacion
                    Case 1
                        lblErrorLogin.Text = "Usuario Invalido"
                    Case 4
                        lblErrorLogin.Text = "Clave Incorrecta"
                    Case 5
                        lblErrorLogin.Text = "Clave Vencida"
                    Case Else
                        lblErrorLogin.Text = "Error de Autenticación"
                End Select

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Async Sub LogOn()
        Me.Cursor = Cursors.WaitCursor
        Dim usuario As Usuario = New Usuario
        Try

            If Not String.IsNullOrEmpty(UsernameTextBox.Text) Then
                usuario.Nombre = UsernameTextBox.Text.ToLower
                usuario.Clave = PasswordTextBox.Text

                SesionActualWindows.SesionActual.EstablecerUsuarioActual(usuario)
                Await SesionActualWindows.SesionActual.LogOn()

                If SesionActualWindows.SesionActual.EstadoAutenticacion = ResultadoAutenticacion.UsuarioValido Then

                    Me.Visible = False

                    SesionActualWindows.SesionActual.AutoRefresh = True

                    Dim _frmCargaInicial As New frmPresentacionCargaInicial()
                    _frmCargaInicial.ShowDialog()

                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()

                Else
                    Select Case SesionActualWindows.SesionActual.EstadoAutenticacion
                        Case 1
                            lblErrorLogin.Text = "Usuario Invalido"
                        Case 4
                            lblErrorLogin.Text = "Clave Incorrecta"
                        Case 5
                            lblErrorLogin.Text = "Clave Vencida"
                        Case Else
                            lblErrorLogin.Text = "Error de Autenticación"
                    End Select
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        LogOn()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub IniciarSesion()
        Me.Visible = True
    End Sub
   
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'UsernameTextBox.Text = "gmedina"
        'PasswordTextBox.Text = "maite03.18"
        'LogOn()
    End Sub
End Class
