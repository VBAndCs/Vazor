Imports Microsoft.AspNetCore.Mvc.ViewFeatures
Imports Vazor

' To add a new vzor page, right-click the folder in solution explorer
' click Add/New item, and chosse the "VazorPage" item from the window
' This will add the .cshtml, .cshtml.vb, .vazor.vb and .vbxml.vb files to the folder.

Public Class IndexView
    Inherits VazorView

    Public ReadOnly Property Students As List(Of Student)

    Public ReadOnly Property ViewData() As ViewDataDictionary

    ' Supply your actual view name, path, and title to the MyBas.New
    ' By defualt, UTF encoding is used to render the view. 
    ' You can send another encoding to the forth param of the MyBase.New.

    Public Sub New(students As List(Of Student), viewData As ViewDataDictionary)
        MyBase.New("Index", "Views\Home", "Hello")
        Me.Students = students
        Me.ViewData = viewData
        viewData("Title") = Title
    End Sub

    '  This function is called as a return value from the "IndexModel.ViewName" property
    ' in the "Index.cshtml.vb" file:
    '    Public ReadOnly Property ViewName As String
    '        Get
    '            Return IndexView.CreateNew(Students, ViewData)
    '        End Get
    '    End Property

    Public Shared Function CreateNew(Students As List(Of Student), viewData As ViewDataDictionary) As String
        Return VazorViewMapper.Add(New IndexView(Students, viewData))
    End Function

    ' GetVbXml( ) is defiend in the Index.vbxml.vb file, 
    ' and it contains the view design

    Public Overrides ReadOnly Property Content() As Byte()
        Get
            Dim html = GetVbXml().ParseTemplate(Students)
            Return Encoding.GetBytes(html)
        End Get
    End Property

End Class
