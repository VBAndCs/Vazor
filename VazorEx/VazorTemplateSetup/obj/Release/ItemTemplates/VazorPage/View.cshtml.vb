Imports Microsoft.AspNetCore.Mvc.RazorPages
Imports Vazor
Imports ZML

Public Class $safeitemname$Model : Inherits PageModel

    ' This property is used in the $safeitemname$.cshtml,
    ' to inject our vbxml code in the page as a pratial view

    Const Title = "$safeitemname$"

    Public ReadOnly Property ViewName As String
        Get
            ViewData("Title") = Title
            Dim html = GetVbXml(Students, ViewData).Parsezml()
            ' Change Pages to the actual folder that contains the Page. For example Pages\Users
            Return VazorPage.CreateNew("$safeitemname$", "Pages", Title, html)
        End Get
    End Property

    Public ReadOnly Property Students As List(Of Student)
        Get
            Return SomeStudents.Students
        End Get
    End Property

    Public Sub OnGet()

    End Sub

End Class
