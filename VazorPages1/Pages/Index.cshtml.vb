Imports Microsoft.AspNetCore.Mvc.RazorPages

Public Class IndexModel : Inherits PageModel

    ' This property is used in the Index.cshtml,
    ' to inject our vbxml code in the page as a pratial view

    Public ReadOnly Property ViewName As String
        Get
            Return IndexView.CreateNew(Students, ViewData)
        End Get
    End Property

    Public Sub OnGet()

    End Sub

End Class
