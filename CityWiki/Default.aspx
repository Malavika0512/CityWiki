<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CityWiki._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <br />
    <div style="text-align: right;">
        
           
        &nbsp;&nbsp;
           
        <asp:Button ID="memberLogin" runat="server" Text="Member Login" OnClick="memberLogin_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:Button ID="memberRegistration" runat="server" Text="Member Registration" OnClick="memberRegistration_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="staffLogin" runat="server" Text="Staff Login"  OnClick="staffLogin_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Logout" runat="server" Text="Logout" OnClick="Logout_Click" />
            &nbsp;&nbsp;&nbsp;
            &nbsp;
           
            <br />
            <br />

            <asp:Label ID="welcome" runat="server"></asp:Label>

            <asp:Label ID="Result" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;

   

            <h2 style="text-align: center;">CityWiki</h2>
        <h2 style="text-align: center;">One stop application to know everything about a City!</h2>
        <p style="text-align: center;">&nbsp;</p>
        <div style="text-align: left;">
            <h3>Functionalities offered to the Members and Staff by the application: </h3>
            <h4>Get instant updates about a city or a place</h4>
        <ol>
           <li><h4>Weather Service</h4></li>
           This service provides 5-day weather forecast for the provided zip code. 
           <li><h4>Find the Nearest Store</h4></li>
           This service finds the provided storeName closest to the zip code and return the store details.
           <li><h4>Find distance and duration</h4></li>
           This service provides the distance between the two places and travel duration to go to a destination.
           <li><h4>NewsFocus</h4></li>
           This service displays the list of news urls in which topics that are given as input occurred.
           <li><h4>Nearby Hotels</h4></li>
           This service displays the nearby hotels and details like name, address, phone number, rating etc.
           <li><h4>Nearby Fuel Stations</h4></li>
           This service display the details of nearby fuel stations.
        </ol>
        <br/>
        <h3>Extra functionalities offered to the Staff by the application: </h3>
            <h4>Looking to move to or start a business in a city? Know the Cost of living and energy resources there! You need special staff credentials to access these information</h4>
            <p>&nbsp;</p>
        <ol>
           <li><h4>Add staff member</h4></li>
            This service allows staff member to add other staff members.
           <li><h4>Natural Hazards Index</h4></li>
           This service provides the index of Natural Hazards occurred in that area or place 
           <li><h4>Energy Resources in the area(Solar, Electricity)</h4></li>
           This service displays the Solar energy resources in an area, Residential, Commercial and Industrial electricity rates along with the utility providers details, Nearby fuel stations and details
        </ol>
        

        <h4>&nbsp;</h4>
            <h2>Sample inputs to test Services and local components</h2>
            The user can test the services by navigating to the respective pages from Member Page and all the services are listed with links in the service directory. Only the authorized users can access the services.<br>
        <h4>What are the test cases or inputs ? </h4>
       <ol>
        <li><h4>Weather Service</h4></li>
        Sample input value: Zipcode - 85281
        <li><h4>Find the Nearest Store</h4></li>
        Sample input values: Zipcode - 85281, Store - Nike
        <li><h4>Find distance and duration</h4></li>
        Sample input value: Source Zipcode - 85281, Destiantion ZipCode - 85282
        <li><h4>NewsFocus</h4></li>
        Sample input value: Places - Tempe, Arizona
        <li><h4>Nearby Hotels</h4></li>
        Sample input value: Placename - Tempe
        <li><h4>Nearby Fuel Stations</h4></li>
        Sample input value: Placename - Tempe
        <li><h4>Natural Hazards Index</h4></li>
            Sample input value: placename - Tempe
        <li><h4>Solar Energy Data in the area</h4></li>
            Sample input value: placename - Tempe
        <li><h4>Electricity rates and Utilities details in the area</h4></li>
            Sample input value: placename - Tempe
        <li><h4>Global.asax for Event handler</h4></li>
            Sample input value: A valid event has to be triggered to show the last accessed time and number of access requests made so far.
        <li><h4>DLL</h4></li>
            Sample input value: Given a valid plain string, it will encrypt the password when user tries to login or register.<li><h4>Cookies and Sessions</h4></li>
        It will show this default page before a user logs in. When user logins after Registration for the 1st time, username and password will be stored in Cookies. From next time onwards, it will take directly to Member/Staff Services Page according to MemberCookie or StaffCoookie
        If user clears Cookies(in welcome page) or logs out anytime then the flow will be Member/Staff Login page --> Member/Staff Services page. It will also take to Main Default page from Member/Staff Login page if MainPage button is clicked. <br/>Sample input value for member -
               Username - Test, Password - Test
           <br/> Sample input values for Staff: Username - TA, Password - Cse445ta!
        <li><h4>User Control</h4></li>
        Test Case : An image will be displayed with a string of length 6. Enter the String in the image correctly in the space provided for verification required for user registration.
        <li><h4>XML Manipulation - Add to XML (Registration)</h4></li>
            Test Case : Enter username and password to add to Member.xml / Staff.xml through Register window for Member and AddStaff window for staffs. This functionality can be verified, if the corresponding login works for the newly added username and password .<br />
        <li><h4>XML Manipulation - Search in XML (Login)</h4></li>
        Test Case : Try logging in with the same credentials, it searches the element from Member/Staff.xml and login succeeds if element search succeeds else fails.   <br />
     </ol>

            </div>
        <br/>
        <div style="text-align: left;">
            <asp:Label ID="ApplicationStartTime" runat="server"></asp:Label>
            <br />
            <asp:Label ID="AccessRequest" runat="server"></asp:Label>
            </div>  
        
    </div>

</asp:Content>
