using System.Threading.Tasks;

namespace CodeTest.Services.dll
{
    public interface ITaxCalculator
    {
        //methods for communicating with an api
        //takes a baseClass so we can send in any custom
        //objects and work on the body and heavy-lifting
        //within the implemented method
        //will return a response base that has commonality error/exception handling
        public Task<ResponseBase> GetTaxRate(RequestBase input);
        public Task<ResponseBase> CalculateTaxes(RequestBase input);
    }
}
