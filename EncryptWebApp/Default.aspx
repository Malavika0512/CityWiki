<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EncryptWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    

    <div class="row">
        <div class="col-md-4">
            
            <asp:Label ID="Label1" runat="server" Text="Enter the String"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Encrypt" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Decrypt" />
            <br />
            <br />
            <asp:Label ID="Note" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Result" runat="server"></asp:Label>
            
        </div>
    </div>

</asp:Content>
