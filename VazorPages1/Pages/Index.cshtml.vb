Imports Microsoft.AspNetCore.Mvc.RazorPages

Public Class IndexModel : Inherits PageModel

    Public Property ViewName As String

    Public Sub OnGet()
        Dim iv = IndexView.CreateInstance(Students, ViewData)
        ViewName = Vazor.VazorViewMapper.Add(iv)
    End Sub

End Class
