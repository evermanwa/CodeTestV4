using System.Threading.Tasks;

namespace CodeTest.Services.dll
{

    public class TaxService : ITaxService
    {
        private readonly ITaxCalculator _calculator;
        private ITaxCalculator Calculator
        {
            get
            {
                return _calculator;
            }
        }

        //pass in all of our dependencies here
        public TaxService(ITaxCalculator calculator)
        {
            _calculator = calculator;
        }

        //pass in a RequestBase object for now because we are not
        //sure which calculator is going to be in this case and we don't care
        //let the calculator deal with dealing with what the object is
        //return ResponseBase which will hold any exception or error that happens
        public async Task<ResponseBase> GetTaxRate(RequestBase data)
        {
            return await Calculator.GetTaxRate(data);
        }

        //pass in a RequestBase object for now because we are not
        //sure which calculator is going to be in this case and we don't care
        //let the calculator deal with dealing with that the object is
        //return ResponseBase which will hold any exception or error that happens
        public async Task<ResponseBase> CalculateTaxes(RequestBase data)
        {
            return await Calculator.CalculateTaxes(data);
        }
    }
}
