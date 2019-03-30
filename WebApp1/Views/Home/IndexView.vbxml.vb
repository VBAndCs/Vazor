Partial Public Class IndexView
    Private Function GetVbXml() As XElement Implements Vazor.IVazorView.GetVbXml
        ViewBag.Title = "Vazor Sample"
        Return _
    <vbxml>
        <h3> Browse Students</h3>
        <p>Select from <%= students.Count() %> students:</p>
        <ul>
            <%= (Iterator Function()
                     For Each std In students
                         Yield <li><%= std.Name %></li>
                     Next
                 End Function)() %>
        </ul>
        <script>
                 var x = 5;
                 document.writeln("students count = <%= students.Count() %>");
           </script>
    </vbxml>

    End Function
End Class
