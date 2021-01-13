<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CityWiki.Member.Register" %>
<%@ Register TagPrefix="userControl" TagName="ImageVerifier" src="../ImageVerification.ascx"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 1132px; text-align: center;">
            <h2 style="text-align: right;">
                    <asp:Button ID="default" runat="server" Text="Go to Main Page" OnClick="default_Click" />
                </h2>
            <h2>Member Registration</h2>
            <p>&nbsp;</p>
            <p id="usernameLabel">
                Enter Username:&nbsp;
                <asp:TextBox ID="Username" runat="server" Width="261px"></asp:TextBox>
&nbsp;&nbsp;
            </p>
            <p id="passwordLabel">
                Enter Password:&nbsp;
                <asp:TextBox ID="Password" TextMode="password"  runat="server" Width="263px"></asp:TextBox>
&nbsp;&nbsp;
            </p>
            <userControl:ImageVerifier ID="Test" runat="Server"></userControl:ImageVerifier>

            &nbsp;
            <asp:TextBox ID="ImageTextBox" runat="server"></asp:TextBox>

            <p>
&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="RegisterButton" runat="server" style="margin-left: 3px" Text="Register" Width="86px" OnClick="RegisterButton_Click" />
            </p>
            <p>
                <asp:Label ID="UserRegistrationResult" runat="server"></asp:Label>
            </p>
        </div>
    </form>
</body>
</html>
