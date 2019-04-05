Imports System.Collections.Concurrent
Imports System.Threading

Public Class VazorViewMapper
    Shared map As New ConcurrentDictionary(Of String, ViewInfo)

    Public Shared Function Add(view As VazorView) As String
        Dim key = view.Name & "_" & Guid.NewGuid.ToString
        If map.TryAdd(key, New ViewInfo(view.Content, 1)) Then Return key
        Return ""
    End Function

    Public Shared Sub AddStatic(view As VazorView)
        map.TryAdd(view.Name, New ViewInfo(view.Content, -1))
    End Sub

    Public Shared Function Find(Path As String) As IO.MemoryStream
        Dim viewName = IO.Path.GetFileNameWithoutExtension(Path)
        If map.ContainsKey(viewName) Then
            Dim vd = map(viewName)
            If vd.Times = 1 Then
                vd.Times = 2
            ElseIf vd.Times = 2 Then ' if Times  = -1 it is a lauout view, don't delete it
                map.TryRemove(viewName, Nothing)
            End If
            Return New IO.MemoryStream(vd.ViewContent)
        Else
            Return Nothing
        End If
    End Function

End Class

