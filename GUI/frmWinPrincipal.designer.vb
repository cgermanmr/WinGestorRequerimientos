Partial Public Class frmWinPrincipal
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWinPrincipal))
        Me.PanelPrincipal = New System.Windows.Forms.Panel()
        Me.PanelGrilla = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.FavoritosTreeView = New System.Windows.Forms.TreeView()
        Me.FavoritosContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NuevoGrupoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarGrupoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txDescripcion = New System.Windows.Forms.TextBox()
        Me.dgvListadoWF = New System.Windows.Forms.DataGridView()
        Me.JobIdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProcesoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SolicitanteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InstanciaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ModificoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TiempoPendienteInstanciaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TiempoPendienteTotalDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaIngresoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IngresoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OtrsVinculadoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EstadoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescripcionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UrlJobDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GrillaContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AbrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TomarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefrescarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarCartaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AgregarAGrupoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitarDeGrupoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FiltrarPorOtrsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RequerimientoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.wbVisorWorkflow = New System.Windows.Forms.WebBrowser()
        Me.BarraHerramientas = New System.Windows.Forms.ToolStrip()
        Me.CartaToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbReload = New System.Windows.Forms.ToolStripDropDownButton()
        Me.RecargarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RecargaAutomaticaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.MisColasToolStripSplitButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ModificarTareaToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.AcercaDe = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbFiltros = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbBuscar = New System.Windows.Forms.ToolStripButton()
        Me.tstBuscar = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.JobsxNroToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.JobsxNroToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.NotificaToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.BarraEstado = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UserToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.FiltroToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.wbInventario = New System.Windows.Forms.WebBrowser()
        Me.BarraTabInventarioToolStrip = New System.Windows.Forms.ToolStrip()
        Me.tsbLogOn = New System.Windows.Forms.ToolStripButton()
        Me.TreeViewImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.PanelPrincipal.SuspendLayout
        Me.PanelGrilla.SuspendLayout
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer1.Panel1.SuspendLayout
        Me.SplitContainer1.Panel2.SuspendLayout
        Me.SplitContainer1.SuspendLayout
        CType(Me.SplitContainer3,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer3.Panel1.SuspendLayout
        Me.SplitContainer3.Panel2.SuspendLayout
        Me.SplitContainer3.SuspendLayout
        Me.FavoritosContextMenuStrip.SuspendLayout
        CType(Me.SplitContainer2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer2.Panel1.SuspendLayout
        Me.SplitContainer2.Panel2.SuspendLayout
        Me.SplitContainer2.SuspendLayout
        CType(Me.dgvListadoWF,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GrillaContextMenuStrip.SuspendLayout
        CType(Me.RequerimientoBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        Me.BarraHerramientas.SuspendLayout
        Me.BarraEstado.SuspendLayout
        Me.TabControl1.SuspendLayout
        Me.TabPage1.SuspendLayout
        Me.TabPage2.SuspendLayout
        Me.BarraTabInventarioToolStrip.SuspendLayout
        Me.SuspendLayout
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Controls.Add(Me.PanelGrilla)
        Me.PanelPrincipal.Controls.Add(Me.BarraHerramientas)
        Me.PanelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelPrincipal.Location = New System.Drawing.Point(3, 3)
        Me.PanelPrincipal.Name = "PanelPrincipal"
        Me.PanelPrincipal.Size = New System.Drawing.Size(1182, 559)
        Me.PanelPrincipal.TabIndex = 4
        '
        'PanelGrilla
        '
        Me.PanelGrilla.Controls.Add(Me.SplitContainer1)
        Me.PanelGrilla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelGrilla.Location = New System.Drawing.Point(0, 25)
        Me.PanelGrilla.Name = "PanelGrilla"
        Me.PanelGrilla.Size = New System.Drawing.Size(1182, 534)
        Me.PanelGrilla.TabIndex = 5
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.wbVisorWorkflow)
        Me.SplitContainer1.Size = New System.Drawing.Size(1182, 534)
        Me.SplitContainer1.SplitterDistance = 237
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.FavoritosTreeView)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer3.Size = New System.Drawing.Size(1182, 237)
        Me.SplitContainer3.SplitterDistance = 123
        Me.SplitContainer3.TabIndex = 1
        '
        'FavoritosTreeView
        '
        Me.FavoritosTreeView.ContextMenuStrip = Me.FavoritosContextMenuStrip
        Me.FavoritosTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FavoritosTreeView.Location = New System.Drawing.Point(0, 0)
        Me.FavoritosTreeView.Name = "FavoritosTreeView"
        Me.FavoritosTreeView.Size = New System.Drawing.Size(123, 237)
        Me.FavoritosTreeView.TabIndex = 0
        '
        'FavoritosContextMenuStrip
        '
        Me.FavoritosContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NuevoGrupoToolStripMenuItem, Me.EliminarGrupoToolStripMenuItem})
        Me.FavoritosContextMenuStrip.Name = "FavoritosContextMenuStrip"
        Me.FavoritosContextMenuStrip.Size = New System.Drawing.Size(142, 48)
        '
        'NuevoGrupoToolStripMenuItem
        '
        Me.NuevoGrupoToolStripMenuItem.Name = "NuevoGrupoToolStripMenuItem"
        Me.NuevoGrupoToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.NuevoGrupoToolStripMenuItem.Text = "Nuevo grupo"
        '
        'EliminarGrupoToolStripMenuItem
        '
        Me.EliminarGrupoToolStripMenuItem.Name = "EliminarGrupoToolStripMenuItem"
        Me.EliminarGrupoToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.EliminarGrupoToolStripMenuItem.Text = "Eliminar grupo"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txDescripcion)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvListadoWF)
        Me.SplitContainer2.Size = New System.Drawing.Size(1055, 237)
        Me.SplitContainer2.SplitterDistance = 65
        Me.SplitContainer2.TabIndex = 6
        '
        'txDescripcion
        '
        Me.txDescripcion.BackColor = System.Drawing.Color.LemonChiffon
        Me.txDescripcion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txDescripcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txDescripcion.Location = New System.Drawing.Point(0, 0)
        Me.txDescripcion.Multiline = True
        Me.txDescripcion.Name = "txDescripcion"
        Me.txDescripcion.ReadOnly = True
        Me.txDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txDescripcion.Size = New System.Drawing.Size(1055, 65)
        Me.txDescripcion.TabIndex = 2
        '
        'dgvListadoWF
        '
        Me.dgvListadoWF.AllowUserToAddRows = False
        Me.dgvListadoWF.AllowUserToDeleteRows = False
        Me.dgvListadoWF.AllowUserToResizeColumns = False
        Me.dgvListadoWF.AllowUserToResizeRows = False
        Me.dgvListadoWF.AutoGenerateColumns = False
        Me.dgvListadoWF.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.dgvListadoWF.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvListadoWF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvListadoWF.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.JobIdDataGridViewTextBoxColumn, Me.ProcesoDataGridViewTextBoxColumn, Me.SolicitanteDataGridViewTextBoxColumn, Me.InstanciaDataGridViewTextBoxColumn, Me.ColaDataGridViewTextBoxColumn, Me.ModificoDataGridViewTextBoxColumn, Me.TiempoPendienteInstanciaDataGridViewTextBoxColumn, Me.TiempoPendienteTotalDataGridViewTextBoxColumn, Me.FechaIngresoDataGridViewTextBoxColumn, Me.IngresoDataGridViewTextBoxColumn, Me.OtrsVinculadoDataGridViewTextBoxColumn, Me.EstadoDataGridViewTextBoxColumn, Me.DescripcionDataGridViewTextBoxColumn, Me.UrlJobDataGridViewTextBoxColumn})
        Me.dgvListadoWF.ContextMenuStrip = Me.GrillaContextMenuStrip
        Me.dgvListadoWF.DataSource = Me.RequerimientoBindingSource
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkKhaki
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvListadoWF.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvListadoWF.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvListadoWF.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvListadoWF.Location = New System.Drawing.Point(0, 0)
        Me.dgvListadoWF.MultiSelect = False
        Me.dgvListadoWF.Name = "dgvListadoWF"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.PaleGoldenrod
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvListadoWF.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvListadoWF.RowHeadersVisible = False
        Me.dgvListadoWF.ShowCellToolTips = False
        Me.dgvListadoWF.Size = New System.Drawing.Size(1055, 168)
        Me.dgvListadoWF.TabIndex = 0
        '
        'JobIdDataGridViewTextBoxColumn
        '
        Me.JobIdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.JobIdDataGridViewTextBoxColumn.DataPropertyName = "JobId"
        Me.JobIdDataGridViewTextBoxColumn.HeaderText = "Job Id"
        Me.JobIdDataGridViewTextBoxColumn.Name = "JobIdDataGridViewTextBoxColumn"
        Me.JobIdDataGridViewTextBoxColumn.ReadOnly = True
        Me.JobIdDataGridViewTextBoxColumn.Width = 61
        '
        'ProcesoDataGridViewTextBoxColumn
        '
        Me.ProcesoDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ProcesoDataGridViewTextBoxColumn.DataPropertyName = "Proceso"
        Me.ProcesoDataGridViewTextBoxColumn.HeaderText = "Proceso"
        Me.ProcesoDataGridViewTextBoxColumn.Name = "ProcesoDataGridViewTextBoxColumn"
        Me.ProcesoDataGridViewTextBoxColumn.ReadOnly = True
        Me.ProcesoDataGridViewTextBoxColumn.Width = 71
        '
        'SolicitanteDataGridViewTextBoxColumn
        '
        Me.SolicitanteDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.SolicitanteDataGridViewTextBoxColumn.DataPropertyName = "Solicitante"
        Me.SolicitanteDataGridViewTextBoxColumn.HeaderText = "Solicitante"
        Me.SolicitanteDataGridViewTextBoxColumn.Name = "SolicitanteDataGridViewTextBoxColumn"
        Me.SolicitanteDataGridViewTextBoxColumn.ReadOnly = True
        Me.SolicitanteDataGridViewTextBoxColumn.Width = 81
        '
        'InstanciaDataGridViewTextBoxColumn
        '
        Me.InstanciaDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.InstanciaDataGridViewTextBoxColumn.DataPropertyName = "Instancia"
        Me.InstanciaDataGridViewTextBoxColumn.HeaderText = "Instancia"
        Me.InstanciaDataGridViewTextBoxColumn.Name = "InstanciaDataGridViewTextBoxColumn"
        Me.InstanciaDataGridViewTextBoxColumn.Width = 75
        '
        'ColaDataGridViewTextBoxColumn
        '
        Me.ColaDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ColaDataGridViewTextBoxColumn.DataPropertyName = "Cola"
        Me.ColaDataGridViewTextBoxColumn.HeaderText = "Cola"
        Me.ColaDataGridViewTextBoxColumn.Name = "ColaDataGridViewTextBoxColumn"
        Me.ColaDataGridViewTextBoxColumn.Width = 53
        '
        'ModificoDataGridViewTextBoxColumn
        '
        Me.ModificoDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ModificoDataGridViewTextBoxColumn.DataPropertyName = "Modifico"
        Me.ModificoDataGridViewTextBoxColumn.HeaderText = "Modificó"
        Me.ModificoDataGridViewTextBoxColumn.Name = "ModificoDataGridViewTextBoxColumn"
        Me.ModificoDataGridViewTextBoxColumn.ReadOnly = True
        Me.ModificoDataGridViewTextBoxColumn.Width = 72
        '
        'TiempoPendienteInstanciaDataGridViewTextBoxColumn
        '
        Me.TiempoPendienteInstanciaDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.TiempoPendienteInstanciaDataGridViewTextBoxColumn.DataPropertyName = "TiempoInstancia"
        Me.TiempoPendienteInstanciaDataGridViewTextBoxColumn.HeaderText = "T Cola"
        Me.TiempoPendienteInstanciaDataGridViewTextBoxColumn.Name = "TiempoPendienteInstanciaDataGridViewTextBoxColumn"
        Me.TiempoPendienteInstanciaDataGridViewTextBoxColumn.ReadOnly = True
        Me.TiempoPendienteInstanciaDataGridViewTextBoxColumn.Width = 63
        '
        'TiempoPendienteTotalDataGridViewTextBoxColumn
        '
        Me.TiempoPendienteTotalDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.TiempoPendienteTotalDataGridViewTextBoxColumn.DataPropertyName = "TiempoTotal"
        Me.TiempoPendienteTotalDataGridViewTextBoxColumn.HeaderText = "T Total"
        Me.TiempoPendienteTotalDataGridViewTextBoxColumn.Name = "TiempoPendienteTotalDataGridViewTextBoxColumn"
        Me.TiempoPendienteTotalDataGridViewTextBoxColumn.ReadOnly = True
        Me.TiempoPendienteTotalDataGridViewTextBoxColumn.Width = 66
        '
        'FechaIngresoDataGridViewTextBoxColumn
        '
        Me.FechaIngresoDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.FechaIngresoDataGridViewTextBoxColumn.DataPropertyName = "FechaIngreso"
        Me.FechaIngresoDataGridViewTextBoxColumn.HeaderText = "F Ingreso"
        Me.FechaIngresoDataGridViewTextBoxColumn.Name = "FechaIngresoDataGridViewTextBoxColumn"
        Me.FechaIngresoDataGridViewTextBoxColumn.ReadOnly = True
        Me.FechaIngresoDataGridViewTextBoxColumn.Width = 76
        '
        'IngresoDataGridViewTextBoxColumn
        '
        Me.IngresoDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.IngresoDataGridViewTextBoxColumn.DataPropertyName = "Ingreso"
        Me.IngresoDataGridViewTextBoxColumn.HeaderText = "Ingresó"
        Me.IngresoDataGridViewTextBoxColumn.Name = "IngresoDataGridViewTextBoxColumn"
        Me.IngresoDataGridViewTextBoxColumn.ReadOnly = True
        Me.IngresoDataGridViewTextBoxColumn.Width = 67
        '
        'OtrsVinculadoDataGridViewTextBoxColumn
        '
        Me.OtrsVinculadoDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.OtrsVinculadoDataGridViewTextBoxColumn.DataPropertyName = "OtrsVinculado"
        Me.OtrsVinculadoDataGridViewTextBoxColumn.HeaderText = "Otrs"
        Me.OtrsVinculadoDataGridViewTextBoxColumn.Name = "OtrsVinculadoDataGridViewTextBoxColumn"
        Me.OtrsVinculadoDataGridViewTextBoxColumn.ReadOnly = True
        Me.OtrsVinculadoDataGridViewTextBoxColumn.Width = 51
        '
        'EstadoDataGridViewTextBoxColumn
        '
        Me.EstadoDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.EstadoDataGridViewTextBoxColumn.DataPropertyName = "Estado"
        Me.EstadoDataGridViewTextBoxColumn.HeaderText = "Estado"
        Me.EstadoDataGridViewTextBoxColumn.Name = "EstadoDataGridViewTextBoxColumn"
        Me.EstadoDataGridViewTextBoxColumn.ReadOnly = True
        Me.EstadoDataGridViewTextBoxColumn.Width = 65
        '
        'DescripcionDataGridViewTextBoxColumn
        '
        Me.DescripcionDataGridViewTextBoxColumn.DataPropertyName = "Descripcion"
        Me.DescripcionDataGridViewTextBoxColumn.HeaderText = "Descripcion"
        Me.DescripcionDataGridViewTextBoxColumn.Name = "DescripcionDataGridViewTextBoxColumn"
        Me.DescripcionDataGridViewTextBoxColumn.ReadOnly = True
        Me.DescripcionDataGridViewTextBoxColumn.Visible = False
        Me.DescripcionDataGridViewTextBoxColumn.Width = 88
        '
        'UrlJobDataGridViewTextBoxColumn
        '
        Me.UrlJobDataGridViewTextBoxColumn.DataPropertyName = "UrlJob"
        Me.UrlJobDataGridViewTextBoxColumn.HeaderText = "UrlJob"
        Me.UrlJobDataGridViewTextBoxColumn.Name = "UrlJobDataGridViewTextBoxColumn"
        Me.UrlJobDataGridViewTextBoxColumn.ReadOnly = True
        Me.UrlJobDataGridViewTextBoxColumn.Visible = False
        Me.UrlJobDataGridViewTextBoxColumn.Width = 62
        '
        'GrillaContextMenuStrip
        '
        Me.GrillaContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirToolStripMenuItem, Me.TomarToolStripMenuItem, Me.RefrescarToolStripMenuItem, Me.GenerarCartaToolStripMenuItem, Me.AgregarAGrupoToolStripMenuItem, Me.QuitarDeGrupoToolStripMenuItem, Me.FiltrarPorOtrsToolStripMenuItem})
        Me.GrillaContextMenuStrip.Name = "ContextMenuStrip1"
        Me.GrillaContextMenuStrip.Size = New System.Drawing.Size(194, 158)
        '
        'AbrirToolStripMenuItem
        '
        Me.AbrirToolStripMenuItem.Image = Global.WinGestorRequerimientos.My.Resources.Resources.ShowCodeCoverageColoring_8594_32
        Me.AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem"
        Me.AbrirToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.AbrirToolStripMenuItem.Text = "Abrir"
        '
        'TomarToolStripMenuItem
        '
        Me.TomarToolStripMenuItem.Image = Global.WinGestorRequerimientos.My.Resources.Resources.WorkItem_32xLG
        Me.TomarToolStripMenuItem.Name = "TomarToolStripMenuItem"
        Me.TomarToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.TomarToolStripMenuItem.Text = "Tomar / Ejecutar / Aprob"
        '
        'RefrescarToolStripMenuItem
        '
        Me.RefrescarToolStripMenuItem.Image = Global.WinGestorRequerimientos.My.Resources.Resources.Workflow_32xLG
        Me.RefrescarToolStripMenuItem.Name = "RefrescarToolStripMenuItem"
        Me.RefrescarToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.RefrescarToolStripMenuItem.Text = "Refrescar"
        '
        'GenerarCartaToolStripMenuItem
        '
        Me.GenerarCartaToolStripMenuItem.Image = Global.WinGestorRequerimientos.My.Resources.Resources.mail
        Me.GenerarCartaToolStripMenuItem.Name = "GenerarCartaToolStripMenuItem"
        Me.GenerarCartaToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.GenerarCartaToolStripMenuItem.Text = "Generar carta"
        '
        'AgregarAGrupoToolStripMenuItem
        '
        Me.AgregarAGrupoToolStripMenuItem.Image = Global.WinGestorRequerimientos.My.Resources.Resources.folder_Closed_32xLG
        Me.AgregarAGrupoToolStripMenuItem.Name = "AgregarAGrupoToolStripMenuItem"
        Me.AgregarAGrupoToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.AgregarAGrupoToolStripMenuItem.Text = "Agregar a grupo ->"
        '
        'QuitarDeGrupoToolStripMenuItem
        '
        Me.QuitarDeGrupoToolStripMenuItem.Image = Global.WinGestorRequerimientos.My.Resources.Resources.delete
        Me.QuitarDeGrupoToolStripMenuItem.Name = "QuitarDeGrupoToolStripMenuItem"
        Me.QuitarDeGrupoToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.QuitarDeGrupoToolStripMenuItem.Text = "Quitar de grupo"
        '
        'FiltrarPorOtrsToolStripMenuItem
        '
        Me.FiltrarPorOtrsToolStripMenuItem.Image = Global.WinGestorRequerimientos.My.Resources.Resources.FilteredObject_13400_32x
        Me.FiltrarPorOtrsToolStripMenuItem.Name = "FiltrarPorOtrsToolStripMenuItem"
        Me.FiltrarPorOtrsToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.FiltrarPorOtrsToolStripMenuItem.Text = "Filtrar por OTRS"
        '
        'RequerimientoBindingSource
        '
        Me.RequerimientoBindingSource.DataSource = GetType(WinGestorRequerimientos.Requerimiento)
        '
        'wbVisorWorkflow
        '
        Me.wbVisorWorkflow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbVisorWorkflow.Location = New System.Drawing.Point(0, 0)
        Me.wbVisorWorkflow.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbVisorWorkflow.Name = "wbVisorWorkflow"
        Me.wbVisorWorkflow.ScriptErrorsSuppressed = True
        Me.wbVisorWorkflow.Size = New System.Drawing.Size(1182, 293)
        Me.wbVisorWorkflow.TabIndex = 1
        Me.wbVisorWorkflow.Url = New System.Uri("", System.UriKind.Relative)
        '
        'BarraHerramientas
        '
        Me.BarraHerramientas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CartaToolStripButton, Me.ToolStripSeparator5, Me.tsbRefresh, Me.ToolStripSeparator7, Me.tsbReload, Me.ToolStripSeparator6, Me.MisColasToolStripSplitButton, Me.ToolStripSeparator1, Me.ModificarTareaToolStripButton, Me.AcercaDe, Me.ToolStripSeparator2, Me.tsbFiltros, Me.ToolStripSeparator3, Me.tsbBuscar, Me.tstBuscar, Me.ToolStripSeparator4, Me.JobsxNroToolStripButton, Me.JobsxNroToolStripTextBox, Me.ToolStripSeparator8, Me.NotificaToolStripButton})
        Me.BarraHerramientas.Location = New System.Drawing.Point(0, 0)
        Me.BarraHerramientas.Name = "BarraHerramientas"
        Me.BarraHerramientas.Size = New System.Drawing.Size(1182, 25)
        Me.BarraHerramientas.TabIndex = 4
        Me.BarraHerramientas.Text = "BarraDeHerramientasToolStrip"
        '
        'CartaToolStripButton
        '
        Me.CartaToolStripButton.Image = Global.WinGestorRequerimientos.My.Resources.Resources.mail
        Me.CartaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CartaToolStripButton.Name = "CartaToolStripButton"
        Me.CartaToolStripButton.Size = New System.Drawing.Size(92, 22)
        Me.CartaToolStripButton.Text = "Enviar Claves"
        Me.CartaToolStripButton.Visible = False
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'tsbRefresh
        '
        Me.tsbRefresh.Image = CType(resources.GetObject("tsbRefresh.Image"), System.Drawing.Image)
        Me.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRefresh.Name = "tsbRefresh"
        Me.tsbRefresh.Size = New System.Drawing.Size(74, 22)
        Me.tsbRefresh.Text = "Refrescar"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'tsbReload
        '
        Me.tsbReload.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RecargarToolStripMenuItem, Me.RecargaAutomaticaToolStripMenuItem})
        Me.tsbReload.Image = CType(resources.GetObject("tsbReload.Image"), System.Drawing.Image)
        Me.tsbReload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbReload.Name = "tsbReload"
        Me.tsbReload.Size = New System.Drawing.Size(80, 22)
        Me.tsbReload.Text = "Recargar"
        Me.tsbReload.ToolTipText = "Recargar jobs abiertos"
        '
        'RecargarToolStripMenuItem
        '
        Me.RecargarToolStripMenuItem.Image = CType(resources.GetObject("RecargarToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RecargarToolStripMenuItem.Name = "RecargarToolStripMenuItem"
        Me.RecargarToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.RecargarToolStripMenuItem.Text = "Recargar"
        '
        'RecargaAutomaticaToolStripMenuItem
        '
        Me.RecargaAutomaticaToolStripMenuItem.CheckOnClick = True
        Me.RecargaAutomaticaToolStripMenuItem.Image = CType(resources.GetObject("RecargaAutomaticaToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RecargaAutomaticaToolStripMenuItem.Name = "RecargaAutomaticaToolStripMenuItem"
        Me.RecargaAutomaticaToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.RecargaAutomaticaToolStripMenuItem.Text = "Recarga automática"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'MisColasToolStripSplitButton
        '
        Me.MisColasToolStripSplitButton.CheckOnClick = True
        Me.MisColasToolStripSplitButton.Image = CType(resources.GetObject("MisColasToolStripSplitButton.Image"), System.Drawing.Image)
        Me.MisColasToolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.MisColasToolStripSplitButton.Name = "MisColasToolStripSplitButton"
        Me.MisColasToolStripSplitButton.Size = New System.Drawing.Size(71, 22)
        Me.MisColasToolStripSplitButton.Text = "Mis Colas"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ModificarTareaToolStripButton
        '
        Me.ModificarTareaToolStripButton.BackColor = System.Drawing.Color.Transparent
        Me.ModificarTareaToolStripButton.Image = CType(resources.GetObject("ModificarTareaToolStripButton.Image"), System.Drawing.Image)
        Me.ModificarTareaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ModificarTareaToolStripButton.Name = "ModificarTareaToolStripButton"
        Me.ModificarTareaToolStripButton.Size = New System.Drawing.Size(146, 22)
        Me.ModificarTareaToolStripButton.Text = "Tomar / Ejecutar / Aprob"
        '
        'AcercaDe
        '
        Me.AcercaDe.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.AcercaDe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AcercaDe.Image = CType(resources.GetObject("AcercaDe.Image"), System.Drawing.Image)
        Me.AcercaDe.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AcercaDe.Name = "AcercaDe"
        Me.AcercaDe.Size = New System.Drawing.Size(23, 22)
        Me.AcercaDe.Text = "Acerca de"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tsbFiltros
        '
        Me.tsbFiltros.Image = CType(resources.GetObject("tsbFiltros.Image"), System.Drawing.Image)
        Me.tsbFiltros.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbFiltros.Name = "tsbFiltros"
        Me.tsbFiltros.Size = New System.Drawing.Size(55, 22)
        Me.tsbFiltros.Text = "Filtrar"
        Me.tsbFiltros.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'tsbBuscar
        '
        Me.tsbBuscar.BackColor = System.Drawing.Color.Transparent
        Me.tsbBuscar.Image = CType(resources.GetObject("tsbBuscar.Image"), System.Drawing.Image)
        Me.tsbBuscar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBuscar.Name = "tsbBuscar"
        Me.tsbBuscar.Size = New System.Drawing.Size(74, 22)
        Me.tsbBuscar.Text = "Buscar ->"
        Me.tsbBuscar.ToolTipText = "Buscar en listado actual"
        '
        'tstBuscar
        '
        Me.tstBuscar.Name = "tstBuscar"
        Me.tstBuscar.Size = New System.Drawing.Size(200, 25)
        Me.tstBuscar.ToolTipText = "Buscar por campos del listado actual"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'JobsxNroToolStripButton
        '
        Me.JobsxNroToolStripButton.BackColor = System.Drawing.Color.Transparent
        Me.JobsxNroToolStripButton.Image = CType(resources.GetObject("JobsxNroToolStripButton.Image"), System.Drawing.Image)
        Me.JobsxNroToolStripButton.Name = "JobsxNroToolStripButton"
        Me.JobsxNroToolStripButton.Size = New System.Drawing.Size(88, 22)
        Me.JobsxNroToolStripButton.Text = "Jobs x N° ->"
        Me.JobsxNroToolStripButton.ToolTipText = "Jobs x Nro de Job"
        '
        'JobsxNroToolStripTextBox
        '
        Me.JobsxNroToolStripTextBox.Name = "JobsxNroToolStripTextBox"
        Me.JobsxNroToolStripTextBox.Size = New System.Drawing.Size(125, 25)
        Me.JobsxNroToolStripTextBox.ToolTipText = "ej 4444,3333,2222"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'NotificaToolStripButton
        '
        Me.NotificaToolStripButton.Image = CType(resources.GetObject("NotificaToolStripButton.Image"), System.Drawing.Image)
        Me.NotificaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NotificaToolStripButton.Name = "NotificaToolStripButton"
        Me.NotificaToolStripButton.Size = New System.Drawing.Size(82, 22)
        Me.NotificaToolStripButton.Text = "Notificacion"
        Me.NotificaToolStripButton.Visible = False
        '
        'BarraEstado
        '
        Me.BarraEstado.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.UserToolStripStatusLabel, Me.FiltroToolStripStatusLabel})
        Me.BarraEstado.Location = New System.Drawing.Point(3, 540)
        Me.BarraEstado.Name = "BarraEstado"
        Me.BarraEstado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BarraEstado.Size = New System.Drawing.Size(1182, 22)
        Me.BarraEstado.TabIndex = 1
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(47, 17)
        Me.ToolStripStatusLabel1.Text = "Usuario:"
        '
        'UserToolStripStatusLabel
        '
        Me.UserToolStripStatusLabel.Name = "UserToolStripStatusLabel"
        Me.UserToolStripStatusLabel.Size = New System.Drawing.Size(23, 17)
        Me.UserToolStripStatusLabel.Text = "...."
        '
        'FiltroToolStripStatusLabel
        '
        Me.FiltroToolStripStatusLabel.Image = CType(resources.GetObject("FiltroToolStripStatusLabel.Image"), System.Drawing.Image)
        Me.FiltroToolStripStatusLabel.Name = "FiltroToolStripStatusLabel"
        Me.FiltroToolStripStatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FiltroToolStripStatusLabel.Size = New System.Drawing.Size(43, 17)
        Me.FiltroToolStripStatusLabel.Text = "....."
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1196, 591)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.BarraEstado)
        Me.TabPage1.Controls.Add(Me.PanelPrincipal)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1188, 565)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Workflow"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.wbInventario)
        Me.TabPage2.Controls.Add(Me.BarraTabInventarioToolStrip)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1188, 565)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Inventario"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'wbInventario
        '
        Me.wbInventario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbInventario.Location = New System.Drawing.Point(3, 28)
        Me.wbInventario.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbInventario.Name = "wbInventario"
        Me.wbInventario.Size = New System.Drawing.Size(1182, 534)
        Me.wbInventario.TabIndex = 0
        Me.wbInventario.Url = New System.Uri("https://main.cajval.sba.com.ar/IN/rg_logon_form.asp", System.UriKind.Absolute)
        '
        'BarraTabInventarioToolStrip
        '
        Me.BarraTabInventarioToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbLogOn})
        Me.BarraTabInventarioToolStrip.Location = New System.Drawing.Point(3, 3)
        Me.BarraTabInventarioToolStrip.Name = "BarraTabInventarioToolStrip"
        Me.BarraTabInventarioToolStrip.Size = New System.Drawing.Size(1182, 25)
        Me.BarraTabInventarioToolStrip.TabIndex = 1
        Me.BarraTabInventarioToolStrip.Text = "ToolStrip1"
        '
        'tsbLogOn
        '
        Me.tsbLogOn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbLogOn.Image = CType(resources.GetObject("tsbLogOn.Image"), System.Drawing.Image)
        Me.tsbLogOn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbLogOn.Name = "tsbLogOn"
        Me.tsbLogOn.Size = New System.Drawing.Size(23, 22)
        Me.tsbLogOn.Text = "Iniciar sesión automaticamente"
        '
        'TreeViewImageList
        '
        Me.TreeViewImageList.ImageStream = CType(resources.GetObject("TreeViewImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TreeViewImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TreeViewImageList.Images.SetKeyName(0, "042b_AddCategory.ico")
        Me.TreeViewImageList.Images.SetKeyName(1, "delete.png")
        Me.TreeViewImageList.Images.SetKeyName(2, "folder_Closed_32xLG.png")
        Me.TreeViewImageList.Images.SetKeyName(3, "folder_Open_32xLG.png")
        '
        'NotifyIcon
        '
        Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
        '
        'frmWinPrincipal
        '
        Me.ClientSize = New System.Drawing.Size(1196, 591)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmWinPrincipal"
        Me.Opacity = 0R
        Me.Text = "Gestor de Requerimientos"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelPrincipal.ResumeLayout(false)
        Me.PanelPrincipal.PerformLayout
        Me.PanelGrilla.ResumeLayout(false)
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        Me.SplitContainer3.Panel1.ResumeLayout(false)
        Me.SplitContainer3.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer3,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer3.ResumeLayout(false)
        Me.FavoritosContextMenuStrip.ResumeLayout(false)
        Me.SplitContainer2.Panel1.ResumeLayout(false)
        Me.SplitContainer2.Panel1.PerformLayout
        Me.SplitContainer2.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer2,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer2.ResumeLayout(false)
        CType(Me.dgvListadoWF,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrillaContextMenuStrip.ResumeLayout(false)
        CType(Me.RequerimientoBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        Me.BarraHerramientas.ResumeLayout(false)
        Me.BarraHerramientas.PerformLayout
        Me.BarraEstado.ResumeLayout(false)
        Me.BarraEstado.PerformLayout
        Me.TabControl1.ResumeLayout(false)
        Me.TabPage1.ResumeLayout(false)
        Me.TabPage1.PerformLayout
        Me.TabPage2.ResumeLayout(false)
        Me.TabPage2.PerformLayout
        Me.BarraTabInventarioToolStrip.ResumeLayout(false)
        Me.BarraTabInventarioToolStrip.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents dgvListadoWF As System.Windows.Forms.DataGridView
    Friend WithEvents txDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents BarraEstado As System.Windows.Forms.StatusStrip
    Friend WithEvents PanelPrincipal As System.Windows.Forms.Panel
    Friend WithEvents BarraHerramientas As System.Windows.Forms.ToolStrip
    Friend WithEvents PanelGrilla As System.Windows.Forms.Panel
    Friend WithEvents wbVisorWorkflow As System.Windows.Forms.WebBrowser
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents wbInventario As System.Windows.Forms.WebBrowser
    Friend WithEvents AcercaDe As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents tsbFiltros As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbBuscar As System.Windows.Forms.ToolStripButton
    Friend WithEvents tstBuscar As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tsbRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequerimientoBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents BarraTabInventarioToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbLogOn As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents JobsxNroToolStripTextBox As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents UserToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents JobsxNroToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MisColasToolStripSplitButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents UrlEjecutarInstanciaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModificarTareaToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GrillaContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TomarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefrescarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CartaToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents GenerarCartaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AbrirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents FavoritosTreeView As System.Windows.Forms.TreeView
    Friend WithEvents TreeViewImageList As System.Windows.Forms.ImageList
    Friend WithEvents AgregarAGrupoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FavoritosContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents NuevoGrupoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EliminarGrupoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QuitarDeGrupoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbReload As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents RecargarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RecargaAutomaticaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FiltroToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents NotifyIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents JobIdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProcesoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SolicitanteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InstanciaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModificoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TiempoPendienteInstanciaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TiempoPendienteTotalDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaIngresoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IngresoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OtrsVinculadoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EstadoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescripcionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UrlJobDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FiltrarPorOtrsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NotificaToolStripButton As System.Windows.Forms.ToolStripButton

End Class
