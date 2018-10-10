Imports System.Windows.Forms

Public Class frmVisor
    Private _jobVisor As Requerimiento
    Public Property JobVisor() As Requerimiento
        Get
            Return _jobVisor
        End Get
        Set(ByVal value As Requerimiento)
            _jobVisor = value
            _jobVisor.Refresh()

        End Set
    End Property

    Private Sub ActualizarVisorHtml(sender As Object, e As System.EventArgs)
        wbVisor.DocumentText = _jobVisor.JobHtml
        EstadoBoton()
        RemoveHandler _jobVisor.JobRefrescadoEvent, AddressOf ActualizarVisorHtml
    End Sub

    Private Sub WebVisor_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles wbVisor.PreviewKeyDown
        If e.KeyCode = Keys.Escape Then
            ' Your code here
            Me.Close()
        End If
    End Sub

    Private Sub TratarJobs()
      
        Dim formDefault As New frmDefault
        formDefault.JobDefault = JobVisor
        formDefault.Show()

    End Sub

    Private Sub ModificarJob()

        If JobVisor.Instancia.Contains("tarea").Equals(False) Then
            TratarJobs()
            Exit Sub
        End If
        Dim input As Object
        Dim pregunta As String = "Nota aclaratoria (no obligatoria): "
        Dim titulo As String = _jobVisor.Instancia & " " & _jobVisor.JobId

        Dim result As MsgBoxResult
        result = MsgBox("¿Confirma " & _jobVisor.Instancia & " ?" + vbCrLf + vbCrLf + _
                        _jobVisor.Descripcion, CType(MsgBoxStyle.OkCancel + MsgBoxStyle.Information, MsgBoxStyle), _
                      "Confirma - " & _jobVisor.Instancia)
        If result = MsgBoxResult.Ok Then
            input = InputBox(pregunta, titulo, String.Empty)
            AddHandler _jobVisor.JobRefrescadoEvent, AddressOf ActualizarVisorHtml
            _jobVisor.ModificarTarea(DirectCast(input, String))
            frmWinPrincipal.AgregarANodoTratados(_jobVisor)
        End If

    End Sub

    Private Sub ModificarButton_Click(sender As Object, e As EventArgs)

        ModificarJob()

    End Sub

    Private Sub frmVisorRequerimiento_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        _jobVisor.Refresh()
        RemoveHandler _jobVisor.JobRefrescadoEvent, AddressOf ActualizarVisorHtml
    End Sub

    Private Sub EstadoBoton()
        If _jobVisor.EsColaPropia.Equals(True) Then

            If _jobVisor.Instancia.Contains("Ejecutar").Equals(False) Then
                ModificarToolStripButton.BackColor = Color.LightCoral
            End If

            If _jobVisor.Instancia.Contains("Ejecutar").Equals(True) Then
                ModificarToolStripButton.BackColor = Color.LightBlue
            End If

            If _jobVisor.Modifico.Contains(SesionActualWindows.SesionActual.ObtenerUsuarioActual.Nombre) And _jobVisor.Instancia = "Ejecutar tarea" Then
                ModificarToolStripButton.BackColor = Color.LightGreen
            End If

        Else
            ModificarToolStripButton.Enabled = False
            ModificarToolStripButton.BackColor = Color.Transparent
        End If
    End Sub

    Private Sub frmVisorRequerimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = _jobVisor.ToString & " - " & _jobVisor.Proceso & " - Solicitante: " & _jobVisor.Solicitante
        AddHandler _jobVisor.JobRefrescadoEvent, AddressOf ActualizarVisorHtml
        wbVisor.DocumentText = _jobVisor.JobHtml
        EstadoBoton()
        ImprimirToolStripButton.Visible = SesionActualWindows.SesionActual.CartaHabilitada
        EnviarCredencialesToolStripButton.Visible = SesionActualWindows.SesionActual.CartaHabilitada

    End Sub

    Private Sub ImprimirButton_Click(sender As Object, e As EventArgs)
        GenerarCarta()
    End Sub

    Private Sub GenerarCarta()
        Dim formCarta As New frmCarta
        formCarta.JobCarta = _jobVisor
        formCarta.Show()
    End Sub

    Private Sub EnviarCredenciales()
        Dim formCredenciales As New EnvioDeCredenciales
        formCredenciales.Job = _jobVisor
        formCredenciales.Show()
    End Sub

    Private Sub ImprimirToolStripButton_Click(sender As Object, e As EventArgs) Handles ImprimirToolStripButton.Click
        GenerarCarta()
    End Sub

    Private Sub ModificarToolStripButton_Click(sender As Object, e As EventArgs) Handles ModificarToolStripButton.Click
        ModificarJob()
    End Sub

    Private Sub Refresh_Click(sender As Object, e As EventArgs) Handles RefreshToolStripButton.Click
        AddHandler _jobVisor.JobRefrescadoEvent, AddressOf ActualizarVisorHtml
        _jobVisor.Refresh()
    End Sub

    Private Sub EnviarCredencialesToolStripButton_Click(sender As Object, e As EventArgs) Handles EnviarCredencialesToolStripButton.Click
        EnviarCredenciales()
    End Sub
End Class