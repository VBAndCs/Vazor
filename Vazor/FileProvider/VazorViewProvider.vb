Imports Microsoft.Extensions.FileProviders
Imports Microsoft.Extensions.Primitives

Public Class VazorViewProvider
    Implements IFileProvider

    Public Function GetDirectoryContents(ByVal path As String) As IDirectoryContents Implements IFileProvider.GetDirectoryContents
        Return Nothing
    End Function

    Public Function GetFileInfo(ByVal path As String) As IFileInfo Implements IFileProvider.GetFileInfo
        Return New VazorFileInfo(path)
    End Function

    Public Function Watch(ByVal filter As String) As IChangeToken Implements IFileProvider.Watch
        Return New NoWatchChangeToken()
    End Function

End Class
