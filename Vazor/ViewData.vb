Friend Class ViewData
    Public View As IVazorView
    Public Times As Integer

    Public Sub New(view As IVazorView, times As Integer)
        Me.View = view
        Me.Times = times
    End Sub
End Class
