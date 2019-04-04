Public MustInherit Class VazorView
    Implements IVazorView

    Public ReadOnly Property Name As String Implements IVazorView.Name

    Public ReadOnly Property Path As String Implements IVazorView.Path

    Public ReadOnly Property Title As String

    Public Overridable ReadOnly Property Encoding As System.Text.Encoding

    Protected Sub New(name As String, path As String, title As String)
        Me.New(name, path, title, System.Text.Encoding.UTF8)
    End Sub

    Protected Sub New(name As String, path As String, title As String, Encoding As System.Text.Encoding)
        Me.Name = name
        Me.Path = path
        Me.Title = title
        Me.Encoding = Encoding
    End Sub

    Public MustOverride ReadOnly Property Content() As Byte() Implements IVazorView.Content


End Class
