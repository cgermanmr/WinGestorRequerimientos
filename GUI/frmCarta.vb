Public Class frmCarta

    Private _jobCarta As Requerimiento
    Public Property JobCarta() As Requerimiento
        Get
            Return _jobCarta
        End Get
        Set(ByVal value As Requerimiento)
            _jobCarta = value
        End Set
    End Property

    Private Sub frmCarta_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SesionActualWindows.SesionActual.LogOnWebBrowser(wbCarta)
        wbCarta.Navigate("https://main.cajval.sba.com.ar/wf/workflow/wf_exportar_job.asp?job_id=" & _jobCarta.JobId.ToString)

    End Sub

    Private Sub wbCarta_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles wbCarta.DocumentCompleted
        If wbCarta.Url.AbsoluteUri = "https://main.cajval.sba.com.ar/wf/default_logueado.asp" Then
            wbCarta.Navigate("https://main.cajval.sba.com.ar/wf/workflow/wf_exportar_job.asp?job_id=" & _jobCarta.JobId.ToString)
            Exit Sub
        End If
        Try
            If _jobCarta.JobDto.Empresa IsNot Nothing And wbCarta.Url.AbsoluteUri = "https://main.cajval.sba.com.ar/wf/workflow/wf_exportar_job.asp?job_id=" & _jobCarta.JobId.ToString Then
               
                If String.IsNullOrEmpty(_jobCarta.JobDto.CodigoAgente) Then
                    wbCarta.Document.All("txtAgente").SetAttribute("Value", _jobCarta.JobDto.Empresa.Substring(0, 4))
                    wbCarta.Document.All("Text2").SetAttribute("Value", _jobCarta.JobDto.Empresa.Substring(0, 4))
                    wbCarta.Document.All("txtEmpresa").SetAttribute("Value", _jobCarta.JobDto.Empresa.Substring(0, 4))
                Else
                    wbCarta.Document.All("txtAgente").SetAttribute("Value", _jobCarta.JobDto.CodigoAgente)
                    wbCarta.Document.All("Text2").SetAttribute("Value", _jobCarta.JobDto.CodigoAgente)
                    wbCarta.Document.All("txtEmpresa").SetAttribute("Value", _jobCarta.JobDto.CodigoAgente)
                End If
                
                wbCarta.Document.All("txtNombreEmpresa").SetAttribute("Value", _jobCarta.JobDto.Empresa)
                wbCarta.Document.All("Text1").SetAttribute("Value", _jobCarta.JobDto.Direccion)

                Dim sistema As String = String.Empty

                If Not String.IsNullOrEmpty(_jobCarta.JobDto.Sistema) Then
                    sistema = _jobCarta.JobDto.Sistema
                    If sistema.Contains("TP8") Then sistema = "TP8"
                    If sistema.Contains("USUARIOS") Then sistema = "USUARIOS"
                End If

                wbCarta.Document.All("txtSistema").SetAttribute("Value", sistema)

                For Each elemento As HtmlElement In wbCarta.Document.All
                    If elemento.Id = "txtPassword" Then
                        elemento.SetAttribute("Value", Requerimiento.GetPasswd)
                    End If
                Next

                'If wbCarta.DocumentText.Contains("<clave></clave>") Then
                '    wbCarta.DocumentText = Replace(wbCarta.DocumentText, "<clave></clave>", "<clave>" & Requerimiento.GetPasswd & "</clave>")
                'End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
      
    End Sub

End Class