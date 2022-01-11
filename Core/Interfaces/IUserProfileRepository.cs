using Core.DTOs;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserProfileRepository
    {
        public Task<UserDTO> GetInfo();
        public Task<UserDTO> GetInfo(Guid userId);
        public Task<StatsDTO> GetStats();
        public Task<StatsDTO> GetStats(Guid userId);

    }
}
