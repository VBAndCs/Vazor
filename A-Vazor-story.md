# I wanted to help:
It all started in 16 Mar 2019, when I tired waiting to get vbhtml support in Razor, so, I decided to do it myself. I got the source of ASP.NET Core project, but failed to build it on my machine, as it was a transitional period to VS.NET 2019 and .Net Core 3 and seems I missed something. Without a compiled source that I can trace, and with the lack of documentation about this source, I gave it up, and decided to go another way. 4 days later, in 20 Mar, I wrote this:
> I want to simplify the problem of vbhtml, by using a code behind file, to eliminate the need for parsing, compiling and debugging the vbhtml file, because it only contains a static placeholders that will be replaced with the evaluated properties at runtime. This will leave this file as a prototype html file and the code-behind file is just a normal vb file which doesn't need any special treatment!

Read the [full details and example of this raw idea](https://github.com/dotnet/vblang/issues/396#issuecomment-474613429)
I concluded:
> This is a simple divide and conquer approach, which makes the problem too simple. We may need to write some longer code in the code behind file, but it is direct and most of it can be generated with snippets or code generators. On the other hand, the vbhtml file will be cleaner. If some one is willing to help me do this, we can have VB razor up and running in a few days


I got too exited and started right away. After a few hours I was stroke with I very pleasant discovery :
> Surprise: VB.NET has a built-in razor pages! That is the XML literals, which can be easily used to "razor" Html5 code! 

Look at this [sample code](https://github.com/dotnet/vblang/issues/396#issuecomment-474785286)
I concluded:
> The only thing we need is an ASP.NET core template for VB, and a little adjustments to generate the render HTML page from the View1.Razor property! voila!

# VBRazor:
The next day, 21 Mar, I announced the first 
working VB.NET ASP.NET MVC Core Razor sample. I implemented a simple VBRazorViewEngine in the VbRazor project, published on the [VB.NET-Razor repo](https://github.com/VBAndCs/VB.NET-Razor)
I announce that [here](https://github.com/dotnet/vblang/issues/396#issuecomment-475199389)
@Nukepayload2 and @ericmutta gave it thumps up. 
I faced some issues about XML literals, that I listed in [this topic](https://github.com/dotnet/vblang/issues/397)
On top of them, the lake of Intellisense support for Html5 in XML literals, and broken xsd-based intellisense in Roslyn. I solved this issue myself recently as I will mention later.
I got a positive feedback form VB.NET developers.
For example, @DualBrain commented on 22 Mar 2019:
> This is AWESOME! Now I might actually take some time to wrap my head around Razor (when, of course, I can come up from air from my current workload). ;-)

@KathleenDollard, a VB.NET project manager,  commented on 22 Mar 2019:
> This is a really interesting direction.
> I think there may be benefit in not calling it Razor as that is often associated with the interpretation of pages, which invites an unnecessary comparison).

# Vazor:
I tried to complete the VBRazorViewEngine, but I faced some difficulties with Tag Helpers and other stuff, so, I changed my way on 25 Mar 2019:
> I want to use a new approach. I can generate a simplified cshtml file from the vbxml code , so the C# Razor can carry out all the work as usual! This will:
> 1- Simplify my job (no need to re-invent the Razor View Engine)
> 2- Eliminate the need to do any future development, as MS is the responsible for improving Razor.

So, I changed the name form VBRazor to Vazor, as Kathleen Dollard advised. 
In 28 Mar 2019, I announced:
> I am excited to announce that: My Work Is Complete!
> I found an easy solution to make Razor do all its magic for our Vazor!
> All I had to do, is to use the IFileProvider to define a virtual file system that delivers the html output produced from Vazor to Razor as if it is the cshtml view that Razor expects! So, Razor resolves the tag helpers, paths, combine the layout and sections, and any other stuff!
> The amazing thing here is that we have a mixed Vazor/Razor! This means we can write some views as Vazor classes, and write some other as Razor views and they will integrate and co-work smoothly!
> This is important to save us unnecessary effort to convert to Vazor classes! any Razor view that doesn't contain any code (like the layout page and View imports pages.. etc).

I published this work at a new repo called [Vazor](https://github.com/VBAndCs/Vazor)
and concluded:
> It seems like a few lines of code, but it is a big step for VB.NET : )

So, practically, the whole thing took less than 2 weeks, since I got the first intention, and only 9 days to implement it with XML literals.

# Note:
In 27 Mar 2019 I discovered that there was an old attempt to use XML literals to create ASP pages:
> Surprise: I found this [old project from 2009, that uses XML literals to create the Views!](https://archive.codeplex.com/?p=vbmvc)
> The project is before razor pages (talking about ASPX pages, and seems using ASP MVC1) and I couldn't load the sample in VB.NET 2019!
> Its described as: View Engine for ASP.NET MVC using VB.NET XML Literals instead of traditional ASP.NET pages. Each view is a VB.NET class and master pages are base classes. Views are compiled into the assembly, so no ASPX files to deploy. Based on code by Dmitry Robsman

This project was not that important those days, since VB.NET already had vbhtml support.

# ZML:
After that I tried to translate eShopOnWeb from C# to VB.NET to try Vazor in actual full app.
I faced little situations that make vbxml code longer than cshtml code, so, in 11 Apr 2019, I came up with a new idea: write structural programming code as XML tags, that can be translated to C# code, so we can have a homogeneous XML code that represents both C# and Html5. This new "XML Razor" is built on top of C# Razor, but is language independent, so, it can be used in C#, VB.NET, and F#. I referred to the term "raZor in xML" as ZML. For example, this is how we can write a VB for loop in ZML:
```XML
<z:for type="Integer" i="0" to="10" step="2">
      <!-- add code or html tags here -->
</z:for>
```
This the [first birth of the idea](https://github.com/dotnet/aspnetcore/issues/9270)
In on 25 April 2019, I announced that ZML has its own [reop](https://github.com/VBAndCs/ZML) and [NuGet](https://www.nuget.org/packages?q=zml)
Vazor also had its [NuGet](https://www.nuget.org/packages/Vazor)
Hence, we can now design web pages in VB.NET with pure Vazor, or our ZML, or use zml tags inside vbxml code in Vazor!

# Html5 Auto Completion:
I stopped working on Vazor and ZML at the end of June 2019, waiting to any of the issues I reported in Roslyn and VBLang repos to be solved. 
In Mar 2020, I revisited Vazor again in its anniversary, updated Vazor to work with ASP.NET core 3.1, and fully translated eShopOnWeb to VB and updated it to ASP.Net Core 3.1, then decided to provide Html auto completion in XML Literals to make using Vazor easy and productive.
I took me two weeks to do it. I thought:
> There is already some limited version of XML completion provider, used for XML document comments, so, why don't I look at?

I did, and this gave me a kick start to write my first completion provider with zero previous knowledge of the subject, and it really was easy, letting me focus on learning xsd format and the .NET XML Schema API used to parse it, which is really hard and needs to be more user friendly. I needed to search for answers in every step forward.
Once again, I found the completion provider waiting for me in VB.Net, like I found Xml Literals waiting to be used as a Razor! 
It is amazing that I discover every time that all gradients are there in VB.NET ready to be used. It is a waste to let such a powerful language go away.

# Start using it now:
You can use Vazor in production today. All you need is to:
1. Install [Vazor project templates](https://github.com/VBAndCs/Vazor/blob/master/VazorTemplateSetup.zip?raw=true)
2. Install [Html5 completion provider VS extension](https://github.com/VBAndCs/Vazor/blob/master/vbxmlCompletionProviderVSIX.zip?raw=true)
3. Use the instructions in the [ReadMe file](https://github.com/VBAndCs/Vazor/blob/master/README.md)
4. Use [eShopOnWeb.VB](https://github.com/VBAndCs/eShopOnWeb_VB.NET) as a guide app.
5. Have fun :)

# To do:
1. I need to provide auto completion for Tag Helpers and ZML tags.
2. I am working now on creating a real Vazor View Engine. Yes, I iam going back to complete the original VBRazor idea, which can make using tag helpers in vbxml easier and more powerful. This will be an upgrade to Vazor, and will not brake any existing Vazor code.

# More Possibilities:
@AnthonyDGreen, a former VB.NET project manager, picked up the XML literal idea and started to put its powerfulness in use in [Pattern-Based XML Literals](https://github.com/dotnet/vblang/issues/483), which leveraged the idea to use not only in ASP.NET Core, but also WPF and Xamarin. This is why I froze my Vazor for a year waiting for his work to be released someday in VB.NET, but unfortunately, he quitted Microsoft last January, and [VB.Net was declared frozen in March 2020](https://devblogs.microsoft.com/vbteam/visual-basic-support-planned-for-net-5-0/) with no new changes to the compiler anymore, so, I had to go back to my Vazor and go on.

# Response:
In 16 April  2019, [Vazor got a mention on the ASP.NET Community Standup](https://youtu.be/ap60h3eQE5Y?t=310) although it was too primitive and has no intellisense support.
And [Vazor repo](https://github.com/VBAndCs/Vazor) got 20 stars so far.
