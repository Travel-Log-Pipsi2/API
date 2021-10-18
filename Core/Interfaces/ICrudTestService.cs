using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICrudTestService
    {
        public Task<ServiceResponse> GetTests();
        public Task<ServiceResponse> CreateTest(string value);
    }
}
