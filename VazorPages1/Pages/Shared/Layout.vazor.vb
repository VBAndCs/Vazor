Imports Vazor

Public Class LayoutView
    Inherits VazorView

    Public Sub New()
        MyBase.New("_Layout", "Pages\Shared", "Vazor Pages")
    End Sub

    Public Overrides ReadOnly Property Content() As Byte()
        Get
            Dim html = GetVbXml().ToHtmlString()
            Return Encoding.GetBytes(html)
        End Get
    End Property

End Class