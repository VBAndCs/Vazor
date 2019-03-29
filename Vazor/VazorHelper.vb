Imports System.Runtime.CompilerServices

Public Module VazorExtensions
    <Extension>
    Public Function ToHtml(x As XElement) As String
        Return x.ToString(SaveOptions.DisableFormatting).
            Replace("<vazor>", "").
            Replace("</vazor>", "").
            Replace("_vazor_amp_", "&")
    End Function

End Module
