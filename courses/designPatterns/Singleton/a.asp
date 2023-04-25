<!DOCTYPE html>
<html>
<body>
<%
Application("NrUses")=Application("NrUses")+1
Session("NrUses")=Session("NrUses")+1
response.write("Application " & Application("NrUses") & "<br />" & "Session "& Session("NrUses"))

%>

</body>
</html> 