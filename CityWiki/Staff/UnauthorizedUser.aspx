<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnauthorizedUser.aspx.cs" Inherits="CityWiki.Staff.UnauthorizedUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p style="text-align: right;">
                <asp:Button ID="Home" runat="server" OnClick="Home_Click" Text="Home" />
            &nbsp;&nbsp;
                <asp:Button ID="Logout" runat="server" Text="Logout" OnClick="Logout_Click" />
                &nbsp;</p>
            <p style="text-align: right;">
                &nbsp;
                <asp:Label ID="Label" runat="server"></asp:Label>
            </p>
            <h2 style="text-align: center;">You are not authorized to access this page! Please login again using valid staff credentials (You will be automatically logged out from your current member account.)</h2>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            <br />

            <div style="text-align: center;">
                <h2 style="text-align: center;">Staff Login</h2>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <br />
                <br />
                Enter Username :&nbsp;&nbsp;
                <asp:TextBox ID="Username" runat="server" Width="296px"></asp:TextBox>
                <br />
                <br />
                Enter Password :&nbsp;&nbsp;
                <asp:TextBox ID="Password" TextMode="password" runat="server" Width="296px"></asp:TextBox>
                <br />
            <br />
            <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click"  />
            <br />
            <br />
            <asp:Label ID="Result" runat="server"></asp:Label>
                <br />
        </div>
        </div>
    </form>
</body>
</html>
