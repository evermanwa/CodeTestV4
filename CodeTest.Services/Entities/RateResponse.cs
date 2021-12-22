using Newtonsoft.Json;

namespace CodeTest.Services.dll
{
    public  class RateResponse : ResponseBase
    {
        [JsonProperty("rate")]
        public RateData TaxRate { get; set; }

        public RateResponse()
        {
            TaxRate = new RateData();
        }
    }

    //Only going to add the response parameters for the US for now for simplicity
    //Not sure if they would need them for other countries and I don't wan the object to be too big for no reason
    public class RateData
    {
        public RateData()
        {
            Zip = string.Empty;
            State = string.Empty;
            StateRate = 0.0;
            County = string.Empty;
            CountyRate = 0.0;
            City = string.Empty;
            CityRate = 0.0;
            CombinedDistrictRate = 0.0;
            CombinedRate = 0.0;
            FreightTaxable = false;            
        }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("state_rate")]
        public double StateRate { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("county_rate")]
        public double CountyRate { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("city_rate")]
        public double CityRate { get; set; }

        [JsonProperty("combined_district_rate")]
        public double CombinedDistrictRate { get; set; }

        [JsonProperty("combined_rate")]
        public double CombinedRate { get; set; }

        [JsonProperty("freight_taxable")]
        public bool FreightTaxable { get; set; }
    }
}
