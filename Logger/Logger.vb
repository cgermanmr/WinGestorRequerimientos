Imports System.IO
Imports System.Text
Imports System.Diagnostics


'http://www.codeproject.com/Articles/15291/Error-and-Event-Logging-in-VB-NET

Public Class Logger

    Public Shared Function WriteLog(ByVal entry As String, _
                    Optional ByVal stkTrace As String = "Evento Auditado", _
                    Optional ByVal eventType As  _
                    EventLogEntryType = EventLogEntryType.Information) As Boolean

        'Registrar en EventLog
        EventLogger.WriteToEventLog(entry, stkTrace, eventType)

        'Registrar en Archivo de Log
        TextLogger.WriteToTextLog(entry, stkTrace, eventType.ToString)

        Return True
    End Function

    Public Shared Function WriteLogExeption(ByVal ex As Exception, Optional idSuseso As Integer = 0) As Boolean

        'Registrar en EventLog
        EventLogger.WriteToEventLog(ex.Message, ex.StackTrace, EventLogEntryType.Error, idSuseso)

        'Registrar en Archivo de Log
        TextLogger.WriteToTextLog(ex.Message, ex.StackTrace, EventLogEntryType.Error.ToString, idSuseso)


        Return True
    End Function

End Class
