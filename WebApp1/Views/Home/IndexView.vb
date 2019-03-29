Imports Vazor

Public Class IndexView
    Implements IVazorView

    Dim students As List(Of Student)

    Private Sub New(students As List(Of Student))
        Me.students = students
    End Sub

    Private Sub New(students As List(Of Student), viewBag As Object)
        Me.students = students
        Me.ViewBag = viewBag
    End Sub

    Public Shared Function CreateInstance(students As List(Of Student), viewBag As Object) As IndexView
        Dim result = VazorViewCache.Find(GetType(IndexView), students)
        If result.Found Then
            ' Hash code could cause a collision so, we will do more checks
            ' If some properties of the ViewBag affects the output of vazor, checl them too.
            Dim iv = TryCast(result.View, IndexView)
            If iv IsNot Nothing AndAlso iv.students.SequenceEqual(students) Then
                Return result.View
            End If
        End If
        Dim view = New IndexView(students, viewBag)
        VazorViewCache.Add(view, students)
        Return view
    End Function
    Public Shared Function CreateInstance(students As List(Of Student)) As IndexView
        Return CreateInstance(students, Nothing)
    End Function

    Public Property ViewBag As Object Implements IVazorView.ViewBag

    ' Public Property ModelState As ModelStateDictionary Implements IVazorView.ModelState

    Public Property Name As String = "Index" Implements IVazorView.Name

    Public Property Path As String = "Views\Home" Implements IVazorView.Path

    Public Function Vazor() As XElement Implements IVazorView.Vazor
        ViewBag.Title = "Vazor Sample"
        Return _
 <vazor>
     <h3> Browse Students</h3>
     <p>Select from <%= students.Count() %> students:</p>
     <ul>
         <%= (Iterator Function()
                  For Each std In students
                      Yield <li><%= std.Name %></li>
                  Next
              End Function)() %>
     </ul>
     <script>
        var x = 5;
        document.writeln("students count = <%= students.Count() %>");
    </script>
 </vazor>

    End Function

    Dim _content As Byte()
    Public ReadOnly Property Content() As Byte() Implements IVazorView.Content
        Get
            If _content Is Nothing Then
                Dim html = Vazor().ToHtmlString()
                _content = System.Text.Encoding.UTF8.GetBytes(html)
            End If
            Return _content
        End Get
    End Property

End Class
