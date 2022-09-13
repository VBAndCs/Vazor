Option Explicit Off
Option Infer On
Option Strict Off
' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.Collections.Immutable

Friend Module GlyphTags
    Public Function GetTags(_glyph As Glyph) As ImmutableArray(Of String)
        Dim tempVar
        Select Case _glyph
            Case = Glyph.Assembly
                tempVar = WellKnownTagArrays.Assembly
            Case = Glyph.BasicFile
                tempVar = WellKnownTagArrays.VisualBasicFile
            Case = Glyph.BasicProject
                tempVar = WellKnownTagArrays.VisualBasicProject
            Case = Glyph.ClassPublic
                tempVar = WellKnownTagArrays.ClassPublic
            Case = Glyph.ClassProtected
                tempVar = WellKnownTagArrays.ClassProtected
            Case = Glyph.ClassPrivate
                tempVar = WellKnownTagArrays.ClassPrivate
            Case = Glyph.ClassInternal
                tempVar = WellKnownTagArrays.ClassInternal
            Case = Glyph.CSharpFile
                tempVar = WellKnownTagArrays.CSharpFile
            Case = Glyph.CSharpProject
                tempVar = WellKnownTagArrays.CSharpProject
            Case = Glyph.ConstantPublic
                tempVar = WellKnownTagArrays.ConstantPublic
            Case = Glyph.ConstantProtected
                tempVar = WellKnownTagArrays.ConstantProtected
            Case = Glyph.ConstantPrivate
                tempVar = WellKnownTagArrays.ConstantPrivate
            Case = Glyph.ConstantInternal
                tempVar = WellKnownTagArrays.ConstantInternal
            Case = Glyph.DelegatePublic
                tempVar = WellKnownTagArrays.DelegatePublic
            Case = Glyph.DelegateProtected
                tempVar = WellKnownTagArrays.DelegateProtected
            Case = Glyph.DelegatePrivate
                tempVar = WellKnownTagArrays.DelegatePrivate
            Case = Glyph.DelegateInternal
                tempVar = WellKnownTagArrays.DelegateInternal
            Case = Glyph.EnumPublic
                tempVar = WellKnownTagArrays.EnumPublic
            Case = Glyph.EnumProtected
                tempVar = WellKnownTagArrays.EnumProtected
            Case = Glyph.EnumPrivate
                tempVar = WellKnownTagArrays.EnumPrivate
            Case = Glyph.EnumInternal
                tempVar = WellKnownTagArrays.EnumInternal
            Case = Glyph.EnumMemberPublic
                tempVar = WellKnownTagArrays.EnumMemberPublic
            Case = Glyph.EnumMemberProtected
                tempVar = WellKnownTagArrays.EnumMemberProtected
            Case = Glyph.EnumMemberPrivate
                tempVar = WellKnownTagArrays.EnumMemberPrivate
            Case = Glyph.EnumMemberInternal
                tempVar = WellKnownTagArrays.EnumMemberInternal
            Case = Glyph.Error
                tempVar = WellKnownTagArrays.Error
            Case = Glyph.EventPublic
                tempVar = WellKnownTagArrays.EventPublic
            Case = Glyph.EventProtected
                tempVar = WellKnownTagArrays.EventProtected
            Case = Glyph.EventPrivate
                tempVar = WellKnownTagArrays.EventPrivate
            Case = Glyph.EventInternal
                tempVar = WellKnownTagArrays.EventInternal
            Case = Glyph.ExtensionMethodPublic
                tempVar = WellKnownTagArrays.ExtensionMethodPublic
            Case = Glyph.ExtensionMethodProtected
                tempVar = WellKnownTagArrays.ExtensionMethodProtected
            Case = Glyph.ExtensionMethodPrivate
                tempVar = WellKnownTagArrays.ExtensionMethodPrivate
            Case = Glyph.ExtensionMethodInternal
                tempVar = WellKnownTagArrays.ExtensionMethodInternal
            Case = Glyph.FieldPublic
                tempVar = WellKnownTagArrays.FieldPublic
            Case = Glyph.FieldProtected
                tempVar = WellKnownTagArrays.FieldProtected
            Case = Glyph.FieldPrivate
                tempVar = WellKnownTagArrays.FieldPrivate
            Case = Glyph.FieldInternal
                tempVar = WellKnownTagArrays.FieldInternal
            Case = Glyph.InterfacePublic
                tempVar = WellKnownTagArrays.InterfacePublic
            Case = Glyph.InterfaceProtected
                tempVar = WellKnownTagArrays.InterfaceProtected
            Case = Glyph.InterfacePrivate
                tempVar = WellKnownTagArrays.InterfacePrivate
            Case = Glyph.InterfaceInternal
                tempVar = WellKnownTagArrays.InterfaceInternal
            Case = Glyph.Intrinsic
                tempVar = WellKnownTagArrays.Intrinsic
            Case = Glyph.Keyword
                tempVar = WellKnownTagArrays.Keyword
            Case = Glyph.Label
                tempVar = WellKnownTagArrays.Label
            Case = Glyph.Local
                tempVar = WellKnownTagArrays.Local
            Case = Glyph.Namespace
                tempVar = WellKnownTagArrays.Namespace
            Case = Glyph.MethodPublic
                tempVar = WellKnownTagArrays.MethodPublic
            Case = Glyph.MethodProtected
                tempVar = WellKnownTagArrays.MethodProtected
            Case = Glyph.MethodPrivate
                tempVar = WellKnownTagArrays.MethodPrivate
            Case = Glyph.MethodInternal
                tempVar = WellKnownTagArrays.MethodInternal
            Case = Glyph.ModulePublic
                tempVar = WellKnownTagArrays.ModulePublic
            Case = Glyph.ModuleProtected
                tempVar = WellKnownTagArrays.ModuleProtected
            Case = Glyph.ModulePrivate
                tempVar = WellKnownTagArrays.ModulePrivate
            Case = Glyph.ModuleInternal
                tempVar = WellKnownTagArrays.ModuleInternal
            Case = Glyph.OpenFolder
                tempVar = WellKnownTagArrays.Folder
            Case = Glyph.Operator
                tempVar = WellKnownTagArrays.Operator
            Case = Glyph.Parameter
                tempVar = WellKnownTagArrays.Parameter
            Case = Glyph.PropertyPublic
                tempVar = WellKnownTagArrays.PropertyPublic
            Case = Glyph.PropertyProtected
                tempVar = WellKnownTagArrays.PropertyProtected
            Case = Glyph.PropertyPrivate
                tempVar = WellKnownTagArrays.PropertyPrivate
            Case = Glyph.PropertyInternal
                tempVar = WellKnownTagArrays.PropertyInternal
            Case = Glyph.RangeVariable
                tempVar = WellKnownTagArrays.RangeVariable
            Case = Glyph.Reference
                tempVar = WellKnownTagArrays.Reference
            Case = Glyph.StructurePublic
                tempVar = WellKnownTagArrays.StructurePublic
            Case = Glyph.StructureProtected
                tempVar = WellKnownTagArrays.StructureProtected
            Case = Glyph.StructurePrivate
                tempVar = WellKnownTagArrays.StructurePrivate
            Case = Glyph.StructureInternal
                tempVar = WellKnownTagArrays.StructureInternal
            Case = Glyph.TypeParameter
                tempVar = WellKnownTagArrays.TypeParameter
            Case = Glyph.Snippet
                tempVar = WellKnownTagArrays.Snippet
            Case = Glyph.CompletionWarning
                tempVar = WellKnownTagArrays.Warning
            Case = Glyph.StatusInformation
                tempVar = WellKnownTagArrays.StatusInformation
            Case Else
                tempVar = ImmutableArray(Of String).Empty
        End Select

        Return tempVar
    End Function
End Module


