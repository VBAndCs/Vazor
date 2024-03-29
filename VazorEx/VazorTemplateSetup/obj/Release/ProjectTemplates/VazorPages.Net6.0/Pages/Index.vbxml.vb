﻿' To have html editor support:
' Download and setup this VSIX: https://github.com/VBAndCs/Vazor/blob/master/vbxmlCompletionProviderVSIX.zip?raw=true

Imports Microsoft.AspNetCore.Mvc.ViewFeatures

Partial Public Class IndexModel
    Protected Function GetVbXml(students As List(Of Student), viewData As ViewDataDictionary) As XElement
        ' <vbxml> is virtual node, and will be deleted.
        ' It is only used to contain all XML node in one root.
        ' If your html code is contained in one parent node, use it instead

        Return _
 _
        <zml xmlns:z="zml">
            <z:viewdata Title='"test"' Message='"OK"' Key='"value"'/>

            <h3 fff=""> Browse Students</h3>
            <p>Select from <%= students.Count() %> students:</p>
            <ul>
                <!--Use lambda expressions to execute vb code block-->
                <%= (Iterator Function()
                         For Each std In students
                             Yield <li><%= std.Name %></li>
                         Next
                     End Function)() %>
            </ul>
            <!--Or use ZML tags directly-->
            <z:if condition="Model.Students.Count > 1 andalso not (Model.Students.Count >= 10)">
                <p>Students details:</p>
                <ul>
                    <z:foreach var="m" in="Model.Students">
                        <li>
                        Id: @m.Id<br/>
                        Name: @m.Name<br/>
                            <p>Grade: @m.Grade</p>
                        </li>
                    </z:foreach>
                </ul>
            </z:if>
            <script>
                 var x = 5;
                document.writeln("students count = @Model.Students.Count");
                
        </script>
        </zml>

    End Function

End Class
