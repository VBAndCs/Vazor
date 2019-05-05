Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.HttpsPolicy
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting

Public Class Startup
    Public Sub New(configuration As IConfiguration)
        configuration = configuration
    End Sub

    Public ReadOnly Property Configuration As IConfiguration

    ' This method gets called by the runtime. Use this method to add services to the container.
    Public Sub ConfigureServices(services As IServiceCollection)
        services.Configure(Of CookiePolicyOptions)(
            Sub(options)
                ' This lambda determines whether user consent for non-essential cookies Is needed for a given request.
                options.CheckConsentNeeded = Function(context) True
                options.MinimumSameSitePolicy = SameSiteMode.None
            End Sub)

        services.AddMvc().AddRazorRuntimeCompilation()
        services.AddMvc().AddNewtonsoftJson()

        services.Configure(Of MvcRazorRuntimeCompilationOptions)(
                   Sub(options) options.FileProviders.Add(New Vazor.VazorViewProvider())
           )
    End Sub

    ' This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    Public Sub Configure(app As IApplicationBuilder, env As IWebHostEnvironment)

        Vazor.VazorSharedView.CreateAll()

        If (env.IsDevelopment()) Then
            app.UseDeveloperExceptionPage()
        Else
            app.UseExceptionHandler("/Home/Error")
            ' The default HSTS value Is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts()
        End If

        app.UseHttpsRedirection()
        app.UseStaticFiles()

        app.UseRouting(
             Sub(routes)
                 routes.MapControllerRoute(
                     "default",
                     "{controller=Home}/{action=Index}/{id?}")
                 routes.MapRazorPages()
             End Sub)

        app.UseCookiePolicy()
        app.UseAuthorization()


    End Sub
End Class