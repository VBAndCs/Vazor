﻿Imports Microsoft.AspNetCore.Mvc.ViewFeatures
Imports Vazor
Imports ZML

' To add anew vzor view, right-click the folder in solution explorer
' click Add/New item, and chosse the "VazorView" item from the window
' This will add both the vazor.vb and vbxml.vb files to the folder.

Public Class $safeitemname$View
    Inherits VazorView

    Public ReadOnly Property Students As List(Of Student)

    Public ReadOnly Property ViewData() As ViewDataDictionary

    ' Supply your actual view name, path, and title to the MyBas.New
    ' By defualt, UTF encoding is used to render the view. 
    ' You can send another encoding to the forth param of the MyBase.New.

    Public Sub New(students As List(Of Student), viewData As ViewDataDictionary)
        ' Change Views\Home to the actual folder that contains the view
        MyBase.New("$safeitemname$", "Views\Home", "$safeitemname$")
        Me.Students = students
        Me.ViewData = viewData
        viewData("Title") = Title
    End Sub

    ' This function is called in the "$safeitemname$" action method in the HomeController:
    ' View($safeitemname$View.CreateNew(Students, ViewData))

    Public Shared Function CreateNew(Students As List(Of Student), viewData As ViewDataDictionary) As String
        Return VazorViewMapper.Add(New $safeitemname$View(Students, viewData))
    End Function

    ' GetVbXml( ) is defiend in the $safeitemname$.vbxml.vb file, 
    ' and it contains the view design
End Class