'Imports System.Reflection

'Public MustInherit Class TagHelper

'    Protected Shared tagHelpers As New Dictionary(Of String, Type)

'    Public Shared Sub Register(name As String, type As Type)
'        If type Is GetType(TagHelper) Then
'            tagHelpers.Add(name, type)
'        End If
'    End Sub

'    Protected Sub New(helperTagName As String, htmlTagName As String, selfClosing As Boolean)
'        Me.HelperTagName = helperTagName
'        Me.HtmlTagName = htmlTagName
'        Me.SelfClosing = selfClosing
'    End Sub

'    Private Property HelperTagName As String

'    Private Property HtmlTagName As String

'    Protected Property HelperAttributes As New Dictionary(Of String, String)

'    Protected Property HtmlAttributes As New Dictionary(Of String, String)

'    Private Property SelfClosing As Boolean

'    Protected Sub AddArributes(attributes As IEnumerable(Of XAttribute))
'        For Each attr In attributes
'            If attr.Name.LocalName.StartsWith("asp-") Then
'                HelperAttributes.Add(attr.Name.ToString(), attr.Value)
'            Else
'                HtmlAttributes.Add(attr.Name.ToString(), attr.Value)
'            End If
'        Next

'    End Sub


'    Public Shared Function GetHtmlTag(HelperTagName As String, attributes As IEnumerable(Of XAttribute)) As XElement
'        If Not tagHelpers.ContainsKey(HelperTagName) Then Return Nothing
'        Dim helper = CType(Activator.CreateInstance(tagHelpers(HelperTagName), True), TagHelper)
'        helper.AddArributes(attributes)
'        Return helper.GetHtmlTag()
'    End Function

'    Protected Function GetHtmlTag() As XElement
'        Dim html As New Text.StringBuilder("<")
'        html.Append(HtmlTagName)
'        For Each attr In HtmlAttributes
'            html.Append(" ")
'            html.Append(attr.Key)
'            html.Append("=")
'            html.Append("""")
'            html.Append(attr.Value)
'            html.Append("""")
'        Next

'        AddHelperAttrs(html)


'        If SelfClosing Then
'            html.Append("/>")
'        Else
'            html.Append(">")
'            html.Append("</")
'            html.Append(HtmlTagName)
'            html.Append(">")
'        End If

'        Return XElement.Parse(html.ToString())
'    End Function

'    Protected MustOverride Sub AddHelperAttrs(html As Text.StringBuilder)


'    Public Shared Function GetAttrNames(type As Type) As List(Of String)
'        Dim attrs As New List(Of String)

'        Dim fieldInfos = type.GetFields(BindingFlags.Public Or BindingFlags.Static)

'        For Each fi As FieldInfo In fieldInfos
'            If fi.IsLiteral AndAlso Not fi.IsInitOnly AndAlso
'                fi.Name.EndsWith("AttributeName") Then attrs.Add(fi.GetValue(Nothing))
'        Next

'        Return attrs
'    End Function

'End Class
