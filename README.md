# Vazor 1.0
These are a few lines of code for a programmer, but a giant leap for VB.NET apps!

Vazor stands for VB.NET Razor. It allows you to write ASP.NET MVC Core applications with VB.NET including designing the views with vb.net code imbedded in XML literals which VB.NET supports!
Till now, there is no project template for that, so, you need to clone this repo to your pc, and modify app1 to build your web app. I welcome all contributions to enhance this works and create a project template.

# Create a Vazor View:
Vazor uses xml literals to compose the HTML code, so all you need is to create a class to represent your view, and make it implements IVazorView Interface. You can name the class as you want, but you must put the view name without any the extension (like "Index", and "_layout") in the Name property. 
You can define a field or a property to hold your model data (like a list of students), and receive these data through the constructor of the class.
You can write the vbxml code that represents the view in the GetVbXml Function, and use the Content property to deliver the rendered View as a byte array. I made this additional step to allow any further processing of the HTML content away from the vbxml code. For example, I cache the content byte array in a field named _content, so that multiple calls to the Content property will call the GetVbXml function only once, then use the cached content afterwards. 

> Note: don't use the _content cache technique if you expect changes in the model data or the view page that makes the view changes. Or you can just create a new instance of the view class every time you. I supplied a caching mechanism in the CreateInstance method in the IndexView class, which uses the VazorViewCache class to save an instance of the view, and updates it if the model data changes. So, you can always call the IndexView.CreateInstance to get the right instance for the job. 
> Alternatively, if you have a large page, with only small sections that can change, you can separate the vbxml code in more than one function, cache the static parts, and join all of them in the content. Do not forget that the view is a VB class, and there is no limit to what you can do with it.

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

    Public Function GetVbXml() As XElement Implements IVazorView.GetVbXml
        ViewBag.Title = "Vazor Sample"
        Return _
 <vbxml>
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
 </vbxml>

    End Function

Dim _content As Byte()
    Public ReadOnly Property Content() As Byte() Implements IVazorView.Content
        Get
            If _content Is Nothing Then
                Dim html = GetVbXml().ToHtmlString()
                _content = System.Text.Encoding.UTF8.GetBytes(html)
            End If
            Return _content
        End Get
    End Property
End Class
```

> Note: You can use all Razor conventions, like helper tags, sections, partial views, scripts… etc. All html tags can be represented in XML. You only use `<%= VBCode %>` instead using @VBCode, and use inline-invoked lambda expression to imbed code blocks, like given in the above sample.

# How does Vazor work?
Vazor uses IFileProvider to define a virtual file system that delivers the html content produced by the View class, to Razor, so that Razor thinks it is a cshtml view and complete the job for us! So, Razor resolves the tag helpers, paths, combine the layout and sections, and do all other stuff!
So in fact, Vazor is just a bridge between the powerful XML literals in VB.NET, and the powerful flexible Razor Engine! 
The amazing thing here is that we can have a mixed Vazor/Razor in the same project! This means you can write some views as Vazor classes, and write some others as Razor views and they will integrate an co-work smoothly!
This is important to save us unnecessary effort to convert Razor views that doesn't contain any code (like the layout page and View imports pages.. etc) to Vazor classes! I converted the layout to Vazor class in the sample project just to prove that all the parts of it can be handled and work normally.
This following image shows the rendered Page resulted from:
- layout and Index as a Vazor classes.
- the rest of the parts (like _viewstart and _viewimports) are Razor cshtml files!

![untitled1](https://user-images.githubusercontent.com/48354902/55183329-3eae4d00-5198-11e9-933d-49e9264c8161.jpg)

# Useing Vazor Views:
* To use Vazor views, configure the virtual file system by adding this to the Startup.ConfigureServices method:
```VB.NET
services.Configure(Of MvcRazorRuntimeCompilationOptions)(
  Sub(options) options.FileProviders.Add(New Vazor.VazorViewProvider())
)
```

* If you converted the _Layout.cshtml view to a Vazor Layout class (as in the sample project), you must map it in the Startup.Configure method. The layout view has a static content, so we will have only one instance of it to use with all pages. Add this in the  Configure method:
`Vazor.VazorViewMapper.AddStatic(New LayoutView())`
You must do the same for any view class with a static content, that doesn't depend on the model data, and its html output is always the same. If the layout has a different title for each page, use `<Title>@ViewBag.Title</Title>` in vbxml code to let Razor evaluate this (Yes, you can use the @ notation!.. This is a Vazor/Razor mixed view!). If you use `<Title><%= ViewBag.Title% ></Title>` VB will try to evaluate it and will cause exception because the ViewBag is empty at this moment. If you need to do more changes in the layout with every page, then don't map it as a static content, and treat it as other dynamic views, which I tell you how in the next line!

* To use the Vazor View classes, map them in the Controllers. For example, the IndexView class should be used in the Home.Index action method like this:
```VB.NET
Public Function Index() As IActionResult
   Dim iv = IndexView.CreateInstance(Students, ViewBag)
   Dim instanceName = Vazor.VazorViewMapper.Add(iv)
   Return View(instanceName)
End Function
```

the VazorViewMapper appends a unique Id to the name of the view. Remember that many users can open the same page in the same time, and the model data can be different for each of them, thus the ViewIndex class will generate a different page for each user. VazorViewMapper gives each page a unique name, so Razor can render them correctly.
You should use the same way in all action methods (of course with the appropriate view class)

And that is all!

# To Do:
1. We need a VB.NET project template.
2. We need more editor support for html5 in xml literals, like intellisense support for tag names, attributes and Tag Helpers.
3. I hope VB allows us to write code directly inside `<%= %>` without the using lambda expressions tricks.

I hope VB team give us the support wee need to make the most of xml literals.

Mohammed Hamdy Ghanem,
Egypt.
