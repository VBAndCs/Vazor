Imports Microsoft.AspNetCore.Mvc.RazorPages

Public Class IndexModel : Inherits PageModel

    Public Property ViewName As String
        Get
            Dim iv = New IndexView(Students, ViewData)
            ViewName = Vazor.VazorViewMapper.Add(iv)
        End Get
        Private Set(value As String)

        End Set
    End Property

    Public Sub OnGet()

    End Sub

End Class
