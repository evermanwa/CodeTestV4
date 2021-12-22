using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CodeTest.Services.dll
{
    //Class for the taxjar api access
    //I would generally use a 3rd party tool as an http helper, but I'll stick with using only .net classes
    //If this class got any bigger there is definite re-factoring potential for the sending/receiving of the data
    //it's still small enough now it will work fine. Refacting woudln't break any unit tests in place either, so 
    //that shouldn't be an issue.
    public class TaxJarCalculator : ITaxCalculator
    {
        private readonly string baseUrl = "https://api.taxjar.com/v2/";
        private readonly string apiToken = "5da2f821eee4035db4771edab942a4cc";

        private readonly HttpClient _client;
        HttpClient Client
        {
            get
            {
                return _client;
            }
        }

        //save the authentication headers in an object
        //potential we could want to re-use the client elsewhere
        //and we don't want default headers on it
        //this way we can attach them to the request object every time
        private readonly AuthenticationHeaderValue _authenticationHeader;
        protected AuthenticationHeaderValue AuthenticationHeader
        {
            get
            {
                return _authenticationHeader;
            }
        }

        //pass in the httpClient that we make outside of this class
        //will have the requried header information on it for us
        public TaxJarCalculator(HttpClient client)
        {
            this._client = client;
            this._authenticationHeader = new AuthenticationHeaderValue("Bearer", apiToken);
        }

        //use a get with the zip in the url to get the tax rate
        //for simplicity reasons, I am just going to send in the zip for now
        //coding for the optional params wouldn't be too tough, but for now
        //I will leave it simple since taxjar prefers that the "taxes" endpoint be used
        //odd it even has the city/state option when this GET in particular
        //requires the zip to be there anyway.
        async Task<ResponseBase> ITaxCalculator.GetTaxRate(RequestBase rate)
        {
            HttpResponseMessage response;
            string responseData;

            try
            {
                //a lot of this is reusable if it wasn't for the in-line zip
                //could potentially send in the entire url as an argument to one function
                //that would require us to cast just to get the zip argument to make the url
                //then once we are in the send we have to cast the object again to send
                //i want to avoid casting that often just to get one variable
                //if there were more api requets like this in the future,
                //would definitely warrant a refactor                
                var inputData = rate as RateRequest;

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(baseUrl + "rates/" + inputData.Zip),
                    Method = HttpMethod.Get
                };

                request.Headers.Authorization = AuthenticationHeader;//set the authorization header on every request

                response = await Client.SendAsync(request);

                responseData = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<RateResponse>(responseData);
            }
            catch(Exception e)
            {
                //In theory if this happens we have bigger problems to worry about
                //api being offline could be a way this is an issue
                //any error messages will be handled once we return back through the service and to the webapi
                //that originally called the service function
                return new RateResponse() { ExceptionMessage = e.Message };
            }
        }

        //use a post to send in the shipping information to get a total tax cost
        //API requires the emeption_type to be there as well even though the api does not say it
        async Task<ResponseBase> ITaxCalculator.CalculateTaxes(RequestBase tax)
        {
            HttpResponseMessage response;
            string responseData;

            try
            {
                var inputData = tax as TaxRequest;

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(baseUrl + "taxes/"),
                    Method = HttpMethod.Post,
                    Content = new StringContent(JsonConvert.SerializeObject(inputData), Encoding.UTF8, "application/json")
                };

                request.Headers.Authorization = AuthenticationHeader;//setting the authorization header every time....dont like doing it here.

                response = await Client.SendAsync(request);

                responseData = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TaxResponse>(responseData);
            }
            catch (Exception e)
            {
                //In theory if this happens we have bigger problems to worry about
                //similar to comment above.
                return new TaxResponse() { ExceptionMessage = e.Message };
            }
        }
    }
}
