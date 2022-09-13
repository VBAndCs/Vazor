' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

' TODO: Skipped Null-able Directive enable 

Imports System.Threading
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Host
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.Text

Partial Friend Module SyntaxTreeExtensions

    <Extension()>
    Public Function IsScript(_syntaxTree As SyntaxTree) As Boolean
        Return _syntaxTree.Options.Kind <> SourceCodeKind.Regular
    End Function

    <Extension()>
    Public Function GetTouchingTokenAsync(
_syntaxTree As SyntaxTree,
position As Integer,
_cancellationToken As CancellationToken,
Optional findInsideTrivia As Boolean = False) As Task(Of SyntaxToken)
        Return GetTouchingTokenAsync(_syntaxTree, position, Function(underscore) True, _cancellationToken, findInsideTrivia)
    End Function

    <Extension()>
    Public Async Function GetTouchingTokenAsync(
_syntaxTree As SyntaxTree,
position As Integer,
_predicate As Predicate(Of SyntaxToken),
_cancellationToken As CancellationToken,
Optional findInsideTrivia As Boolean = False) As Task(Of SyntaxToken)
        If _syntaxTree Is Nothing Then Throw New ArgumentNullException()

        If position >= _syntaxTree.Length Then
            Return CType(Nothing, SyntaxToken)
        End If

        Dim root As SyntaxNode = Await _syntaxTree.GetRootAsync(_cancellationToken).ConfigureAwait(False)
        Dim token As SyntaxToken = root.FindToken(position, findInsideTrivia)

        If (token.Span.Contains(position) OrElse token.Span.End = position) AndAlso _predicate(token) Then
            Return token
        End If

        token = token.GetPreviousToken()

        If token.Span.End = position AndAlso _predicate(token) Then
            Return token
        End If

        ' SyntaxKind = None
        Return CType(Nothing, SyntaxToken)
    End Function


    <Extension()>
    Public Async Function IsBeforeFirstTokenAsync(
_syntaxTree As SyntaxTree, position As Integer, _cancellationToken As CancellationToken) As Task(Of Boolean)
        Dim root As SyntaxNode = Await _syntaxTree.GetRootAsync(_cancellationToken).ConfigureAwait(False)
        Dim firstToken As SyntaxToken = root.GetFirstToken(includeZeroWidth:=True, includeSkipped:=True)

        Return position <= firstToken.SpanStart
    End Function


    ''' <summary>
    ''' If the position is inside of token, return that token; otherwise, return the token to the right.
    ''' </summary>
    <Extension()>
    Public Function FindTokenOnRightOfPosition(
        _syntaxTree As SyntaxTree,
        position As Integer,
        _cancellationToken As CancellationToken,
        Optional includeSkipped As Boolean = True,
        Optional includeDirectives As Boolean = False,
        Optional includeDocumentationComments As Boolean = False) As SyntaxToken

        Return _syntaxTree.GetRoot(_cancellationToken).FindTokenOnRightOfPosition(
            position, includeSkipped, includeDirectives, includeDocumentationComments)
    End Function


    Private Function GetInitialToken(
root As SyntaxNode,
position As Integer,
Optional includeSkipped As Boolean = False,
Optional includeDirectives As Boolean = False,
Optional includeDocumentationComments As Boolean = False) As SyntaxToken
        Return If((position < root.FullSpan.End OrElse Not (TypeOf root Is ICompilationUnitSyntax)), root.FindToken(position, includeSkipped OrElse includeDirectives OrElse includeDocumentationComments) _
        , root.GetLastToken(includeZeroWidth:=True, includeSkipped:=True, includeDirectives:=True, includeDocumentationComments:=True).
GetPreviousToken(includeZeroWidth:=False, includeSkipped:=includeSkipped, includeDirectives:=includeDirectives, includeDocumentationComments:=includeDocumentationComments))
    End Function

    <Extension()>
    Public Function IsParentKind(node As SyntaxNode, kind As SyntaxKind) As Boolean
        Return node IsNot Nothing AndAlso
                   node.Parent.IsKind(kind)
    End Function

    <Extension()>
    Public Function IsChildToken(Of TParent As SyntaxNode)(token As SyntaxToken, childGetter As Func(Of TParent, SyntaxToken)) As Boolean
        Dim ancestor = token.GetAncestor(Of TParent)()

        If ancestor Is Nothing Then
            Return False
        End If

        Dim ancestorToken = childGetter(ancestor)

        Return token = ancestorToken
    End Function

    Private Function FindSkippedTokenForward(triviaList As SyntaxTriviaList, position As Integer) As SyntaxToken
        For Each trivia In triviaList
            If trivia.HasStructure Then
                Dim skippedTokensTrivia As ISkippedTokensTriviaSyntax = TryCast(trivia.GetStructure(), ISkippedTokensTriviaSyntax)
                If skippedTokensTrivia IsNot Nothing Then
                    For Each token In skippedTokensTrivia.Tokens
                        If token.Span.Length > 0 AndAlso position <= token.Span.End Then
                            Return token
                        End If
                    Next
                End If
            End If
        Next

        Return CType(Nothing, SyntaxToken)
    End Function


    Private ReadOnly _s_findSkippedTokenForward As Func(Of SyntaxTriviaList, Integer, SyntaxToken) = AddressOf FindSkippedTokenForward

    <Extension()>
    Public Function FindTokenOnRightOfPosition(
        root As SyntaxNode,
        position As Integer,
        Optional includeSkipped As Boolean = False,
        Optional includeDirectives As Boolean = False,
        Optional includeDocumentationComments As Boolean = False) As SyntaxToken

        Dim findSkippedToken = If(includeSkipped, _s_findSkippedTokenForward, (Function(l, p) Nothing))

        Dim token = GetInitialToken(root, position, includeSkipped, includeDirectives, includeDocumentationComments)

        If position < token.SpanStart Then
            Dim skippedToken = findSkippedToken(token.LeadingTrivia, position)
            token = If(skippedToken.RawKind <> 0, skippedToken, token)
        ElseIf token.Span.End <= position Then
            Do
                Dim skippedToken = findSkippedToken(token.TrailingTrivia, position)
                token = If(skippedToken.RawKind <> 0, skippedToken _
                , token.GetNextToken(includeZeroWidth:=False, includeSkipped:=includeSkipped, includeDirectives:=includeDirectives, includeDocumentationComments:=includeDocumentationComments))
            Loop While token.RawKind <> 0 AndAlso token.Span.End <= position AndAlso token.Span.End <= root.FullSpan.End
        End If

        If token.Span.Length = 0 Then
            token = token.GetNextToken()
        End If

        Return token
    End Function


    ''' <summary>
    ''' If the position is inside of token, return that token; otherwise, return the token to the left.
    ''' </summary>
    <Extension()>
    Public Function FindTokenOnLeftOfPosition(
        _syntaxTree As SyntaxTree,
        position As Integer,
        _cancellationToken As CancellationToken,
        Optional includeSkipped As Boolean = True,
        Optional includeDirectives As Boolean = False,
        Optional includeDocumentationComments As Boolean = False) As SyntaxToken

        Return _syntaxTree.GetRoot(_cancellationToken).FindTokenOnLeftOfPosition(
            position, includeSkipped, includeDirectives, includeDocumentationComments)
    End Function


    <Extension()>
    Public Function GetAncestor(Of TNode As SyntaxNode
)(node As SyntaxNode) As TNode
        Dim current As SyntaxNode = node.Parent
        While current IsNot Nothing

            Dim _tNode As TNode = TryCast(current, TNode)
            If _tNode IsNot Nothing Then
                Return _tNode
            End If

            current = current.GetParent(ascendOutOfTrivia:=True)
        End While

        Return Nothing
    End Function

    <Extension()>
    Public Function GetLanguageService(Of TLanguageService As {Class, ILanguageService})(_document As Document) As TLanguageService
        Return _document?.Project?.GetLanguageService(Of TLanguageService)()
    End Function

    <Extension()>
    Public Function GetLanguageService(Of TLanguageService As {Class, ILanguageService})(_project As Project) As TLanguageService
        Return _project?.GetExtendedLanguageServices().GetService(Of TLanguageService)()
    End Function

    <Extension()>
    Public Function GetExtendedLanguageServices(_project As Project) As HostLanguageServices
        Return _project.Solution.Workspace.Services.GetExtendedLanguageServices(_project.Language)
    End Function

    <Extension()>
    Public Function GetExtendedLanguageServices(_hostWorkspaceServices As HostWorkspaceServices, languageName As String) As HostLanguageServices
        Dim languageServices = _hostWorkspaceServices.GetLanguageServices(languageName)

#If CODE_STYLE Then
            languageServices = CodeStyleHostLanguageServices.GetRequiredMappedCodeStyleLanguageServices(languageServices);
#End If
        Return languageServices
    End Function

    <Extension()>
    Public Function GetSemanticModelForNodeAsync(_document As Document, node As SyntaxNode, _cancellationToken As CancellationToken) As Task(Of SemanticModel)
        Dim syntaxFactService = _document.GetLanguageService(Of ISyntaxFactsService)()
        Dim semanticModelService = _document.Project.Solution.Workspace.Services.GetService(Of ISemanticModelService)()
        If semanticModelService Is Nothing OrElse syntaxFactService Is Nothing OrElse node Is Nothing Then
            Return _document.GetSemanticModelAsync(_cancellationToken)
        End If

        Return GetSemanticModelForNodeAsync(semanticModelService, syntaxFactService, _document, node, node.FullSpan, _cancellationToken)
    End Function


    Private Function GetSemanticModelForNodeAsync(
            semanticModelService As ISemanticModelService, syntaxFactService As ISyntaxFactsService,
            _document As Document, node As SyntaxNode, span As TextSpan, _cancellationToken As CancellationToken) As Task(Of SemanticModel)

        ' check whether given span is a valid span to do speculative binding
        Dim speculativeBindingSpan = syntaxFactService.GetMemberBodySpanForSpeculativeBinding(node)
        If Not speculativeBindingSpan.Contains(span) Then
            Return _document.GetSemanticModelAsync(_cancellationToken)
        End If

        Return semanticModelService.GetSemanticModelForNodeAsync(_document, node, _cancellationToken)
    End Function


    <Extension()>
    Public Function GetParent(node As SyntaxNode, ascendOutOfTrivia As Boolean) As SyntaxNode
        Dim _parent = node.Parent
        If _parent Is Nothing AndAlso ascendOutOfTrivia Then
            Dim structuredTrivia As IStructuredTriviaSyntax = TryCast(node, IStructuredTriviaSyntax)
            If structuredTrivia IsNot Nothing Then
                _parent = structuredTrivia.ParentTrivia.Token.Parent
            End If
        End If

        Return _parent
    End Function

    Private ReadOnly _s_notNullTest As Func(Of Object, Boolean) = Function(x) x IsNot Nothing
    Private ReadOnly _s_findSkippedTokenBackward As Func(Of SyntaxTriviaList, Integer, SyntaxToken) = AddressOf FindSkippedTokenBackward

    Private Function FindSkippedTokenBackward(triviaList As SyntaxTriviaList, position As Integer) As SyntaxToken
        For Each trivia In triviaList.Reverse()
            If trivia.HasStructure Then
                Dim skippedTokensTrivia As ISkippedTokensTriviaSyntax = TryCast(trivia.GetStructure(), ISkippedTokensTriviaSyntax)
                If skippedTokensTrivia IsNot Nothing Then
                    For Each token In skippedTokensTrivia.Tokens
                        If token.Span.Length > 0 AndAlso token.SpanStart <= position Then
                            Return token
                        End If
                    Next
                End If
            End If
        Next

        Return CType(Nothing, SyntaxToken)
    End Function


    <Extension()>
    Public Function FindTokenOnLeftOfPosition(
            root As SyntaxNode,
            position As Integer,
            Optional includeSkipped As Boolean = False,
            Optional includeDirectives As Boolean = False,
            Optional includeDocumentationComments As Boolean = False) As SyntaxToken

        Dim findSkippedToken = If(includeSkipped, _s_findSkippedTokenBackward, (Function(l, p) Nothing))

        Dim token = GetInitialToken(root, position, includeSkipped, includeDirectives, includeDocumentationComments)

        If position <= token.SpanStart Then
            Do
                Dim skippedToken = findSkippedToken(token.LeadingTrivia, position)
                token = If(skippedToken.RawKind <> 0, skippedToken _
                , token.GetPreviousToken(includeZeroWidth:=False, includeSkipped:=includeSkipped, includeDirectives:=includeDirectives, includeDocumentationComments:=includeDocumentationComments))
            Loop While position <= token.SpanStart AndAlso root.FullSpan.Start < token.SpanStart
        ElseIf token.Span.End < position Then
            Dim skippedToken = findSkippedToken(token.TrailingTrivia, position)
            token = If(skippedToken.RawKind <> 0, skippedToken, token)
        End If

        If token.Span.Length = 0 Then
            token = token.GetPreviousToken()
        End If

        Return token
    End Function

    <Extension()>
    Public Function WhereNotNull(Of T As Class)(source As IEnumerable(Of T)) As IEnumerable(Of T)
        If source Is Nothing Then
            Return New List(Of T)()
        End If

        Return source.Where(CType(_s_notNullTest, Func(Of T, Boolean)))
    End Function

    <Extension()>
    Public Function ToSet(Of T)(source As IEnumerable(Of T)) As ISet(Of T)
        If source Is Nothing Then
            Throw New ArgumentNullException(NameOf(source))
        End If

        Return If(TryCast(source, ISet(Of T)), New HashSet(Of T)(source))
    End Function

    <Extension()>
    Public Function IsKind(token As SyntaxToken, ParamArray kinds As SyntaxKind()) As Boolean
        Return kinds.Contains(token.Kind)
    End Function

    <Extension()>
    Public Function IntersectsWith(token As SyntaxToken, position As Integer) As Boolean
        Return token.Span.IntersectsWith(position)
    End Function

End Module

Friend Interface ISemanticModelService
    Inherits IWorkspaceService
    Function GetSemanticModelForNodeAsync(_document As Document, node As SyntaxNode, _cancellationToken As CancellationToken) As Task(Of SemanticModel)
End Interface