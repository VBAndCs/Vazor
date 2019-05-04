Public MustInherit Class VazorView

    Public ReadOnly Property Name As String

    Public ReadOnly Property Path As String

    Public ReadOnly Property Title As String

    Public ReadOnly Property Encoding As System.Text.Encoding

    Protected Sub New(name As String, path As String, title As String)
        Me.New(name, path, title, System.Text.Encoding.UTF8)
    End Sub

    Protected Sub New(name As String, path As String, title As String, Encoding As System.Text.Encoding)
        Me.Name = name
        Me.Path = path
        Me.Title = title
        Me.Encoding = Encoding
    End Sub

    Public Overridable ReadOnly Property Content() As Byte()
        Get
            Dim html = GetVbXml().ParseZml
            Return Encoding.GetBytes(html)
        End Get
    End Property

    Public MustOverride Function GetVbXml() As XElement
End Class
