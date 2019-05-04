Imports Vazor

Public Class LayoutView
    Inherits VazorView

    Public Sub New()
        MyBase.New("_Layout", "Pages\Shared", "Vazor Pages")
    End Sub

    Friend Shared Sub CreateNew()
        Vazor.VazorViewMapper.AddStatic(New LayoutView())
    End Sub

End Class