using CodeTest.Services.dll;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RichardSzalay.MockHttp;
using System.Net.Http;
using Xunit;

namespace CodeTest.UnitTests.Services
{
    public class TaxJarCalculatorUnitTests
    {
        [Fact]
        public void GetTaxRate_ZipCode_ReturnsCombinedRate()
        {
            HttpClient client = GenerateMockRateHttpClient();
            RateRequest input = new RateRequest
            {
                Zip = "90404"
            };

            ITaxCalculator calculator = new TaxJarCalculator(client);

            RateResponse response = calculator.GetTaxRate(input).Result as RateResponse;

            Assert.True(response.TaxRate.CombinedRate == 0.0975);
        }

        [Fact]
        public void GetTaxRate_AllData_ReturnsCombinedRate()
        {
            HttpClient client = GenerateMockRateHttpClient();
            RateRequest input = new RateRequest
            {
                Zip = "77539",
                Country = "United States",
                State = "Texas",
                City = "Dickinson",
                Street = "Bayou dr"
            };

            ITaxCalculator calculator = new TaxJarCalculator(client);

            RateResponse response = calculator.GetTaxRate(input).Result as RateResponse;

            Assert.True(response.TaxRate.CombinedRate == 0.0981);
        }

        [Fact]
        public void GetTaxRate_SendNullData_ReturnsHasErrorTrue()
        {
            HttpClient client = GenerateMockRateHttpClient();

            ITaxCalculator calculator = new TaxJarCalculator(client);

            RateResponse response = calculator.GetTaxRate(null).Result as RateResponse;

            Assert.True(response.HasException);
        }

        [Fact]
        public void GetTaxRate_SendNullHttpClient_ReturnsHasErrorTrue()
        {
            ITaxCalculator calculator = new TaxJarCalculator(null);
            RateResponse response = calculator.GetTaxRate(null).Result as RateResponse;
            Assert.True(response.HasException);
        }

        [Fact]
        public void GetTaxRate_SendBlankObject_ReturnsApiError()
        {
            HttpClient client = GenerateMockRateHttpClient();
            RateRequest input = new RateRequest
            {
                Zip = "12345"
            };

            ITaxCalculator calculator = new TaxJarCalculator(client);
            RateResponse response = calculator.GetTaxRate(input).Result as RateResponse;

            Assert.True(response.StatusCode == 401);
        }

        [Fact]
        public void CalculateTaxes_CountryShipping_ReturnMockTotalAmount()
        {
            TaxResponse mockResponse = new TaxResponse();
            mockResponse.TaxData.OrderTotalAmount = 50.99;
            HttpClient client = GenerateMockTaxHttpClient(mockResponse);

            TaxRequest input = new TaxRequest
            {
                Shipping = 1.5,
                FromCountry = "US"
            };

            ITaxCalculator calculator = new TaxJarCalculator(client);
            TaxResponse response = calculator.CalculateTaxes(input).Result as TaxResponse;

            Assert.True(response.TaxData.OrderTotalAmount == 50.99);
        }

        [Fact]
        public void CalculateTaxes_SendBlankObject_ReturnsApiError()
        {
            JObject errorMsg = JObject.Parse("{\"status\":401,\"error\":\"Unauthorized\",\"detail\":\"Not authorized for route 'GET /v2/rates/:zip'\"}");
            HttpClient client = GenerateMockTaxHttpClient(errorMsg);

            ITaxCalculator calculator = new TaxJarCalculator(client);
            TaxResponse response = calculator.CalculateTaxes(new TaxRequest()).Result as TaxResponse;

            Assert.True(response.StatusCode == 401);
        }

        [Fact]
        public void CalculateTaxes_SendNullHttpClient_ReturnsHasErrorTrue()
        {
            ITaxCalculator calculator = new TaxJarCalculator(null) ;
            TaxResponse response = calculator.CalculateTaxes(null).Result as TaxResponse;
            Assert.True(response.HasException);
        }

        #region "Helper Methods"

        private HttpClient GenerateMockRateHttpClient()
        {
            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

            RateResponse response = new RateResponse();
            response.TaxRate.CombinedRate = 0.0975;

            RateResponse response1 = new RateResponse();
            response1.TaxRate.CombinedRate = 0.0981;

            JObject response3 = JObject.Parse("{\"status\":401,\"error\":\"Unauthorized\",\"detail\":\"Not authorized for route 'GET /v2/rates/:zip'\"}") ;

            mockHttp.When("https://api.taxjar.com/v2/rates/90404").Respond("application/json", JsonConvert.SerializeObject(response));
            mockHttp.When("https://api.taxjar.com/v2/rates/77539").Respond("application/json", JsonConvert.SerializeObject(response1));
            mockHttp.When("https://api.taxjar.com/v2/rates/12345").Respond("application/json", JsonConvert.SerializeObject(response3));
            mockHttp.When("https://*").Respond("application/json", "ERROR");
            return new HttpClient(mockHttp);
        }
        
        private HttpClient GenerateMockTaxHttpClient(object data)
        {
            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://api.taxjar.com/v2/taxes/").Respond("application/json", JsonConvert.SerializeObject(data));            
            mockHttp.When("https://*").Respond("application/json", "ERROR");

            return new HttpClient(mockHttp);
        }

        #endregion
    }
}
