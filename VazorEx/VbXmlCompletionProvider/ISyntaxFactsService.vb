Option Explicit Off
Option Infer On
Option Strict Off
' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.Collections.Immutable
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Host
Imports Microsoft.CodeAnalysis.Text

Friend Interface ISyntaxFactsService
    Inherits ISyntaxFacts, ILanguageService
    Function IsInInactiveRegion(_syntaxTree As SyntaxTree, position As Integer, _cancellationToken As CancellationToken) As Boolean

    Function IsInNonUserCode(_syntaxTree As SyntaxTree, position As Integer, _cancellationToken As CancellationToken) As Boolean

    Function IsPossibleTupleContext(_syntaxTree As SyntaxTree, position As Integer, _cancellationToken As CancellationToken) As Boolean

    Function GetSelectedFieldsAndPropertiesAsync(_syntaxTree As SyntaxTree, _textSpan As TextSpan, allowPartialSelection As Boolean, _cancellationToken As CancellationToken) As Task(Of ImmutableArray(Of SyntaxNode))

    ' Walks the tree, starting from contextNode, looking for the first construct
    ' with a missing close brace.  If found, the close brace will be added and the
    ' updates root will be returned.  The context node in that new tree will also
    ' be returned.
    ' TODO: This method should be moved out of ISyntaxFactsService.
    Sub AddFirstMissingCloseBrace(Of TContextNode As SyntaxNode)(
root As SyntaxNode, contextNode As TContextNode,
<Runtime.InteropServices.Out> ByRef newRoot As SyntaxNode, <Runtime.InteropServices.Out> ByRef newContextNode As TContextNode)
End Interface
