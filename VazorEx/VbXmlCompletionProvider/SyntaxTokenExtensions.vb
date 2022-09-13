Option Explicit Off
Option Infer On
Option Strict Off
' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

' TODO: Skipped Null-able Directive enable 

Imports System.Threading
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis

Friend Module SyntaxTokenExtensions
    <Extension()>
    Public Function GetAncestor(token As SyntaxToken, predicate As Func(Of SyntaxNode, Boolean)) As SyntaxNode
        Return token.GetAncestor(Of SyntaxNode)(predicate)
    End Function

    <Extension()>
    Public Function GetAncestor(Of T As SyntaxNode
)(token As SyntaxToken, Optional predicate As Func(Of T, Boolean) = Nothing) As T
        Return If(token.Parent IsNot Nothing, token.Parent.FirstAncestorOrSelf(predicate) _
            , CType(Nothing, T))
    End Function

    <Extension()>
    Public Function GetAncestors(Of T As SyntaxNode
)(token As SyntaxToken) As IEnumerable(Of T)
        Return If(token.Parent IsNot Nothing, token.Parent.AncestorsAndSelf().OfType(Of T)(), New List(Of T)())
    End Function

    <Extension()>
    Public Function GetAncestors(token As SyntaxToken, predicate As Func(Of SyntaxNode, Boolean)) As IEnumerable(Of SyntaxNode)
        Return If(token.Parent IsNot Nothing, token.Parent.AncestorsAndSelf().Where(predicate),
            New List(Of SyntaxNode)())
    End Function


    <Extension()>
    Public Function CheckParent(Of T As SyntaxNode
)(token As SyntaxToken, valueChecker As Func(Of T, Boolean)) As Boolean
        ' TODO TASK: VB has no direct equivalent To C# pattern variables 'is' expressions:' Original Statement:
        ' if (!(token.Parent is T parentNode))
        ' {
        ' return false;
        ' }
        ' An attempt was made to correctly port the code, check the code below for correctness
        Dim parentNode As T = TryCast(token.Parent, T)
        If Not (parentNode IsNot Nothing) Then
            Return False
        End If

        Return valueChecker(parentNode)
    End Function

    <Extension()>
    Public Function Width(token As SyntaxToken) As Integer
        Return token.Span.Length
    End Function

    <Extension()>
    Public Function FullWidth(token As SyntaxToken) As Integer
        Return token.FullSpan.Length
    End Function

    <Extension()>
    Public Function FindTokenFromEnd(root As SyntaxNode, position As Integer, Optional includeZeroWidth As Boolean = True, Optional findInsideTrivia As Boolean = False) As SyntaxToken
        Dim token As SyntaxToken = root.FindToken(position, findInsideTrivia)
        Dim previousToken As SyntaxToken = token.GetPreviousToken(
            includeZeroWidth, findInsideTrivia, findInsideTrivia, findInsideTrivia)

        If token.SpanStart = position AndAlso
                previousToken.RawKind <> 0 AndAlso
                previousToken.Span.End = position Then

            Return previousToken
        End If

        Return token
    End Function

    <Extension()>
    Public Function GetNextTokenOrEndOfFile(
            token As SyntaxToken,
            Optional includeZeroWidth As Boolean = False,
            Optional includeSkipped As Boolean = False,
            Optional includeDirectives As Boolean = False,
            Optional includeDocumentationComments As Boolean = False) As SyntaxToken

        Dim nextToken As SyntaxToken = token.GetNextToken(includeZeroWidth, includeSkipped, includeDirectives, includeDocumentationComments)

        Return If(nextToken.RawKind = 0, CType(token.Parent.SyntaxTree.GetRoot(CancellationToken.None), ICompilationUnitSyntax).EndOfFileToken _
            , nextToken)
    End Function

    <Extension()>
    Public Function WithoutTrivia(
token As SyntaxToken) As SyntaxToken
        If Not token.LeadingTrivia.Any() AndAlso Not token.TrailingTrivia.Any() Then
            Return token
        End If

        Return token.With(New SyntaxTriviaList(), New SyntaxTriviaList())
    End Function

    <Extension()>
    Public Function [With](token As SyntaxToken, leading As SyntaxTriviaList, trailing As SyntaxTriviaList) As SyntaxToken
        Return token.WithLeadingTrivia(leading).WithTrailingTrivia(trailing)
    End Function

    <Extension()>
    Public Function WithPrependedLeadingTrivia(
token As SyntaxToken,
ParamArray trivia As SyntaxTrivia()) As SyntaxToken
        If trivia.Length = 0 Then
            Return token
        End If

        Return token.WithPrependedLeadingTrivia(CType(trivia, IEnumerable(Of SyntaxTrivia)))
    End Function

    <Extension()>
    Public Function WithPrependedLeadingTrivia(
token As SyntaxToken,
trivia As SyntaxTriviaList) As SyntaxToken
        If trivia.Count = 0 Then
            Return token
        End If

        Return token.WithLeadingTrivia(trivia.Concat(token.LeadingTrivia))
    End Function

    <Extension()>
    Public Function WithPrependedLeadingTrivia(
token As SyntaxToken,
trivia As IEnumerable(Of SyntaxTrivia)) As SyntaxToken
        Dim list As SyntaxTriviaList = New SyntaxTriviaList()
        list = list.AddRange(trivia)

        Return token.WithPrependedLeadingTrivia(list)
    End Function

    <Extension()>
    Public Function WithAppendedTrailingTrivia(
token As SyntaxToken,
ParamArray trivia As SyntaxTrivia()) As SyntaxToken
        Return token.WithAppendedTrailingTrivia(CType(trivia, IEnumerable(Of SyntaxTrivia)))
    End Function

    <Extension()>
    Public Function WithAppendedTrailingTrivia(
token As SyntaxToken,
trivia As IEnumerable(Of SyntaxTrivia)) As SyntaxToken
        Return token.WithTrailingTrivia(token.TrailingTrivia.Concat(trivia))
    End Function


    <Extension()>
    Public Function TypeSwitch(Of TBaseType, TDerivedType1 As TBaseType, TDerivedType2 As TBaseType, TDerivedType3 As TBaseType, TResult)(obj As TBaseType, matchFunc1 As Func(Of TDerivedType1, TResult), matchFunc2 As Func(Of TDerivedType2, TResult), matchFunc3 As Func(Of TDerivedType3, TResult), Optional defaultFunc As Func(Of TBaseType, TResult) = Nothing) As TResult
        If TypeOf obj Is TDerivedType1 Then
            Return matchFunc1(CType(obj, TDerivedType1))
        ElseIf TypeOf obj Is TDerivedType2 Then
            Return matchFunc2(CType(obj, TDerivedType2))
        ElseIf TypeOf obj Is TDerivedType3 Then
            Return matchFunc3(CType(obj, TDerivedType3))
        ElseIf defaultFunc IsNot Nothing Then
            Return defaultFunc(obj)

        Else
            Return Nothing
        End If
    End Function

End Module
