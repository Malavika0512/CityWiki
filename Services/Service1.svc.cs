using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;


namespace Services
{
    // class to store result of Google's Geocode API. Structure of this class is same as the structure of API response.
    public class Location
    {
        public string status { get; set; }
        public Result[] results { get; set; }

        // class to store place_id from Google's Geocode API's response. 
        // In the API documentation it was suggested to use placeId instead of latitude and logitude.
        public class Result
        {
            public string place_id { get; set; }
        }
    }

    // class to store result of Google's Distance Matrix API. Structure of this class is same as the structure of API response.
    public class DistanceAndDuration
    {
        public string status { get; set; }
        public Rows[] rows { get; set; }
        public class Rows
        {
            public Elements[] elements { get; set; }
            public class Elements
            {
                public string status { get; set; }
                public Duration duration { get; set; }
                public Distance distance { get; set; }
                public class Duration
                {
                    public string text { get; set; }
                }
                public class Distance
                {
                    public string text { get; set; }
                }
            }
        }
    }

    // used to deserialize JSON returned by google's nearbysearch API
    public class NearByStore
    {
        public string status { get; set; }
        public Results[] results { get; set; }
        // store class used for store details
        public class Results
        {
            public string name { get; set; }
            public string[] types { get; set; }
            public float rating { get; set; }
            public OpeningHours opening_hours { get; set; }
            public string vicinity { get; set; }

            public class OpeningHours
            {
                public bool open_now { get; set; }
            }
        }
    }

    // used to deserialize JSON returned by weatherbit API
    public class WeatherAPIResult
    {
        public Data[] data { get; set; }

        public class Data
        {
            public string max_temp { get; set; } // Maximum Temperature (default Celcius)
            public string min_temp { get; set; } // Minimum Temperature (default Celcius)
            public string pop { get; set; } // Probability of Precipitation (%)
            public string valid_date { get; set; } // Date the forecast is valid for in format YYYY-MM-DD [Midnight to midnight local time]
            public string wind_spd { get; set; } // Wind speed (Default m/s)
            public string wind_dir { get; set; } // Wind direction (degrees)
        }
    }

    // used to deserialize JSON returned by google's geocode API
    public class GeocodeAPIResult
    {
        public Results[] results { get; set; }
        public string status { get; set; }
        // used to deserialize the list of result in the JSON returned by google's geocode API
        public class Results
        {
            public Geometry geometry { get; set; }
            // used to deserialize geometry in JSON returned by google's geocode API
            public class Geometry
            {
                public Location location { get; set; }
                // used to deserialize location in JSON returned by google's geocode API
                public class Location
                {
                    public float lat { get; set; }
                    public float lng { get; set; }
                }
            }

        }
    }

    public class Service1 : IService1
    {
        // method to calculate distance and time required to travel from source to destination.
        public string[] calculateDistanceAndDuration(string destination, string source)
        {
            List<string> result = new List<string>(); // list to store final response
            string destAddress = "";
            destination = destination.Replace(',', ' '); // replace all the ',' with empty cahracter
            var destArray = destination.Split('|'); // split the string by '|'. It was appended to distinguish between address input and zipcode.
            var destAddressArray = destArray[1].Split(' '); //split the string by empty space 
            if (destArray[0] == "A") // if address is provided by the user
            {
                // creating a string with all the words separated by '+' to meet the requirement of the geocode api.
                foreach (string word in destAddressArray)
                {
                    destAddress = destAddress + word + "+";
                }
                destAddress = destAddress.TrimEnd('+');
                destination = destAddress + ",+" + destArray[2];
            }
            else // if zipcode is provided by the user
            {
                destination = destArray[1];
            }

            string sourceAddress = "";
            source = source.Replace(',', ' '); // replace all the ',' with empty cahracter
            var sourceArray = source.Split('|'); // split the string by '|'. It was appended to distinguish between address input and zipcode.
            var sourceAddressArray = sourceArray[1].Split(' '); //split the string by empty space 
            if (sourceArray[0] == "A") // if address is provided by the user
            {
                // creating a string with all the words separated by '+' to meet the requirement of the geocode api.
                foreach (string word in sourceAddressArray)
                {
                    sourceAddress = sourceAddress + word + "+";
                }
                sourceAddress = sourceAddress.TrimEnd('+');
                source = sourceAddress + ",+" + sourceArray[2];
            }
            else // if zipcode is provided by the user
            {
                source = sourceArray[1];
            }

            var webClient = new WebClient();

            string destPlaceId;
            string sourcePlaceId;

            using (webClient)
            {
                //calling Google's Geocode API to get place_id for destination address.
                string geoCodeURLForDest = "https://maps.googleapis.com/maps/api/geocode/json?address=" + destination + "&key=AIzaSyCjrhiG9e7PJYhJJ8_TkufijGg3W8MoSa8";
                var destJson = webClient.DownloadString(geoCodeURLForDest); // json to store response of geocode api
                Location destLocation = JsonConvert.DeserializeObject<Location>(destJson); // converting json to the object of class Location

                if (destLocation.status == "OK") // success reponse returned by geocode api
                {
                    destPlaceId = destLocation.results[0].place_id;
                }
                else // error response returned by geocode api
                {
                    result.Add("Error returned by Google's Geocode API while trying to fetch placeId for destination");
                    return result.ToArray();
                }

                // calling Google's Geocode API to get place_id for source address
                string geoCodeURLForSource = "https://maps.googleapis.com/maps/api/geocode/json?address=" + source + "&key=AIzaSyCjrhiG9e7PJYhJJ8_TkufijGg3W8MoSa8";
                var sourceJson = webClient.DownloadString(geoCodeURLForSource); // json to store response of geocode api
                Location sourceLocation = JsonConvert.DeserializeObject<Location>(sourceJson); // converting json to the object of class Location

                if (sourceLocation.status == "OK") // success reponse returned by geocode api
                {
                    sourcePlaceId = sourceLocation.results[0].place_id;
                }
                else // error response returned by geocode api
                {
                    result.Add("Error returned by Google's Geocode API while trying to fetch placeId for source");
                    return result.ToArray();
                }

                // calling Google's Distance Matrix API to get distance and time required to travel
                string distanceAPIUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&key=AIzaSyCjrhiG9e7PJYhJJ8_TkufijGg3W8MoSa8&origins=place_id:" + sourcePlaceId + "&destinations=place_id:" + destPlaceId;
                var distanceJson = webClient.DownloadString(distanceAPIUrl); // json to store response of distance matrix api

                DistanceAndDuration distAndDuration = new DistanceAndDuration();
                distAndDuration = JsonConvert.DeserializeObject<DistanceAndDuration>(distanceJson); // converting json to the object of class DistanceAndDuration

                if (distAndDuration.status == "OK") // success reponse returned by Distance Matrix API
                {
                    if (distAndDuration.rows[0].elements[0].status == "OK") // ensures that distance and duration is fetched correctly
                    {
                        string duration = distAndDuration.rows[0].elements[0].duration.text;
                        string distance = distAndDuration.rows[0].elements[0].distance.text;
                        result.Add("OK");
                        result.Add(distance);
                        result.Add(duration);
                    }
                    else // error in fetching distance and duration for the given locations
                    {
                        result.Add("Invalid source or destination");
                    }
                }
                else // error returned by distance matrix API
                {
                    result.Add("Google's Distance API returned error.");
                }

            }
            return result.ToArray();
        }

        // returns the address of the provided storeName closest to the zipcode.
        public string getNearestStore(string storeName, string zipCode)
        {
            string location; // variable used to store latitude and longitude of the given zipCode
            string nearestStore = "";
            string url = "https://maps.googleapis.com/maps/api/geocode/json?key=AIzaSyCjrhiG9e7PJYhJJ8_TkufijGg3W8MoSa8&components=postal_code:" + zipCode + "&sensor=false"; // invoke google's geocode API to get latitude and longitude of the given zipcode
            NearByStore nearByStore = new NearByStore(); // create object used to deserialize JSON returned by google's geocode API
            GeocodeAPIResult geocodeAPIResult = new GeocodeAPIResult(); // deserialize JSON returned by google's geocode API

            using (var webClient = new System.Net.WebClient())
            {
                geocodeAPIResult = JsonConvert.DeserializeObject<GeocodeAPIResult>(webClient.DownloadString(url)); // deserialize JSON to geocodeAPIResult object
                if (geocodeAPIResult.status == "OK") // ensures that we continue only if we have valid latitude and longitude
                {
                    location = geocodeAPIResult.results[0].geometry.location.lat.ToString() + ","
                                + geocodeAPIResult.results[0].geometry.location.lng.ToString();

                    // url to get the nearest store within 20 miles radius by using Google's nearbysearch API
                    url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="
                        + location + "&radius=32187&keyword=" + storeName + "&key=AIzaSyCjrhiG9e7PJYhJJ8_TkufijGg3W8MoSa8";

                    nearByStore = JsonConvert.DeserializeObject<NearByStore>(webClient.DownloadString(url)); // deserialize JSON to NearByStore object

                    if (nearByStore.status == "OK" && nearByStore.results != null && nearByStore.results.Length > 0) // ensures that we continue only if we get a valid store
                    {
                        nearestStore += nearByStore.results[0].name + "|" + nearByStore.results[0].rating.ToString() + "|" + nearByStore.results[0].vicinity
                            + "|" + nearByStore.results[0].opening_hours.open_now + "|";
                        foreach (var type in nearByStore.results[0].types)
                        {
                            nearestStore += type + ", ";
                        }
                        nearestStore = nearestStore.TrimEnd(',');
                    }
                    else
                    {
                        nearestStore += storeName + " does not exist within 20 miles of this area"; // show error if no store is returned by the nearbysearch API
                    }
                }
                else
                    nearestStore += "Please enter a valid zip code"; //show invalid zipcode error message
            }
            return nearestStore;
        }

        //Calls Weatherbit API and returns weather forecast for 5 days for the given zipCode
        public string[] getWeatherDetails(string zipCode)
        {
            string[] weatherDetails = new string[5]; // variable to store weather forecast for 5 days
            WeatherAPIResult weatherAPIResult = new WeatherAPIResult(); // object used to deserialize JSON returned by weatherbit API
            string url = "https://api.weatherbit.io/v2.0/forecast/daily?key=01bb9ada77e24bd2b667e9030deca59b&postal_code=" + zipCode; // url of the weatherbit API

            using (var webClient = new System.Net.WebClient())
            {
                weatherAPIResult = JsonConvert.DeserializeObject<WeatherAPIResult>(webClient.DownloadString(url)); // deserializing JSON returned by weatherbit API to WeatherAPIResult object

                // ensures that result is not null or empty
                if (weatherAPIResult != null && weatherAPIResult.data != null && weatherAPIResult.data.Length > 0)
                {
                    // iterate over data to get weather forecast for 5 days. API provides weather forecast for 16 days.
                    for (int i = 0; i < weatherAPIResult.data.Length; i++)
                    {
                        weatherDetails[i] = "<b>Date</b>: " + weatherAPIResult.data[i].valid_date + " | <b>Min Temperature (Celcius)</b>: " +
                            weatherAPIResult.data[i].min_temp + " | <b>Max Temperature (Celcius)</b>: " + weatherAPIResult.data[i].max_temp + " | <b>Probability of Precipitation (%)</b>: " +
                            weatherAPIResult.data[i].pop + " | <b>Wind speed (m/s)</b>: " + weatherAPIResult.data[i].wind_spd + " |  <b>Wind direction (degrees)</b>: " +
                            weatherAPIResult.data[i].wind_dir;

                        // We need weather forecast for only 5 days
                        if (i == 4)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    // if API call fails for any reason then return null. This scenario will be handled by calling service.
                    return null;
                }
            }

            return weatherDetails;
        }

        //function to get the Solar Energy Data for the given place or city name
        public string GetSolarEnergyIndex(string placename)
        {
            //Converting placename to lat and long
            string latlong_url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + placename + "&key=AIzaSyDpx3AxxT9bfrA_Lwulb6j_CT8hJzW85b8";
            var response = new WebClient().DownloadString(latlong_url);
            var jsonObj = JObject.Parse(response);
            var res = (String)jsonObj["status"];
            string lat = (string)jsonObj["results"][0]["geometry"]["location"]["lat"];
            string lng = (string)jsonObj["results"][0]["geometry"]["location"]["lng"];

            //Passing the lat and long parameters to the Solar energy API 
            string url = "https://developer.nrel.gov/api/solar/solar_resource/v1.json?api_key=JMS4IMl7gE8rGSwu6Spc2LbzvLgJNGW8PEUWZpVM&lat=" + lat + "&lon=" + lng;

            //Parsing the result
            var json = new WebClient().DownloadString(url);
            var jo = JObject.Parse(json);

            //List of defined type - SolarIndex
            List<SolarIndex> result = new List<SolarIndex>();
            SolarIndex temp = new SolarIndex();

            //Month array for get the monthly average Direct normal irradiance , global horizontal irradiance and latitude tilt.
            string[] months = new string[12] { "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };
            temp.direct_normal_irradiance_annual = (String)jo["outputs"]["avg_dni"]["annual"];
            string monthlyavg = "";
            foreach (var month in months)
            {
                monthlyavg = monthlyavg + month + ":" + (String)jo["outputs"]["avg_dni"]["monthly"][month] + ",";
            }
            temp.direct_normal_irradiance_monthly = monthlyavg;
            temp.global_horizontal_irradiance_annual = (String)jo["outputs"]["avg_ghi"]["annual"];
            monthlyavg = "";
            foreach (var month in months)
            {
                monthlyavg = monthlyavg + month + ":" + (String)jo["outputs"]["avg_ghi"]["monthly"][month] + ",";
            }
            temp.global_horizontal_irradiance_monthly = monthlyavg;
            temp.latitude_tilt_annual = (String)jo["outputs"]["avg_lat_tilt"]["annual"];
            monthlyavg = "";
            foreach (var month in months)
            {
                monthlyavg = monthlyavg + month + ":" + (String)jo["outputs"]["avg_lat_tilt"]["monthly"][month] + ",";
            }
            temp.latitude_tilt_monthly = monthlyavg;
            result.Add(temp);

            var final = new JavaScriptSerializer().Serialize(result);
            return final;
        }

        //function for getting the Electricity Rates for a given place or city
        public string GetElectricityRates(string placename)
        {
            //Converting placename to lat and long
            string latlong_url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + placename + "&key=AIzaSyDpx3AxxT9bfrA_Lwulb6j_CT8hJzW85b8";
            var response = new WebClient().DownloadString(latlong_url);
            var jsonObj = JObject.Parse(response);
            string lat = (string)jsonObj["results"][0]["geometry"]["location"]["lat"];
            string lng = (string)jsonObj["results"][0]["geometry"]["location"]["lng"];

            //Passing the lat and long to get the electricity rates
            string url = "https://developer.nrel.gov/api/utility_rates/v3.json?api_key=JMS4IMl7gE8rGSwu6Spc2LbzvLgJNGW8PEUWZpVM&lat=" + lat + "&lon=" + lng;
            var json = new WebClient().DownloadString(url);
            var jo = JObject.Parse(json);

            //List of type UtilityData for storing results
            List<UtilityData> result = new List<UtilityData>();
            UtilityData temp = new UtilityData();

            //Getting the different utility rates, utility name and id
            temp.commercial = (String)jo["outputs"]["commercial"];
            temp.industrial = (String)jo["outputs"]["industrial"];
            temp.residential = (String)jo["outputs"]["residential"];
            temp.company_id = (String)jo["outputs"]["company_id"];
            temp.utility_name = (String)jo["outputs"]["utility_name"];
            result.Add(temp);

            var final = new JavaScriptSerializer().Serialize(result);
            return final;
        }

        //function to return the Nearby Fuel stations count and details for a given place or city
        public string GetFuelStations(string placename)
        {
            //converting place to lat and long
            string latlong_url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + placename + "&key=AIzaSyDpx3AxxT9bfrA_Lwulb6j_CT8hJzW85b8";
            var response = new WebClient().DownloadString(latlong_url);
            var jsonObj = JObject.Parse(response);
            string lat = (string)jsonObj["results"][0]["geometry"]["location"]["lat"];
            string lng = (string)jsonObj["results"][0]["geometry"]["location"]["lng"];

            //passing the lat and long to get the nearby fuel station count and details limiting to the nearest 5
            string url = "https://developer.nrel.gov/api/alt-fuel-stations/v1/nearest.json?api_key=JMS4IMl7gE8rGSwu6Spc2LbzvLgJNGW8PEUWZpVM&limit=5&latitude=" + lat + "&longitude=" + lng;
            var json = new WebClient().DownloadString(url);
            var jo = JObject.Parse(json);

            //List of type FuelStations
            List<FuelStations> result = new List<FuelStations>();
            FuelStations temp = new FuelStations();

            //Count of different type of fuel stations nearby
            temp.Biodiesel = (String)jo["station_counts"]["fuels"]["BD"]["total"];
            temp.CNG = (String)jo["station_counts"]["fuels"]["CNG"]["total"];
            temp.Hydrogen = (String)jo["station_counts"]["fuels"]["HY"]["total"];
            temp.LPG = (String)jo["station_counts"]["fuels"]["LPG"]["total"];
            temp.LNG = (String)jo["station_counts"]["fuels"]["LNG"]["total"];
            temp.Electric = (String)jo["station_counts"]["fuels"]["ELEC"]["total"];

            //Details of nearby stations including their name, phone number, address, fuel type, cards acceptance and access code.
            List<StationDetails> stations = new List<StationDetails>();
            foreach (var item in jo["fuel_stations"])
            {
                StationDetails station = new StationDetails();
                station.access_code = (String)item["access_code"];
                station.cards_accepted = (String)item["cards_accepted"];
                station.fuel_type_code = (String)item["fuel_type_code"];
                station.station_name = (String)item["station_name"];
                station.station_phone = (String)item["station_phone"];
                station.street_address = (String)item["street_address"];
                station.zip = (String)item["zip"];
                stations.Add(station);
            }
            temp.details = stations;
            result.Add(temp);

            var final = new JavaScriptSerializer().Serialize(result);
            return final;

        }

        //Function to get nearby Hotels Data for a given city or placename
        public string GetHotelData(string placename)
        {
            //passing the placename to the google places search api
            string url = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=restaurants+in+" + placename + "&key=AIzaSyDpx3AxxT9bfrA_Lwulb6j_CT8hJzW85b8";

            //List of type HotelList 
            List<HotelList> res = new List<HotelList>();
            List<Object> list = new List<object>();
            var json = new WebClient().DownloadString(url);
            var jo = JObject.Parse(json);
            //parsing the results
            foreach (var item in jo["results"])
            {
                HotelList temp = new HotelList();
                temp.name = (String)item["name"];
                temp.place_id = (String)item["place_id"];
                temp.opened_now = (String)item["opening_hours"]["open_now"];
                temp.vicinity = (String)item["vicinity"];

                //passing the place id to get more details about the hotel
                HotelDetails result = GetPerHotelDetails((String)item["place_id"]);

                //Assigning the details output to list
                temp.rating = result.rating;
                temp.url = result.url;
                temp.website = result.website;
                temp.formatted_address = result.formatted_address;
                temp.phone_number = result.phone_number;
                list.Add(temp);
            }

            //Adding all the details to the result list
            for (int i = 0; i < list.Count; i++)
            {
                res.Add((HotelList)list[i]);
            }
            var final = new JavaScriptSerializer().Serialize(res);
            return final ;
        }

        // This function takes in the referenceId and gives reviews about a particular restaurant.
        public HotelDetails GetPerHotelDetails(string place_id)
        {
            //passing the place id to the google place details api
            string details_url = "https://maps.googleapis.com/maps/api/place/details/json?place_id=" + place_id + "&fields=name,rating,formatted_phone_number&key=AIzaSyDpx3AxxT9bfrA_Lwulb6j_CT8hJzW85b8";
            var json = new WebClient().DownloadString(details_url);
            var jo = JObject.Parse(json);

            //list of type hotel details
            HotelDetails temp = new HotelDetails();
            temp.rating = (String)jo["result"]["rating"];
            temp.url = (String)jo["result"]["url"];
            temp.website = (String)jo["result"]["website"];
            temp.formatted_address = (String)jo["result"]["formatted_address"];
            temp.phone_number = (String)jo["result"]["formatted_phone_number"];
            return temp;
        }

        //getnews function takes input as a list of topics or cities name and returns the news urls that where these topics have occurred in a list
        public List<String> getnews(string[] topic)
        {
            List<String> res = new List<string>();
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            foreach (var i in topic)
            {
                //Calls the newsapi url with the topic and TODAY'S date as parameters
                var url = "http://newsapi.org/v2/everything?" +
               "q=" + i + "&" +
               "from=" + today + "&" +
               "sortBy=popularity&" +
                "apiKey=21ef216bc33645e4a07a252e9f391fc3";

                //Downloading the response and parsing it
                var json = new WebClient().DownloadString(url);
                var jo = JObject.Parse(json);

                //Adding all the urls from the response to a list
                foreach (var item in jo["articles"])
                {
                    res.Add((String)item["url"]);
                }
            }
            //returning the list with urls
            return res;
        }

        //Returns the Earthquake function for a given latitude and longitude
        public int GetEarthquakeIndex(double latitude, double longitude)
        {
            //Using earthquake.usgs.gov api and passing todays date,latitude and longitude as parameters
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string URL = "https://earthquake.usgs.gov/fdsnws/event/1/count?starttime=1917-09-23&endtime=" + today + "&latitude=" + latitude + "&longitude=" + longitude + "&maxradiuskm=100&minmagnitude=2.5&eventtype=earthquake&orderby=time";
            int NaturalHazardsIndex;
            var response = "";

            //downloading the response and converting it to integer
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(URL);
            }
            NaturalHazardsIndex = Convert.ToInt32(response);

            //returning the index value
            return NaturalHazardsIndex;
        }

    }
}
