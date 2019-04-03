﻿Imports Microsoft.AspNetCore.Mvc.ViewFeatures
Imports Vazor
Imports WebApp1

Public Class IndexView
    Inherits VazorView

    Public Overrides ReadOnly Property Name As String = "Index"

    Public Overrides ReadOnly Property Path As String = "Views\Home"

    Public Overrides ReadOnly Property Title As String = "Hello"

    Public ReadOnly Property Students As List(Of Student)

    Public ReadOnly Property ViewData() As ViewDataDictionary

    Public Sub New(students As List(Of Student), viewData As ViewDataDictionary)
        Me.Students = students
        Me.ViewData = viewData
        viewData("Title") = Title
    End Sub

    Public Overrides ReadOnly Property Content() As Byte()
        Get
            Dim html = GetVbXml(Me).ParseTemplate(Students)
            Return Encoding.GetBytes(html)
        End Get
    End Property


End Class