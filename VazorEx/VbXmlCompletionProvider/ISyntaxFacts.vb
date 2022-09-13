' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.Collections.Immutable
Imports System.Threading
Imports Microsoft.CodeAnalysis.Text
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic
Imports System.Diagnostics.CodeAnalysis

#If CODE_STYLE Then
using Microsoft.CodeAnalysis.Internal.Editing;
#Else
Imports Microsoft.CodeAnalysis.Editing
#End If

Partial Friend Interface ISyntaxFacts
    ReadOnly Property IsCaseSensitive As Boolean
    ReadOnly Property StringComparer As StringComparer

    ReadOnly Property ElasticMarker As SyntaxTrivia
    ReadOnly Property ElasticCarriageReturnLineFeed As SyntaxTrivia

    ReadOnly Property SyntaxKinds As SyntaxKind

    Function SupportsIndexingInitializer(options As ParseOptions) As Boolean
    Function SupportsThrowExpression(options As ParseOptions) As Boolean

    Function SupportsLocalFunctionDeclaration(options As ParseOptions) As Boolean

    Function ParseToken(text As String) As SyntaxToken
    Function ParseLeadingTrivia(text As String) As SyntaxTriviaList

    Function IsVerbatimIdentifier(token As SyntaxToken) As Boolean
    Function IsOperator(token As SyntaxToken) As Boolean
    Function IsPredefinedType(token As SyntaxToken) As Boolean
    Function IsPredefinedType(token As SyntaxToken, type As PredefinedType) As Boolean
    Function IsPredefinedOperator(token As SyntaxToken) As Boolean
    Function IsPredefinedOperator(token As SyntaxToken, op As PredefinedOperator) As Boolean

    ''' <summary>
    ''' Returns 'true' if this a 'reserved' keyword for the language.  A 'reserved' keyword is a
    ''' identifier that is always treated as being a special keyword, regardless of where it is
    ''' found in the token stream.  Examples of this are tokens like <see langword="class"/> and
    ''' <see langword="Class"/> in C# and VB respectively.
    ''' 
    ''' Importantly, this does *not* include contextual keywords.  If contextual keywords are
    ''' important for your scenario, use <see cref= "IsContextualKeyword"/> or <see
    ''' cref=
    ''' "ISyntaxFactsExtensions.IsReservedOrContextualKeyword"/>.  Also, consider using
    ''' <see cref= "ISyntaxFactsExtensions.IsWord"/> if all you need is the ability to know 
    ''' if this is effectively any identifier in the language, regardless of whether the language
    ''' is treating it as a keyword or not.
    ''' </summary>
    Function IsReservedKeyword(token As SyntaxToken) As Boolean

    ''' <summary>
    ''' Returns <see langword="true"/> if this a 'contextual' keyword for the language.  A
    ''' 'contextual' keyword is a identifier that is only treated as being a special keyword in
    ''' certain *syntactic* contexts.  Examples of this is 'yield' in C#.  This is only a
    ''' keyword if used as 'yield return' or 'yield break'.  Importantly, identifiers like <see
    ''' langword="var"/>, <see langword="dynamic"/> and <see langword="nameof"/> are *not*
    ''' 'contextual' keywords.  This is because they are not treated as keywords depending on
    ''' the syntactic context around them.  Instead, the language always treats them identifiers
    ''' that have special *semantic* meaning if they end up not binding to an existing symbol.
    ''' 
    ''' Importantly, if <paramref name="token"/> is not in the syntactic construct where the
    ''' language thinks an identifier should be contextually treated as a keyword, then this
    ''' will return <see langword="false"/>.
    ''' 
    ''' Or, in other words, the parser must be able to identify these cases in order to be a
    ''' contextual keyword.  If identification happens afterwards, it's not contextual.
    ''' </summary>
    Function IsContextualKeyword(token As SyntaxToken) As Boolean

    ''' <summary>
    ''' The set of identifiers that have special meaning directly after the `#` token in a
    ''' preprocessor directive.  For example `if` or `pragma`.
    ''' </summary>
    Function IsPreprocessorKeyword(token As SyntaxToken) As Boolean

    Function IsLiteral(token As SyntaxToken) As Boolean
    Function IsStringLiteralOrInterpolatedStringLiteral(token As SyntaxToken) As Boolean

    Function IsNumericLiteral(token As SyntaxToken) As Boolean
    Function IsVerbatimStringLiteral(token As SyntaxToken) As Boolean

    Function IsTypeNamedVarInVariableOrFieldDeclaration(token As SyntaxToken, parent As SyntaxNode) As Boolean
    Function IsTypeNamedDynamic(token As SyntaxToken, parent As SyntaxNode) As Boolean
    Function IsUsingOrExternOrImport(node As SyntaxNode) As Boolean
    Function IsUsingAliasDirective(node As SyntaxNode) As Boolean
    Function IsGlobalAttribute(node As SyntaxNode) As Boolean
    Function IsDeclaration(node As SyntaxNode) As Boolean
    Function IsTypeDeclaration(node As SyntaxNode) As Boolean

    Function IsRegularComment(trivia As SyntaxTrivia) As Boolean
    Function IsDocumentationComment(trivia As SyntaxTrivia) As Boolean
    Function IsElastic(trivia As SyntaxTrivia) As Boolean

    Function IsDocumentationComment(node As SyntaxNode) As Boolean
    Function IsNumericLiteralExpression(node As SyntaxNode) As Boolean
    Function IsLiteralExpression(node As SyntaxNode) As Boolean

    Function GetText(kind As Integer) As String
    Function IsEntirelyWithinStringOrCharOrNumericLiteral(_syntaxTree As SyntaxTree, position As Integer, _cancellationToken As CancellationToken) As Boolean

    Function TryGetPredefinedType(token As SyntaxToken, <Runtime.InteropServices.Out> ByRef type As PredefinedType) As Boolean
    Function TryGetPredefinedOperator(token As SyntaxToken, <Runtime.InteropServices.Out> ByRef op As PredefinedOperator) As Boolean
    Function TryGetExternalSourceInfo(directive As SyntaxNode, <Runtime.InteropServices.Out> ByRef info As ExternalSourceInfo) As Boolean

    Function IsObjectCreationExpressionType(node As SyntaxNode) As Boolean
    Function GetObjectCreationInitializer(node As SyntaxNode) As SyntaxNode
    Function GetObjectCreationType(node As SyntaxNode) As SyntaxNode

    Function IsBinaryExpression(node As SyntaxNode) As Boolean
    Sub GetPartsOfBinaryExpression(node As SyntaxNode, <Runtime.InteropServices.Out> ByRef left As SyntaxNode, <Runtime.InteropServices.Out> ByRef operatorToken As SyntaxToken, <Runtime.InteropServices.Out> ByRef right As SyntaxNode)

    Sub GetPartsOfConditionalExpression(node As SyntaxNode, <Runtime.InteropServices.Out> ByRef condition As SyntaxNode, <Runtime.InteropServices.Out> ByRef whenTrue As SyntaxNode, <Runtime.InteropServices.Out> ByRef whenFalse As SyntaxNode)

    Function IsCastExpression(node As SyntaxNode) As Boolean
    Sub GetPartsOfCastExpression(node As SyntaxNode, <Runtime.InteropServices.Out> ByRef type As SyntaxNode, <Runtime.InteropServices.Out> ByRef expression As SyntaxNode)

    Function IsExpressionOfInvocationExpression(node As SyntaxNode) As Boolean
    Sub GetPartsOfInvocationExpression(node As SyntaxNode, <Runtime.InteropServices.Out> ByRef expression As SyntaxNode, <Runtime.InteropServices.Out> ByRef argumentList As SyntaxNode)

    Function GetExpressionOfExpressionStatement(node As SyntaxNode) As SyntaxNode

    Function IsExpressionOfAwaitExpression(node As SyntaxNode) As Boolean
    Function GetExpressionOfAwaitExpression(node As SyntaxNode) As SyntaxNode
    Function IsExpressionOfForeach(node As SyntaxNode) As Boolean

    Sub GetPartsOfTupleExpression(Of TArgumentSyntax As SyntaxNode)(node As SyntaxNode,
<Runtime.InteropServices.Out> ByRef openParen As SyntaxToken, <Runtime.InteropServices.Out> ByRef arguments As SeparatedSyntaxList(Of TArgumentSyntax), <Runtime.InteropServices.Out> ByRef closeParen As SyntaxToken)

    Function GetOperandOfPrefixUnaryExpression(node As SyntaxNode) As SyntaxNode
    Function GetOperatorTokenOfPrefixUnaryExpression(node As SyntaxNode) As SyntaxToken

    ' Left side of = assignment.
    Function IsLeftSideOfAssignment(node As SyntaxNode) As Boolean

    Function IsSimpleAssignmentStatement(statement As SyntaxNode) As Boolean
    Sub GetPartsOfAssignmentStatement(statement As SyntaxNode, <Runtime.InteropServices.Out> ByRef left As SyntaxNode, <Runtime.InteropServices.Out> ByRef operatorToken As SyntaxToken, <Runtime.InteropServices.Out> ByRef right As SyntaxNode)
    Sub GetPartsOfAssignmentExpressionOrStatement(statement As SyntaxNode, <Runtime.InteropServices.Out> ByRef left As SyntaxNode, <Runtime.InteropServices.Out> ByRef operatorToken As SyntaxToken, <Runtime.InteropServices.Out> ByRef right As SyntaxNode)

    ' Left side of any assignment (for example = or ??= or *=  or += )
    Function IsLeftSideOfAnyAssignment(node As SyntaxNode) As Boolean
    ' Left side of compound assignment (for example ??= or *=  or += )
    Function IsLeftSideOfCompoundAssignment(node As SyntaxNode) As Boolean
    Function GetRightHandSideOfAssignment(node As SyntaxNode) As SyntaxNode

    Function IsInferredAnonymousObjectMemberDeclarator(node As SyntaxNode) As Boolean
    Function IsOperandOfIncrementExpression(node As SyntaxNode) As Boolean
    Function IsOperandOfIncrementOrDecrementExpression(node As SyntaxNode) As Boolean

    Function IsLeftSideOfDot(node As SyntaxNode) As Boolean
    Function GetRightSideOfDot(node As SyntaxNode) As SyntaxNode

    ''' <summary>
    ''' Get the node on the left side of the dot if given a dotted expression. 
    ''' </summary>
    ''' <param name="allowImplicitTarget">
    ''' In VB, we have a member access expression with a null expression, this may be one of the
    ''' following forms:
    '''     1) new With { .a = 1, .b = .a      .a refers to the anonymous type
    '''     2) With obj : .m                   .m refers to the obj type
    '''     3) new T() With { .a = 1, .b = .a  'a refers to the T type
    ''' If `allowImplicitTarget` is set to true, the returned node will be set to approperiate node, otherwise, it will return null.
    ''' This parameter has no affect on C# node.
    ''' </param>
    Function GetLeftSideOfDot(node As SyntaxNode, Optional allowImplicitTarget As Boolean = False) As SyntaxNode

    Function IsRightSideOfQualifiedName(node As SyntaxNode) As Boolean
    Function IsLeftSideOfExplicitInterfaceSpecifier(node As SyntaxNode) As Boolean

    ' TODO: Skipped Null-able Directive enable 
    Function IsNameOfMemberAccessExpression(node As SyntaxNode) As Boolean
    ' TODO: Skipped Null-able Directive restore 
    Function IsExpressionOfMemberAccessExpression(node As SyntaxNode) As Boolean

    Function GetNameOfMemberAccessExpression(node As SyntaxNode) As SyntaxNode

    ''' <summary>
    ''' Returns the expression node the member is being accessed off of.  If <paramref name="allowImplicitTarget"/>
    ''' is <see langword="false"/>, this will be the node directly to the left of the dot-token.  If <paramref name="allowImplicitTarget"/>
    ''' is <see langword="true"/>, then this can return another node in the tree that the member will be accessed
    ''' off of.  For example, in VB, if you have a member-access-expression of the form ".Length" then this
    ''' may return the expression in the surrounding With-statement.
    ''' </summary>
    Function GetExpressionOfMemberAccessExpression(node As SyntaxNode, Optional allowImplicitTarget As Boolean = False) As SyntaxNode
    Sub GetPartsOfMemberAccessExpression(node As SyntaxNode, <Runtime.InteropServices.Out> ByRef expression As SyntaxNode, <Runtime.InteropServices.Out> ByRef operatorToken As SyntaxToken, <Runtime.InteropServices.Out> ByRef name As SyntaxNode)

    Function GetTargetOfMemberBinding(node As SyntaxNode) As SyntaxNode

    Function IsPointerMemberAccessExpression(node As SyntaxNode) As Boolean

    Function IsNamedParameter(node As SyntaxNode) As Boolean
    Function GetNameOfParameter(node As SyntaxNode) As SyntaxToken?
    Function GetDefaultOfParameter(node As SyntaxNode) As SyntaxNode
    Function GetParameterList(node As SyntaxNode) As SyntaxNode

    Function IsDocumentationCommentExteriorTrivia(trivia As SyntaxTrivia) As Boolean

    Sub GetPartsOfElementAccessExpression(node As SyntaxNode, <Runtime.InteropServices.Out> ByRef expression As SyntaxNode, <Runtime.InteropServices.Out> ByRef argumentList As SyntaxNode)

    Function GetExpressionOfArgument(node As SyntaxNode) As SyntaxNode
    Function GetExpressionOfInterpolation(node As SyntaxNode) As SyntaxNode
    Function GetNameOfAttribute(node As SyntaxNode) As SyntaxNode

    Sub GetPartsOfConditionalAccessExpression(node As SyntaxNode, <Runtime.InteropServices.Out> ByRef expression As SyntaxNode, <Runtime.InteropServices.Out> ByRef operatorToken As SyntaxToken, <Runtime.InteropServices.Out> ByRef whenNotNull As SyntaxNode)

    ' TODO: Skipped Null-able Directive enable 
    Function IsMemberBindingExpression(node As SyntaxNode) As Boolean
    ' TODO: Skipped Null-able Directive restore 
    Function IsPostfixUnaryExpression(node As SyntaxNode) As Boolean

    Function GetExpressionOfParenthesizedExpression(node As SyntaxNode) As SyntaxNode

    Function GetIdentifierOfGenericName(node As SyntaxNode) As SyntaxToken
    Function GetIdentifierOfSimpleName(node As SyntaxNode) As SyntaxToken
    Function GetIdentifierOfVariableDeclarator(node As SyntaxNode) As SyntaxToken
    Function GetTypeOfVariableDeclarator(node As SyntaxNode) As SyntaxNode

    ''' <summary>
    ''' True if this is an argument with just an expression and nothing else (i.e. no ref/out,
    ''' no named params, no omitted args).
    ''' </summary>
    Function IsSimpleArgument(node As SyntaxNode) As Boolean
    Function IsArgument(node As SyntaxNode) As Boolean
    Function GetRefKindOfArgument(node As SyntaxNode) As RefKind

    Sub GetNameAndArityOfSimpleName(node As SyntaxNode, <Runtime.InteropServices.Out> ByRef name As String, <Runtime.InteropServices.Out> ByRef arity As Integer)
    Function LooksGeneric(simpleName As SyntaxNode) As Boolean

    Function GetContentsOfInterpolatedString(interpolatedString As SyntaxNode) As SyntaxList(Of SyntaxNode)

    Function GetArgumentsOfInvocationExpression(node As SyntaxNode) As SeparatedSyntaxList(Of SyntaxNode)
    Function GetArgumentsOfObjectCreationExpression(node As SyntaxNode) As SeparatedSyntaxList(Of SyntaxNode)
    Function GetArgumentsOfArgumentList(node As SyntaxNode) As SeparatedSyntaxList(Of SyntaxNode)

    Function IsUsingDirectiveName(node As SyntaxNode) As Boolean

    Function IsAttributeName(node As SyntaxNode) As Boolean
    Function GetAttributeLists(node As SyntaxNode) As SyntaxList(Of SyntaxNode)

    Function IsAttributeNamedArgumentIdentifier(node As SyntaxNode) As Boolean
    Function IsObjectInitializerNamedAssignmentIdentifier(node As SyntaxNode) As Boolean
    Function IsObjectInitializerNamedAssignmentIdentifier(node As SyntaxNode, <Runtime.InteropServices.Out> ByRef initializedInstance As SyntaxNode) As Boolean

    Function IsDirective(node As SyntaxNode) As Boolean
    Function IsStatement(node As SyntaxNode) As Boolean
    Function IsExecutableStatement(node As SyntaxNode) As Boolean

    Function IsDeconstructionAssignment(node As SyntaxNode) As Boolean
    Function IsDeconstructionForEachStatement(node As SyntaxNode) As Boolean

    ''' <summary>
    ''' Returns true for nodes that represent the body of a method.
    ''' 
    ''' For VB this will be 
    ''' MethodBlockBaseSyntax.  This will be true for things like constructor, method, operator
    ''' bodies as well as accessor bodies.  It will not be true for things like sub() function()
    ''' lambdas.  
    ''' 
    ''' For C# this will be the BlockSyntax or ArrowExpressionSyntax for a 
    ''' method/constructor/deconstructor/operator/accessor.  It will not be included for local
    ''' functions.
    ''' </summary>
    Function IsMethodBody(node As SyntaxNode) As Boolean

    Function GetExpressionOfReturnStatement(node As SyntaxNode) As SyntaxNode

    Function IsLocalFunctionStatement(node As SyntaxNode) As Boolean

    Function IsDeclaratorOfLocalDeclarationStatement(declarator As SyntaxNode, localDeclarationStatement As SyntaxNode) As Boolean
    Function GetVariablesOfLocalDeclarationStatement(node As SyntaxNode) As SeparatedSyntaxList(Of SyntaxNode)
    Function GetInitializerOfVariableDeclarator(node As SyntaxNode) As SyntaxNode
    Function GetValueOfEqualsValueClause(node As SyntaxNode) As SyntaxNode

    Function IsThisConstructorInitializer(token As SyntaxToken) As Boolean
    Function IsBaseConstructorInitializer(token As SyntaxToken) As Boolean
    Function IsQueryKeyword(token As SyntaxToken) As Boolean
    Function IsThrowExpression(node As SyntaxNode) As Boolean
    Function IsElementAccessExpression(node As SyntaxNode) As Boolean
    Function IsIndexerMemberCRef(node As SyntaxNode) As Boolean
    Function IsIdentifierStartCharacter(c As Char) As Boolean
    Function IsIdentifierPartCharacter(c As Char) As Boolean
    Function IsIdentifierEscapeCharacter(c As Char) As Boolean
    Function IsStartOfUnicodeEscapeSequence(c As Char) As Boolean

    Function IsValidIdentifier(identifier As String) As Boolean
    Function IsVerbatimIdentifier(identifier As String) As Boolean

    ''' <summary>
    ''' Returns true if the given character is a character which may be included in an
    ''' identifier to specify the type of a variable.
    ''' </summary>
    Function IsTypeCharacter(c As Char) As Boolean

    Function IsBindableToken(token As SyntaxToken) As Boolean

    Function IsInStaticContext(node As SyntaxNode) As Boolean
    Function IsUnsafeContext(node As SyntaxNode) As Boolean

    Function IsInNamespaceOrTypeContext(node As SyntaxNode) As Boolean

    Function IsBaseTypeList(node As SyntaxNode) As Boolean

    Function IsAnonymousFunction(n As SyntaxNode) As Boolean

    Function IsInConstantContext(node As SyntaxNode) As Boolean
    Function IsInConstructor(node As SyntaxNode) As Boolean
    Function IsMethodLevelMember(node As SyntaxNode) As Boolean
    Function IsTopLevelNodeWithMembers(node As SyntaxNode) As Boolean
    Function HasIncompleteParentMember(node As SyntaxNode) As Boolean

    ''' <summary>
    ''' A block that has no semantics other than introducing a new scope. That is only C# BlockSyntax.
    ''' </summary>
    Function IsScopeBlock(node As SyntaxNode) As Boolean

    ''' <summary>
    ''' A node that contains a list of statements. In C#, this is BlockSyntax and SwitchSectionSyntax.
    ''' In VB, this includes all block statements such as a MultiLineIfBlockSyntax.
    ''' </summary>
    Function IsExecutableBlock(node As SyntaxNode) As Boolean
    Function GetExecutableBlockStatements(node As SyntaxNode) As SyntaxList(Of SyntaxNode)
    Function FindInnermostCommonExecutableBlock(nodes As IEnumerable(Of SyntaxNode)) As SyntaxNode

    ''' <summary>
    ''' A node that can host a list of statements or a single statement. In addition to
    ''' every "executable block", this also includes C# embedded statement owners.
    ''' </summary>
    Function IsStatementContainer(node As SyntaxNode) As Boolean
    Function GetStatementContainerStatements(node As SyntaxNode) As IReadOnlyList(Of SyntaxNode)

    Function AreEquivalent(token1 As SyntaxToken, token2 As SyntaxToken) As Boolean
    Function AreEquivalent(node1 As SyntaxNode, node2 As SyntaxNode) As Boolean

    Function GetDisplayName(node As SyntaxNode, options As DisplayNameOptions, Optional rootNamespace As String = Nothing) As String

    Function GetContainingTypeDeclaration(root As SyntaxNode, position As Integer) As SyntaxNode
    Function GetContainingMemberDeclaration(root As SyntaxNode, position As Integer, Optional useFullSpan As Boolean = True) As SyntaxNode
    Function GetContainingVariableDeclaratorOfFieldDeclaration(node As SyntaxNode) As SyntaxNode

    Function FindTokenOnLeftOfPosition(node As SyntaxNode, position As Integer, Optional includeSkipped As Boolean = True, Optional includeDirectives As Boolean = False, Optional includeDocumentationComments As Boolean = False) As SyntaxToken
    Function FindTokenOnRightOfPosition(node As SyntaxNode, position As Integer, Optional includeSkipped As Boolean = True, Optional includeDirectives As Boolean = False, Optional includeDocumentationComments As Boolean = False) As SyntaxToken

    Sub GetPartsOfParenthesizedExpression(node As SyntaxNode, <Runtime.InteropServices.Out> ByRef openParen As SyntaxToken, <Runtime.InteropServices.Out> ByRef expression As SyntaxNode, <Runtime.InteropServices.Out> ByRef closeParen As SyntaxToken)
    Function WalkDownParentheses(node As SyntaxNode) As SyntaxNode

    Function ConvertToSingleLine(node As SyntaxNode, Optional useElasticTrivia As Boolean = False) As SyntaxNode

    Function IsClassDeclaration(node As SyntaxNode) As Boolean
    Function IsNamespaceDeclaration(node As SyntaxNode) As Boolean
    Function GetMethodLevelMembers(root As SyntaxNode) As List(Of SyntaxNode)
    Function GetMembersOfTypeDeclaration(typeDeclaration As SyntaxNode) As SyntaxList(Of SyntaxNode)
    Function GetMembersOfNamespaceDeclaration(namespaceDeclaration As SyntaxNode) As SyntaxList(Of SyntaxNode)
    Function GetMembersOfCompilationUnit(compilationUnit As SyntaxNode) As SyntaxList(Of SyntaxNode)

    Function ContainsInMemberBody(node As SyntaxNode, span As TextSpan) As Boolean
    Function GetMethodLevelMemberId(root As SyntaxNode, node As SyntaxNode) As Integer
    Function GetMethodLevelMember(root As SyntaxNode, memberId As Integer) As SyntaxNode
    Function GetInactiveRegionSpanAroundPosition(tree As SyntaxTree, position As Integer, _cancellationToken As CancellationToken) As TextSpan

    ''' <summary>
    ''' Given a <see cref= "SyntaxNode"/>, return the <see cref= "TextSpan"/> representing the span of the member body
    ''' it is contained within. This <see cref= "TextSpan"/> is used to determine whether speculative binding should be
    ''' used in performance-critical typing scenarios. Note: if this method fails to find a relevant span, it returns
    ''' an empty <see cref= "TextSpan"/> at position 0.
    ''' </summary>
    Function GetMemberBodySpanForSpeculativeBinding(node As SyntaxNode) As TextSpan

    ''' <summary>
    ''' Returns the parent node that binds to the symbols that the IDE prefers for features like
    ''' Quick Info and Find All References. For example, if the token is part of the type of
    ''' an object creation, the parenting object creation expression is returned so that binding
    ''' will return constructor symbols.
    ''' </summary>
    Function GetBindableParent(token As SyntaxToken) As SyntaxNode

    Function GetConstructors(root As SyntaxNode, _cancellationToken As CancellationToken) As IEnumerable(Of SyntaxNode)
    Function TryGetCorrespondingOpenBrace(token As SyntaxToken, <Runtime.InteropServices.Out> ByRef openBrace As SyntaxToken) As Boolean

    ''' <summary>
    ''' Given a <see cref= "SyntaxNode"/>, that represents and argument return the string representation of
    ''' that arguments name.
    ''' </summary>
    Function GetNameForArgument(argument As SyntaxNode) As String

    Function IsNameOfSubpattern(node As SyntaxNode) As Boolean
    Function IsPropertyPatternClause(node As SyntaxNode) As Boolean

    Function IsOnTypeHeader(root As SyntaxNode, position As Integer, <Runtime.InteropServices.Out> ByRef typeDeclaration As SyntaxNode) As Boolean

    Function IsOnPropertyDeclarationHeader(root As SyntaxNode, position As Integer, <Runtime.InteropServices.Out> ByRef propertyDeclaration As SyntaxNode) As Boolean
    Function IsOnParameterHeader(root As SyntaxNode, position As Integer, <Runtime.InteropServices.Out> ByRef parameter As SyntaxNode) As Boolean
    Function IsOnMethodHeader(root As SyntaxNode, position As Integer, <Runtime.InteropServices.Out> ByRef method As SyntaxNode) As Boolean
    Function IsOnLocalFunctionHeader(root As SyntaxNode, position As Integer, <Runtime.InteropServices.Out> ByRef localFunction As SyntaxNode) As Boolean
    Function IsOnLocalDeclarationHeader(root As SyntaxNode, position As Integer, <Runtime.InteropServices.Out> ByRef localDeclaration As SyntaxNode) As Boolean
    Function IsOnIfStatementHeader(root As SyntaxNode, position As Integer, <Runtime.InteropServices.Out> ByRef ifStatement As SyntaxNode) As Boolean
    Function IsOnForeachHeader(root As SyntaxNode, position As Integer, <Runtime.InteropServices.Out> ByRef foreachStatement As SyntaxNode) As Boolean
    Function IsBetweenTypeMembers(_sourceText As SourceText, root As SyntaxNode, position As Integer, <Runtime.InteropServices.Out> ByRef typeDeclaration As SyntaxNode) As Boolean

    Function GetNextExecutableStatement(statement As SyntaxNode) As SyntaxNode

    Function GetLeadingBlankLines(node As SyntaxNode) As ImmutableArray(Of SyntaxTrivia)
    Function GetNodeWithoutLeadingBlankLines(Of TSyntaxNode As SyntaxNode)(node As TSyntaxNode) As TSyntaxNode

    Function GetFileBanner(root As SyntaxNode) As ImmutableArray(Of SyntaxTrivia)
    Function GetFileBanner(firstToken As SyntaxToken) As ImmutableArray(Of SyntaxTrivia)

    Function ContainsInterleavedDirective(node As SyntaxNode, _cancellationToken As CancellationToken) As Boolean
    Function ContainsInterleavedDirective(nodes As ImmutableArray(Of SyntaxNode), _cancellationToken As CancellationToken) As Boolean

    Function GetBannerText(documentationCommentTriviaSyntax As SyntaxNode, maxBannerLength As Integer, _cancellationToken As CancellationToken) As String

    Function GetModifiers(node As SyntaxNode) As SyntaxTokenList
    Function WithModifiers(node As SyntaxNode, modifiers As SyntaxTokenList) As SyntaxNode

    Function GetDeconstructionReferenceLocation(node As SyntaxNode) As Location

    Function GetDeclarationIdentifierIfOverride(token As SyntaxToken) As SyntaxToken?

    Function SpansPreprocessorDirective(nodes As IEnumerable(Of SyntaxNode)) As Boolean

    Function CanHaveAccessibility(declaration As SyntaxNode) As Boolean

    ''' <summary>
    ''' Gets the accessibility of the declaration.
    ''' </summary>
    Function GetAccessibility(declaration As SyntaxNode) As Accessibility

    Sub GetAccessibilityAndModifiers(modifierList As SyntaxTokenList, <Runtime.InteropServices.Out> ByRef _accessibility As Accessibility, <Runtime.InteropServices.Out> ByRef modifiers As DeclarationModifiers, <Runtime.InteropServices.Out> ByRef isDefault As Boolean)

    Function GetModifierTokens(declaration As SyntaxNode) As SyntaxTokenList

    ''' <summary>
    ''' Gets the <see cref= "DeclarationKind"/> for the declaration.
    ''' </summary>
    Function GetDeclarationKind(declaration As SyntaxNode) As DeclarationKind
End Interface

<Flags>
Friend Enum DisplayNameOptions
    None = 0
    IncludeMemberKeyword = 1
    IncludeNamespaces = 1 << 1
    IncludeParameters = 1 << 2
    IncludeType = 1 << 3
    IncludeTypeParameters = 1 << 4
End Enum

Friend Enum PredefinedType
    None = 0
    [Boolean] = 1
    [Byte] = 1 << 1
    [Char] = 1 << 2
    DateTime = 1 << 3
    [Decimal] = 1 << 4
    [Double] = 1 << 5
    Int16 = 1 << 6
    Int32 = 1 << 7
    Int64 = 1 << 8
    [Object] = 1 << 9
    [SByte] = 1 << 10
    [Single] = 1 << 11
    [String] = 1 << 12
    UInt16 = 1 << 13
    UInt32 = 1 << 14
    UInt64 = 1 << 15
    Void = 1 << 16
End Enum

Friend Enum PredefinedOperator
    None = 0
    Addition = 1
    BitwiseAnd = 1 << 1
    BitwiseOr = 1 << 2
    Complement = 1 << 3    ' ~ or ! in C#, 'Not' in VB.
    Concatenate = 1 << 4
    Decrement = 1 << 5
    Division = 1 << 6
    Equality = 1 << 7
    ExclusiveOr = 1 << 8
    Exponent = 1 << 9
    GreaterThan = 1 << 10
    GreaterThanOrEqual = 1 << 11
    Increment = 1 << 12
    Inequality = 1 << 13
    IntegerDivision = 1 << 14
    LeftShift = 1 << 15
    LessThan = 1 << 16
    LessThanOrEqual = 1 << 17
    [Like] = 1 << 18
    Modulus = 1 << 19
    Multiplication = 1 << 20
    RightShift = 1 << 21
    Subtraction = 1 << 22
End Enum

Friend Structure ExternalSourceInfo
    Public ReadOnly StartLine As Integer?
    Public ReadOnly Ends As Boolean

    Public Sub New(_startLine As Integer?, _ends As Boolean)
        Me.StartLine = _startLine
        Me.Ends = _ends
    End Sub
End Structure