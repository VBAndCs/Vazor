imports System
imports System.Collections.Generic
imports System.Diagnostics
imports System.Linq
imports System.Threading.Tasks
imports Microsoft.AspNetCore.Mvc
imports Microsoft.AspNetCore.Mvc.RazorPages


<ResponseCache(Duration:=0, Location:=ResponseCacheLocation.None, NoStore:=True)>
Public Class ErrorModel : Inherits PageModel

    Public Property RequestId As String

    Public Property ShowRequestId As Boolean = Not String.IsNullOrEmpty(RequestId)

    Public Sub OnGet()
        RequestId = If(Activity.Current?.Id, HttpContext.TraceIdentifier)
    End Sub
End Class
