Imports Microsoft.AspNetCore.Mvc.ModelBinding
Imports Microsoft.AspNetCore.Mvc.ViewFeatures
Imports Vazor

Public Class LayoutView
    Implements IVazorView

    Public Sub New()

    End Sub
    Public Sub New(ViewData As ViewDataDictionary)
        Me.ViewData = ViewData
    End Sub

    Public Property ViewData As ViewDataDictionary

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