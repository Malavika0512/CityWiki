using EncryptionDecryption;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace CityWiki.Staff
{
    public partial class StaffPage : System.Web.UI.Page
    {
        Services.Service1Client serviceClient = new Services.Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie memberCookies = Request.Cookies["MemberCookie"];
            HttpCookie staffCookies = Request.Cookies["StaffCookie"];
            if (memberCookies != null)
            {
                Session["Role"] = "2";
                Session["Username"] = memberCookies["Name"];
            }
            else if (staffCookies != null)
            {
                Session["Role"] = "3";
                Session["Username"] = staffCookies["Name"];
            }

            if (Request.Cookies["MemberCookie"] == null && Request.Cookies["StaffCookie"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else if (Session["Role"] != null && Session["Role"].Equals("2"))
            {
                Response.Redirect("UnauthorizedUser.aspx");
            }
            user.Text = "Welcome " + Session["Username"].ToString();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            LoginService.Service1Client client = new LoginService.Service1Client();
            try
            {
                // ensures username, password and image string textboxes are not empty
                if (!String.IsNullOrWhiteSpace(Username.Text) && !String.IsNullOrWhiteSpace(Password.Text))
                {
                    string encryptedPassword = Class1.Encrypt(Password.Text);
                    //string response = client.addUser(UserInput.Text, Cryption.Encrypt(PasswordInput.Text), 2);  // Adding the user by manipulating XML file
                    string registrationResponse = client.addtoXML(Username.Text, encryptedPassword, "Staff");
                    if (registrationResponse.Equals("Success"))
                    {
                        //  Error.Text = "User has been registered Successfully!";
                        Session["StaffRegistration"] = "Success";
                        Result.Text = "<p style = 'color: green'>Staff member successfully added.</p>";
                        //Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        Result.Text = registrationResponse;
                    }
                }
                else
                {
                    // display error if any of the input textbox is empty
                    Result.Text = "<p style='color: red'>Please enter username and password</p>";
                }
            }
            catch (Exception ex)
            {
                // display exception occurred error message
                Result.Text = "<p style = 'color: red' > Something went wrong Try again.</p>";
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookies = new HttpCookie("StaffCookie");
                cookies.Expires = DateTime.Now.AddMonths(-6);
                Response.Cookies.Add(cookies);
                Session["Username"] = null;
                Session["Role"] = null;
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                Label.Text = ex.Message;
            }
        }

        protected void FIndNearestStore_Click(object sender, EventArgs e)
        {
            string zipCode = NearestStoreZipcodeTextBox.Text;
            string serviceOutput = ""; // variable to store data returned by Nearest Store service
            string storeName = StoreNameTextBox.Text;
            NearestStoreResult.Text = "";

            // check if user has entered both zipcode and store name.
            if (!String.IsNullOrWhiteSpace(zipCode) && !String.IsNullOrWhiteSpace(storeName))
            {
                try
                {
                    if (validateZipCode(zipCode)) // checks ZIP code validity 
                    {
                        serviceOutput = serviceClient.getNearestStore(storeName, zipCode); // call getNearestStore method of the Nearest Store service.
                        var serviceOutputArray = serviceOutput.Split(new[] { '|' }); // splits the result by '|'

                        if (serviceOutputArray.Length > 1) // ensure that pipe separated string is returned by the service
                        {
                            // Add result to the result label
                            NearestStoreResult.Text += "<b> Store Details: </b><br />";
                            NearestStoreResult.Text += "<b>Name</b> - " + serviceOutputArray[0] + "<br />";
                            NearestStoreResult.Text += "<b>Rating</b> - " + serviceOutputArray[1] + "<br />";
                            NearestStoreResult.Text += "<b>Vicinity</b> - " + serviceOutputArray[2] + "<br />";
                            NearestStoreResult.Text += "<b>Open Now</b> - " + serviceOutputArray[3] + "<br />";
                            NearestStoreResult.Text += "<b>Types</b> - " + serviceOutputArray[4];
                        }
                        else
                            NearestStoreResult.Text = "<p style='color: red'>" + serviceOutputArray[0] + "</p>"; // display error returned by the service

                    }
                    else
                    {

                        NearestStoreResult.Text = "<p style='color: red'>Invalid Zipcode! Please try again.</p>"; // invalid zipcode
                    }
                }
                catch (Exception ex)
                {
                    // display exception occurred error message
                    NearestStoreResult.Text = "<p style = 'color: red' > Something went wrong Try again.</p>";
                }
            }
            else
            {
                NearestStoreResult.Text = "<p style='color: red'>Please enter zipcode and store name to proceed.</p>"; // Display error when user didn't enter either zipcode or store name.
            }
        }

        // Checks the validity of the ZIP code
        protected Boolean validateZipCode(string zipCode)
        {
            string USZipCode = @"^\d{5}(?:[-\s]\d{4})?$"; // makes sure that the zipcode is a valid US zipcode
            return (Regex.Match(zipCode, USZipCode).Success);
        }

        protected void GetWeatherDetails_Click(object sender, EventArgs e)
        {
            string zipCode = WeatherServiceZipcodeTextBox.Text;
            string[] weatherResult = new string[5]; // variable to store result of weather service
            WeatherServiceResult.Text = "";

            // check if user has entered zipcode
            if (!String.IsNullOrWhiteSpace(zipCode))
            {
                try
                {


                    if (validateZipCode(zipCode)) // checks if entered zipcode is valid US zipcode 
                    {
                        weatherResult = serviceClient.getWeatherDetails(zipCode); // 
                        if (weatherResult != null && weatherResult.Length > 0)
                        {
                            WeatherServiceResult.Text += "<b> Weather Details: </b><br />";
                            for (int i = 0; i < weatherResult.Length; i++)
                            {
                                // add weather forecast for each day in the result label
                                WeatherServiceResult.Text += weatherResult[i] + "<br />";
                            }
                        }
                        else
                        {
                            // display error message if weather service returns null or empty array
                            WeatherServiceResult.Text = "<p style='color: red'>Something went wrong! No result found.</p>";
                        }

                    }
                    else
                    {
                        // display error message if user enters invalid zipcode
                        WeatherServiceResult.Text = "<p style='color: red'>Invalid Zipcode! Please try again.</p>";
                    }
                }
                catch (Exception ex)
                {
                    // display exception occurred error message
                    WeatherServiceResult.Text = "<p style = 'color: red' > Something went wrong Try again.</p>";
                }
            }
            else
            {
                // Display error when user didn't enter zipcode 
                WeatherServiceResult.Text = "<p style='color: red'>Please enter zipcode to proceed.</p>";
            }
        }

        // Address radio button is selected for source
        protected void selectSourceAddressRadioButton(object sender, EventArgs e)
        {
            if (sourceAddressRadio.Checked)
            {
                sourceZipRadio.Checked = false;
                sourceLabel1.Text = "Enter Street Address: ";
                sourceLabel2.Text = "Enter City: ";
                sourceTextBox1.Visible = true;
                sourceTextBox2.Visible = true;
            }
        }

        // Zipcode radio button is selected for source
        protected void selectSourceZipRadioButton(object sender, EventArgs e)
        {
            if (sourceZipRadio.Checked)
            {
                sourceAddressRadio.Checked = false;
                sourceLabel1.Text = "Enter Zipcode: ";
                sourceLabel2.Visible = false;
                sourceTextBox1.Visible = true;
                sourceTextBox2.Visible = false;
            }
        }

        // Address radio button is selected for destination
        protected void selectDestAddressRadioButton(object sender, EventArgs e)
        {
            if (destAddressRadio.Checked)
            {
                destZipRadio.Checked = false;
                destLabel1.Text = "Enter Street Address: ";
                destLabel2.Text = "Enter City: ";
                destTextBox1.Visible = true;
                destTextBox2.Visible = true;
            }
        }

        // Zipcode radio button is selected for destination
        protected void selectDestZipRadioButton(object sender, EventArgs e)
        {
            if (destZipRadio.Checked)
            {
                destAddressRadio.Checked = false;
                destLabel1.Text = "Enter Zipcode: ";
                destLabel2.Visible = false;
                destTextBox1.Visible = true;
                destTextBox2.Visible = false;
            }
        }

        // call findDistance service
        protected void FindDistance_Click(object sender, EventArgs e)
        {
            FindDistanceResult.Text = "";
            string source = "";
            string destination = "";
            try
            {
                // address radiobutton is selected for source
                if (sourceAddressRadio.Checked)
                {
                    // ensure both textboxes are not empty
                    if (String.IsNullOrWhiteSpace(sourceTextBox1.Text) || String.IsNullOrWhiteSpace(sourceTextBox2.Text))
                    {
                        FindDistanceResult.Text = "<p style='color: red'>Please enter both street address and city for source.</p>";
                        return;
                    }
                    else
                    {
                        // Append 'A' in string to tell the service that this is address
                        source += "A|" + sourceTextBox1.Text + "|" + sourceTextBox2.Text;
                    }
                }
                else if (sourceZipRadio.Checked) // zipcode radiobutton is selected for source
                {
                    if (String.IsNullOrWhiteSpace(sourceTextBox1.Text)) // ensure input textbox is not empty
                    {
                        FindDistanceResult.Text = "<p style='color: red'>Please enter zipcode for source.</p>";
                        return;
                    }
                    else
                    {
                        // Append 'Z' in string to tell the service that this is zipcode
                        source += "Z|" + sourceTextBox1.Text;
                    }
                }
                else
                {
                    // error message if no radio button is selected
                    FindDistanceResult.Text = "<p style='color: red'>Please select radio button to proceed.</p>";
                    return;
                }

                if (destAddressRadio.Checked) // address radiobutton is selected for destination
                {
                    // ensure both textboxes are not empty
                    if (String.IsNullOrWhiteSpace(destTextBox1.Text) || String.IsNullOrWhiteSpace(destTextBox2.Text))
                    {
                        FindDistanceResult.Text = "<p style='color: red'>Please enter both street address and city for destination.</p>";
                        return;
                    }
                    else
                    {
                        // Append 'A' in string to tell the service that this is address
                        destination += "A|" + destTextBox1.Text + "|" + destTextBox2.Text;
                    }
                }
                else if (destZipRadio.Checked) // zipcode radiobutton is selected for destination
                {
                    if (String.IsNullOrWhiteSpace(destTextBox1.Text)) // ensure input textbox is not empty
                    {
                        FindDistanceResult.Text = "<p style='color: red'>Please enter zipcode for destination.</p>";
                        return;
                    }
                    else
                    {
                        // Append 'Z' in string to tell the service that this is zipcode
                        destination += "Z|" + destTextBox1.Text;
                    }
                }
                else
                {
                    // error message if no radio button is selected
                    FindDistanceResult.Text = "<p style='color: red'>Please select radio button to proceed.</p>";
                    return;
                }

                string[] result = serviceClient.calculateDistanceAndDuration(destination, source); // 
                if (result != null && result.Length > 0 && result[0].ToString() == "OK")
                {
                    // display the success response
                    FindDistanceResult.Text = "Distance: " + result[1].ToString() + "<br />" + "Duration: " + result[2].ToString();
                }
                else
                {
                    // display the error returned.
                    FindDistanceResult.Text = "<p style='color: red'>" + result[0].ToString() + "</p>";
                }
            }
            catch (Exception ex)
            {
                // display exception occurred error message
                FindDistanceResult.Text = "<p style = 'color: red' > Something went wrong Try again.</p>";
            }

        }

        protected void NewsButton_Click(object sender, EventArgs e)
        {

            //Displaying the error message when the topics text entry box is empty
            if (String.IsNullOrWhiteSpace(NewsTextBox.Text))
            {
                NewsResult.Text = "<p style = 'color: red' > Please enter a valid topic or name of a city.</p>";
            }
            else
            {

                try
                {
                    //Parsing the string entered and calling the getnews function with the Service reference variable
                    String topic = NewsTextBox.Text;
                    String[] topics = topic.Split(',');
                    String[] result = serviceClient.getnews(topics);
                    NewsResult.Text = "\n" + String.Join("\n", result); ;
                }
                catch
                {
                    //Handling all other error conditions
                    NewsResult.Text = "<p style = 'color: red' > Enter valid topics separated by comma.</p>";
                }
            }
        }

        protected void HotelsButton_Click(object sender, EventArgs e)
        {
            HotelsResult.Text = "";
            if (String.IsNullOrWhiteSpace(HotelsTextbox.Text))
            {
                HotelsResult.Text = "<p style = 'color: red' > Please enter valid placename.</p>";
            }
            else
            {
                try
                {
                    string result = serviceClient.GetHotelData((String)HotelsTextbox.Text);
                    JArray obj = JArray.Parse(result);
                    for (int i = 0; i < obj.Count; i++)
                    {
                        HotelsResult.Text = HotelsResult.Text + "<b>Name :</b> " + obj[i]["name"] + ", " +
                            "<b>Opened Now : </b>" + obj[i]["opened_now"] + ", " +
                            "<b>Phone number: </b>" + obj[i]["phone_number"] + ", " +
                            "<b>Place ID: </b>" + obj[i]["place_id"] + ", " +
                            "<b> Rating : </b>" + obj[i]["rating"] + ", " + "</br>";

                    }

                }
                catch
                {
                    HotelsResult.Text = "<p style = 'color: red' > Something went wrong Try again.</p>";
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HotelsResult.Text = "";
            if (String.IsNullOrWhiteSpace(HotelsTextbox.Text))
            {
                HotelsResult.Text = "<p style = 'color: red' > Please enter valid placename.</p>";
            }
            else
            {
                try
                {
                    string result = serviceClient.GetFuelStations((String)HotelsTextbox.Text);
                    JArray stations = JArray.Parse(result);
                    for (int i = 0; i < stations.Count; i++)
                    {
                        //setting the response of station counts
                        HotelsResult.Text = HotelsResult.Text + "<b>No.of Biodiesel stations :</b> " + stations[i]["Biodiesel"] + " <br />" +
                            "<b>No.of CNG stations : </b>" + stations[i]["CNG"] +
                            "<br /><b>No.of Hydrogen stations : </b>" + stations[i]["Hydrogen"] +
                            "<br /><b>No.of LPG stations </b>" + stations[i]["LPG"] +
                            "<br /><b>No.of LNG stations </b>" + stations[i]["LNG"] +
                            "<br /><b> No.of Electric stations</b> " + stations[i]["Electric"] + " <br/> ";

                    }
                }
                catch
                {
                    HotelsResult.Text = "<p style = 'color: red' > Something went wrong Try again.</p>";
                }
            }
        }


        protected void ElectricityButton_Click(object sender, EventArgs e)
        {
            ElectricityResult.Text = "";
            if (!String.IsNullOrWhiteSpace(ElectricityTextBox.Text))
            {
                try
                {

                    //Calling the function GetElectricityRates with the service reference
                    string res = serviceClient.GetElectricityRates((string)ElectricityTextBox.Text);
                    JArray elec = JArray.Parse(res);
                    for (int i = 0; i < elec.Count; i++)
                    {
                        //Setting the response
                        ElectricityResult.Text = ElectricityResult.Text + "<b>Commercial Utility Rate :</b> " + elec[i]["commercial"] + " <br /><b>Industrial Utility Rate : </b>" + elec[i]["industrial"] + "<br /><b>Residential Utility Rate : </b>" + elec[i]["residential"] + "<br /><b>Company id : </b>" + elec[i]["company_id"] + "<br /><b>Utility Name : </b>" + elec[i]["utility_name"] + "<br />";
                        ElectricityNote.Text = "All values are in  ($/kWh)";
                    }
                }
                catch
                {
                    ElectricityResult.Text = "<p style = 'color: red' > Something went wrong Try again.</p>";
                }
            }
            else
            {
                ElectricityResult.Text = "<p style = 'color: red' > Please enter placename or city.</p>";
            }
        }

        protected void SolarButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(SolarTextBox.Text))
            {
                try
                {
                    //Calling the function GetSolarEnergyIndex with the service reference

                    string solars = serviceClient.GetSolarEnergyIndex((String)SolarTextBox.Text);
                    JArray solar = JArray.Parse(solars);
                    SolarResult.Text = "";
                    SolarNote.Text = "";
                    for (int i = 0; i < solar.Count; i++)
                    {
                        SolarResult.Text = SolarResult.Text + "<b>Annual direct normal irradiance :</b> " + solar[i]["direct_normal_irradiance_annual"] + " <br /><b>Monthly direct normal irradiance : </b>" + solar[i]["direct_normal_irradiance_monthly"] + "<br /><b>Annual global horizontal irradiance : </b>" + solar[i]["global_horizontal_irradiance_annual"] + "<br /><b>Monthly global horizontal irradiance : </b>" + solar[i]["global_horizontal_irradiance_monthly"] + "<br /><b>Annual latitude tilt : </b>" + solar[i]["latitude_tilt_annual"] + "<br /><b>Monthly Latitude tilt : </b>" + solar[i]["latitude_tilt_monthly"] + "<br />";
                        SolarNote.Text = "Direct normal irradiance :The values returned are kWh/m2/day (kilowatt hours per square meter per day). The insolation values represent the resource available to concentrating systems that track the sun throughout the day.  Average Global Horizontal Irradiance: The values returned are kWh/m2/day (kilowatt hours per square meter per day). The insolation values represent the global horizontal resource - the geometric sum of direct normal and diffuse irradiance components, representing total energy available on a planar surface.   Average Tilt at Latitude: The insolation values represent the global horizontal resource - the geometric sum of direct normal and diffuse irradiance components, representing total energy available on a planar surface.";
                    }
                }
                catch
                {
                    SolarResult.Text = "<p style = 'color: red' > Something went wrong Try again.</p>";
                }
            }
            else
            {
                SolarResult.Text = "<p style = 'color: red' > Please enter placename or city.</p>";
            }
        }

        protected void HazardsButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(HazardsTextbox.Text))
            {
                try
                {
                    //place to lat,long conversion
                    string latlong_url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + HazardsTextbox.Text + "&key=AIzaSyDpx3AxxT9bfrA_Lwulb6j_CT8hJzW85b8";
                    var response = new WebClient().DownloadString(latlong_url);
                    var jsonObj = JObject.Parse(response);
                    var res = (String)jsonObj["status"];
                    double lat = (double)jsonObj["results"][0]["geometry"]["location"]["lat"];
                    double lng = (double)jsonObj["results"][0]["geometry"]["location"]["lng"];

                    //Calling the function to get index
                    int result = serviceClient.GetEarthquakeIndex(lat, lng);
                    HazardsResult.Text = "Natural Hazards Index is : " + result.ToString();

                }
                catch
                {
                         //error case
                         HazardsResult.Text = "<p style = 'color: red' > Something went wrong Try again.</p>";
                }
            }
            else
            {
                HazardsResult.Text = "<p style = 'color: red' > Please enter placename or city.</p>";
            }
        }

        protected void Home_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}