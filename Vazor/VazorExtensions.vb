Imports System.Runtime.CompilerServices

Public Module VazorExtensions
    <Extension>
    Public Function ToHtmlString(x As XElement, ParamArray tagsToRemove() As String) As String
        Dim html = x.ToString(SaveOptions.DisableFormatting).
            Replace("<xvbxml>", "").
            Replace("</xvbxml>", "").
            Replace("_vazor_amp_", "&")

        For Each tag In tagsToRemove
            Dim open, close As String
            If tag.StartsWith("<") Then
                open = tag
                close = tag.Insert(1, "/")
            Else
                open = "<" + tag
                close = "</" + tag
            End If

            If Not tag.EndsWith(">") Then
                open += ">"
                close += ">"
            End If

            html = html.Replace(open, "").
                       Replace(close, "")

        Next

        Return html
    End Function

End Module
