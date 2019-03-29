Imports Microsoft.Extensions.Primitives

Friend Class NoWatchChangeToken
    Implements IChangeToken

    Public ReadOnly Property ActiveChangeCallbacks() As Boolean Implements IChangeToken.ActiveChangeCallbacks
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property HasChanged() As Boolean Implements IChangeToken.HasChanged
        Get
            Return False
        End Get
    End Property


    Public Function RegisterChangeCallback(ByVal callback As Action(Of Object), ByVal state As Object) As IDisposable Implements IChangeToken.RegisterChangeCallback
        Return New EmptyDisposable()
    End Function

End Class
Public Class EmptyDisposable
        Implements IDisposable
        Public Sub Dispose() Implements IDisposable.Dispose

        End Sub
    End Class
