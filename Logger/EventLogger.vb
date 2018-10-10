Imports System.Diagnostics


<CLSCompliant(True)> _
Public Class EventLogger

    Public Sub New()

        'default constructor

    End Sub

    Public Shared Function WriteToEventLog(ByVal entry As String, _
                    ByVal stkTrace As String, _
                    Optional ByVal eventType As  _
                    EventLogEntryType = EventLogEntryType.Information, _
                    Optional ByVal idSuseso As Integer = 0, _
                    Optional ByVal appName As String = "WinRequerimientos", _
                    Optional ByVal logName As String = "WinRequerimientos Logs") As Boolean

        Try

            My.Application.Log.WriteEntry(entry, TraceEventType.Critical)
            My.Application.Log.TraceSource.Flush()


            If Not EventLog.SourceExists("WinRequerimientos") Then
                ' Registrar su applicación de demostración como un origne de suceso.
                EventLog.CreateEventSource("WinRequerimientos", "WinRequerimientos Logs")
            End If

            ' Crear un objeto EventLog conectado a este registro de sucesos.
            Dim evLog As New EventLog("WinRequerimientos Logs", ".", "WinRequerimientos")

            ' Escribir dos entradas al registro de aplicaciones.
            evLog.WriteEntry(entry, eventType, idSuseso)

            Return True

        Catch Ex As Exception

            Return False

        End Try

    End Function

End Class
