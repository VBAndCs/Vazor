
Imports System.Runtime.CompilerServices
Imports Microsoft.AspNetCore.Mvc.ModelBinding

Public Interface IVazorView

    Property ViewBag As <Dynamic> Object
    ' Property ModelState As ModelStateDictionary

    Property Name As String

    Property Path As String
    Function GetVbXml() As XElement

    ReadOnly Property Content As Byte()
End Interface
