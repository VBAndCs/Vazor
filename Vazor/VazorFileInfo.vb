Imports System.IO
Imports System.Text
Imports Microsoft.Extensions.FileProviders

Public Class VazorFileInfo
    Implements IFileInfo

    Private view As MemoryStream
    Private path As String
    Public Sub New(ByVal path As String)
        Me.path = path
        view = VazorViewMapper.Find(path)
    End Sub

    Public ReadOnly Property Exists() As Boolean Implements IFileInfo.Exists
        Get
            Return view IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property IsDirectory() As Boolean = False Implements IFileInfo.IsDirectory

    Public ReadOnly Property LastModified() As DateTimeOffset Implements IFileInfo.LastModified
        Get
            Return DateTimeOffset.MinValue
        End Get
    End Property

    Public ReadOnly Property Length() As Long Implements IFileInfo.Length
        Get
            Return view.Length
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements IFileInfo.Name
        Get
            Return path
        End Get
    End Property

    Public ReadOnly Property PhysicalPath() As String Implements IFileInfo.PhysicalPath
        Get
            Return String.Empty
        End Get
    End Property

    Public Function CreateReadStream() As Stream Implements IFileInfo.CreateReadStream
        Return view
    End Function

End Class
