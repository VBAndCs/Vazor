' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports Microsoft.CodeAnalysis

Public MustInherit Class OptionStorageLocation

End Class


''' <summary>
''' Specifies that the option should be stored into a roamed profile across machines.
''' </summary>
Friend NotInheritable Class RoamingProfileStorageLocation
    Inherits OptionStorageLocation

    Private ReadOnly _keyNameFromLanguageName As Func(Of String, String)

    Public Function GetKeyNameForLanguage(languageName As String) As String
        Dim unsubstitutedKeyName As String = _keyNameFromLanguageName(languageName)

        If languageName Is Nothing Then
            Return unsubstitutedKeyName

        Else
            Dim substituteLanguageName As String =
                If(languageName = LanguageNames.CSharp,
                    "CSharp",
                    If(languageName = LanguageNames.VisualBasic,
                        "VisualBasic",
                        languageName
                     )
                )

            Return unsubstitutedKeyName.Replace("%LANGUAGE%", substituteLanguageName)
        End If
    End Function

    Public Sub New(keyName As String)
        _keyNameFromLanguageName = Function(underscore) keyName
    End Sub

    ''' <summary>
    ''' Creates a <see cref= "RoamingProfileStorageLocation"/> that has different key names for different languages.
    ''' </summary>
    ''' <param name="keyNameFromLanguageName">A function that maps from a <see cref= "LanguageNames"/> value to the key name.</param>
    Public Sub New(keyNameFromLanguageName As Func(Of String, String))
        _keyNameFromLanguageName = keyNameFromLanguageName
    End Sub
End Class
