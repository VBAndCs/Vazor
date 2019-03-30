Imports System.Collections.Concurrent

Public Class VazorViewCache
    Shared cache As New ConcurrentDictionary(Of String, (View As IVazorView, LastAccessed As Date))
    Public Shared ExpireMinutes As Byte = 5

    Shared lastSweep As Date = Now

    Public Shared Sub Add(view As IVazorView, modelData As Object)
        Dim key = view.GetType().Name & "_" & modelData.GetType.Name & modelData.GetHashCode
        Dim t = Now
        cache.TryAdd(key, (view, t))

        If (t - lastSweep).TotalMinutes >= ExpireMinutes Then _
            Task.Run(AddressOf Sweep)
    End Sub

    Shared Sub Sweep()
        Dim t = Now
        lastSweep = t
        For Each kv In cache
            If (t - kv.Value.LastAccessed).TotalMinutes >= ExpireMinutes Then
                cache.TryRemove(kv.Key, Nothing)
            End If
        Next
    End Sub


    Public Shared Function Find(type As Type, modelData As Object) As (Found As Boolean, View As IVazorView)
        Dim data As (View As IVazorView, LastAccessed As Date)
        Dim key = type.Name & "_" & modelData.GetType.Name & modelData.GetHashCode
        If cache.TryGetValue(key, data) Then
            data.LastAccessed = now
            Return (True, data.View)
        Else
            Return (False, Nothing)
        End If
    End Function

End Class

