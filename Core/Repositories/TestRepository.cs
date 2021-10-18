using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    class TestRepository : BaseRepository<Test>, ITestRepository
    {

        public TestRepository(ApiDbContext context) : base(context) { }

        public async Task<IEnumerable<Test>> GetTests()
        {
            var reviews = await _context.TestModels.ToListAsync();
            return reviews;
        }

        public async Task<IEnumerable<Test>> GetTest(int? testId)
        {
            var reviews = await FindByConditions(t => t.Id == testId);
            return reviews;
        }

        public async Task<Test> CreateTest(string value)
        {
            Test testRequest = new Test() { TestValue = value };
            await Create(testRequest);
            return testRequest;
        }

        public async Task<IEnumerable<Test>> DeleteTest(int testId)
        {
            IEnumerable<Test> testModel = await FindByConditions( t => t.Id == testId);
            await Delete(testModel.First());
            return testModel;
        }
    }
}
