Imports Microsoft.AspNetCore.Mvc.RazorPages

Public Class IndexModel : Inherits PageModel

    Public ReadOnly Property ViewName As String
        Get
            Return IndexView.CreateNew(Students, ViewData)
        End Get
    End Property

    Public Sub OnGet()

    End Sub

End Class
