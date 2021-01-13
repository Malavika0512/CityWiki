<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffPage.aspx.cs" Inherits="CityWiki.Staff.StaffPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2 style="text-align: center;">&nbsp;<asp:Label ID="user" runat="server"></asp:Label>
             <p style="text-align: right;">
            
                <asp:Label ID="Label" runat="server"></asp:Label>
&nbsp;
                 <asp:Button ID="Home" runat="server" Text="Home" OnClick="Home_Click" />
                &nbsp;
                <asp:Button ID="Logout" runat="server" Text="Logout" OnClick="Logout_Click" style="height: 29px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </p>
        </h2>
       <h3 style="text-align: center;">As a staff of City Wiki, you can add other staff. You can also get instant updates about a city or a place along with the cost of living and energy resources there. </h3>
        <div>
                Enter Username :&nbsp;&nbsp;
                <asp:TextBox ID="Username" runat="server" Width="296px"></asp:TextBox>
                <br />
                <br />
                Enter Password :&nbsp;&nbsp;
                <asp:TextBox ID="Password" TextMode="password" runat="server" Width="296px"></asp:TextBox>
                <br />
            <br />
            <asp:Button ID="AddStaffButton" runat="server" Text="Add Staff Member" OnClick="LoginButton_Click"  />
            <br />
            <br />
            <asp:Label ID="Result" runat="server"></asp:Label>
                <br />
                <br />
        </div>
        <p>
            <asp:Label ID="ElectricityRates" runat="server" Font-Bold="True" Font-Size="Larger" Text="Electricity Rates"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label3" runat="server" Text=" Description : Returns Electricity Rates and Utilities Details for a given city or place"></asp:Label>
        </p>
        <asp:Label ID="Label4" runat="server" Text="Enter city or placename"></asp:Label>
        <asp:TextBox ID="ElectricityTextBox" runat="server"></asp:TextBox>
        <p>
            <asp:Button ID="ElectricityButton" runat="server" OnClick="ElectricityButton_Click" Text="Get Electricity Rates" />
        </p>
        <asp:Label ID="ElectricityResult" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="ElectricityNote" runat="server"></asp:Label>
        <br />
        <br />
        <br />
            <asp:Label ID="ElectricityRates0" runat="server" Font-Bold="True" Font-Size="Larger" Text="Solar Energy "></asp:Label>
        <br />
        <br />
            <asp:Label ID="WeatherServiceDescription0" runat="server" Text=" Description : Retuns Solar Energy Data for a city or place"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Enter city or placename"></asp:Label>
        :&nbsp;&nbsp;
        <asp:TextBox ID="SolarTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="SolarButton" runat="server" OnClick="SolarButton_Click" Text="Get Solar Energy Data" />
        <br />
        <br />
        <asp:Label ID="SolarResult" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="SolarNote" runat="server"></asp:Label>
        <br />
        <br />
        <br />
            <asp:Label ID="ElectricityRates1" runat="server" Font-Bold="True" Font-Size="Larger" Text="Natural Hazards Index"></asp:Label>
        <br />
        <br />
            <asp:Label ID="WeatherServiceDescription1" runat="server" Text=" Description : Returns Natural Hazards Index (Earthquake) for a city or place"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Enter city or placename"></asp:Label>
        :
        <asp:TextBox ID="HazardsTextbox" runat="server"></asp:TextBox>
        <p>
            <asp:Button ID="HazardsButton" runat="server" OnClick="HazardsButton_Click" Text="Get Hazards Index" />
        </p>
        <p>
            <asp:Label ID="HazardsResult" runat="server"></asp:Label>
        </p>
        <p>
            &nbsp;</p>


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
