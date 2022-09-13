Option Explicit Off
Option Infer On
Option Strict Off
' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.Collections.Immutable
Imports Microsoft.CodeAnalysis

Public Module WellKnownTags
    ' accessibility
    Public Const [Public] As String = NameOf([Public])
    Public Const [Protected] As String = NameOf([Protected])
    Public Const [Private] As String = NameOf([Private])
    Public Const Internal As String = NameOf(Internal)

    ' project elements
    Public Const File As String = NameOf(File)
    Public Const Project As String = NameOf(Project)
    Public Const Folder As String = NameOf(Folder)
    Public Const Assembly As String = NameOf(Assembly)

    ' language elements
    Public Const [Class] As String = NameOf([Class])
    Public Const Constant As String = NameOf(Constant)
    Public Const [Delegate] As String = NameOf([Delegate])
    Public Const [Enum] As String = NameOf([Enum])
    Public Const EnumMember As String = NameOf(EnumMember)
    Public Const [Event] As String = NameOf([Event])
    Public Const ExtensionMethod As String = NameOf(ExtensionMethod)
    Public Const Field As String = NameOf(Field)
    Public Const [Interface] As String = NameOf([Interface])
    Public Const Intrinsic As String = NameOf(Intrinsic)
    Public Const Keyword As String = NameOf(Keyword)
    Public Const Label As String = NameOf(Label)
    Public Const Local As String = NameOf(Local)
    Public Const [Namespace] As String = NameOf([Namespace])
    Public Const Method As String = NameOf(Method)
    Public Const [Module] As String = NameOf([Module])
    Public Const [Operator] As String = NameOf([Operator])
    Public Const Parameter As String = NameOf(Parameter)
    Public Const [Property] As String = NameOf([Property])
    Public Const RangeVariable As String = NameOf(RangeVariable)
    Public Const Reference As String = NameOf(Reference)
    Public Const [Structure] As String = NameOf([Structure])
    Public Const TypeParameter As String = NameOf(TypeParameter)

    ' other
    Public Const Snippet As String = NameOf(Snippet)
    Public Const [Error] As String = NameOf([Error])
    Public Const Warning As String = NameOf(Warning)

    Friend Const StatusInformation As String = NameOf(StatusInformation)

    Friend Const AddReference As String = NameOf(AddReference)
    Friend Const NuGet As String = NameOf(NuGet)
    Friend Const TargetTypeMatch As String = NameOf(TargetTypeMatch)
End Module


Friend Module WellKnownTagArrays
    Friend ReadOnly Assembly As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Assembly)
    Friend ReadOnly ClassPublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Class, WellKnownTags.Public)
    Friend ReadOnly ClassProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Class, WellKnownTags.Protected)
    Friend ReadOnly ClassPrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Class, WellKnownTags.Private)
    Friend ReadOnly ClassInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Class, WellKnownTags.Internal)
    Friend ReadOnly ConstantPublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Constant, WellKnownTags.Public)
    Friend ReadOnly ConstantProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Constant, WellKnownTags.Protected)
    Friend ReadOnly ConstantPrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Constant, WellKnownTags.Private)
    Friend ReadOnly ConstantInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Constant, WellKnownTags.Internal)
    Friend ReadOnly DelegatePublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Delegate, WellKnownTags.Public)
    Friend ReadOnly DelegateProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Delegate, WellKnownTags.Protected)
    Friend ReadOnly DelegatePrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Delegate, WellKnownTags.Private)
    Friend ReadOnly DelegateInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Delegate, WellKnownTags.Internal)
    Friend ReadOnly EnumPublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Enum, WellKnownTags.Public)
    Friend ReadOnly EnumProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Enum, WellKnownTags.Protected)
    Friend ReadOnly EnumPrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Enum, WellKnownTags.Private)
    Friend ReadOnly EnumInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Enum, WellKnownTags.Internal)
    Friend ReadOnly EnumMemberPublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.EnumMember, WellKnownTags.Public)
    Friend ReadOnly EnumMemberProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.EnumMember, WellKnownTags.Protected)
    Friend ReadOnly EnumMemberPrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.EnumMember, WellKnownTags.Private)
    Friend ReadOnly EnumMemberInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.EnumMember, WellKnownTags.Internal)
    Friend ReadOnly EventPublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Event, WellKnownTags.Public)
    Friend ReadOnly EventProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Event, WellKnownTags.Protected)
    Friend ReadOnly EventPrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Event, WellKnownTags.Private)
    Friend ReadOnly EventInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Event, WellKnownTags.Internal)
    Friend ReadOnly ExtensionMethodPublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.ExtensionMethod, WellKnownTags.Public)
    Friend ReadOnly ExtensionMethodProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.ExtensionMethod, WellKnownTags.Protected)
    Friend ReadOnly ExtensionMethodPrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.ExtensionMethod, WellKnownTags.Private)
    Friend ReadOnly ExtensionMethodInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.ExtensionMethod, WellKnownTags.Internal)
    Friend ReadOnly FieldPublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Field, WellKnownTags.Public)
    Friend ReadOnly FieldProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Field, WellKnownTags.Protected)
    Friend ReadOnly FieldPrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Field, WellKnownTags.Private)
    Friend ReadOnly FieldInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Field, WellKnownTags.Internal)
    Friend ReadOnly InterfacePublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Interface, WellKnownTags.Public)
    Friend ReadOnly InterfaceProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Interface, WellKnownTags.Protected)
    Friend ReadOnly InterfacePrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Interface, WellKnownTags.Private)
    Friend ReadOnly InterfaceInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Interface, WellKnownTags.Internal)
    Friend ReadOnly Intrinsic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Intrinsic)
    Friend ReadOnly Keyword As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Keyword)
    Friend ReadOnly Label As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Label)
    Friend ReadOnly Local As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Local)
    Friend ReadOnly [Namespace] As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Namespace)
    Friend ReadOnly MethodPublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Method, WellKnownTags.Public)
    Friend ReadOnly MethodProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Method, WellKnownTags.Protected)
    Friend ReadOnly MethodPrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Method, WellKnownTags.Private)
    Friend ReadOnly MethodInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Method, WellKnownTags.Internal)
    Friend ReadOnly ModulePublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Module, WellKnownTags.Public)
    Friend ReadOnly ModuleProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Module, WellKnownTags.Protected)
    Friend ReadOnly ModulePrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Module, WellKnownTags.Private)
    Friend ReadOnly ModuleInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Module, WellKnownTags.Internal)
    Friend ReadOnly Folder As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Folder)
    Friend ReadOnly [Operator] As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Operator)
    Friend ReadOnly Parameter As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Parameter)
    Friend ReadOnly PropertyPublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Property, WellKnownTags.Public)
    Friend ReadOnly PropertyProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Property, WellKnownTags.Protected)
    Friend ReadOnly PropertyPrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Property, WellKnownTags.Private)
    Friend ReadOnly PropertyInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Property, WellKnownTags.Internal)
    Friend ReadOnly RangeVariable As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.RangeVariable)
    Friend ReadOnly Reference As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Reference)
    Friend ReadOnly StructurePublic As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Structure, WellKnownTags.Public)
    Friend ReadOnly StructureProtected As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Structure, WellKnownTags.Protected)
    Friend ReadOnly StructurePrivate As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Structure, WellKnownTags.Private)
    Friend ReadOnly StructureInternal As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Structure, WellKnownTags.Internal)
    Friend ReadOnly TypeParameter As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.TypeParameter)
    Friend ReadOnly Snippet As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Snippet)

    Friend ReadOnly [Error] As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Error)
    Friend ReadOnly Warning As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Warning)
    Friend ReadOnly StatusInformation As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.StatusInformation)

    Friend ReadOnly AddReference As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.AddReference)
    Friend ReadOnly NuGet As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.NuGet)
    Friend ReadOnly TargetTypeMatch As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.TargetTypeMatch)

    Friend ReadOnly CSharpFile As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.File, LanguageNames.CSharp)
    Friend ReadOnly VisualBasicFile As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.File, LanguageNames.VisualBasic)

    Friend ReadOnly CSharpProject As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Project, LanguageNames.CSharp)
    Friend ReadOnly VisualBasicProject As ImmutableArray(Of String) = ImmutableArray.Create(WellKnownTags.Project, LanguageNames.VisualBasic)
End Module
