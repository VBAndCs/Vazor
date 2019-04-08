' To have html editor support:
'     1. Close this file.
'     2. Right clicke On "Index.vbxml.vb" in solution explorer.
'     3. Click "Open with" from the context menu.
'     4. Choose "Html Editor" and click Ok.
' To go back to edit vb code, close the file, and double click on
' "Index.vbxml.vb" in solution explorer.

Partial Public Class IndexView
    Protected Function GetVbXml() As XElement
        ' <vbxml> is virtual node, and will be deleted.
        ' It is only used to contain all XML node in one root.
        ' If your html code is contained in one parent node, use it instead

        Return _
 _
        <vbxml>
            <h3> Browse Students</h3>
            <p>Select from <%= Students.Count() %> students:</p>
            <ul>
                <!--Use lambda expressions to execute vb code block-->
                <%= (Iterator Function()
                         For Each std In Students
                             Yield <li><%= std.Name %></li>
                         Next
                     End Function)() %>
            </ul>
            <p>Students details:</p>
            <ul>
                <!--Use my ForEach item template to generate html tags for each elemnt in the data model-->
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
