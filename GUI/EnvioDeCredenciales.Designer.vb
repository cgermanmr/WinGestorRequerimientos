<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EnvioDeCredenciales
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.EnviarBtn = New System.Windows.Forms.Button()
        Me.CorreoSolicitanteComboBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grillaUsuarios = New System.Windows.Forms.DataGridView()
        Me.NombreApellido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NombreUsuario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sistema = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Clave = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Correo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ClaveZIP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NombreArchivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SolicitanteCredencialBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.NroWFLabel = New System.Windows.Forms.Label()
        Me.TWSButton = New System.Windows.Forms.Button()
        Me.NroWFTextBox = New System.Windows.Forms.TextBox()
        Me.UsuarioInternoCheckBox = New System.Windows.Forms.CheckBox()
        Me.SinExtensionCheckBox = New System.Windows.Forms.CheckBox()
        CType(Me.grillaUsuarios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SolicitanteCredencialBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'EnviarBtn
        '
        Me.EnviarBtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EnviarBtn.Location = New System.Drawing.Point(15, 290)
        Me.EnviarBtn.Name = "EnviarBtn"
        Me.EnviarBtn.Size = New System.Drawing.Size(796, 23)
        Me.EnviarBtn.TabIndex = 4
        Me.EnviarBtn.Text = "Enviar Claves"
        Me.EnviarBtn.UseVisualStyleBackColor = True
        '
        'CorreoSolicitanteComboBox
        '
        Me.CorreoSolicitanteComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CorreoSolicitanteComboBox.FormattingEnabled = True
        Me.CorreoSolicitanteComboBox.Location = New System.Drawing.Point(15, 70)
        Me.CorreoSolicitanteComboBox.Name = "CorreoSolicitanteComboBox"
        Me.CorreoSolicitanteComboBox.Size = New System.Drawing.Size(796, 21)
        Me.CorreoSolicitanteComboBox.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Correo Solicitante:"
        '
        'grillaUsuarios
        '
        Me.grillaUsuarios.AllowUserToDeleteRows = False
        Me.grillaUsuarios.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grillaUsuarios.AutoGenerateColumns = False
        Me.grillaUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grillaUsuarios.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NombreApellido, Me.NombreUsuario, Me.Sistema, Me.Clave, Me.Correo, Me.ClaveZIP, Me.NombreArchivo})
        Me.grillaUsuarios.DataSource = Me.SolicitanteCredencialBindingSource
        Me.grillaUsuarios.Location = New System.Drawing.Point(15, 97)
        Me.grillaUsuarios.Name = "grillaUsuarios"
        Me.grillaUsuarios.RowHeadersVisible = False
        Me.grillaUsuarios.Size = New System.Drawing.Size(796, 184)
        Me.grillaUsuarios.TabIndex = 3
        '
        'NombreApellido
        '
        Me.NombreApellido.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.NombreApellido.DataPropertyName = "NombreYApellido"
        Me.NombreApellido.HeaderText = "Nombre y Apellido"
        Me.NombreApellido.Name = "NombreApellido"
        '
        'NombreUsuario
        '
        Me.NombreUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.NombreUsuario.DataPropertyName = "NombreUsuario"
        Me.NombreUsuario.HeaderText = "Usuario"
        Me.NombreUsuario.Name = "NombreUsuario"
        Me.NombreUsuario.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NombreUsuario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NombreUsuario.Width = 49
        '
        'Sistema
        '
        Me.Sistema.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Sistema.DataPropertyName = "Sistema"
        Me.Sistema.HeaderText = "Sistema"
        Me.Sistema.Name = "Sistema"
        Me.Sistema.Width = 69
        '
        'Clave
        '
        Me.Clave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Clave.DataPropertyName = "Clave"
        Me.Clave.HeaderText = "Clave"
        Me.Clave.Name = "Clave"
        Me.Clave.Width = 59
        '
        'Correo
        '
        Me.Correo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Correo.DataPropertyName = "DireccionCorreo"
        Me.Correo.HeaderText = "Correo"
        Me.Correo.Name = "Correo"
        Me.Correo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Correo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Correo.Width = 44
        '
        'ClaveZIP
        '
        Me.ClaveZIP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.ClaveZIP.DataPropertyName = "ClaveZIP"
        Me.ClaveZIP.HeaderText = "Clave ZIP"
        Me.ClaveZIP.Name = "ClaveZIP"
        Me.ClaveZIP.Visible = False
        '
        'NombreArchivo
        '
        Me.NombreArchivo.HeaderText = "NombreArchivo"
        Me.NombreArchivo.Name = "NombreArchivo"
        Me.NombreArchivo.Visible = False
        '
        'NroWFLabel
        '
        Me.NroWFLabel.AutoSize = True
        Me.NroWFLabel.Location = New System.Drawing.Point(12, 20)
        Me.NroWFLabel.Name = "NroWFLabel"
        Me.NroWFLabel.Size = New System.Drawing.Size(47, 13)
        Me.NroWFLabel.TabIndex = 5
        Me.NroWFLabel.Text = "Nro WF:"
        '
        'TWSButton
        '
        Me.TWSButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TWSButton.Location = New System.Drawing.Point(705, 20)
        Me.TWSButton.Name = "TWSButton"
        Me.TWSButton.Size = New System.Drawing.Size(96, 23)
        Me.TWSButton.TabIndex = 6
        Me.TWSButton.Text = "TWS"
        Me.TWSButton.UseVisualStyleBackColor = True
        '
        'NroWFTextBox
        '
        Me.NroWFTextBox.Location = New System.Drawing.Point(65, 17)
        Me.NroWFTextBox.Name = "NroWFTextBox"
        Me.NroWFTextBox.Size = New System.Drawing.Size(100, 20)
        Me.NroWFTextBox.TabIndex = 7
        '
        'UsuarioInternoCheckBox
        '
        Me.UsuarioInternoCheckBox.AutoSize = True
        Me.UsuarioInternoCheckBox.Location = New System.Drawing.Point(464, 24)
        Me.UsuarioInternoCheckBox.Name = "UsuarioInternoCheckBox"
        Me.UsuarioInternoCheckBox.Size = New System.Drawing.Size(98, 17)
        Me.UsuarioInternoCheckBox.TabIndex = 8
        Me.UsuarioInternoCheckBox.Text = "Usuario Interno"
        Me.UsuarioInternoCheckBox.UseVisualStyleBackColor = True
        '
        'SinExtensionCheckBox
        '
        Me.SinExtensionCheckBox.AutoSize = True
        Me.SinExtensionCheckBox.Location = New System.Drawing.Point(579, 24)
        Me.SinExtensionCheckBox.Name = "SinExtensionCheckBox"
        Me.SinExtensionCheckBox.Size = New System.Drawing.Size(105, 17)
        Me.SinExtensionCheckBox.TabIndex = 9
        Me.SinExtensionCheckBox.Text = "Sin extension zip"
        Me.SinExtensionCheckBox.UseVisualStyleBackColor = True
        '
        'EnvioDeCredenciales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(823, 325)
        Me.Controls.Add(Me.SinExtensionCheckBox)
        Me.Controls.Add(Me.UsuarioInternoCheckBox)
        Me.Controls.Add(Me.NroWFTextBox)
        Me.Controls.Add(Me.TWSButton)
        Me.Controls.Add(Me.NroWFLabel)
        Me.Controls.Add(Me.grillaUsuarios)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CorreoSolicitanteComboBox)
        Me.Controls.Add(Me.EnviarBtn)
        Me.Name = "EnvioDeCredenciales"
        Me.Text = "Envio de Claves"
        CType(Me.grillaUsuarios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SolicitanteCredencialBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents EnviarBtn As System.Windows.Forms.Button
    Friend WithEvents CorreoSolicitanteComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grillaUsuarios As System.Windows.Forms.DataGridView
    Friend WithEvents SolicitanteCredencialBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents NroWFLabel As System.Windows.Forms.Label
    Friend WithEvents DireccionCorreoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NroRequerimientoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TWSButton As System.Windows.Forms.Button
    Friend WithEvents NombreApellido As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NombreUsuario As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sistema As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Clave As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Correo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClaveZIP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NombreArchivo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NroWFTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UsuarioInternoCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SinExtensionCheckBox As System.Windows.Forms.CheckBox

End Class
