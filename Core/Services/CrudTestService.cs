using Core.Interfaces;
using Core.Response;
using Storage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CrudTestService : ICrudTestService
    {
        readonly ITestRepository _testRepository;

        public CrudTestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<ServiceResponse> GetTests()
        {
            IEnumerable<Test> tests = await _testRepository.GetTests();
            return ServiceResponse<IEnumerable<Test>>.Success(tests);
        }

        public async Task<ServiceResponse> CreateTest(string value)
        {
            Test createdTest = await _testRepository.CreateTest(value);
            return ServiceResponse<Test>.Success(createdTest);
        }
    }
}
