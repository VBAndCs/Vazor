Imports Microsoft.AspNetCore.Mvc.ModelBinding
Imports Vazor

Public Class LayoutView
    Implements IVazorView

    Public Sub New()

    End Sub
    Public Sub New(viewBag As Object)
        Me.ViewBag = viewBag
    End Sub

    Public Property ViewBag As Object Implements IVazorView.ViewBag

    'Public Property ModelState As ModelStateDictionary Implements ILayoutVazor.ModelState

    Public Property Name As String = "_Layout" Implements IVazorView.Name

    Public Property Path As String = "Views\Shared" Implements IVazorView.Path

    Dim _content As Byte() = Nothing

    Public ReadOnly Property Content() As Byte() Implements IVazorView.Content
        Get
            If _content Is Nothing Then
                Dim html = GetVbXml().ToHtmlString()
                _content = Text.Encoding.UTF8.GetBytes(html)
            End If
            Return _content
        End Get
    End Property

End Class