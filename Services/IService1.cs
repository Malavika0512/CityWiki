using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string[] calculateDistanceAndDuration(string destination, string source); // method to calculate distance and time required to travel from source to destination.

        //returns weather details of 5 days for a particular zipCode.
        [OperationContract]
        string[] getWeatherDetails(string zipCode);

        //returns the address of the provided storeName closest to the zipcode.
        [OperationContract]
        string getNearestStore(string storeName, string zipCode);

        [OperationContract]
        string GetSolarEnergyIndex(string placename);
        [OperationContract]
        string GetElectricityRates(string placename);
        [OperationContract]
        string GetFuelStations(string placename);
        [OperationContract]
        string GetHotelData(string placename);
       
        HotelDetails GetPerHotelDetails(string place_id);
        [OperationContract]
        List<String> getnews(string[] topic);
        [OperationContract]
        int GetEarthquakeIndex(double latitude, double longitude);

        // TODO: Add your service operations here
    }


    [DataContract]
    public class SolarIndex
    {
        [DataMember]
        public string direct_normal_irradiance_annual { get; set; }
        [DataMember]
        public string direct_normal_irradiance_monthly { get; set; }
        [DataMember]
        public string global_horizontal_irradiance_annual { get; set; }
        [DataMember]
        public string global_horizontal_irradiance_monthly { get; set; }
        [DataMember]
        public string latitude_tilt_annual { get; set; }
        [DataMember]
        public string latitude_tilt_monthly { get; set; }

    }
    [DataContract]
    public class UtilityData
    {
        [DataMember]
        public string commercial { get; set; }
        [DataMember]
        public string industrial { get; set; }
        [DataMember]
        public string residential { get; set; }

        [DataMember]
        public string company_id { get; set; }
        [DataMember]
        public string utility_name { get; set; }
    }

    [DataContract]
    public class FuelStations
    {
        [DataMember]
        public string Biodiesel { get; set; }
        [DataMember]
        public string CNG { get; set; }
        [DataMember]
        public string Electric { get; set; }

        [DataMember]
        public string Hydrogen { get; set; }
        [DataMember]
        public string LNG { get; set; }

        [DataMember]
        public string LPG { get; set; }

        [DataMember]
        public List<StationDetails> details { get; set; }
    }
    [DataContract]
    public class StationDetails
    {
        [DataMember]
        public string access_code { get; set; }
        [DataMember]
        public string cards_accepted { get; set; }
        [DataMember]
        public string fuel_type_code { get; set; }

        [DataMember]
        public string station_name { get; set; }
        [DataMember]
        public string station_phone { get; set; }

        [DataMember]
        public string street_address { get; set; }
        [DataMember]
        public string zip { get; set; }
    }
    //defining HotelList data type
    [DataContract]
    public class HotelList
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string opened_now { get; set; }
        [DataMember]
        public string vicinity { get; set; }

        [DataMember]
        public string place_id { get; set; }

        [DataMember]
        public string rating { get; set; }

        [DataMember]
        public string formatted_address { get; set; }

        [DataMember]
        public string phone_number { get; set; }

        [DataMember]
        public string reviews { get; set; }

        [DataMember]
        public string website { get; set; }

        [DataMember]
        public string url { get; set; }

    }

    //defining Hotel details datatype
    [DataContract]
    public class HotelDetails
    {
        [DataMember]
        public string rating { get; set; }

        [DataMember]
        public string formatted_address { get; set; }

        [DataMember]
        public string phone_number { get; set; }

        [DataMember]
        public string reviews { get; set; }

        [DataMember]
        public string website { get; set; }

        [DataMember]
        public string url { get; set; }
    }
}
