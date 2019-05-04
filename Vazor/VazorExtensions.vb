' ZML Parser: Converts ZML tags to C# Razor statements.
' Copyright (c) 2019 Mohammad Hamdy Ghanem


Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports ZML

Public Module VazorExtensions
    Public Const Ampersand = "__amp__;"
    Public Const GreaterThan = "__gtn__;"
    Public Const LessThan = "__ltn__;"

    <Extension>
    Public Function ToHtmlString(x As XElement, ParamArray tagsToRemove() As String) As String
        Dim html = x.ToString(SaveOptions.DisableFormatting).
            Replace("<vbxml>", "").
            Replace("</vbxml>", "").
            Replace(Ampersand, "&")

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
    Public Function ParseTemplate(Of T)(xml As XElement, model As T) As String
        Dim result = From elm In xml.Descendants()
                     Where elm.Attribute("ForEach") IsNot Nothing

        Dim newHtml As New Text.StringBuilder(xml.ToString(SaveOptions.DisableFormatting))
        Dim mFields() As FieldInfo = Nothing
        Dim mProperties() As PropertyInfo = Nothing

        Dim tag = ""
        Dim attrValue = ""
        Dim newContent As Text.StringBuilder = Nothing

        Dim Evaluate = Sub(m As Object)
                           Dim newTag = tag
                           Dim exp As String
                           For Each f In mFields
                               exp = $"<{attrValue}.{f.Name} />"
                               If newTag.Contains(exp) Then newTag = newTag.Replace(exp, f.GetValue(m))
                           Next

                           For Each p In mProperties
                               exp = $"<{attrValue}.{p.Name} />"
                               If newTag.Contains(exp) Then newTag = newTag.Replace(exp, p.GetValue(m, Nothing))
                           Next
                           newContent.AppendLine(newTag)
                       End Sub

        For Each elm In result
            Dim content = elm.ToString(SaveOptions.DisableFormatting)
            attrValue = elm.Attribute("ForEach").Value
            tag = content.Replace($" ForEach={"""" + attrValue + """"}", "")
            newContent = New Text.StringBuilder()

            Dim modelType = GetType(T)
            Dim type As Type = Nothing
            If modelType.IsGenericType Then
                type = modelType.GetGenericArguments()(0)
            ElseIf modelType.IsArray Then
                type = modelType.GetElementType()
            ElseIf modelType Is GetType(IEnumerable) Then
                type = GetType(Object)
            End If

            If type IsNot Nothing Then
                mFields = type.GetFields()
                mProperties = type.GetProperties()
                Dim list = CType(model, IEnumerable)
                For Each m In list
                    Evaluate(m)
                Next
            Else
                mFields = GetType(T).GetFields()
                mProperties = GetType(T).GetProperties()
                Evaluate(model)
            End If
            newHtml.Replace(content, newContent.ToString())
        Next

        newHtml.Replace("<vbxml>", "").
            Replace("</vbxml>", "").
            Replace(Ampersand, "&")

        Return newHtml.ToString()
    End Function

    <Extension>
    Public Function ParseZML(x As XElement) As String
        Return ZML.ParseZml(x)
    End Function

    <Extension>
    Public Function ParseZML(s As String) As String
        Return ZML.ParseZml(s)
    End Function
End Module
