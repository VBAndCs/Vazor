Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.Hosting

Module Program
    Sub Main(args As String())
        CreateHostBuilder(args).Build().Run()
    End Sub

    Public Function CreateHostBuilder(args() As String) As IHostBuilder
        Return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(
               Sub(webBuilder)
                   webBuilder.UseStartup(Of Startup)()
               End Sub
        )
    End Function
End Module
