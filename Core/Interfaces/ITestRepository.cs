
using Storage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITestRepository
    {
        public Task<IEnumerable<Test>> GetTests();
        public Task<IEnumerable<Test>> GetTest(int? testId);
        public Task<Test> CreateTest(string value);
        public Task<IEnumerable<Test>> DeleteTest(int testId);
    }
}
