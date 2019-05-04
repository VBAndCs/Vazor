Imports System.Collections.Concurrent
Imports System.Threading

Public Class VazorViewMapper
    Shared map As New ConcurrentDictionary(Of String, ViewInfo)

    Public Shared Function Add(view As VazorView) As String
        Dim key = view.Name & "_" & Guid.NewGuid.ToString
        If map.TryAdd(key, New ViewInfo(view.Content, view.Path, 1)) Then Return key
        Return ""
    End Function

    Public Shared Sub AddStatic(view As VazorView)
        Dim key = IO.Path.Combine(ViewInfo.FixPath(view.Path), view.Name) + ".cshtml"
        map.TryAdd(key, New ViewInfo(view.Content, view.Path, -1))
    End Sub

    Public Shared Function Find(Path As String) As IO.MemoryStream
        Dim key = ViewInfo.FixPath(Path)
        If map.ContainsKey(key) Then
            Return New IO.MemoryStream(map(key).ViewContent)
        Else
            Dim viewPath = ViewInfo.FixPath(IO.Path.GetDirectoryName(Path))
            Dim viewName = IO.Path.GetFileNameWithoutExtension(Path)

            If map.ContainsKey(viewName) Then
                Dim vd = map(viewName)
                If vd.Path = viewPath Then
                    If vd.Times = 1 Then
                        vd.Times = 2
                    ElseIf vd.Times = 2 Then ' if Times  = -1 it is a lauout view, don't delete it
                        map.TryRemove(viewName, Nothing)
                    End If
                    Return New IO.MemoryStream(vd.ViewContent)
                ElseIf Not viewName.StartsWith("_") Then
                    Return New IO.MemoryStream(Text.Encoding.UTF8.GetBytes(CreatePage(viewName)))
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End If

    End Function

    Private Shared Function CreatePage(viewName As String) As Char()
        Return _
$"@page
@model {viewName}Model

<div>
     @Html.Partial(Model.ViewName + {""""}.cshtml{""""})
</div>"
    End Function
End Class

