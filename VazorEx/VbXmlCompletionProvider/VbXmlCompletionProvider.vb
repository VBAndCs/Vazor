' Modified By Mohammad Hamdy Ghanem
' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.Composition
Imports System.Threading
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Completion
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax

Namespace Microsoft.CodeAnalysis.VisualBasic.Completion.Providers
    <ExportCompletionProvider(NameOf(VbXmlCompletionProvider), LanguageNames.VisualBasic)>
    <[Shared]>
    Friend Class VbXmlCompletionProvider
        Inherits AbstractVbXmlCompletionProvider

        <ImportingConstructor>
        Public Sub New()
            MyBase.New(s_defaultRules)
        End Sub

        Protected Overrides Async Function GetItemsWorkerAsync(document As Document, position As Integer, trigger As CompletionTrigger, cancellationToken As CancellationToken) As Task(Of IEnumerable(Of CompletionItem))
            Dim items = New List(Of CompletionItem)()

            Try
                Dim tree = Await document.GetSyntaxTreeAsync(cancellationToken).ConfigureAwait(False)
                Dim token = tree.FindTokenOnLeftOfPosition(position, cancellationToken)

                Dim parents = token.GetAncestors(Of XmlElementSyntax)()

                Dim XmlRootElement = parents(parents.Count - 1)

                If parents.Count = 0 Then Return Nothing

                Dim root = GetStartTagName(XmlRootElement)
                If root <> VbXmlRootName AndAlso root <> ZmlRootName Then
                    Return Nothing
                End If

                If token.ValueText = "<%" OrElse (token.ValueText = "%" AndAlso token.GetPreviousToken().ValueText = "<") Then
                    items.Add(GetItem(LambdaExpression, True))
                    Return items
                End If

                If token.ValueText = "<(" OrElse (token.ValueText = "(" AndAlso token.GetPreviousToken().ValueText = "<") Then
                    items.Add(GetItem(IteratorLambdaExpression, True, "("))
                    Return items
                End If

                Dim parent = parents(0)
                Dim trueParent = If(parent.ToString().StartsWith(token.ValueText) OrElse parent.ToString().StartsWith("<" & token.ValueText), parent.Parent, parent)

                ' If the user is typing in xml text, don't trigger on backspace.
                If token.IsKind(SyntaxKind.XmlTextLiteralToken) AndAlso
                    trigger.Kind = CompletionTriggerKind.Deletion Then
                    Return Nothing
                End If

                Dim AddCloseTag = ShouldAddCloseTag(token.GetNextToken(), trueParent.Span.End)

                ' Never provide any items inside a cref
                If token.Parent.IsKind(SyntaxKind.XmlString) AndAlso token.Parent.Parent.IsKind(SyntaxKind.XmlAttribute) Then
                    Dim attribute = DirectCast(token.Parent.Parent, XmlAttributeSyntax)
                    Dim name = TryCast(attribute.Name, XmlNameSyntax)
                    Dim value = TryCast(attribute.Value, XmlStringSyntax)
                    If name?.LocalName.ValueText = CrefAttributeName AndAlso Not token = value?.EndQuoteToken Then
                        Return Nothing
                    End If
                End If

                Dim declaration = parent.GetAncestor(Of DeclarationStatementSyntax)()

                ' Maybe we're going to suggest the close tag
                If token.Kind = SyntaxKind.LessThanSlashToken Then
                    Return GetCloseTagItem(token, AddCloseTag)
                ElseIf token.IsKind(SyntaxKind.XmlNameToken) AndAlso token.GetPreviousToken().IsKind(SyntaxKind.LessThanSlashToken) Then
                    Return GetCloseTagItem(token.GetPreviousToken(), AddCloseTag)
                End If

                Dim semanticModel = Await document.GetSemanticModelForNodeAsync(declaration, cancellationToken).ConfigureAwait(False)
                Dim symbol As ISymbol = Nothing

                If declaration IsNot Nothing Then
                    symbol = semanticModel.GetDeclaredSymbol(declaration, cancellationToken)
                End If

                If symbol IsNot Nothing Then
                    ' Maybe we're going to do attribute completion
                    If TryGetAttributes(token, position, items, symbol) Then Return items
                End If

                If trueParent.IsKind(SyntaxKind.XmlElement) Then
                    Dim xmlNameOnly = token.IsKind(SyntaxKind.LessThanToken) OrElse token.Parent.IsKind(SyntaxKind.XmlName)
                    Dim includeKeywords = Not xmlNameOnly
                    AddXmlElementItems(items, trueParent, AddCloseTag)
                Else ' Consider this as the vbxml tag to show all html tags
                    AddXmlElementItems(items, XmlRootElement, AddCloseTag)
                End If

                Return items
            Catch e As Exception When ReportWithoutCrashUnlessCanceled(e)
                Return items
            End Try
        End Function

        Private Function ReportWithoutCrashUnlessCanceled(Exception As Exception) As Boolean

            If TypeOf Exception Is OperationCanceledException Then Return False

            Return True
        End Function

        Private Function ShouldAddCloseTag(token As SyntaxToken, endPos As Integer) As Boolean
            If token.Span.Start > endPos Then Return True

            Dim txt = token.ValueText
            If txt = "" OrElse txt.Contains("<") OrElse txt = "%>" Then Return True
            If txt.Contains(">") Then Return False
            Return ShouldAddCloseTag(token.GetNextToken(), endPos)
        End Function

        Private Sub AddXmlElementItems(items As List(Of CompletionItem), xmlElement As SyntaxNode, AddCloseTag As Boolean)
            Dim startTagName = GetStartTagName(xmlElement)
            items.AddRange(GetChildItems(startTagName, AddCloseTag))
        End Sub

        Private Function GetCloseTagItem(
                               token As SyntaxToken,
                               addCloseTag As Boolean
                     ) As IEnumerable(Of CompletionItem)

            Dim endTag = TryCast(token.Parent, XmlElementEndTagSyntax)
            If endTag Is Nothing Then Return Nothing

            Dim element = TryCast(endTag.Parent, XmlElementSyntax)
            If element Is Nothing Then Return Nothing

            Dim startElement = element.StartTag
            Dim name = TryCast(startElement.Name, XmlNameSyntax)
            If name Is Nothing Then Return Nothing

            Dim nameToken = name.LocalName
            Dim endTagName = TryCast(endTag.Name, XmlNameSyntax)?.LocalName.ValueText

            If nameToken.ValueText = endTagName Then Return Nothing

            If Not nameToken.IsMissing AndAlso nameToken.ValueText.Length > 0 Then
                Return New List(Of CompletionItem) From {
                        CreateCompletionItem(
                              nameToken.ValueText,
                              nameToken.ValueText & If(addCloseTag, ">", ""),
                              "",
                              endTagName)
                   }
            End If

            Return Nothing
        End Function

        Private Shared Function GetStartTagName(element As SyntaxNode) As String
            Return DirectCast(DirectCast(element, XmlElementSyntax).StartTag.Name, XmlNameSyntax).LocalName.ValueText
        End Function

        Private Function TryGetAttributes(token As SyntaxToken,
                                     position As Integer,
                                     items As List(Of CompletionItem),
                                     symbol As ISymbol) As Boolean

            Dim tagNameSyntax As XmlNameSyntax = Nothing
            Dim tagAttributes As SyntaxList(Of XmlNodeSyntax) = Nothing '

            Dim tokenText = token.ValueText
            tokenText = If(token.TrailingTrivia.Count > 0 And position > token.Span.End, "", token.ValueText)

            Dim startTagSyntax = token.GetAncestor(Of XmlElementStartTagSyntax)()
            If startTagSyntax IsNot Nothing Then
                tagNameSyntax = TryCast(startTagSyntax.Name, XmlNameSyntax)
                tagAttributes = startTagSyntax.Attributes
            Else

                Dim emptyElementSyntax = token.GetAncestor(Of XmlEmptyElementSyntax)()
                If emptyElementSyntax IsNot Nothing Then
                    tagNameSyntax = TryCast(emptyElementSyntax.Name, XmlNameSyntax)
                    tagAttributes = emptyElementSyntax.Attributes
                End If

            End If

            If tagNameSyntax IsNot Nothing Then
                Dim targetToken = GetPreviousTokenIfTouchingText(token, position)
                Dim tagName = tagNameSyntax.LocalName.ValueText
                If tagName = "" Then Return False

                Dim nextTokenText = token.GetNextToken().ValueText

                If targetToken.IsChildToken(Function(n As XmlNameSyntax) n.LocalName) AndAlso targetToken.Parent Is tagNameSyntax Then
                    ' <exception |
                    items.AddRange(GetAttributes(tagName, tokenText, nextTokenText, tagAttributes))
                    Return True
                    '<exception a|
                ElseIf targetToken.IsChildToken(Function(n As XmlNameSyntax) n.LocalName) AndAlso targetToken.Parent.IsParentKind(SyntaxKind.XmlAttribute) Then
                    ' <exception |
                    items.AddRange(GetAttributes(tagName, tokenText, nextTokenText, tagAttributes))
                    Return True
                    '<exception a=""|
                ElseIf (targetToken.IsChildToken(Function(s As XmlStringSyntax) s.EndQuoteToken) AndAlso
                            targetToken.Parent.IsParentKind(SyntaxKind.XmlAttribute)) OrElse
                            targetToken.IsChildToken(Function(a As XmlNameAttributeSyntax) a.EndQuoteToken) OrElse
                            targetToken.IsChildToken(Function(a As XmlCrefAttributeSyntax) a.EndQuoteToken) Then

                    items.AddRange(GetAttributes(tagName, tokenText, nextTokenText, tagAttributes))
                    Return True
                    ' <param name="|"
                Else
                    Dim attributeName As String

                    Dim xmlAttributeName = targetToken.GetAncestor(Of XmlNameAttributeSyntax)()
                    If xmlAttributeName IsNot Nothing Then
                        attributeName = xmlAttributeName.Name.LocalName.ValueText
                    Else
                        attributeName = TryCast(targetToken.GetAncestor(Of XmlAttributeSyntax)()?.Name, XmlNameSyntax)?.LocalName.ValueText
                    End If

                    If attributeName <> "" Then
                        If (targetToken.IsChildToken(Function(s As XmlStringSyntax) s.StartQuoteToken) AndAlso
                           targetToken.Parent.IsParentKind(SyntaxKind.XmlAttribute)) OrElse
                           targetToken.IsChildToken(Function(a As XmlNameAttributeSyntax) a.StartQuoteToken) Then
                            Dim delStr = tokenText
                            If delStr = Quote OrElse delStr = "" Then delStr = " "

                            items.AddRange(GetAttributeValueItems(
                                           tagName,
                                           attributeName,
                                           False,
                                           token.GetNextToken.ValueText <> Quote,
                                           delStr
                                       ))
                            Return True
                        ElseIf targetToken.ValueText = "=" AndAlso TypeOf targetToken.Parent Is XmlAttributeSyntax Then
                            items.AddRange(GetAttributeValueItems(tagName, attributeName, True, True, " "))
                            Return True
                        End If
                    End If
                End If
            End If
            Return False
        End Function

        Private Function GetAttributes(tagName As String, attrName As String, nextString As String, attributes As SyntaxList(Of XmlNodeSyntax)) As IEnumerable(Of CompletionItem)
            Dim existingAttributeNames = attributes.Select(AddressOf GetAttributeName).WhereNotNull().ToSet()
            Return GetAttributeItems(tagName, attrName, nextString, existingAttributeNames)
        End Function

        Private Shared Function GetAttributeName(node As XmlNodeSyntax) As String
            Dim nameSyntax As XmlNameSyntax = node.TypeSwitch(
                Function(attribute As XmlAttributeSyntax) TryCast(attribute.Name, XmlNameSyntax),
                Function(attribute As XmlNameAttributeSyntax) attribute.Name,
                Function(attribute As XmlCrefAttributeSyntax) attribute.Name)

            Return nameSyntax?.LocalName.ValueText
        End Function

        Private Shared s_defaultRules As CompletionItemRules =
            CompletionItemRules.Create(
                filterCharacterRules:=FilterRules,
                enterKeyRule:=EnterKeyRule.Never)

    End Class
End Namespace
