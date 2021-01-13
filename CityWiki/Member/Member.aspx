<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="CityWiki.Member.Member" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .absolute {
            width: 620px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <div  style="text-align: right;">
            <h2 style="text-align: center;"><asp:Label ID="Label" runat="server"></asp:Label>
&nbsp;&nbsp;
            <asp:Label ID="user" runat="server"></asp:Label></h2>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;
                <asp:Button ID="Home" runat="server" Text="Home" OnClick="Home_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="Logout" runat="server" OnClick="Logout_Click" style="margin-left: 0px" Text="Logout" />
                
&nbsp;&nbsp;</div>
            <h3 style="text-align: center;">As a member of City Wiki, you can get instant updates about a city or a place </h3>
            <br />
            <p>
            <asp:Label ID="WeatherService" runat="server" Font-Bold="True" Font-Size="Larger" Text="Weather Service"></asp:Label>
        </p>
        <p>
            <asp:Label ID="WeatherServiceDescription" runat="server" Text=" Description : Returns 5-day weather forecast for the given zipcode location"></asp:Label>
        </p>
            <p>
                Sample input value: Zipcode - 85281</p>
        <p id="WeatherServiceZipcodeLabel">
            Enter Zipcode
            <asp:TextBox ID="WeatherServiceZipcodeTextBox" runat="server" Width="156px"></asp:TextBox>
&nbsp;&nbsp;
            <asp:Button ID="GetWeatherDetails" runat="server" Text="Get Weather Details" Width="165px" OnClick="GetWeatherDetails_Click" />
        </p>
        <p>
            <asp:Label ID="WeatherServiceResult" runat="server"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
        <p style="height: 31px; width: 413px">
            <asp:Label ID="NearestStoreService" runat="server" Font-Bold="True" Font-Size="Larger" Text="Find the Nearest Store Service"></asp:Label>
        </p>
        <p style="height: 25px">
            <asp:Label ID="NearestStoreDescription" runat="server" Text=" Description : Returns the address of the provided store name closest to the zipcode."></asp:Label>
        </p>
            <p style="height: 25px">
                Sample input value: Zipcode - 85281, Store name - Nike</p>
        <p id="NearestStoreZipcodeLabel" style="height: 25px; width: 331px">
            Enter Zipcode&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="NearestStoreZipcodeTextBox" runat="server" Width="156px"></asp:TextBox>
&nbsp;&nbsp;
        </p>
        <p id="StoreNameLabel" style="height: 25px; width: 330px">
            Enter Store Name
            <asp:TextBox ID="StoreNameTextBox" runat="server" Width="156px"></asp:TextBox>
        </p>
        <p style="height: 32px; width: 160px">
&nbsp;<asp:Button ID="FIndNearestStore" runat="server" OnClick="FIndNearestStore_Click" Text="Find Nearest Store" Width="152px" />
        </p>
        <asp:Label ID="NearestStoreResult" runat="server"></asp:Label>
            <br />
            <br />
            <br />
            <p>
                <asp:Label ID="FindDistance" runat="server" Font-Bold="True" Font-Size="Larger" Text="Find Distance and Duration Service"></asp:Label>
            </p>
            <p>
                <asp:Label ID="FindDistanceDescription" runat="server" Text=" Description : Returns distance and duration required to travel from source to destination."></asp:Label>
            </p>
            <p>
                Sample input value: Source Zipcode - 85281, Destiantion ZipCode - 85282</p>
            <p class="absolute">
                Source:&nbsp;
                <asp:RadioButton ID="sourceAddressRadio" runat="server" AutoPostBack="True" OnCheckedChanged="selectSourceAddressRadioButton" Text="Address" />
&nbsp;
                <asp:RadioButton ID="sourceZipRadio" runat="server" AutoPostBack="True" OnCheckedChanged="selectSourceZipRadioButton" Text="Zipcode" />
            </p>
            <p class="absolute">
                <asp:Label ID="sourceLabel1" runat="server"></asp:Label>
&nbsp;
                <asp:TextBox ID="sourceTextBox1" runat="server" Visible="False"></asp:TextBox>
                &nbsp;</p>
            <p class="absolute">
                <asp:Label ID="sourceLabel2" runat="server"></asp:Label>
&nbsp;
                <asp:TextBox ID="sourceTextBox2" runat="server" Visible="False"></asp:TextBox>
            </p>
            <p class="absolute">
                Destination:&nbsp;
                <asp:RadioButton ID="destAddressRadio" runat="server" AutoPostBack="True" OnCheckedChanged="selectDestAddressRadioButton" Text="Address" />
&nbsp;
                <asp:RadioButton ID="destZipRadio" runat="server" AutoPostBack="True" OnCheckedChanged="selectDestZipRadioButton" Text="Zipcode" />
            </p>
            <p class="absolute">
                <asp:Label ID="destLabel1" runat="server"></asp:Label>
&nbsp;
                <asp:TextBox ID="destTextBox1" runat="server" Visible="False"></asp:TextBox>
            </p>
            <p class="absolute">
                <asp:Label ID="destLabel2" runat="server"></asp:Label>
&nbsp;
                <asp:TextBox ID="destTextBox2" runat="server" Visible="False"></asp:TextBox>
            </p>
            <p id="FindDistanceZipcodeLabel">
                &nbsp;<asp:Button ID="FindDistanceButton" runat="server" OnClick="FindDistance_Click" Text="Find Distance and Duration" Width="209px" />
            </p>
            <p>
                <asp:Label ID="FindDistanceResult" runat="server"></asp:Label>
            </p>
            <p>
                &nbsp;</p>
        <p>
                <asp:Label ID="FindDistance0" runat="server" Font-Bold="True" Font-Size="Larger" Text="Get News"></asp:Label>
            </p>
        </div>

        
        <p>
                <asp:Label ID="FindDistanceDescription0" runat="server" Text=" Description : Returns latest News for a place"></asp:Label>
            </p>
        <p>
                Sample input value: Places - Tempe, Arizona</p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Enter topics or places separated by comma "></asp:Label>
            <asp:TextBox ID="NewsTextBox" runat="server" Width="262px"></asp:TextBox>
        </p>
        <asp:Button ID="NewsButton" runat="server" OnClick="NewsButton_Click" Text="Get News" />
        
        <p>
            <asp:Label ID="NewsResult" runat="server" Visible="True"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
        <p>
                <asp:Label ID="FindDistance1" runat="server" Font-Bold="True" Font-Size="Larger" Text="Get Nearby Hotels and Fuel Stations"></asp:Label>
            </p>
        <p>
                <asp:Label ID="FindDistanceDescription1" runat="server" Text=" Description : Returns Nearby Hotels"></asp:Label>
            </p>
        <p>
                Sample input value: Placename - Tempe</p>

        
        <p>
            <asp:Label ID="Label2" runat="server" Text="Enter a placename "></asp:Label>
            <asp:TextBox ID="HotelsTextbox" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="HotelsButton" runat="server" OnClick="HotelsButton_Click" Text="Get Nearby Hotels" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Get Nearby Fuel Stations" />
        <br />
        <p>
            <asp:Label ID="HotelsResult" runat="server"></asp:Label>
        </p>
        <p>
            &nbsp;</p>

        
    </form>
</body>
</html>
