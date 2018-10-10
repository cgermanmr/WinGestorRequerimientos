<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVisor
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
        Me.wbVisor = New System.Windows.Forms.WebBrowser()
        Me.BarraVisualizadorToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ModificarToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ImprimirToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RefreshToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EnviarCredencialesToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.BarraVisualizadorToolStrip.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'wbVisor
        '
        Me.wbVisor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbVisor.Location = New System.Drawing.Point(0, 0)
        Me.wbVisor.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbVisor.Name = "wbVisor"
        Me.wbVisor.ScriptErrorsSuppressed = True
        Me.wbVisor.Size = New System.Drawing.Size(748, 562)
        Me.wbVisor.TabIndex = 0
        '
        'BarraVisualizadorToolStrip
        '
        Me.BarraVisualizadorToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModificarToolStripButton, Me.ImprimirToolStripButton, Me.ToolStripSeparator1, Me.RefreshToolStripButton, Me.EnviarCredencialesToolStripButton})
        Me.BarraVisualizadorToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.BarraVisualizadorToolStrip.Name = "BarraVisualizadorToolStrip"
        Me.BarraVisualizadorToolStrip.Size = New System.Drawing.Size(748, 25)
        Me.BarraVisualizadorToolStrip.TabIndex = 1
        Me.BarraVisualizadorToolStrip.Text = "ToolStrip1"
        '
        'ModificarToolStripButton
        '
        Me.ModificarToolStripButton.BackColor = System.Drawing.SystemColors.Control
        Me.ModificarToolStripButton.Image = Global.WinGestorRequerimientos.My.Resources.Resources.WorkItem_32xLG
        Me.ModificarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ModificarToolStripButton.Name = "ModificarToolStripButton"
        Me.ModificarToolStripButton.Size = New System.Drawing.Size(156, 22)
        Me.ModificarToolStripButton.Text = "Tomar / Ejecutar / Aprobar"
        '
        'ImprimirToolStripButton
        '
        Me.ImprimirToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ImprimirToolStripButton.Image = Global.WinGestorRequerimientos.My.Resources.Resources.ShowCodeCoverageColoring_8594_32
        Me.ImprimirToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ImprimirToolStripButton.Name = "ImprimirToolStripButton"
        Me.ImprimirToolStripButton.Size = New System.Drawing.Size(95, 22)
        Me.ImprimirToolStripButton.Text = "Imprimir Carta"
        Me.ImprimirToolStripButton.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'RefreshToolStripButton
        '
        Me.RefreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RefreshToolStripButton.Image = Global.WinGestorRequerimientos.My.Resources.Resources._112_RefreshArrow_Blue
        Me.RefreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RefreshToolStripButton.Name = "RefreshToolStripButton"
        Me.RefreshToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.RefreshToolStripButton.Text = "Refrescar"
        '
        'EnviarCredencialesToolStripButton
        '
        Me.EnviarCredencialesToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.EnviarCredencialesToolStripButton.Image = Global.WinGestorRequerimientos.My.Resources.Resources.mail
        Me.EnviarCredencialesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.EnviarCredencialesToolStripButton.Name = "EnviarCredencialesToolStripButton"
        Me.EnviarCredencialesToolStripButton.Size = New System.Drawing.Size(98, 22)
        Me.EnviarCredencialesToolStripButton.Text = "Enviar Correos"
        Me.EnviarCredencialesToolStripButton.Visible = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.BarraVisualizadorToolStrip)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.wbVisor)
        Me.SplitContainer1.Size = New System.Drawing.Size(748, 591)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.TabIndex = 2
        '
        'frmVisor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(748, 591)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "frmVisor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmVisorRequerimiento"
        Me.BarraVisualizadorToolStrip.ResumeLayout(False)
        Me.BarraVisualizadorToolStrip.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents wbVisor As System.Windows.Forms.WebBrowser
    Friend WithEvents BarraVisualizadorToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ModificarToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImprimirToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RefreshToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EnviarCredencialesToolStripButton As System.Windows.Forms.ToolStripButton
End Class
