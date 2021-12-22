using Newtonsoft.Json;

namespace CodeTest.Services.dll
{
    //base class to hold any errors or exceptions that may occur
    //common no matter which entity uses it for any calculator
    public class ResponseBase
    {
        //Error properties that can come back from
        //TaxJar api if there is any kind of issue
        //these are also pretty standard web responses
        //so these should work for most api's that want to be implemented
        [JsonProperty("status")]
        public int StatusCode { get; set; } = 0;
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;
        [JsonProperty("detail")]
        public string ErrorDetail { get; set; } = string.Empty;

        //Error properties if we have anything happen to cause an exception
        public string ExceptionMessage { get; set; } = string.Empty;        
        public bool HasException
        {
            get
            {
                return ExceptionMessage.Length > 0;
            }            
        }

        public ResponseBase()
        {
            
        }
    }
}
