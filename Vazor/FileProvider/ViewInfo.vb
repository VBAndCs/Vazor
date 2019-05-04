Friend Class ViewInfo
    Public ViewContent As Byte()
    Public Path As String
    Public Times As Integer

    Public Sub New(viewContent() As Byte, path As String, times As Integer)
        Me.ViewContent = viewContent
        Me.Path = fixpath(path)
        Me.Times = times
    End Sub

    Public Shared Function FixPath(path As String) As String
        Return path.Replace("/"c, "\"c).Trim("\"c)
    End Function
End Class
