Imports System.Xml
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.ComponentModel
Imports System.Linq
Imports System.IO
Imports Microsoft.VisualBasic

Public Class frmWinPrincipal

    Private _gestorFiltro As GestorRequerimiento
    Public Property GestorFiltro() As GestorRequerimiento
        Get
            Return _gestorFiltro
        End Get
        Set(ByVal value As GestorRequerimiento)
            _gestorFiltro = value

            AddHandler _gestorFiltro.NuevoJobsCargadoEvent, AddressOf ProgresoConsulta
            AddHandler _gestorFiltro.JobsListadoCompletoEvent, AddressOf AgregarNodoResultadoFiltro

            _gestorFiltro.Start()
        End Set
    End Property

    Delegate Sub ProgresoConsultaCallback()
    Private Sub ProgresoConsulta()
        If Me.InvokeRequired Then
            Dim d As New ProgresoConsultaCallback(AddressOf ProgresoConsulta)
            Me.Invoke(d)
        Else
            FiltroToolStripStatusLabel.Text = GestorFiltro.ObtenerProgreso
        End If
    End Sub

    Private Sub Iniciar()

        Dim _frmLogin As New frmLogin()

        _frmLogin.ShowDialog()

        If _frmLogin.DialogResult = Windows.Forms.DialogResult.OK Then
            UserToolStripStatusLabel.Text = SesionActualWindows.SesionActual.ObtenerUsuarioActual.Nombre
            ListadoCompletoManejador()
            AddHandler SesionActualWindows.SesionActual.JobsPorcentajeModificadoSesionEvent, AddressOf ActualizarProgreso
            AddHandler SesionActualWindows.SesionActual.JobsListadoCompletoSesionEvent, AddressOf ListadoCompletoManejador
            'AddHandler SesionActualWindows.SesionActual.JobRefrescadoSesionEvent, AddressOf GrillaRefresh
            AddHandler SesionActualWindows.SesionActual.JobRefrescadoSesionEvent, AddressOf ListadoCompletoManejador
            AddHandler SesionActualWindows.SesionActual.NuevoJobSesionEvent, AddressOf NuevoJobManejador

            SesionActualWindows.SesionActual.LogOnInventario(wbInventario)
        Else
            Me.Close()
        End If
    End Sub

    Private Sub frmWinPrincipal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SesionActualWindows.SesionActual.Cerrar()
        RemoveHandler SesionActualWindows.SesionActual.JobsPorcentajeModificadoSesionEvent, AddressOf ActualizarProgreso
        RemoveHandler SesionActualWindows.SesionActual.JobsListadoCompletoSesionEvent, AddressOf ListadoCompletoManejador
        RemoveHandler SesionActualWindows.SesionActual.JobRefrescadoSesionEvent, AddressOf GrillaRefresh
    End Sub


    Sub ConfigurarTreeView()
        FavoritosTreeView.Nodes.Add("Abiertos", "Abiertos")
        FavoritosTreeView.ImageList = TreeViewImageList
        FavoritosTreeView.SelectedImageIndex = 3
        FavoritosTreeView.ImageIndex = 2
        STreeOperaciones.LeerTreeView(FavoritosTreeView)
    End Sub


    Private Sub frmWinPrincipal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Visible = False

        ConfigurarTreeView()

        Iniciar()

    End Sub

#Region "Manejo de Interfaz Grafica"

    Private Sub InicializarGrilla()
        dgvListadoWF.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvListadoWF.RowHeadersVisible = False
        ' Set the column header style.
        Dim columnHeaderStyle As New DataGridViewCellStyle()
        columnHeaderStyle.BackColor = Color.Beige
        dgvListadoWF.ColumnHeadersDefaultCellStyle = columnHeaderStyle
    End Sub

    Private Sub dgvListadoWF_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvListadoWF.CellContentDoubleClick
        VisualizarEjecutarWF()
    End Sub

    Delegate Sub FormatoGrilla(sender As Object, e As DataGridViewCellFormattingEventArgs)
    Private Sub dgvListadoWF_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvListadoWF.CellFormatting

        If Me.InvokeRequired Then
            Dim d As New FormatoGrilla(AddressOf dgvListadoWF_CellFormatting)
            Me.Invoke(d)
        Else

            Me.dgvListadoWF.CellBorderStyle = DataGridViewCellBorderStyle.SunkenHorizontal

            e.CellStyle.BackColor = Color.LemonChiffon
            e.CellStyle.ForeColor = Color.Black

            Dim formatWf As Requerimiento = TryCast(dgvListadoWF.Rows(e.RowIndex).DataBoundItem, Requerimiento)

            Select Case e.ColumnIndex
                Case 0 To 14
                    If formatWf.EsColaPropia.Equals(True) Then

                        If formatWf.Instancia.Contains("Ejecutar").Equals(False) Then
                            e.CellStyle.BackColor = Color.LightCoral
                        End If

                        If formatWf.Instancia.Contains("Ejecutar").Equals(True) Then
                            e.CellStyle.BackColor = Color.LightBlue
                        End If

                        If formatWf.Modifico.Contains(SesionActualWindows.SesionActual.ObtenerUsuarioActual.Nombre) And formatWf.Instancia = "Ejecutar tarea" Then
                            e.CellStyle.BackColor = Color.LightGreen
                        End If

                    End If

            End Select

        End If

    End Sub

    Private Sub dgvListadoWF_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvListadoWF.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            VisualizarEjecutarWF()
            e.SuppressKeyPress = True
        End If

        If e.KeyCode = Keys.F5 Then
            ' Your code here
            ObtenerJobSeleccionadoGrilla.Refresh()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private _jobseleccionadoGrilla As Requerimiento
    Private Property JobSeleccionadoGrilla() As Requerimiento
        Get
            Return _jobseleccionadoGrilla
        End Get
        Set(ByVal value As Requerimiento)
            _jobseleccionadoGrilla = value
        End Set
    End Property


    Private Sub dgvListadoWF_SelectionChanged(sender As Object, e As EventArgs) Handles dgvListadoWF.SelectionChanged
        Dim r As Requerimiento = ObtenerJobSeleccionadoGrilla()
        If r IsNot Nothing Then
            txDescripcion.Text = r.Descripcion
            wbVisorWorkflow.DocumentText = r.JobHtml

            If r.EsColaPropia.Equals(False) Then
                ModificarTareaToolStripButton.Enabled = False
                TomarToolStripMenuItem.Enabled = False
                dgvListadoWF.RowsDefaultCellStyle.SelectionForeColor = Color.Black
            Else
                ModificarTareaToolStripButton.Enabled = True
                TomarToolStripMenuItem.Enabled = True

                If r.Instancia.Contains("Ejecutar").Equals(False) Then
                    dgvListadoWF.RowsDefaultCellStyle.SelectionForeColor = Color.Crimson
                End If

                If r.Instancia.Contains("Ejecutar").Equals(True) Then
                    dgvListadoWF.RowsDefaultCellStyle.SelectionForeColor = Color.DodgerBlue
                End If

                If r.Modifico.Contains(SesionActualWindows.SesionActual.ObtenerUsuarioActual.Nombre) And r.Instancia = "Ejecutar tarea" Then
                    dgvListadoWF.RowsDefaultCellStyle.SelectionForeColor = Color.ForestGreen
                End If

                'If r.Cola.Equals("OP Seguridad Operativa") Then
                '    CartaToolStripButton.Visible = True
                'End If

            End If
        Else
            txDescripcion.Text = String.Empty
            wbVisorWorkflow.DocumentText = Nothing
        End If

    End Sub

#End Region

    Private Sub VisualizarEjecutarWF()
        Dim selWf As Requerimiento = ObtenerJobSeleccionadoGrilla()

        If selWf Is Nothing Then
            Exit Sub
        End If

        Dim formVisor As New frmVisor()
        formVisor.JobVisor = selWf
        formVisor.Show()

    End Sub

    Delegate Sub ActualizarProgresoCallback()
    Private Sub ActualizarProgreso()

        If Me.InvokeRequired Then
            Dim d As New ActualizarProgresoCallback(AddressOf ActualizarProgreso)
            Me.Invoke(d)
        Else
            Me.Text = SesionActualWindows.SesionActual.ObtenerProgresoCarga
        End If

    End Sub

    Delegate Sub NuevoJobManejadorCallback(sender As Object, e As EventArgs)
    Private Sub NuevoJobManejador(sender As Object, e As EventArgs)
        If Me.InvokeRequired Then
            Dim d As New NuevoJobManejadorCallback(AddressOf NuevoJobManejador)
            Me.Invoke(d, sender, e)
        Else
            If DirectCast(sender, Requerimiento).Instancia.Contains("Ejecutar").Equals(False) Then
                Dim slice As New ToastForm(600000, DirectCast(sender, Requerimiento))
                slice.BackColor = Color.LightCoral
                slice.Height = 100
                slice.Show()
            End If
        End If
    End Sub
    '*************************************************************************************************************************
    Delegate Sub ListadoCompletoMenejadorCallback()
    Private Sub ListadoCompletoManejador()
        If Me.InvokeRequired Then
            Dim d As New ListadoCompletoMenejadorCallback(AddressOf ListadoCompletoManejador)
            Me.Invoke(d)
        Else
            If Me.IsDisposed Then Exit Sub

            InicializarGrilla()

            JobSeleccionadoGrilla = ObtenerJobSeleccionadoGrilla()

            If Me.Opacity < 1 Then
                Me.Opacity = 100
                Me.Visible = True
                FavoritosTreeView.SelectedNode = FavoritosTreeView.Nodes(0)
            End If

            If FavoritosTreeView.Nodes("Abiertos").IsSelected Then

                dgvListadoWF.DataSource = SesionActualWindows.SesionActual.Listar

                MantenerSeleccionGrilla()

                Me.Text = SesionActualWindows.SesionActual.HoraUltimaActualizacion

            End If

            If SesionActualWindows.SesionActual.CartaHabilitada Then
                CartaToolStripButton.Visible = True
            End If

            IconoNotificacion()

            If Now.Hour = 21 Then
                Me.Close()
            End If

        End If
    End Sub
    '*******************************************************************************************************************
    Private Sub IconoNotificacion()
        If SesionActualWindows.SesionActual.Listar.FindAll(Function(x) x.EsColaPropia.Equals(True) And x.Instancia.Contains("Ejecutar").Equals(False)).Count > 0 Then
            NotifyIcon.Visible = True
        Else
            NotifyIcon.Visible = False
        End If
    End Sub

    Private Sub MantenerSeleccionGrilla()

        DirectCast(dgvListadoWF.DataSource, List(Of Requerimiento)).Sort(SesionActualWindows.SesionActual.Orden)

        If JobSeleccionadoGrilla IsNot Nothing Then
            If DirectCast(dgvListadoWF.DataSource, List(Of Requerimiento)).Find(Function(x) x.JobId.Equals(JobSeleccionadoGrilla.JobId)) IsNot Nothing Then
                dgvListadoWF.Rows(ObtenerIndiceGrillaJobSeleccionado).Selected = True
            End If
        End If

    End Sub

    Private Function ObtenerIndiceGrillaJobSeleccionado() As Integer

        If JobSeleccionadoGrilla Is Nothing Then
            Return 1
        End If

        For Each row As DataGridViewRow In Me.dgvListadoWF.Rows
            If JobSeleccionadoGrilla.JobId.Equals(TryCast(row.DataBoundItem, Requerimiento).JobId) Then
                Return row.Index
            End If
        Next

    End Function

    Private Function ObtenerJobSeleccionadoGrilla() As Requerimiento
        Dim selWf As Requerimiento = Nothing
        For Each row As DataGridViewRow In Me.dgvListadoWF.SelectedRows
            selWf = TryCast(row.DataBoundItem, Requerimiento)
        Next
        Return selWf
    End Function

    Private Sub dgvListadoWF_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvListadoWF.ColumnHeaderMouseClick

        Dim newColumn As DataGridViewColumn = dgvListadoWF.Columns(e.ColumnIndex)
        Dim direction As SortOrder

        If newColumn.HeaderCell.SortGlyphDirection = SortOrder.None Then
            direction = SortOrder.Ascending
        End If

        If newColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending Then
            direction = SortOrder.Descending
        End If

        If newColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending Then
            direction = SortOrder.Ascending
        End If

        Dim comparador As IComparer(Of Requerimiento) '= Requerimiento.FechaIngresoComparer(SortOrder.Ascending)
        Select Case dgvListadoWF.Columns(e.ColumnIndex).HeaderText
            Case "Job Id"
                comparador = Requerimiento.JobIdComparer(direction)
            Case "Proceso"
                comparador = Requerimiento.ProcesoComparer(direction)
            Case "Solicitante"
                comparador = Requerimiento.SolicitanteComparer(direction)
            Case "Cola"
                comparador = Requerimiento.ColaComparer(direction)
            Case "Instancia"
                comparador = Requerimiento.InstanciaComparer(direction)
            Case "Modificó"
                comparador = Requerimiento.ModificoComparer(direction)
            Case "Otrs"
                comparador = Requerimiento.OtrsVinculadoComparer(direction)
            Case "Estado"
                comparador = Requerimiento.EstadoComparer(direction)
            Case "Ingresó"
                comparador = Requerimiento.IngresoComparer(direction)
            Case "F Ingreso"
                comparador = Requerimiento.FechaIngresoComparer(direction)
            Case "T Total"
                comparador = Requerimiento.TiempoTotalComparer(direction)
            Case "T Cola"
                comparador = Requerimiento.TiempoEnColaComparer(direction)
            Case Else
                comparador = Requerimiento.JobIdComparer(direction)
        End Select

        DirectCast(dgvListadoWF.DataSource, List(Of Requerimiento)).Sort(comparador)

        SesionActualWindows.SesionActual.Orden = comparador

        dgvListadoWF.Refresh()

        For Each col As DataGridViewColumn In dgvListadoWF.Columns
            col.HeaderCell.SortGlyphDirection = SortOrder.None
        Next

        If direction = SortOrder.Ascending Then
            newColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
        Else
            newColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
        End If

    End Sub

    Private Sub AcercaDe_Click(sender As Object, e As EventArgs) Handles AcercaDe.Click
        Dim AcercaDe As New frmAcercaDe()
        AcercaDe.ShowDialog()
    End Sub

    Private Sub tsbFiltros_Click(sender As Object, e As EventArgs) Handles tsbFiltros.Click

        Dim _filtro As New frmFiltro()
        If _filtro.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.Text = "Ejecutando consulta..."
        End If

    End Sub

    Delegate Sub GrillaRefreshCallback()
    Private Sub GrillaRefresh()
        If Me.InvokeRequired Then
            Dim d As New GrillaRefreshCallback(AddressOf GrillaRefresh)
            Me.Invoke(d)
        Else
            IconoNotificacion()
            InicializarGrilla()
            dgvListadoWF.Refresh()
        End If
    End Sub

    Private Sub tsbRefresh_Click(sender As Object, e As EventArgs) Handles tsbRefresh.Click
        If SesionActualWindows.SesionActual.RecargaEnCurso Then
            Exit Sub
        End If

        If ObtenerJobSeleccionadoGrilla() IsNot Nothing Then
            ObtenerJobSeleccionadoGrilla().Refresh()
            GrillaRefresh()
        End If
    End Sub

    Private Sub FiltrarPorTexto()
        If FavoritosTreeView.Nodes("Abiertos").IsSelected Then
            dgvListadoWF.DataSource = SesionActualWindows.SesionActual.FiltrarPorTexto(tstBuscar.Text)
            Exit Sub
        Else
            dgvListadoWF.DataSource = DirectCast(FavoritosTreeView.SelectedNode.Tag, GestorRequerimiento).FiltrarPorTexto(tstBuscar.Text)
        End If

    End Sub

    Private Sub tstBuscar_KeyDown(sender As Object, e As KeyEventArgs) Handles tstBuscar.KeyDown
        FiltrarPorTexto()
    End Sub

    Private Sub tsbBuscar_Click(sender As Object, e As EventArgs) Handles tsbBuscar.Click
        FiltrarPorTexto()
    End Sub

    Delegate Sub AgregarNodoResultadoFiltroCallback(sender As Object, e As EventArgs)
    Private Sub AgregarNodoResultadoFiltro(sender As Object, e As EventArgs)

        If Me.InvokeRequired Then
            Dim d As New AgregarNodoResultadoFiltroCallback(AddressOf AgregarNodoResultadoFiltro)
            Me.Invoke(d, sender, e)
        Else
            FiltroToolStripStatusLabel.Text = "...."
            Dim nodo As New TreeNode(Date.Now.ToString("dd/MM HH:mm:ss \h\s"))
            nodo.Tag = sender

            SesionActualWindows.SesionActual.GestoresJobs.Add(nodo.Text, DirectCast(sender, GestorRequerimiento))

            FavoritosTreeView.Nodes.Add(nodo)
            FavoritosTreeView.SelectedNode = nodo
            FavoritosTreeView.LabelEdit = True
            nodo.BeginEdit()
        End If

    End Sub


    Private Sub CargarListadoJobsPorNro(lista As String())
        Dim listaNros As New List(Of Integer)
        For Each j As String In lista
            If IsNumeric(j) Then
                listaNros.Add(Integer.Parse(j))
            End If
        Next

        Me.GestorFiltro = SesionActualWindows.SesionActual.getGestorJobsPorListadoNros(listaNros.ToArray)

    End Sub

    Private Sub CargarListadoJobsPorIntervalo(lista As String())
        If lista.Length <> 2 Then
            Exit Sub
        End If

        If IsNumeric(lista(0)) And IsNumeric(lista(1)) Then
            If lista(0) < lista(1) Then
                Me.GestorFiltro = SesionActualWindows.SesionActual.getGestorJobsPorIntervalo(Integer.Parse(lista(0)), Integer.Parse(lista(1)))
            Else
                Me.GestorFiltro = SesionActualWindows.SesionActual.getGestorJobsPorIntervalo(Integer.Parse(lista(1)), Integer.Parse(lista(2)))
            End If
        End If

    End Sub

    Private Sub AplicarFiltroJobsxNro(txt As String)

        If String.IsNullOrEmpty(txt) Then
            SesionActualWindows.SesionActual.GestionarJobsAbiertos()
            Exit Sub
        End If

        If txt.Contains("-") Then
            CargarListadoJobsPorIntervalo(Split(txt.ToLower.Trim, "-"))
            Exit Sub
        End If

        If txt.Contains(",") Or Split(txt.ToLower.Trim, ",").Length = 1 Then
            CargarListadoJobsPorNro(Split(txt.ToLower.Trim, ","))
            Exit Sub
        End If

    End Sub

    Private Sub JobsxNroToolStripTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles JobsxNroToolStripTextBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            AplicarFiltroJobsxNro(DirectCast(sender, ToolStripTextBox).Text)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub JobsxNroToolStripButton_Click(sender As Object, e As EventArgs) Handles JobsxNroToolStripButton.Click
        AplicarFiltroJobsxNro(JobsxNroToolStripTextBox.Text)
    End Sub

    Private Sub MisColasToolStripSplitButton_Click(sender As Object, e As EventArgs) Handles MisColasToolStripSplitButton.Click
        If FavoritosTreeView.SelectedNode.IsEditing Then
            FavoritosTreeView.SelectedNode.EndEdit(False)
        End If

        SesionActualWindows.SesionActual.FiltroMisColas = MisColasToolStripSplitButton.Checked

        dgvListadoWF.DataSource = SesionActualWindows.SesionActual.Listar

        FavoritosTreeView.SelectedNode = FavoritosTreeView.Nodes(0)

        Me.Text = SesionActualWindows.SesionActual.HoraUltimaActualizacion

    End Sub

    Private Sub TomarToolStripButton_Click(sender As Object, e As EventArgs) Handles ModificarTareaToolStripButton.Click
        ModificarTarea()
    End Sub

    Private Sub Tomar_Click(sender As Object, e As EventArgs) Handles TomarToolStripMenuItem.Click
        ModificarTarea()
    End Sub

    Private Sub TratarJobs()
        Dim selWf As Requerimiento = ObtenerJobSeleccionadoGrilla()

        If selWf Is Nothing Then
            Exit Sub
        End If

        Dim formDefault As New frmDefault
        formDefault.JobDefault = selWf
        formDefault.Show()

    End Sub


    Private Sub ModificarTarea()

        If ObtenerJobSeleccionadoGrilla() Is Nothing Then Exit Sub

        If ObtenerJobSeleccionadoGrilla.Instancia.Contains("tarea").Equals(False) Then
            TratarJobs()
            Exit Sub
        End If

        Dim input As Object
        Dim pregunta As String = "Nota aclaratoria (no obligatoria): "
        Dim titulo As String = ObtenerJobSeleccionadoGrilla.Instancia & " " & ObtenerJobSeleccionadoGrilla.JobId

        Dim result As MsgBoxResult
        result = MsgBox("¿Confirma " & ObtenerJobSeleccionadoGrilla.Instancia & " ?" + vbCrLf + vbCrLf + _
                        ObtenerJobSeleccionadoGrilla.Descripcion, CType(MsgBoxStyle.OkCancel + MsgBoxStyle.Information, MsgBoxStyle), _
                      "Confirma - " & ObtenerJobSeleccionadoGrilla.Instancia)
        If result = MsgBoxResult.Ok Then
            input = InputBox(pregunta, titulo, String.Empty)
            ObtenerJobSeleccionadoGrilla.ModificarTarea(DirectCast(input, String))
            AgregarANodoTratados(ObtenerJobSeleccionadoGrilla)
        End If

    End Sub

    Public Sub AgregarANodoTratados(job As Requerimiento)
        Dim nodoTratado As TreeNode = Nothing
        Dim txtTratado As String = Date.Now.ToString("dd/MM") & " Tratados"

        For Each no As TreeNode In FavoritosTreeView.Nodes
            If no.Text.Equals(txtTratado) Then
                nodoTratado = no
            End If
        Next

        If nodoTratado Is Nothing Then
            nodoTratado = New TreeNode(txtTratado)
            nodoTratado.Tag = New GestorRequerimiento
            FavoritosTreeView.Nodes.Add(nodoTratado)
        End If

        DirectCast(nodoTratado.Tag, GestorRequerimiento).AddJob(job)

        STreeOperaciones.GrabarTreeView(FavoritosTreeView)

    End Sub


    Private Sub Refrescar_Click(sender As Object, e As EventArgs) Handles RefrescarToolStripMenuItem.Click
        ObtenerJobSeleccionadoGrilla.Refresh()
    End Sub

    Private Sub tsbLogOn_Click(sender As Object, e As EventArgs) Handles tsbLogOn.Click
        SesionActualWindows.SesionActual.LogOnInventario(wbInventario)
    End Sub

    Private Sub GenerarCarta()
        Dim selWf As Requerimiento = ObtenerJobSeleccionadoGrilla()

        If selWf Is Nothing Then
            Exit Sub
        End If

        Dim formCarta As New frmCarta
        formCarta.JobCarta = selWf
        formCarta.Show()
    End Sub

    Private Sub EnviarCredenciales()
        Dim selWf As Requerimiento = ObtenerJobSeleccionadoGrilla()

        If selWf Is Nothing Then
            Exit Sub
        End If

        Dim formCredenciales As New EnvioDeCredenciales
        formCredenciales.Job = selWf
        formCredenciales.Show()

    End Sub

    Private Sub CartaToolStripButton_Click(sender As Object, e As EventArgs) Handles CartaToolStripButton.Click
        'GenerarCarta()
        EnviarCredenciales()
    End Sub

    Private Sub GenerarCartaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerarCartaToolStripMenuItem.Click
        GenerarCarta()
    End Sub

    Private Sub AbrirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirToolStripMenuItem.Click
        VisualizarEjecutarWF()
    End Sub

    Private Sub FavoritosTreeView_AfterLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles FavoritosTreeView.AfterLabelEdit
        If e.Label IsNot Nothing Then
            SesionActualWindows.SesionActual.GestoresJobs.Add(e.Label, SesionActualWindows.SesionActual.GestoresJobs(e.Node.Text))
            SesionActualWindows.SesionActual.GestoresJobs.Remove(e.Node.Text)
        End If
        STreeOperaciones.GrabarTreeView(FavoritosTreeView)
        Me.Text = String.Format("{0} - {1} elementos", FavoritosTreeView.SelectedNode.Text, DirectCast(FavoritosTreeView.SelectedNode.Tag, GestorRequerimiento).Listar.Count)
    End Sub

    Private Sub FavoritosTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles FavoritosTreeView.AfterSelect

        FavoritosTreeView.LabelEdit = Not FavoritosTreeView.Nodes("Abiertos").IsSelected

        If FavoritosTreeView.Nodes("Abiertos").IsSelected Then
            dgvListadoWF.DataSource = SesionActualWindows.SesionActual.Listar
            Me.Text = SesionActualWindows.SesionActual.HoraUltimaActualizacion
            Exit Sub
        End If

        Me.Text = String.Format("{0} - {1} elementos", FavoritosTreeView.SelectedNode.Text, DirectCast(FavoritosTreeView.SelectedNode.Tag, GestorRequerimiento).Listar.Count)

        dgvListadoWF.DataSource = DirectCast(FavoritosTreeView.SelectedNode.Tag, GestorRequerimiento).Listar

        STreeOperaciones.GrabarTreeView(FavoritosTreeView)

    End Sub

    'Private Sub FavoritosTreeView_MouseEnter(sender As Object, e As EventArgs) Handles FavoritosTreeView.MouseEnter
    '    SplitContainer3.SplitterDistance = 200
    'End Sub

    'Private Sub FavoritosTreeView_MouseLeave(sender As Object, e As EventArgs) Handles FavoritosTreeView.MouseLeave
    '    SplitContainer3.SplitterDistance = 90
    'End Sub

    Private Sub GrillaContextMenuStrip_Opening(sender As Object, e As CancelEventArgs) Handles GrillaContextMenuStrip.Opening
        Dim listaGrupos As New List(Of ToolStripMenuItem)
        listaGrupos.Add(New ToolStripMenuItem("Nuevo grupo", TreeViewImageList.Images(0), New EventHandler(AddressOf AddJobNuevoGrupo)))

        GrillaContextMenuStrip.Items(5).Visible = Not FavoritosTreeView.Nodes("Abiertos").IsSelected

        For Each nodo As TreeNode In FavoritosTreeView.Nodes
            If nodo.Text.Contains("Abiertos").Equals(False) Then
                listaGrupos.Add(New ToolStripMenuItem(nodo.Text, TreeViewImageList.Images(2), New EventHandler(AddressOf AddJobAGrupo)))
            End If
        Next
        DirectCast(GrillaContextMenuStrip.Items(4), ToolStripMenuItem).DropDownItems.Clear()
        DirectCast(GrillaContextMenuStrip.Items(4), ToolStripMenuItem).DropDownItems.AddRange(listaGrupos.ToArray)

    End Sub

    Private Sub AddJobAGrupo(sender As Object, e As EventArgs)
        If ObtenerJobSeleccionadoGrilla() IsNot Nothing Then
            For Each nodo As TreeNode In FavoritosTreeView.Nodes
                If nodo.Text.Equals(DirectCast(sender, ToolStripMenuItem).Text) Then
                    DirectCast(nodo.Tag, GestorRequerimiento).AddJob(ObtenerJobSeleccionadoGrilla)
                    STreeOperaciones.GrabarTreeView(FavoritosTreeView)
                End If
            Next
        End If
    End Sub

    Private Sub DelJobGrupo(sender As Object, e As EventArgs) Handles QuitarDeGrupoToolStripMenuItem.Click

        DirectCast(FavoritosTreeView.SelectedNode.Tag, GestorRequerimiento).RemoveJob(ObtenerJobSeleccionadoGrilla)

        STreeOperaciones.GrabarTreeView(FavoritosTreeView)

        dgvListadoWF.DataSource = DirectCast(FavoritosTreeView.SelectedNode.Tag, GestorRequerimiento).Listar

    End Sub

    Private Sub AddJobNuevoGrupo(sender As Object, e As EventArgs)

        Dim _jobs As New GestorRequerimiento()
        _jobs.AddJob(ObtenerJobSeleccionadoGrilla)
        Dim nodo As New TreeNode(String.Format("Grupo {0}", FavoritosTreeView.Nodes.Count.ToString))
        nodo.Tag = _jobs
        FavoritosTreeView.Nodes.Add(nodo)
        FavoritosTreeView.LabelEdit = True
        nodo.BeginEdit()

        STreeOperaciones.GrabarTreeView(FavoritosTreeView)

    End Sub

    Private Sub FavoritosContextMenuStrip_Opening(sender As Object, e As CancelEventArgs) Handles FavoritosContextMenuStrip.Opening
        FavoritosContextMenuStrip.Items(0).Image = TreeViewImageList.Images(0)
        FavoritosContextMenuStrip.Items(1).Image = TreeViewImageList.Images(1)
    End Sub

    Private Sub NuevoGrupo() '(sender As Object, e As EventArgs)
        Dim _jobs As New GestorRequerimiento
        Dim nodo As New TreeNode(String.Format("Grupo {0}", FavoritosTreeView.Nodes.Count))
        nodo.Tag = _jobs
        FavoritosTreeView.Nodes.Add(nodo)
        FavoritosTreeView.LabelEdit = True
        nodo.BeginEdit()
    End Sub

    Private Sub EliminarGrupo() '(sender As Object, e As EventArgs)
        If FavoritosTreeView.SelectedNode.Text.Equals("Abiertos") Then Exit Sub
        FavoritosTreeView.SelectedNode.Remove()
        STreeOperaciones.GrabarTreeView(FavoritosTreeView)

    End Sub

    Private Sub NuevoGrupoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoGrupoToolStripMenuItem.Click
        NuevoGrupo()
    End Sub

    Private Sub EliminarGrupoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarGrupoToolStripMenuItem.Click
        EliminarGrupo()
    End Sub

    Private Sub RecargarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecargarToolStripMenuItem.Click
        SesionActualWindows.SesionActual.ReCarga()
    End Sub

    Private Sub RecargaAutomaticaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecargaAutomaticaToolStripMenuItem.Click
        SesionActualWindows.SesionActual.AutoRefresh = RecargaAutomaticaToolStripMenuItem.Checked
    End Sub

    Private Sub TabPage2_Enter(sender As Object, e As EventArgs) Handles TabPage2.Enter
        If SesionActualWindows.SesionActual.AutoRefresh Then
            SesionActualWindows.SesionActual.AutoRefresh = False
            SesionActualWindows.SesionActual.LogOnInventario(wbInventario)
        End If

    End Sub

    Private Sub TabPage1_Enter(sender As Object, e As EventArgs) Handles TabPage1.Enter
        SesionActualWindows.SesionActual.AutoRefresh = RecargaAutomaticaToolStripMenuItem.Checked
    End Sub


    Private Sub FiltroPorOTRS()
        Dim _datosFiltro As New DatosFiltro
        '_datosFiltro.SolicitanteId = wbFiltro.Document.All("solicitante_id").GetAttribute("value")
        '_datosFiltro.ProcesoId = wbFiltro.Document.All("proceso_id").GetAttribute("value")
        _datosFiltro.EstadoJob = String.Empty
        '_datosFiltro.InstanciaId = wbFiltro.Document.All("instancia_id").GetAttribute("value")
        _datosFiltro.Usuario = String.Empty
        _datosFiltro.OtrsId = ObtenerJobSeleccionadoGrilla.OtrsVinculado.ToString
        '_datosFiltro.BuscarTexto = wbFiltro.Document.All("buscar_texto").GetAttribute("value")

        Me.GestorFiltro = SesionActualWindows.SesionActual.getGestorFiltro(_datosFiltro)
    End Sub

    Private Sub FiltrarPorOtrsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FiltrarPorOtrsToolStripMenuItem.Click
        FiltroPorOTRS()
    End Sub

    'Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
    '    Dim slice As New ToastForm(10000, ObtenerJobSeleccionadoGrilla)
    '    slice.BackColor = Color.LightCoral
    '    'slice.Height = Me.rng.Next(50, 200)
    '    slice.Height = 100
    '    slice.Show()
    'End Sub


    Private Sub NotificaToolStripButton_Click(sender As Object, e As EventArgs) Handles NotificaToolStripButton.Click
        NuevoJobManejador(ObtenerJobSeleccionadoGrilla, New EventArgs())
        System.Threading.Thread.Sleep(500)
    End Sub

  
End Class

