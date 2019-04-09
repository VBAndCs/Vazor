Imports Microsoft.AspNetCore.Mvc.RazorPages
Imports Vazor

Public Class IndexModel : Inherits PageModel

    ' This property is used in the Index.cshtml,
    ' to inject our vbxml code in the page as a pratial view

    Const Title = "Index"

    Public ReadOnly Property ViewName As String
        Get
            ViewData("Title") = Title
            Dim html = GetVbXml(Students, ViewData).ParseTemplate(Students)
            Return VazorPage.CreateNew("Index", "Pages", Title, html)
        End Get
    End Property

    Public Sub OnGet()

    End Sub

End Class
