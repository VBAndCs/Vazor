'' Licensed to the .NET Foundation under one or more agreements.
'' The .NET Foundation licenses this file to you under the MIT license.
'' See the LICENSE file in the project root for more information.

'Imports System.Collections.Immutable
'Imports System.Threading
'Imports Microsoft.CodeAnalysis
'Imports Microsoft.CodeAnalysis.Completion
'Imports Microsoft.CodeAnalysis.Options
'Imports Microsoft.CodeAnalysis.[Shared].Extensions
'Imports Microsoft.CodeAnalysis.Text

'Public MustInherit Class CommonCompletionProvider
'    Inherits CompletionProvider

'    Friend Overridable Function IsInsertionTrigger(text As SourceText, insertedCharacterPosition As Integer, options As OptionSet) As Boolean
'        Return False
'    End Function


'    Public  Overrides Async Function GetChangeAsync(
'                 _document As Document,
'                 item As CompletionItem,
'                  commitKey As Char?,
'                 _cancellationToken As CancellationToken
'             ) As Task(Of CompletionChange)

'        Dim change = If((Await GetTextChangeAsync(_document, item, commitKey, _cancellationToken).ConfigureAwait(False)),
'                New TextChange(item.Span, item.DisplayText))
'        Return CompletionChange.Create(change)
'    End Function

'    Public Overridable Function GetTextChangeAsync(_document As Document, selectedItem As CompletionItem, ch As Char?, _cancellationToken As CancellationToken) As Task(Of TextChange?)
'        Return GetTextChangeAsync(selectedItem, ch, _cancellationToken)
'    End Function

'    Protected Overridable Function GetTextChangeAsync(selectedItem As CompletionItem, ch As Char?, _cancellationToken As CancellationToken) As Task(Of TextChange?)
'        Return Task.FromResult(Of TextChange?)(Nothing)
'    End Function

'    Private Shared ReadOnly _s_suggestionItemRules As CompletionItemRules = CompletionItemRules.Create(_enterKeyRule:=EnterKeyRule.Never)

'    Protected Function CreateSuggestionModeItem(_displayText As String, description As String) As CompletionItem
'        Return CommonCompletionItem.Create(
'            displayText:=If(_displayText, String.Empty),
'            displayTextSuffix:="",
'            description:=Nothing,
'            rules:=_s_suggestionItemRules)
'    End Function
'End Class
