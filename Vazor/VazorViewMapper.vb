Imports System.Collections.Concurrent

Public Class VazorViewMapper
    Shared map As New ConcurrentDictionary(Of String, ViewData)
    Shared Id As Integer

    Public Shared Function Add(view As IVazorView) As String
        Dim key = view.Name

        If Id = Integer.MaxValue Then
            Id = 1
        Else
            Id += 1
        End If

        key += "_ID_" + Id.ToString
        If map.TryAdd(key, New ViewData(view, 1)) Then Return key
        Return ""
    End Function

    Public Shared Function AddStatic(view As IVazorView) As String
        Dim key = view.Name
        If map.TryAdd(key, New ViewData(view, -1)) Then Return key
        Return ""
    End Function

    Public Shared Function Find(Path As String) As IVazorView
        Dim viewName = IO.Path.GetFileNameWithoutExtension(Path)
        If map.ContainsKey(viewName) Then
            Dim vd = map(viewName)
            If vd.Times = 1 Then
                vd.Times = 2
            ElseIf vd.Times = 2 Then ' if Times  = -1 it is a lauout view, don't delete it
                Dim v As ViewData
                map.TryRemove(viewName, Nothing)
            End If
            Return vd.View
        Else
            Return Nothing
        End If
    End Function

End Class

