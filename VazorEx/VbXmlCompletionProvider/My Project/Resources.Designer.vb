﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("vbxmlCompProv.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;?xml version=&apos;1.0&apos; encoding=&apos;utf-8&apos;?&gt;
        '''&lt;xsd:schema
        '''	xmlns:xsd=&apos;http://www.w3.org/2001/XMLSchema&apos;
        '''	xmlns:vs=&apos;http://schemas.microsoft.com/Visual-Studio-Intellisense&apos;&gt;
        '''
        '''	&lt;xsd:include schemaLocation=&quot;I18Languages.xsd&quot;/&gt;
        '''	&lt;xsd:import schemaLocation=&quot;svg.xsd&quot;/&gt;
        '''
        '''	&lt;xsd:attributeGroup name=&quot;coreServerAttributeGroup&quot;&gt;
        '''		&lt;!-- Attributes --&gt;
        '''		&lt;xsd:attribute name=&quot;EnableTheming&quot; type=&quot;xsd:boolean&quot; vs:nonbrowseable=&quot;true&quot; vs:category=&quot;ASP.NET&quot; vs:disallowedonmobilepages=&quot;true&quot; default=&quot;true&quot; vs:serverattribu [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property CommonHTML5Types() As String
            Get
                Return ResourceManager.GetString("CommonHTML5Types", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;?xml version=&apos;1.0&apos; encoding=&apos;utf-8&apos;?&gt;
        '''&lt;xsd:schema xmlns=&apos;http://schemas.microsoft.com/intellisense/html5&apos;
        '''	xmlns:xsd=&apos;http://www.w3.org/2001/XMLSchema&apos;
        '''	targetNamespace=&apos;http://schemas.microsoft.com/intellisense/html5&apos;
        '''	xmlns:vs=&apos;http://schemas.microsoft.com/Visual-Studio-Intellisense&apos;
        '''	xmlns:svg=&quot;http://www.w3.org/2000/svg&quot;
        '''	vs:clientom=&quot;w3c-dom1.tlb&quot;
        '''	vs:ishtmlschema=&quot;true&quot;
        '''	vs:isserverschema=&quot;false&quot;
        '''	vs:htmlflavor=&quot;5.0&quot;
        '''	vs:cssschema=&quot;CSS 2.1&quot;
        '''	vs:SuccinctFriendlyName=&quot;HTML5&quot;
        '''	vs:customattrp [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property Html_5() As String
            Get
                Return ResourceManager.GetString("Html_5", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
