Imports System
Imports System.Diagnostics
Imports System.ComponentModel

Public Class EnvioDeCredenciales

    Private _job As Requerimiento
    Public Property Job() As Requerimiento
        Get
            Return _job
        End Get
        Set(ByVal value As Requerimiento)
            _job = value
        End Set
    End Property

    Private Sub Enviar_Click(sender As Object, e As EventArgs) Handles EnviarBtn.Click

        If Not String.IsNullOrEmpty(NroWFTextBox.Text) And IsNumeric(NroWFTextBox.Text) Then
            solicitante.NroRequerimiento = NroWFTextBox.Text
            For Each u As UsuarioSistema In solicitante.Usuarios
                u.NroRequerimiento = solicitante.NroRequerimiento
            Next
        Else
            MsgBox("Verificar nro de requerimiento", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Faltán datos")
            Exit Sub
        End If

        If solicitante.Usuarios.FindAll(Function(x) String.IsNullOrEmpty(x.DireccionCorreo)).Count = solicitante.Usuarios.Count Then
            MsgBox("Ingresar al menos un correo de usuario", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Faltán datos")
            Exit Sub
        End If

        Dim result As MsgBoxResult

        If UsuarioInternoCheckBox.Checked Then
            result = MsgBox("¿Confirmar envio de claves WF " & solicitante.NroRequerimiento & "?" + vbCrLf + vbCrLf + _
                        solicitante.ResumenEnvioInternos, CType(MsgBoxStyle.OkCancel + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, MsgBoxStyle), "Confirmación envío de claves")
            If result = MsgBoxResult.Ok Then
                solicitante.EnviarUsuarioInterno()
                EnviarBtn.Enabled = False
            End If

        Else

            If String.IsNullOrEmpty(CorreoSolicitanteComboBox.Text).Equals(False) Then
                solicitante.DireccionCorreo = CorreoSolicitanteComboBox.Text
            Else
                MsgBox("Completar correo solicitante", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Faltán datos")
                Exit Sub
            End If

            result = MsgBox("¿Confirmar envio de claves WF " & solicitante.NroRequerimiento & "?" + vbCrLf + vbCrLf + _
                        solicitante.ResumenEnvio, CType(MsgBoxStyle.OkCancel + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, MsgBoxStyle), "Confirmación envío de claves")
            If result = MsgBoxResult.Ok Then
                solicitante.Enviar()
                EnviarBtn.Enabled = False
            End If
        End If



    End Sub

    Dim solicitante As SolicitanteCredencial

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text += " " & Me.Job.JobId.ToString

        solicitante = New SolicitanteCredencial(Me.Job)

        Dim colCorreos As New DataGridViewComboBoxColumn
        colCorreos.Name = "Listado de Correos"
        colCorreos.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        grillaUsuarios.Columns.Add(colCorreos)

        For i As Integer = 1 To 2
            solicitante.NuevoUsuario()
        Next

        CorreoSolicitanteComboBox.DataSource = solicitante.ListaCorreos
        grillaUsuarios.DataSource = solicitante.Usuarios

        NroWFTextBox.Text = solicitante.NroRequerimiento.ToString

    End Sub

    Private Sub grillaUsuarios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grillaUsuarios.CellClick
        If e.ColumnIndex = grillaUsuarios.Columns("Listado de Correos").Index Then
            Dim comboboxColumn As DataGridViewComboBoxCell = TryCast(grillaUsuarios.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewComboBoxCell)
            comboboxColumn.DataSource = DirectCast(grillaUsuarios.Rows(0).DataBoundItem, UsuarioCredencial).ListaCorreos
        End If

        If Not grillaUsuarios.Columns("Listado de Usuarios") Is Nothing Then
            If e.ColumnIndex = grillaUsuarios.Columns("Listado de Usuarios").Index Then
                Dim comboboxColumn As DataGridViewComboBoxCell = TryCast(grillaUsuarios.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewComboBoxCell)
                comboboxColumn.DataSource = solicitante.ListaUsuarios
            End If
        End If
        

    End Sub

    Private Sub grillaUsuarios_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles grillaUsuarios.CellValidating
        If e.ColumnIndex = grillaUsuarios.Columns("Listado de Correos").Index Then
            grillaUsuarios.Rows(e.RowIndex).Cells("Correo").Value = e.FormattedValue
        End If

        If Not grillaUsuarios.Columns("Listado de Usuarios") Is Nothing Then
            If e.ColumnIndex = grillaUsuarios.Columns("Listado de Usuarios").Index Then
                grillaUsuarios.Rows(e.RowIndex).Cells("Usuario TWS").Value = e.FormattedValue
            End If
        End If

        
    End Sub

    'Private Sub grillaUsuarios_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles grillaUsuarios.EditingControlShowing
    '    If e.Control.GetType Is GetType(DataGridViewComboBoxEditingControl) Then
    '        Dim cb As ComboBox = TryCast(e.Control, ComboBox)
    '        If cb IsNot Nothing Then
    '            cb.DropDownStyle = ComboBoxStyle.DropDown
    '        End If
    '    End If
    'End Sub

    
    Private Sub TWSButton_Click(sender As Object, e As EventArgs) Handles TWSButton.Click
        Dim colTWS As New DataGridViewTextBoxColumn
        colTWS.Name = "Usuario TWS"
        colTWS.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        colTWS.DataPropertyName = "UsuarioTWS"
        grillaUsuarios.Columns.Add(colTWS)

        Dim colListaUsuarios As New DataGridViewComboBoxColumn
        colListaUsuarios.Name = "Listado de Usuarios"
        colListaUsuarios.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        grillaUsuarios.Columns.Add(colListaUsuarios)

        grillaUsuarios.Columns("NombreApellido").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

    End Sub

    Private Sub UsuarioInternoCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles UsuarioInternoCheckBox.CheckedChanged
        TWSButton.Enabled = Not UsuarioInternoCheckBox.Checked
        SinExtensionCheckBox.Enabled = Not UsuarioInternoCheckBox.Checked
    End Sub

    Private Sub SinExtensionCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles SinExtensionCheckBox.CheckedChanged
        solicitante.SinExtension = SinExtensionCheckBox.Checked
    End Sub
End Class
