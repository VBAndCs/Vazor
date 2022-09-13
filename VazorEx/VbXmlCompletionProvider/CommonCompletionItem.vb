Imports System.Collections.Immutable
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Completion

Friend Module CommonCompletionItem

    Function Create(ByVal displayText As String, ByVal displayTextSuffix As String, ByVal rules As CompletionItemRules, ByVal Optional glyph As Glyph? = Nothing, ByVal Optional description As ImmutableArray(Of SymbolDisplayPart) = Nothing, ByVal Optional sortText As String = Nothing, ByVal Optional filterText As String = Nothing, ByVal Optional showsWarningIcon As Boolean = False, ByVal Optional properties As ImmutableDictionary(Of String, String) = Nothing, ByVal Optional tags As ImmutableArray(Of String) = Nothing, ByVal Optional inlineDescription As String = Nothing) As CompletionItem
        tags = tags.NullToEmpty()

        If glyph IsNot Nothing Then
            tags = GlyphTags.GetTags(glyph.Value).AddRange(tags)
        End If

        If showsWarningIcon Then
            tags = tags.Add(WellKnownTags.Warning)
        End If

        properties = If(properties, ImmutableDictionary(Of String, String).Empty)


        If Not description.IsDefault AndAlso description.Length > 0 Then
            properties = properties.Add("Description", "")
        End If

        Return CompletionItem.Create(displayText:=displayText, displayTextSuffix:=displayTextSuffix, filterText:=filterText, sortText:=sortText, properties:=properties, tags:=tags, rules:=rules, inlineDescription:=inlineDescription)
    End Function

End Module
