Imports Microsoft.AspNetCore.Mvc.ViewFeatures
Imports Vazor

Public Class IndexView
    Implements IVazorView

    Dim students As List(Of Student)

    Private Sub New(students As List(Of Student))
        Me.students = students
    End Sub

    Private Sub New(students As List(Of Student), viewData As ViewDataDictionary)
        Me.students = students
        Me.ViewData = viewData
    End Sub

    Public Shared Function CreateInstance(students As List(Of Student), viewData As ViewDataDictionary) As IndexView
        Dim result = VazorViewCache.Find(GetType(IndexView), students)
        If result.Found Then
            ' Hash code could cause a collision so, we will do more checks
            ' If some properties of the ViewData affects the output of vazor, checl them too.
            Dim iv = TryCast(result.View, IndexView)
            If iv IsNot Nothing AndAlso iv.students.SequenceEqual(students) Then
                Return result.View
            End If
        End If
        Dim view = New IndexView(students, viewData)
        VazorViewCache.Add(view, students)
        Return view
    End Function
    Public Shared Function CreateInstance(students As List(Of Student)) As IndexView
        Return CreateInstance(students, Nothing)
    End Function

    Public Property ViewData As ViewDataDictionary

    Public Property Name As String = "Index" Implements IVazorView.Name

    Public Property Path As String = "Views\Home" Implements IVazorView.Path

    Dim _content As Byte()
    Public ReadOnly Property Content() As Byte() Implements IVazorView.Content
        Get
            If _content Is Nothing Then
                Dim html = GetVbXml().ParseTemplate(students)
                _content = System.Text.Encoding.UTF8.GetBytes(html)
            End If
            Return _content
        End Get
    End Property

End Class
