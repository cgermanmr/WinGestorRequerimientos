Imports System.Text
Public Class frmFiltro

    Private Sub frmFiltro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        wbFiltro.DocumentText = SesionActualWindows.HTML_FILTRO
    End Sub

    Private Sub CancelarButton_Click(sender As Object, e As EventArgs) Handles CancelarButton.Click
        Me.Close()
    End Sub

    Private Sub AceptarButton_Click(sender As Object, e As EventArgs) Handles AceptarButton.Click
        AplicarFiltro()
        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub AplicarFiltro()

        Me.Visible = False

        Dim _datosFiltro As New DatosFiltro
        _datosFiltro.SolicitanteId = wbFiltro.Document.All("solicitante_id").GetAttribute("value")
        _datosFiltro.ProcesoId = wbFiltro.Document.All("proceso_id").GetAttribute("value")
        _datosFiltro.EstadoJob = wbFiltro.Document.All("estado_job").GetAttribute("value")
        _datosFiltro.InstanciaId = wbFiltro.Document.All("instancia_id").GetAttribute("value")
        _datosFiltro.Usuario = wbFiltro.Document.All("usuario").GetAttribute("value")
        _datosFiltro.OtrsId = wbFiltro.Document.All("otrs_id").GetAttribute("value")
        _datosFiltro.BuscarTexto = wbFiltro.Document.All("buscar_texto").GetAttribute("value")


        frmWinPrincipal.GestorFiltro = SesionActualWindows.SesionActual.getGestorFiltro(_datosFiltro)
        'SesionActualWindows.SesionActual.Filtrar(_datosFiltro)

    End Sub

End Class