
Public Interface IVazorView

    Property Name As String

    Property Path As String

    Function GetVbXml() As XElement

    ReadOnly Property Content As Byte()
End Interface
