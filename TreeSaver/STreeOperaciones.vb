Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

Public Class STreeOperaciones

    Private Shared Function ObtenerArrayJobsDto(gr As Object) As RequerimientoDTO()
        Dim listaDto As New List(Of RequerimientoDTO)
        If gr IsNot Nothing Then
            For Each jb As Requerimiento In DirectCast(gr, GestorRequerimiento).Listar
                listaDto.Add(jb.JobDto)
            Next
        End If

        Return listaDto.ToArray

    End Function

    Public Shared Function PrepararEscrituraTreeView(treeView As TreeView) As STreeNode()
        Try
            Dim nodosAGrabar As New List(Of STreeNode)
            For Each tr As TreeNode In treeView.Nodes
                If tr.Text.Equals("Abiertos").Equals(False) Then
                    Dim str As New STreeNode
                    str.Text = tr.Text
                    str.Tag = ObtenerArrayJobsDto(tr.Tag)
                    nodosAGrabar.Add(str)
                End If
            Next

            Return nodosAGrabar.ToArray
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Sub GrabarTreeView(treeView As TreeView)
        Dim fs As FileStream = Nothing
        Try
            'check the file
            If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp\")
            End If

            fs = New FileStream(Application.StartupPath & "\tmp\favoritos.bin", FileMode.Create, FileAccess.ReadWrite)

            'Get a Binary Formatter instance
            Dim bf As New BinaryFormatter()

            ' Serialize the instance to the file.
            bf.Serialize(fs, PrepararEscrituraTreeView(treeView))

        Catch ex As Exception
            Logger.WriteLogExeption(ex)
        Finally
            ' Close the file and release resources.
            fs.Close()
        End Try
    End Sub

    Public Shared Function PrepararLecturaTreeView(nodosLeidos As STreeNode()) As TreeNode
        Try
            Dim treeNode As New TreeNode
            Dim gr As GestorRequerimiento

            For index As Integer = 0 To nodosLeidos.Length - 1
                Dim tn As New TreeNode
                tn.Text = nodosLeidos(index).Text
                gr = New GestorRequerimiento(nodosLeidos(index).Tag)
                gr.Start()
                tn.Tag = gr
                treeNode.Nodes.Add(tn)
            Next

            Return treeNode
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Sub LeerTreeView(treeView As TreeView) ' As TreeNode
        Dim fs As FileStream = Nothing
        Try

            If File.Exists(Application.StartupPath & "\tmp\favoritos.bin").Equals(False) Then Exit Sub

            fs = New FileStream(Application.StartupPath & "\tmp\favoritos.bin", FileMode.Open)

            'Get a Binary Formatter instance
            Dim bf As New BinaryFormatter()

            ' Deserialize from the file, creating an instance of SerializableClass.
            ' The deserialized object must be cast to the proper type.
            Dim treeNodosLeidos As TreeNode = PrepararLecturaTreeView(CType(bf.Deserialize(fs), STreeNode()))

            For Each nodo As TreeNode In treeNodosLeidos.Nodes
                treeView.Nodes.Add(nodo)
                SesionActualWindows.SesionActual.GestoresJobs.Add(nodo.Text, DirectCast(nodo.Tag, GestorRequerimiento))
                DirectCast(nodo.Tag, GestorRequerimiento).AutoRefresh = True
            Next

        Catch ex As System.IO.FileNotFoundException
            MsgBox(ex.Message, CType(vbOKOnly + MsgBoxStyle.Information, MsgBoxStyle))
            Logger.WriteLogExeption(ex)
        Finally
            ' Close the file and release resources.
            If fs IsNot Nothing Then fs.Close()

        End Try

    End Sub

End Class
