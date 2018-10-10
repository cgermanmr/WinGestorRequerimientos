Public NotInheritable Class frmPresentacionCargaInicial

    'TODO: Este formulario se puede establecer fácilmente como pantalla de presentación para la aplicación desde la pestaña "Aplicación"
    '  del Diseñador de proyectos ("Propiedades" bajo el menú "Proyecto").

    Private Sub frmPresentacionCargaInicial_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Configure el texto del cuadro de diálogo en tiempo de ejecución según la información del ensamblado de la aplicación.  

        'TODO: Personalice la información del ensamblado de la aplicación en el panel "Aplicación" del cuadro de diálogo 
        '  propiedades del proyecto (bajo el menú "Proyecto").

        'Título de la aplicación
        If My.Application.Info.Title <> "" Then
            ApplicationTitle.Text = My.Application.Info.Title
        Else
            'Si falta el título de la aplicación, utilice el nombre de la aplicación sin la extensión
            ApplicationTitle.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If

        'Dé formato a la información de versión usando el texto establecido en el control de versiones en tiempo de diseño como
        '  cadena de formato. Esto le permite una localización efectiva si lo desea.
        '  Se pudo incluir la información de compilación y revisión usando el siguiente código y cambiando el 
        '  texto en tiempo de diseño del control de versiones a "Versión {0}.{1:00}.{2}.{3}" o algo parecido. Consulte
        '  String.Format() en la Ayuda para obtener más información.
        '
        '    Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)

        Version.Text = String.Format("Versión {0}", My.Application.Info.Version.ToString)

        'Información de Copyright
        Copyright.Text = My.Application.Info.Copyright

        AddHandler SesionActualWindows.SesionActual.JobsPorcentajeModificadoSesionEvent, AddressOf ActualizarProgreso
        AddHandler SesionActualWindows.SesionActual.JobsListadoCompletoSesionEvent, AddressOf CargaInicialFinalizada


        If SesionActualWindows.SesionActual.ExisteArchivoUltimaSesion Then
            SesionActualWindows.SesionActual.Deserializar()
        Else
            SesionActualWindows.SesionActual.ReCarga()
        End If

    End Sub

    Delegate Sub ActualizarProgresoCallback()
    Private Sub ActualizarProgreso()
        If Me.ProgresoLabel.InvokeRequired Then
            Dim d As New ActualizarProgresoCallback(AddressOf ActualizarProgreso)
            Me.Invoke(d) ', New Object() {[Text]})
        Else
            ProgresoLabel.Text = SesionActualWindows.SesionActual.ObtenerProgresoCarga
            InicioProgressBar.Maximum = SesionActualWindows.SesionActual.JobsTotal
            If SesionActualWindows.SesionActual.JobsProcesados > 0 Then
                InicioProgressBar.Value = SesionActualWindows.SesionActual.JobsProcesados
            End If
        End If
    End Sub

    Delegate Sub CargaInicialFinalizadaCallback()
    Private Sub CargaInicialFinalizada()

        If Me.InvokeRequired Then
            RemoveHandler SesionActualWindows.SesionActual.JobsPorcentajeModificadoSesionEvent, AddressOf ActualizarProgreso
            RemoveHandler SesionActualWindows.SesionActual.JobsListadoCompletoSesionEvent, AddressOf CargaInicialFinalizada

            Dim d As New CargaInicialFinalizadaCallback(AddressOf CargaInicialFinalizada)
            Me.Invoke(d)
        Else
            Me.Close()
        End If

    End Sub


    Private Sub CancelarButton_Click(sender As Object, e As EventArgs) Handles CancelarButton.Click
        CargaInicialFinalizada()
        frmWinPrincipal.Close()
    End Sub

End Class



