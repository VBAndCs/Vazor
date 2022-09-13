Imports System.Globalization
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Options
Imports Microsoft.CodeAnalysis.Text
Imports Microsoft.CodeAnalysis.VisualBasic

Namespace Microsoft.CodeAnalysis.VisualBasic.Completion.Providers

    Partial Friend Class VbXmlCompletionProvider

        Friend Overrides Function IsInsertionTrigger(text As SourceText, characterPosition As Integer, options As OptionSet) As Boolean
            Dim isStartOfTag = text(characterPosition) = "<"c
            Dim isClosingTag = (text(characterPosition) = "/"c AndAlso characterPosition > 0 AndAlso text(characterPosition - 1) = "<"c)
            Dim isDoubleQuote = text(characterPosition) = """"c
            Dim isEquality = text(characterPosition) = "="c

            Return isStartOfTag OrElse
                      isClosingTag OrElse
                      isDoubleQuote OrElse
                      isEquality OrElse
                      IsTriggerAfterSpaceOrStartOfWordCharacter(
                             text, characterPosition, options)
        End Function

        Public Function IsTriggerAfterSpaceOrStartOfWordCharacter(text As SourceText, characterPosition As Integer, options As OptionSet) As Boolean
            ' Bring up on space or at the start of a word.
            Dim ch = text(characterPosition)

            Return ch = " "c OrElse IsStartingNewWord(text, characterPosition, options)
        End Function

        Private Function IsStartingNewWord(text As SourceText, characterPosition As Integer, options As OptionSet) As Boolean
            Return IsStartingNewWord(
                text, characterPosition, AddressOf IsWordStartCharacter, AddressOf IsWordCharacter)
        End Function

        Private Function IsWordCharacter(ch As Char) As Boolean
            Return IsIdentifierStartCharacter(ch) OrElse IsIdentifierPartCharacter(ch)
        End Function

        Public Shared Function IsIdentifierPartCharacter(c As Char) As Boolean
            If c < ChrW(128) Then
                Return IsNarrowIdentifierCharacter(Convert.ToUInt16(c))
            End If

            Return IsWideIdentifierCharacter(c)
        End Function

        Friend Shared Function IsWideIdentifierCharacter(c As Char) As Boolean
            Dim CharacterProperties As UnicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c)

            Return IsPropAlphaNumeric(CharacterProperties) OrElse
                IsPropLetterDigit(CharacterProperties) OrElse
                IsPropConnectorPunctuation(CharacterProperties) OrElse
                IsPropCombining(CharacterProperties) OrElse
                IsPropOtherFormat(CharacterProperties)
        End Function

        Friend Shared Function IsPropOtherFormat(CharacterProperties As UnicodeCategory) As Boolean
            Return CharacterProperties = UnicodeCategory.Format
        End Function

        Friend Shared Function IsPropCombining(CharacterProperties As UnicodeCategory) As Boolean
            Return CharacterProperties >= UnicodeCategory.NonSpacingMark AndAlso
                CharacterProperties <= UnicodeCategory.EnclosingMark
        End Function

        Friend Shared Function IsPropAlphaNumeric(CharacterProperties As UnicodeCategory) As Boolean
            Return CharacterProperties <= UnicodeCategory.DecimalDigitNumber
        End Function

        Friend Shared Function IsNarrowIdentifierCharacter(c As UInt16) As Boolean
            Return s_isIDChar(c)
        End Function

        Private Shared ReadOnly s_isIDChar As Boolean() =
        {
            False, False, False, False, False, False, False, False, False, False,
            False, False, False, False, False, False, False, False, False, False,
            False, False, False, False, False, False, False, False, False, False,
            False, False, False, False, False, False, False, False, False, False,
            False, False, False, False, False, False, False, False, True, True,
            True, True, True, True, True, True, True, True, False, False,
            False, False, False, False, False, True, True, True, True, True,
            True, True, True, True, True, True, True, True, True, True,
            True, True, True, True, True, True, True, True, True, True,
            True, False, False, False, False, True, False, True, True, True,
            True, True, True, True, True, True, True, True, True, True,
            True, True, True, True, True, True, True, True, True, True,
            True, True, True, False, False, False, False, False
        }

        Private Function IsWordStartCharacter(ch As Char) As Boolean
            Return IsIdentifierStartCharacter(ch)
        End Function

        Public Shared Function IsIdentifierStartCharacter(
            c As Char
        ) As Boolean
            'TODO: make easy cases fast (or check if they already are)
            Dim CharacterProperties As UnicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c)

            Return IsPropAlpha(CharacterProperties) OrElse
            IsPropLetterDigit(CharacterProperties) OrElse
            IsPropConnectorPunctuation(CharacterProperties)
        End Function

        Friend Shared Function IsPropConnectorPunctuation(CharacterProperties As UnicodeCategory) As Boolean
            Return CharacterProperties = UnicodeCategory.ConnectorPunctuation
        End Function

        Friend Shared Function IsPropLetterDigit(CharacterProperties As UnicodeCategory) As Boolean
            Return CharacterProperties = UnicodeCategory.LetterNumber
        End Function

        Friend Shared Function IsPropAlpha(CharacterProperties As UnicodeCategory) As Boolean
            Return CharacterProperties <= UnicodeCategory.OtherLetter
        End Function

        Public Shared Function IsStartingNewWord(ByVal text As SourceText, ByVal characterPosition As Integer, ByVal isWordStartCharacter As Func(Of Char, Boolean), ByVal isWordCharacter As Func(Of Char, Boolean)) As Boolean
            Dim ch = text(characterPosition)

            If Not isWordStartCharacter(ch) Then
                Return False
            End If

            If characterPosition > 0 AndAlso isWordCharacter(text(characterPosition - 1)) Then
                Return False
            End If

            If characterPosition < text.Length - 1 AndAlso isWordCharacter(text(characterPosition + 1)) Then
                Return False
            End If

            Return True
        End Function

        Public Function GetPreviousTokenIfTouchingText(token As SyntaxToken, position As Integer) As SyntaxToken
            Return If(token.IntersectsWith(position) AndAlso IsText(token),
                      token.GetPreviousToken(includeSkipped:=True),
                      token)
        End Function

        Private Function IsText(token As SyntaxToken) As Boolean
            Return token.IsKind(SyntaxKind.XmlNameToken, SyntaxKind.XmlTextLiteralToken, SyntaxKind.IdentifierToken)
        End Function

    End Class

End Namespace