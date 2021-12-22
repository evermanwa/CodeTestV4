using Newtonsoft.Json;

namespace CodeTest.Services.dll
{
    public class RateRequest : RequestBase
    {
        public RateRequest()
        {
            Country = string.Empty;
            Zip = string.Empty;
            State = string.Empty;
            City = string.Empty;
            Street = string.Empty;
        }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }
    }
}
