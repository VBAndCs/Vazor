Imports System.IO
Imports System.Text
Imports Microsoft.Extensions.FileProviders

Public Class VazorFileInfo
    Implements IFileInfo

    Private view As IVazorView

    Public Sub New(ByVal path As String)
        view = VazorViewMapper.Find(path)
    End Sub

    Public Sub New(ByVal view As IVazorView)
        view = view
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
            If view Is Nothing Then Return 0
            Dim content = view.Content.ToString
            If content Is Nothing Then Return 0
            Dim bytes() As Byte = Encoding.UTF8.GetBytes(content)
            Return bytes.Length
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements IFileInfo.Name
        Get
            If view IsNot Nothing AndAlso view.Path IsNot Nothing Then
                Return view.Path
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property PhysicalPath() As String Implements IFileInfo.PhysicalPath
        Get
            Return String.Empty
        End Get
    End Property

    Public Function CreateReadStream() As Stream Implements IFileInfo.CreateReadStream
        If view Is Nothing Then Return Nothing
        Dim Content = view.Content.ToString()
        If Content Is Nothing Then Return Nothing
        Dim bytes() As Byte = Encoding.UTF8.GetBytes(Content)
        Dim ms As New MemoryStream(bytes)
        Return ms
    End Function

End Class
