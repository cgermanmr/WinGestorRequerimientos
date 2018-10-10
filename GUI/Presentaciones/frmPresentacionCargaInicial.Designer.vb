<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPresentacionCargaInicial
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
    Friend WithEvents ApplicationTitle As System.Windows.Forms.Label
    Friend WithEvents MainLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DetailsLayoutPanel As System.Windows.Forms.TableLayoutPanel

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPresentacionCargaInicial))
        Me.MainLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.DetailsLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.Version = New System.Windows.Forms.Label()
        Me.Copyright = New System.Windows.Forms.Label()
        Me.ProgresoLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ProgresoLabel = New System.Windows.Forms.Label()
        Me.InicioProgressBar = New System.Windows.Forms.ProgressBar()
        Me.CancelarButton = New System.Windows.Forms.Button()
        Me.LogoTituloLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ApplicationTitle = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MainLayoutPanel.SuspendLayout()
        Me.DetailsLayoutPanel.SuspendLayout()
        Me.ProgresoLayoutPanel.SuspendLayout()
        Me.LogoTituloLayoutPanel.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainLayoutPanel
        '
        Me.MainLayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MainLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MainLayoutPanel.ColumnCount = 1
        Me.MainLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 362.0!))
        Me.MainLayoutPanel.Controls.Add(Me.DetailsLayoutPanel, 0, 1)
        Me.MainLayoutPanel.Controls.Add(Me.ProgresoLayoutPanel, 0, 2)
        Me.MainLayoutPanel.Controls.Add(Me.LogoTituloLayoutPanel, 0, 0)
        Me.MainLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.MainLayoutPanel.Name = "MainLayoutPanel"
        Me.MainLayoutPanel.RowCount = 4
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.95734!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.90047!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.14218!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.MainLayoutPanel.Size = New System.Drawing.Size(362, 431)
        Me.MainLayoutPanel.TabIndex = 0
        '
        'DetailsLayoutPanel
        '
        Me.DetailsLayoutPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DetailsLayoutPanel.BackColor = System.Drawing.Color.Transparent
        Me.DetailsLayoutPanel.ColumnCount = 1
        Me.DetailsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.DetailsLayoutPanel.Controls.Add(Me.Version, 0, 0)
        Me.DetailsLayoutPanel.Controls.Add(Me.Copyright, 0, 1)
        Me.DetailsLayoutPanel.Location = New System.Drawing.Point(3, 294)
        Me.DetailsLayoutPanel.Name = "DetailsLayoutPanel"
        Me.DetailsLayoutPanel.RowCount = 2
        Me.DetailsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.DetailsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.DetailsLayoutPanel.Size = New System.Drawing.Size(356, 40)
        Me.DetailsLayoutPanel.TabIndex = 1
        '
        'Version
        '
        Me.Version.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Version.AutoSize = True
        Me.Version.Location = New System.Drawing.Point(3, 3)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(350, 13)
        Me.Version.TabIndex = 3
        Me.Version.Text = "Versión {0}.{1:00}"
        Me.Version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Copyright
        '
        Me.Copyright.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Copyright.AutoSize = True
        Me.Copyright.Location = New System.Drawing.Point(3, 23)
        Me.Copyright.Name = "Copyright"
        Me.Copyright.Size = New System.Drawing.Size(350, 13)
        Me.Copyright.TabIndex = 4
        Me.Copyright.Text = "Copyright"
        Me.Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ProgresoLayoutPanel
        '
        Me.ProgresoLayoutPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgresoLayoutPanel.ColumnCount = 1
        Me.ProgresoLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ProgresoLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ProgresoLayoutPanel.Controls.Add(Me.ProgresoLabel, 0, 0)
        Me.ProgresoLayoutPanel.Controls.Add(Me.InicioProgressBar, 0, 1)
        Me.ProgresoLayoutPanel.Controls.Add(Me.CancelarButton, 0, 2)
        Me.ProgresoLayoutPanel.Location = New System.Drawing.Point(3, 340)
        Me.ProgresoLayoutPanel.Name = "ProgresoLayoutPanel"
        Me.ProgresoLayoutPanel.RowCount = 3
        Me.ProgresoLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.44444!))
        Me.ProgresoLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.55556!))
        Me.ProgresoLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.ProgresoLayoutPanel.Size = New System.Drawing.Size(356, 79)
        Me.ProgresoLayoutPanel.TabIndex = 2
        '
        'ProgresoLabel
        '
        Me.ProgresoLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgresoLabel.AutoSize = True
        Me.ProgresoLabel.Location = New System.Drawing.Point(3, 4)
        Me.ProgresoLabel.Name = "ProgresoLabel"
        Me.ProgresoLabel.Size = New System.Drawing.Size(350, 13)
        Me.ProgresoLabel.TabIndex = 0
        Me.ProgresoLabel.Text = "Cargando Jobs ..."
        Me.ProgresoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'InicioProgressBar
        '
        Me.InicioProgressBar.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InicioProgressBar.ForeColor = System.Drawing.Color.LightGreen
        Me.InicioProgressBar.Location = New System.Drawing.Point(3, 29)
        Me.InicioProgressBar.Name = "InicioProgressBar"
        Me.InicioProgressBar.Size = New System.Drawing.Size(350, 14)
        Me.InicioProgressBar.TabIndex = 1
        '
        'CancelarButton
        '
        Me.CancelarButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CancelarButton.Location = New System.Drawing.Point(3, 53)
        Me.CancelarButton.Name = "CancelarButton"
        Me.CancelarButton.Size = New System.Drawing.Size(350, 23)
        Me.CancelarButton.TabIndex = 5
        Me.CancelarButton.Text = "Cancelar"
        Me.CancelarButton.UseVisualStyleBackColor = True
        '
        'LogoTituloLayoutPanel
        '
        Me.LogoTituloLayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogoTituloLayoutPanel.ColumnCount = 1
        Me.LogoTituloLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.LogoTituloLayoutPanel.Controls.Add(Me.PictureBox1, 0, 0)
        Me.LogoTituloLayoutPanel.Controls.Add(Me.ApplicationTitle, 0, 1)
        Me.LogoTituloLayoutPanel.Location = New System.Drawing.Point(3, 3)
        Me.LogoTituloLayoutPanel.Name = "LogoTituloLayoutPanel"
        Me.LogoTituloLayoutPanel.RowCount = 2
        Me.LogoTituloLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.6161!))
        Me.LogoTituloLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.3839!))
        Me.LogoTituloLayoutPanel.Size = New System.Drawing.Size(356, 285)
        Me.LogoTituloLayoutPanel.TabIndex = 3
        '
        'ApplicationTitle
        '
        Me.ApplicationTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ApplicationTitle.BackColor = System.Drawing.Color.Transparent
        Me.ApplicationTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ApplicationTitle.Location = New System.Drawing.Point(3, 249)
        Me.ApplicationTitle.Name = "ApplicationTitle"
        Me.ApplicationTitle.Size = New System.Drawing.Size(350, 36)
        Me.ApplicationTitle.TabIndex = 0
        Me.ApplicationTitle.Text = "Título de la aplicación"
        Me.ApplicationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(16, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(323, 243)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.UseWaitCursor = True
        '
        'frmPresentacionCargaInicial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 430)
        Me.ControlBox = False
        Me.Controls.Add(Me.MainLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPresentacionCargaInicial"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.MainLayoutPanel.ResumeLayout(False)
        Me.DetailsLayoutPanel.ResumeLayout(False)
        Me.DetailsLayoutPanel.PerformLayout()
        Me.ProgresoLayoutPanel.ResumeLayout(False)
        Me.ProgresoLayoutPanel.PerformLayout()
        Me.LogoTituloLayoutPanel.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ProgresoLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ProgresoLabel As System.Windows.Forms.Label
    Friend WithEvents LogoTituloLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents Copyright As System.Windows.Forms.Label
    Friend WithEvents InicioProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents CancelarButton As System.Windows.Forms.Button

End Class
