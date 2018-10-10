Public Class frmDefault

    Private _jobDefault As Requerimiento
    Public Property JobDefault() As Requerimiento
        Get
            Return _jobDefault
        End Get
        Set(ByVal value As Requerimiento)
            _jobDefault = value
        End Set
    End Property

    Private Sub frmDefault_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0} {1}", JobDefault.Instancia, JobDefault.JobId.ToString)

        SesionActualWindows.SesionActual.LogOnWebBrowser(wbDefault)
        wbDefault.Navigate("https://main.cajval.sba.com.ar/wf/workflow/wf_default_job.asp?job_id=" & _jobDefault.JobId.ToString)

    End Sub

    'https://main.cajval.sba.com.ar/wf/default.asp?job_id=53945

    Private Sub wbDefault_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles wbDefault.DocumentCompleted
        If wbDefault.Url.AbsoluteUri = "https://main.cajval.sba.com.ar/wf/default_logueado.asp" Then
            wbDefault.Navigate("https://main.cajval.sba.com.ar/wf/workflow/wf_default_job.asp?job_id=" & _jobDefault.JobId.ToString)
            Exit Sub
        End If

    End Sub
End Class