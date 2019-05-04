Public Class VazorPage
    Inherits VazorView

    Public Sub New(name As String, path As String, title As String, html As String, Optional encoding As Text.Encoding = Nothing)
        MyBase.New(name, path, title, If(encoding, Text.Encoding.UTF8))
        Me.Content = MyBase.Encoding.GetBytes(html)
    End Sub

    ' This function is called in the "Index" action method in the HomeController:
    ' View(IndexView.CreateNew(Students, ViewData))

    Public Shared Function CreateNew(name As String, path As String, title As String,
                                     html As String, Optional encoding As Text.Encoding = Nothing) As String

        Return VazorViewMapper.Add(New VazorPage(name, path, title, html, encoding))
    End Function

    Public Overrides Function GetVbXml() As XElement
        Throw New NotImplementedException()
    End Function

    Public Overrides ReadOnly Property Content() As Byte()

End Class
