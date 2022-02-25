'Public Class AnchorTagHelper
'    Inherits TagHelper

'    Private Sub New()
'        MyBase.New("anchor", "a", False)
'    End Sub

'    Private Const ActionAttributeName As String = "asp-action"
'    Private Const ControllerAttributeName As String = "asp-controller"
'    Private Const AreaAttributeName As String = "asp-area"
'    Private Const PageAttributeName As String = "asp-page"
'    Private Const PageHandlerAttributeName As String = "asp-page-handler"
'    Private Const FragmentAttributeName As String = "asp-fragment"
'    Private Const HostAttributeName As String = "asp-host"
'    Private Const ProtocolAttributeName As String = "asp-protocol"
'    Private Const RouteAttributeName As String = "asp-route"
'    Private Const RouteValuesDictionaryName As String = "asp-all-route-data"
'    Private Const RouteValuesPrefix As String = "asp-route-"
'    Private Const Href As String = "href"

'    Private _routeValues As IDictionary(Of String, String)


'    Protected Overrides Sub AddHelperAttrs(html As Text.StringBuilder)
'        Dim attrs = GetAttrNames(Me.GetType())

'    End Sub

'    Public Sub Process()

'        ' If "href" is already set, it means the user is attempting to use a normal anchor.
'        If HtmlAttributes.ContainsKey(Href) Then
'            If HelperAttributes.ContainsKey(ActionAttributeName) OrElse
'                    HelperAttributes.ContainsKey(ControllerAttributeName) OrElse
'                    HelperAttributes.ContainsKey(AreaAttributeName) OrElse
'                    HelperAttributes.ContainsKey(PageAttributeName) OrElse
'                    HelperAttributes.ContainsKey(PageHandlerAttributeName) OrElse
'                    HelperAttributes.ContainsKey(RouteAttributeName) OrElse
'                    HelperAttributes.ContainsKey(ProtocolAttributeName) OrElse
'                    HelperAttributes.ContainsKey(HostAttributeName) OrElse
'                    HelperAttributes.ContainsKey(FragmentAttributeName) OrElse
'                    (_routeValues IsNot Nothing AndAlso _routeValues.Count > 0) Then
'                ' User specified an href and one of the bound attributes; can't determine the href attribute.
'                Throw New InvalidOperationException("Cannot Override Href")
'            End If
'            Return
'        End If

'        Dim routeLink As Boolean = HelperAttributes.ContainsKey(RouteAttributeName)
'        Dim actionLink As Boolean = HelperAttributes.ContainsKey(ControllerAttributeName) OrElse HelperAttributes.ContainsKey(ActionAttributeName)
'        Dim pageLink As Boolean = HelperAttributes.ContainsKey(PageAttributeName) OrElse HelperAttributes.ContainsKey(PageHandlerAttributeName)

'        If (routeLink AndAlso actionLink) OrElse (routeLink AndAlso pageLink) OrElse (actionLink AndAlso pageLink) Then
'            Throw New InvalidOperationException("Cannot determine href dttribute")
'        End If

'        If HelperAttributes.ContainsKey(AreaAttributeName) Then
'            ' Unconditionally replace any value from asp-route-area.

'            _routeValues("area") = HelperAttributes(AreaAttributeName)
'        End If

'        Dim _tagBuilder As TagBuilder
'        If pageLink Then
'            var url = urlHelper.Page(pageName, pageHandler, routeValues, protocol, hostname, fragment);

'            _tagBuilder = Generator.GeneratePageLink(
'pageName:=Page,
'_pageHandler:=PageHandler,
'_protocol:=Protocol,
'_hostname:=Host,
'_fragment:=Fragment,
'routeValues:=routeValues,
'htmlAttributes:=Nothing)
'        ElseIf routeLink Then
'            _tagBuilder = Generator.GenerateRouteLink(
'ViewContext,
'linkText:=String.Empty,
'routeName:=Route,
'_protocol:=Protocol,
'hostName:=Host,
'_fragment:=Fragment,
'routeValues:=routeValues,
'htmlAttributes:=Nothing)

'        Else
'            _tagBuilder = Generator.GenerateActionLink(
'ViewContext,
'linkText:=String.Empty,
'actionName:=Action,
'controllerName:=Controller,
'_protocol:=Protocol,
'_hostname:=Host,
'_fragment:=Fragment,
'routeValues:=routeValues,
'htmlAttributes:=Nothing)
'        End If

'        output.MergeAttributes(_tagBuilder)
'    End Sub


'End Class
