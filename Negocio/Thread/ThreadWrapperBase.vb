Public MustInherit Class ThreadWrapperBase
    ' This public member exposes the Thread object.
    Public ReadOnly Thread As System.Threading.Thread

    ' The constructor creates the thread object and runs the code
    Sub New()
        Me.Thread = New System.Threading.Thread(AddressOf Me.RunThread)
    End Sub

    ' This starts the thread
    Overridable Sub Start()
        Me.Thread.Start()
    End Sub

    ' This private procedure is where the thread starts its execution;
    ' when the thread terminates, the Done flag is set to 
    Private Sub RunThread()
        m_Done = False
        OnStart()
        m_Done = True
    End Sub

    Dim m_Done As Boolean

    ' This property returns True if the thread has completed its task.
    ReadOnly Property Done() As Boolean
        Get
            Return m_Done
        End Get
    End Property

    ' Derived classes must override this to provide the actual code for the thread.
    Protected MustOverride Sub OnStart()
End Class