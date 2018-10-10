<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCarta
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
        Me.wbCarta = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'wbCarta
        '
        Me.wbCarta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbCarta.Location = New System.Drawing.Point(0, 0)
        Me.wbCarta.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbCarta.Name = "wbCarta"
        Me.wbCarta.Size = New System.Drawing.Size(653, 445)
        Me.wbCarta.TabIndex = 0
        '
        'frmCarta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(653, 445)
        Me.Controls.Add(Me.wbCarta)
        Me.Name = "frmCarta"
        Me.Text = "Impresión de Carta"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents wbCarta As System.Windows.Forms.WebBrowser
End Class
