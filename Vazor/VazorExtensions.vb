Imports System.Runtime.CompilerServices

Public Module VazorExtensions
    <Extension>
    Public Function ToHtmlString(x As XElement, ParamArray tagsToRemove() As String) As String
        Dim html = x.ToString(SaveOptions.DisableFormatting).
            Replace("<vbxml>", "").
            Replace("</vbxml>", "").
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

    <Extension>
    Public Function ParseTemplate(Of T)(xml As XElement, model As IEnumerable(Of T)) As String
        Dim result = From elm In xml.Descendants()
                     Where elm.Attribute("ForEach") IsNot Nothing

        Dim newHtml As New Text.StringBuilder(xml.ToString(SaveOptions.DisableFormatting))
        Dim mFields = GetType(T).GetFields()
        Dim mProperties = GetType(T).GetProperties()
        For Each elm In result
            Dim content = elm.ToString(SaveOptions.DisableFormatting)
            Dim attrValue = elm.Attribute("ForEach").Value
            Dim tag = content.Replace($" ForEach={Chr(34) + attrValue + Chr(34)}", "")
            Dim newContent As New Text.StringBuilder()
            For Each m In model
                Dim newTag = tag
                Dim exp As String
                For Each f In mFields
                    exp = $"<{attrValue}.{f.Name} />"
                    If newTag.Contains(exp) Then _
                        newTag = newTag.Replace(exp, f.GetValue(m))
                Next

                For Each p In mProperties
                    exp = $"<{attrValue}.{p.Name} />"
                    If newTag.Contains(exp) Then _
                       newTag = newTag.Replace(exp, p.GetValue(m, Nothing))
                Next
                newContent.AppendLine(newTag)
            Next
            newHtml.Replace(content, newContent.ToString())
        Next

        newHtml.Replace("<vbxml>", "").
            Replace("</vbxml>", "").
            Replace("_vazor_amp_", "&")

        Return newHtml.ToString()
    End Function

End Module
