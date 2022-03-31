Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports Vazor

Friend Module Program
  Public Sub Main(args As String())
    Dim provider As VazorViewProvider
    Dim builder As WebApplicationBuilder
    Dim app As WebApplication

    VazorSharedView.CreateAll()

    provider = New VazorViewProvider
    builder = WebApplication.CreateBuilder
    builder.
      Services.
      AddControllersWithViews.
      AddRazorRuntimeCompilation(Sub(options)
                                   options.FileProviders.Add(provider)
                                 End Sub)

    app = builder.Build

    If app.Environment.IsDevelopment Then
      app.UseDeveloperExceptionPage
    Else
      app.UseExceptionHandler("/Home/Error")
      ' The default HSTS value Is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      app.UseHsts
    End If

    app.UseHttpsRedirection
    app.UseStaticFiles

    app.UseRouting
    app.UseAuthorization

    app.UseEndpoints(
         Sub(routes)
           routes.MapControllerRoute(
             name:="default", pattern:="{controller=Home}/{action=Index}/{id?}")
         End Sub)

    app.Run()
  End Sub
End Module
