<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CityWiki.Member.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
                <h2 style="text-align: right;">
                    <asp:Button ID="default" runat="server" Text="Go to Main Page" OnClick="default_Click" />
                </h2>
                <h2 style="text-align: center;">Member Login</h2>
                <p style="text-align: center;">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </p><br />
                <br />
                Enter Username :&nbsp;&nbsp;
                <asp:TextBox ID="Username" runat="server" Width="296px"></asp:TextBox>
                <br />
                <br />
                Enter Password :&nbsp;&nbsp;
                <asp:TextBox ID="Password" TextMode="password"  runat="server" Width="296px"></asp:TextBox>
                <br />
            <br />
            <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click"  />
            <br />
            <br />
            <asp:Label ID="Result" runat="server"></asp:Label>
                <br />
        </div>
    </form>
</body>
</html>
