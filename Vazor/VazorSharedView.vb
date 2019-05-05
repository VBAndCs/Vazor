Public MustInherit Class VazorSharedView
    Inherits VazorView

    Protected Sub New(name As String, path As String, title As String)
        MyBase.New(name, path, title, System.Text.Encoding.UTF8)
        VazorViewMapper.AddStatic(Me)
    End Sub

    Public Shared Sub CreateAll()

        Dim types = From t In Reflection.Assembly.GetCallingAssembly().GetTypes()
                    Where t.IsClass AndAlso
                        Not t.IsAbstract AndAlso
                        t.IsSubclassOf(GetType(VazorSharedView))

        For Each type As Type In types
            Activator.CreateInstance(type)
        Next type

    End Sub

End Class
