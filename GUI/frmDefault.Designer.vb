<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefault
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
        Me.wbDefault = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout
        '
        'wbDefault
        '
        Me.wbDefault.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbDefault.Location = New System.Drawing.Point(0, 0)
        Me.wbDefault.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbDefault.Name = "wbDefault"
        Me.wbDefault.Size = New System.Drawing.Size(708, 445)
        Me.wbDefault.TabIndex = 0
        '
        'frmDefault
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(708, 445)
        Me.Controls.Add(Me.wbDefault)
        Me.Name = "frmDefault"
        Me.Text = "Modificar Job"
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents wbDefault As System.Windows.Forms.WebBrowser
End Class
