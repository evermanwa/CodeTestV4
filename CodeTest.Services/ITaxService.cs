using System.Threading.Tasks;

namespace CodeTest.Services.dll
{
    public interface ITaxService
    {
        public  Task<ResponseBase> GetTaxRate(RequestBase data);
        public Task<ResponseBase> CalculateTaxes(RequestBase data);
    }
}