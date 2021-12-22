using CodeTest.Services.dll;
using System.Threading.Tasks;
using Xunit;

namespace CodeTest.UnitTests.Services
{
    public class TaxServiceUnitTests
    {
        [Fact]
        public void GetTaxRate_SendCalculator_ReturnCombinedRate()
        {
            ITaxCalculator calculator = new MockCalculator();
            ITaxService service = new TaxService(calculator);

            RateResponse response = service.GetTaxRate(new RequestBase()).Result as RateResponse;

            Assert.True(response.TaxRate.CombinedRate == 10.0);
        }

        [Fact]
        public void GetTaxRate_SendMockErrorCalculator_ReturnStatusCode()
        {
            ITaxCalculator calculator = new MockErrorCalculator();
            ITaxService service = new TaxService(calculator);

            RateResponse response = service.GetTaxRate(new RequestBase()).Result as RateResponse;

            Assert.True(response.StatusCode == 400);
        }

        [Fact]
        public void GetTaxRate_SendMockExceptionCalculator_ReturnExceptionMessage()
        {
            ITaxCalculator calculator = new MockExceptoinCalculator();
            ITaxService service = new TaxService(calculator);

            RateResponse response = service.GetTaxRate(new RequestBase()).Result as RateResponse;

            Assert.True(response.ExceptionMessage == "GetTaxRateException: Null value or something");
        }

        [Fact]
        public void GetCalculateTaxes_SendCalculator_ReturnCombinedRate()
        {
            ITaxCalculator calculator = new MockCalculator();
            ITaxService service = new TaxService(calculator);

            TaxResponse response = service.CalculateTaxes(new RequestBase()).Result as TaxResponse;

            Assert.True(response.TaxData.AmountToCollect == 15.0);
        }

        [Fact]
        public void GetCalculateTaxes_SendCalculator_ReturnStatusCode()
        {
            ITaxCalculator calculator = new MockErrorCalculator();
            ITaxService service = new TaxService(calculator);

            TaxResponse response = service.CalculateTaxes(new RequestBase()).Result as TaxResponse;

            Assert.True(response.StatusCode == 400);
        }

        [Fact]
        public void GetCalculateTaxes_SendCalculator_ReturnExceptionMessage()
        {
            ITaxCalculator calculator = new MockExceptoinCalculator();
            ITaxService service = new TaxService(calculator);

            TaxResponse response = service.CalculateTaxes(new RequestBase()).Result as TaxResponse;

            Assert.True(response.ExceptionMessage == "CalculateTaxesException: Null value or something");
        }
    }

    class MockCalculator : ITaxCalculator
    {
        public async Task<ResponseBase> CalculateTaxes(RequestBase input)
        {
            TaxResponse response = new TaxResponse();

            response.TaxData.AmountToCollect = 15.0;

            return response;
        }

        public async Task<ResponseBase> GetTaxRate(RequestBase input)
        {
            RateResponse response = new RateResponse();

            response.TaxRate.CombinedRate = 10.0;

            return response;
        }
    }

    class MockErrorCalculator : ITaxCalculator
    {
        public async Task<ResponseBase> CalculateTaxes(RequestBase input)
        {
            TaxResponse response = new TaxResponse
            {
                StatusCode = 400,
                Error = "ERROR",
                ErrorDetail = "ERROR: Testing the return from calculator to service"
            };

            return response;
        }

        public async Task<ResponseBase> GetTaxRate(RequestBase input)
        {
            RateResponse response = new RateResponse
            {
                StatusCode = 400,
                Error = "ERROR",
                ErrorDetail = "ERROR: Testing the return from calculator to service"
            };

            return response;
        }
    }

    class MockExceptoinCalculator : ITaxCalculator
    {
        public async Task<ResponseBase> CalculateTaxes(RequestBase input)
        {
            TaxResponse response = new TaxResponse
            {
                ExceptionMessage = "CalculateTaxesException: Null value or something"
            };

            return response;
        }

        public async Task<ResponseBase> GetTaxRate(RequestBase input)
        {
            RateResponse response = new RateResponse
            {
                ExceptionMessage = "GetTaxRateException: Null value or something"
            };

            return response;
        }
    }
}
