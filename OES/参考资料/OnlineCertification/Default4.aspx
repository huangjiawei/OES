<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Default4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script src="Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="Scripts/Index.js" type="text/javascript"></script>
    <script language="javascript">

        function a() {
            $("#d1 #t").val("helllo");
           // document.write("hello");
            // alert("hello");
            alert("a");
        }
        function b() {
            $("#d2 #t").val("world");
            alert("b");
            

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="d1">
       <input type="text" value="" id="t" />
    </div>
    <div id="d2">
       <input type="text" value="" id="t" />
   </div>
   <input type="button" onclick="a()"  name="btn" title="hello" />
  <asp:Button ID="Button1" runat="server" Text="Button"  OnClientClick="b()" />
  
    </form>
</body>
</html>
