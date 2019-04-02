Friend Class ViewInfo
    Public ViewContent As Byte()
    Public Times As Integer

    Public Sub New(viewContent() As Byte, times As Integer)
        Me.ViewContent = viewContent
        Me.Times = times
    End Sub
End Class
