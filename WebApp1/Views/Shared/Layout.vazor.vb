Imports Vazor

Public Class LayoutView
    Inherits VazorView

    Public Sub New()
        MyBase.New("_Layout", "Views\Shared", "Vazor MVC")
    End Sub

    Friend Shared Sub CreateNew()
        VazorViewMapper.AddStatic(New LayoutView())
    End Sub

    Public Overrides ReadOnly Property Content() As Byte()
        Get
            Dim html = GetVbXml().ToHtmlString()
            Return Encoding.GetBytes(html)
        End Get
    End Property

End Class