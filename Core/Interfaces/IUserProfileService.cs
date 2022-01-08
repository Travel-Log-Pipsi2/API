using Core.Response;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserProfileService
    {
        public Task<ServiceResponse> GetInfo();
        public Task<ServiceResponse> GetInfo(Guid userId);
        public Task<ServiceResponse> GetStats();
        public Task<ServiceResponse> GetStats(Guid userId);

    }
}
