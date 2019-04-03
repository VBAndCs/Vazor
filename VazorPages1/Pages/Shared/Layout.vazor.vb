Imports Vazor

Public Class LayoutView
    Inherits VazorView

    Public Overrides ReadOnly Property Name As String = "_Layout"

    Public Overrides ReadOnly Property Path As String = "Views\Shared"

    Public Overrides ReadOnly Property Title As String = "Vazor Pages"

    Public Overrides ReadOnly Property Content() As Byte()
        Get
            Dim html = GetVbXml().ToHtmlString()
            Return Encoding.GetBytes(html)
        End Get
    End Property

End Class