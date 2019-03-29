Imports Microsoft.AspNetCore.Mvc.ModelBinding
Imports Vazor

Public Class LayoutView
    Implements IVazorView

    Public Sub New()

    End Sub
    Public Sub New(viewBag As Object)
        Me.ViewBag = viewBag
    End Sub

    Public Property ViewBag As Object Implements IVazorView.ViewBag

    'Public Property ModelState As ModelStateDictionary Implements ILayoutVazor.ModelState

    Public Property Name As String = "_Layout" Implements IVazorView.Name

    Public Property Path As String = "Views\Shared" Implements IVazorView.Path

    Function Vazor() As XElement Implements IVazorView.Vazor
        Return _
<html>
    <head>
        <meta charset="utf-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
        <title>@ViewBag.Title - WebApplication2</title>

        <environment include="Development">
            <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.css"/>
        </environment>
        <environment exclude="Development">
            <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.3/css/bootstrap.min.css"
                asp-fallback-href="~/lib/bootstrap/dist/css/bootstrap.min.css"
                asp-fallback-test-class="sr-only" asp-fallback-test-property="position" asp-fallback-test-value="absolute"
                crossorigin="anonymous"
                integrity="sha256-eSi1q2PG6J7g7ib17yAaWMcrr5GrtohYChqibrV7PBE="/>
        </environment>
        <link rel="stylesheet" href="~/css/site.css"/>
    </head>
    <body>
        <header>
            <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div class="container">
                    <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">WebApplication2</a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                        <ul class="navbar-nav flex-grow-1">
                            <li class="nav-item">
                                <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
        <div class="container">
            <partial name="_CookieConsentPartial"/>
            <main role="main" class="pb-3">
                @RenderBody()
            </main>
        </div>

        <footer class="border-top footer text-muted">
            <div class="container">
                _vazor_amp_copy; 2019 - WebApplication2 - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
            </div>
        </footer>

        <environment include="Development">
            <script src="~/lib/jquery/dist/jquery.js"></script>
            <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.js"></script>
        </environment>
        <environment exclude="Development">
            <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"
                asp-fallback-src="~/lib/jquery/dist/jquery.min.js"
                asp-fallback-test="window.jQuery"
                crossorigin="anonymous"
                integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=">
            </script>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.3/js/bootstrap.bundle.min.js"
                asp-fallback-src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"
                asp-fallback-test="window.jQuery _vazor_amp__vazor_amp_ window.jQuery.fn _vazor_amp__vazor_amp_ window.jQuery.fn.modal"
                crossorigin="anonymous"
                integrity="sha256-E/V4cWE4qvAeO5MOhjtGtqDzPndRO1LBk8lJ/PR7CA4=">
            </script>
        </environment>
        <script src="~/js/site.js" asp-append-version="true"></script>

    @RenderSection("Scripts", required: false)
</body>
</html>


    End Function

    Dim _content As Byte() = Nothing
    Public ReadOnly Property Content() As Byte() Implements IVazorView.Content
        Get
            If _content Is Nothing Then
                _content = System.Text.Encoding.UTF8.GetBytes(Vazor().ToString(SaveOptions.DisableFormatting).Replace("_vazor_amp_", "&"))
            End If
            Return _content
        End Get
    End Property

End Class