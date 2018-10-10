Public Class frmPruebaTreeView

    Dim _clientes As New List(Of cliente)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For index As Integer = 1 To 10
            Dim cli As New cliente("cliente " & index.ToString, index + 20)
            _clientes.Add(cli)
        Next

        DataGridView1.DataSource = _clientes

        Dim nodo1 As New TreeNode("Todos")
        nodo1.Tag = _clientes
        TreeView1.Nodes.Add(nodo1)
        TreeView1.ImageList = ImageList1
        TreeView1.SelectedImageIndex = 3
        TreeView1.ImageIndex = 2

    End Sub


    Private Sub AddClienteNuevoGrupo(sender As Object, e As EventArgs)

        Dim _clientes As New List(Of cliente)
        _clientes.Add(ObtenerClienteSeleccionadoGrilla)

        Dim nodo As New TreeNode("Grupo " & TreeView1.Nodes.Count)
        nodo.Tag = _clientes
        TreeView1.Nodes.Add(nodo)
        nodo.BeginEdit()
        
    End Sub

    Private Sub AddNuevoGrupo()
        Dim categoria As String = InputBox("Ingrese el nombre de la categoria: ", "Ingresar categoria", "Nuevo Grupo " & TreeView1.Nodes.Count.ToString)
        TreeView1.Nodes.Add(categoria).Tag = New List(Of cliente)
    End Sub

    Private Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click
        AddNuevoGrupo()
    End Sub

    Private Function ObtenerClienteSeleccionadoGrilla() As cliente
        Dim cli As cliente = Nothing
        For Each row As DataGridViewRow In Me.DataGridView1.SelectedRows
            cli = TryCast(row.DataBoundItem, cliente)
        Next
        Return cli
    End Function
    
    Private Sub AddCliente_Click(sender As Object, e As EventArgs) ' Handles AddCliente.Click
        If ObtenerClienteSeleccionadoGrilla() IsNot Nothing Then
            For Each nodo As TreeNode In TreeView1.Nodes
                If nodo.Text.Equals(DirectCast(sender, ToolStripMenuItem).Text) Then
                    DirectCast(nodo.Tag, List(Of cliente)).Add(ObtenerClienteSeleccionadoGrilla())
                End If
            Next
        End If
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        DataGridView1.DataSource = DirectCast(TreeView1.SelectedNode.Tag, List(Of cliente))
    End Sub

    Private Sub AddContextMenuStrip_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles AddContextMenuStrip.Opening
        Dim listaCategorias As New List(Of ToolStripMenuItem)
        listaCategorias.Add(New ToolStripMenuItem("Nuevo grupo", ImageList1.Images(0), New EventHandler(AddressOf AddClienteNuevoGrupo)))
        For Each nodo As TreeNode In TreeView1.Nodes
            If nodo.Text.Contains("Todos").Equals(False) Then
                listaCategorias.Add(New ToolStripMenuItem(nodo.Text, ImageList1.Images(2), New EventHandler(AddressOf AddCliente_Click)))

            End If
        Next
        DirectCast(AddContextMenuStrip.Items(0), ToolStripMenuItem).DropDownItems.Clear()
        DirectCast(AddContextMenuStrip.Items(0), ToolStripMenuItem).DropDownItems.AddRange(listaCategorias.ToArray)

    End Sub

    Private Sub TreeViewContextMenuStrip_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TreeViewContextMenuStrip.Opening
        TreeViewContextMenuStrip.Items(0).Image = ImageList1.Images(0)
        TreeViewContextMenuStrip.Items(1).Image = ImageList1.Images(1)

    End Sub

    Private Sub AgregarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgregarToolStripMenuItem.Click
        Dim nodo As New TreeNode("Grupo " & TreeView1.Nodes.Count)
        TreeView1.Nodes.Add(nodo)
        nodo.BeginEdit()

    End Sub


    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click
        EliminarGrupo()
    End Sub

    Private Sub EliminarGrupo()
        If TreeView1.SelectedNode.Text = "Todos" Then Exit Sub
        TreeView1.SelectedNode.Remove()
    End Sub


    Private Sub SerializarButton_Click(sender As Object, e As EventArgs) Handles SerializarButton.Click

    End Sub
End Class

<Serializable()> Public Class cliente
    Sub New(nombre As String, edad As Integer)
        _nombre = nombre
        _edad = edad
    End Sub

    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _edad As Integer
    Public Property Edad() As Integer
        Get
            Return _edad
        End Get
        Set(ByVal value As Integer)
            _edad = value
        End Set
    End Property

End Class
