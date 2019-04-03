Public MustInherit Class VazorView
    Implements IVazorView

    Public MustOverride ReadOnly Property Name As String Implements IVazorView.Name

    Public MustOverride ReadOnly Property Path As String Implements IVazorView.Path

    Public MustOverride ReadOnly Property Title As String

    Public Overridable ReadOnly Property Encoding As System.Text.Encoding = System.Text.Encoding.UTF8

    Public MustOverride ReadOnly Property Content() As Byte() Implements IVazorView.Content


End Class
