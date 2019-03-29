# Vazor 1.0
A few lines of code, yet a great leap of VB.NET!

Vazor stands for VB.NET Razor. It allows you to write ASP.NET MVC Core applications with VB.NET including designing the views with vb.net code imbedded in XML literals which VB.NET supports!
Till now, there is no project template for that, so, you need to clone this repo to your pc, and modify app1 to build your web app. I welcome all contributions to enhance this works and create a project template.

# Create a Vazor View:
Vazor uses xml literals to compose the HTML code, so all you need is to create a class to represent your view, and make it implements IVazorView Interface. You can name the class as you want, but you must put the view name without any the extension (like "Index", and "_layout") in the Name property. 
You can define a field or a property to hold your model data (like a list of students), and receive these data through the constructor of the class.
You can write the vbxml code that represents the view in the Vazor Function, and use the Content propert to deliver the rendered View. I made this additional step to allow any further processing of the HTML content away from the vbxml code. For example, I cache the content in a field named _content, so that multiple calls to the Content property will call the Vazor function only once, then use the cached content. 
 This is an example of a class to represent the Index View:
```VB.NET
Imports Vazor

Public Class IndexView
    Implements IVazorView

    Dim students As List(Of Student)

    public Sub New(students As List(Of Student))
        Me.students = students
    End Sub

    Public Sub New(students As List(Of Student), viewBag As Object)
        Me.students = students
        Me.ViewBag = viewBag
    End Sub

    Public Property ViewBag As Object Implements IVazorView.ViewBag

    Public Property Name As String = "Index" Implements IVazorView.Name

    Public Property Path As String = "Views\Home" Implements IVazorView.Path

    Public Function Vazor() As XElement Implements IVazorView.Vazor
        ViewBag.Title = "Vazor Sample"
        Return _
 <p>
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
 </p>

    End Function

    Dim _content As XElement = Nothing
    Public ReadOnly Property Content() As XElement Implements IVazorView.Content
        Get
            If _content Is Nothing Then _content = Vazor()
            Return _content
        End Get
    End Property

End Class

```

Note: You can use all Razor conventions, like helper tags, sections, partial views, scripts… etc. All html tags can be represented in XML. You only use `<%= VBCode %>` instead using @VBCode, and use inline invojed lambda expression to imbed code blocks, like givin in the above sample.

# How Vazor works?
Vazor is uses IFileProvider to define a virtual file system that delivers the html content produced by the View class, to Razor, so that Razor thinks it is a cshtml view and complete the job for us! So, Razor resolves the tag helpers, paths, combine the layout and sections, and do all other stuff!
So in fact, Vazor is just a bridge to make us of the powerful XML literals in VB.NEt, and the powerful Razor Engine! 
The amazing thing here is that we can have a mixed Vazor/Razor in the same project! This means you can write some views as Vazor classes, and write some others as Razor views and they will integrate an co-work smoothly!
This is important to save us unnecessary effort to convert Razor views that doesn't contain any code (like the layout page and View imports pages.. etc) to Vazor classes! I converted the layout to Vazor class in the sample project just to prove that all the parts of it can be handled and work normally.
This following image shows the rendered Page resulted from:
- layout and Index as a Vazor classes.
- the rest of the parts (like _viewstart and _viewimports) are Razor cshtml files!


# Useing Vazor Views:
If you converted the _Layout.cshtml view to a Vazor Layout class (as in the sample project), you must map it in the Startup.Configure method. The layout has a static content, so we will have only one instance of it to use with all views. Add this in the  Configure method:
`Vazor.VazorViewMapper.AddStatic(New LayoutView())`
You must do the same for any view class with a static content, i.e doesn't depent on the model data, so its html output is always the same. If the layout has a different title for each page, use `<Title>@ViewBag.Title</Title>` to let Razor evaluate this. If you use `<Title><%= ViewBag.Title% ></Title>` VB will try to evaluate it and will cause exception because the ViewBag is empty at this moment. If you need to do more changes in the layout with every page, then don't map it as a static content, and treat it as other dynamic views, which I tell you how in the next line!
To use the Vazor View classes, map them in the Controllers. For example, the IndexView class should be used in the Home.Index action method like this:
```
Public Function Index() As IActionResult
   Dim iv = New IndexView(Students, ViewBag)
   Dim viewUniqueName = Vazor.VazorViewMapper.Add(iv)
   Return View(viewUniqueName)
End Function
```

the VazorViewMapper adds a unique Id to the name of the view. Remember that many users will open tha same page in the same time, and the model data can be different for each of them, thus the ViewIndex class will generate a different view for each of them, so we gave each of these views a unique name, so Razor can render them correctly.
You should use the same way in all action methods (of course with the appropriate view class)
And that is all!

# To Do:
1- We need a VB.NET project template>
2- We need more editor support for html5 in xml literals, like intellisense support for tag names, attributes and Tag Helpers.
3- I hope VB allows us to write code directly inside `<%= %>` without the using lambda expressions tricks.
I hope VB team give us the support wee need to make the most of xml literals.

Mohammed Hamdy Ghanem,
Egypt.
