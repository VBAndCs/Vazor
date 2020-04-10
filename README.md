# Vazor 2.0
Copyright (c) 2019-2020 Mohammad Hamdy Ghanem.

These are a few lines of code for a programmer, but a giant leap for VB.NET apps!

![vazorpng](https://user-images.githubusercontent.com/48354902/77369645-c1e08f80-6d67-11ea-9929-c62b5529154e.png)

Vazor stands for VB.NET Razor. It allows you to write ASP.NET (both MVC Core and Razor Pages) applications with VB.NET including designing the views with vb.net code imbedded in XML literals which VB.NET supports!

# Vazor Story:
Read about [Vazor history](https://github.com/VBAndCs/Vazor/blob/master/A-Vazor-story.md): how the idea was born, and grown.

# Html5 Auto-Completion in Vazor:
And here is the [VSIX installer](https://github.com/VBAndCs/Vazor/blob/master/vbxmlCompletionProviderVSIX.zip?raw=true) to add an Html5 CompletionProvider inside XML literals to your VS as an extension:
![untitled1](https://user-images.githubusercontent.com/48354902/78731288-9e107280-793f-11ea-8927-db336c28b6f9.jpg)

It provide auto completion only inside vbxml tags:
```VB.NET
Dim x = <vbxml>
   <!—auto completion for HTML 5 is available here -->
</vbxml>
```

You can press Ctrl+space after `<%` to get this block written for you:
```VB.NET
         <%= (Function()
                    Return < />
              End Function)()
         %>
``` 

# A complere Vazor website sample:
[eShopOnWeb_VB.NET](https://github.com/VBAndCs/eShopOnWeb_VB.NET) is a full ASP.NET Core 3.1 in VB.NET powered by Vazor and ZML.

# Adding ZML support in ver 1.6:
Now, you can use ZML tags inside vbxml code, and call ParseZML to compile ZML tags C# Razor code.
For more info, see [ZML repo](https://github.com/VBAndCs/ZML).


# Project and Item Templates
Download this file:
https://github.com/VBAndCs/Vazor/blob/master/VazorTemplateSetup.zip?raw=true
then unzip it. Double-click the file VazorTemplateSetup.vsix to setuo the Vazor templates:

1- A Vazor project template for ASP.NET MVC Core 3.1 .

2- A Vazor project template for ASP.NET Web Pages Core 3.1 .
3- A VazorView item template to add a new vazor view (.vazor and .vbxml.vb files) to the MVC project.
4- A VazorPage item template to add a new vazor page (.cshtml, .cshtml.vb, and .vbxml.vb files) to the Razor Pages project.
After installation, open .net and create a new project. In the search box, write Vazor, and choose one of the 4 vazor project templates. In the project created, right-click a folder in solution explorer and select Add/New Item. From the dialoge box select VazorView (if this is an MVC project) or VazorPage (if this is a Razor Pages project).

# Vazor Viewes:
Vazor uses xml literals to compose the HTML code, so all you need is to create a class to represent your view, and make it inherit Vazor.VazorView class. You can name the class as you want, but you must pass the view name without any the extension (like "Index", and "_layout") to the Name property by a call to the base class constructor.
You can define a field or a property to hold your model data (like a list of students), and receive these data through the constructor of the class.
Write the vbxml code that represents the view in the GetVbXml Function.
Do not forget that our view is a VB class, and there is no limit to what you can do with it.

This is an example of a class to represent the Index View. You will find it in the Index.vazor.vb file:

```VB.NET
Imports Microsoft.AspNetCore.Mvc.ViewFeatures
Imports Vazor

Public Class IndexView
    Inherits VazorView

    Public ReadOnly Property Students As List(Of Student)
    Public ReadOnly Property ViewData() As ViewDataDictionary

    Public Sub New(students As List(Of Student), viewData As ViewDataDictionary)
        MyBase.New("Index", "Views\Home", "Hello")
        Me.Students = students
        Me.ViewData = viewData
        viewData("Title") = Title
    End Sub

    Public Shared Function CreateNew(Students As List(Of Student), viewData As ViewDataDictionary) As String
        Return VazorViewMapper.Add(New IndexView(Students, viewData))
    End Function
End Class
```

note that you need to call the `IndexView.CreateNew` method and pass it as a parameter to the `View` method in the action method, like this:
```VB.NET
Public Class HomeController : Inherits Controller

  Public Function Index() As IActionResult
     Return View(IndexView.CreateNew(Students, ViewData), Students)
  End Function

  '......
End Class
```

The only step left is to design the view.
I separated the vbxml code in a partial class, so the view design is separated from vazor code. You will find this in the Index.vbxml.vb file:
```VB.NET
Partial Public Class IndexView
    Public overrides Function GetVbXml(view As IndexView) As XElement
        Return _
 _
        <vbxml>
            <h3> Browse Students</h3>
            <p>Select from <%= view.Students.Count() %> students:</p>
            <ul>
                <%= (Iterator Function()
                         For Each std In view.Students
                             Yield <li><%= std.Name %></li>
                         Next
                     End Function)() %>
            </ul>
            <p>Students details:</p>
            <ul>
                <li ForEach="m">
                Id: <m.Id/><br/>
                Name: <m.Name/><br/>
                    <p>Grade: <m.Grade/></p>
                </li>
            </ul>
            <script>
                 var x = 5;
                 document.writeln("students count = <%= view.Students.Count() %>");
        </script>
        </vbxml>

    End Function

End Class
```


# VBXML Code Rules:
In vbxml code you can follow these rules:
* XML literals have only one root. So, it you don't eant to add extra html5 tag to contain the page content, wrap your code in a `<vbxml>` tag.
* All html tags and their attributes can be represented in XML, but there is no intellisense support for them until now.
* Use Razor conventions and tools, like helper tags, sections, partial views, scripts… etc. 
* Use `<%= VBCode %>` to insert vb code.
* You can use @VBCode, but vb will consider it as a plain text, so you will have no intellisense for it, but it will be evaluated by Razor in runtime. This is why you must use c# syntax for expressions written after the @ symbol.
* Use inline-invoked lambda expression to imbed code blocks, like given in the above sample.
* You can use C# code blocks, but vb will treat them as a plain text with no intellisense or syntax check. They will be compiled by Razor in runtime. 
* Instead of using VB Eor Each, you can use Vazor data templates, by adding the ForEach="m" attribute to in the tag you want to repeat for each elemnt in the data model, like this part in the above example:
```VB.NET
     <ul>
         <li ForEach="m">
             <p>Id: <m.Id/></p>
             <p>Name: <m.Name/></p>
             <p>Grade: <m.Grade/></p>
         </li>
     </ul>
```

this code will add an `<li>` element for each student. To make this happen, override the VazorView.Content property and call the extension method ParseTemplate() and pass the students list to it, so it evaluates the template. Add this to the IndexView class:
```VB.NET
    Public Overrides ReadOnly Property Content() As Byte()
        Get
            Dim html = GetVbXml().ParseTemplate(students)
            Return Encoding.GetBytes(html)
        End Get
    End Property
```

If you don't want to use the data template, use the ToHtmlString like this:
```VB.NET
Dim html = GetVbXml().ToHtmlString()
```

Note that the original Content propert written in the VazorView class calls the ParseZml method to allow you to use ZML tangs insid vbxml code.
```VB.NET
    Public Overridable ReadOnly Property Content() As Byte()
        Get
            Dim html = GetVbXml().ParseZml
            Return Encoding.GetBytes(html)
        End Get
    End Property
```

# How does Vazor work?
Vazor uses IFileProvider to define a virtual file system that delivers the html content produced by the View class, to Razor, so that Razor thinks it is a cshtml view and complete the job for us! So, Razor resolves the tag helpers, paths, combine the layout and sections, and do all other stuff!
So in fact, Vazor is just a bridge between the powerful XML literals in VB.NET, and the powerful flexible Razor Engine! 
The amazing thing here is that we can have a mixed Vazor/Razor in the same project! This means you can write some views as Vazor classes, and write some others as Razor views and they will integrate an co-work smoothly!
This is important to save us unnecessary effort to convert Razor views that doesn't contain any code (like the layout page and View imports pages.. etc) to Vazor classes! I converted the layout to Vazor class in the sample project just to prove that all the parts of it can be handled and work normally.
The following image shows the rendered Page resulted from:
* layout and Index as a Vazor classes.
* the rest of the parts (like _viewstart and _viewimports) are Razor cshtml files!

![untitled1](https://user-images.githubusercontent.com/48354902/55183329-3eae4d00-5198-11e9-933d-49e9264c8161.jpg)

# Useing Vazor Views:
* To use Vazor view classes instead of cshtml files, configure the virtual file system by adding this to the Startup.ConfigureServices method. For MVC apps use:
```VB.NET
services.AddControllersWithViews(). ' Enable Vazor
   AddRazorRuntimeCompilation(
        Sub(options) options.FileProviders.Add(New Vazor.VazorViewProvider())
   ) 
 ```

* If you converted the _Layout.cshtml view to a Vazor Layout class (as in 

the sample project), let it inherit the VazorSharedView class defiended in 

Vazor 1.8.3, so you can create and map all shared views by using this 

single line of code in the Startup.Configure method:
`Vazor.VazorSharedView.CreateAll( )`
The layout view has a static content, so we will have only one instance of it 

to use with all pages.
You must do the same for any shared view class with a static content, that 

doesn't contain any vb code, so its html output is always the same. If the 

layout has a different title for each page, use 

`<Title>@ViewBag.Title</Title>` in vbxml code to let Razor evaluate this. 

Yes, this is a Vazor/Razor mixed view!
If you used `<Title><%= ViewBag.Title% ></Title>` VB will try to evaluate it and will cause an exception because the ViewBag is empty at this moment. 

The VazorViewMapper appends a unique Id to the name of the view. Remember that many users can open the same page in the same time, and the model data can be different for each of them, so the ViewIndex class will generate a different page for each user. VazorViewMapper gives each page a unique name, so Razor can render them for each user correctly.
You should use the same way in all action methods (of course with the appropriate view class)

* To use the page View classes in Razor Pages projects, configuare the Vazor virtual file provider in the Startup.ConfigureServices Method like this:
For Razor pages apps use:
```VB.NET
services.AddRazorPages(). ' Enable Vazor
   AddRazorRuntimeCompilation(
        Sub(options) options.FileProviders.Add(New Vazor.VazorViewProvider())
   )
```

To use Vazor to design Razor pages, we have to keep the cshtml file of the page body, because it is the entry point that references the PageModel calss. So, we will use the cshtml file as an empty shell and inject our vbxml view inside it as a partial view. Take the Index.cshtml file as an example:

```Razor
@page
@model IndexModel

    <div>
        @Html.Partial(Model.ViewName + ".cshtml")
    </div>
```

We we can use the Index.vazor.vb and Index.vbxml.vb files to define the partial view the same way we did in MVC project, and add a ViewName property in the code behind Index.cshtml.vb  file to map our vazor view as the partial view to be injected in the cshtml file like this:
```VB.NET
Public Class IndexModel : Inherits PageModel

    Public Property ViewName As String
        Get
            Dim iv = New IndexView(Students, ViewData)
            ViewName = Vazor.VazorViewMapper.Add(iv)
        End Get
        Private Set(value As String)

        End Set
    End Property

    Public Sub OnGet()

    End Sub

End Class
```

And that is all!
You can now write ASP.NET Core apps with VB.NET, and design your view as:
1- A pure cshtml file.
2- A cshtml file injected with vbxml view.
3- A pure vbxml view.
4- A vbxml view that contains some @Razor expressions an C# code blocks!

And you have a mix of all those type of views in one project!
Have fun.

# To Do:
1. We need editor support for html5 in xml literals, like intellisense support for tag names, attributes and Tag Helpers.
2. I hope VB allows us to write code directly inside `<( )>` without the using lambda expressions tricks.

I hope VB team give us the support wee need to make the most of xml literals.

Mohammed Hamdy Ghanem,
Egypt.
