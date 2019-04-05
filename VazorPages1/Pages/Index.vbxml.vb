Partial Public Class IndexView
    Protected Function GetVbXml() As XElement
        Return _
 _
        <vbxml>
            <h3> Browse Students</h3>
            <p>Select from <%= Students.Count() %> students:</p>
            <ul>
                <%= (Iterator Function()
                         For Each std In Students
                             Yield <li><%= std.Name %></li>
                         Next
                     End Function)() %>
            </ul>
            <p>Students details:</p>
            <ul>
                <li ForEach="m">
                Id: <m.Id/><br/>
                Name: <m.Name/><br/>
                    <p>Grade: <m.Grade/></p>
                </li>
            </ul>
            <script>
                 var x = 5;
                 document.writeln("students count = <%= Students.Count() %>");
        </script>
        </vbxml>

    End Function

End Class
