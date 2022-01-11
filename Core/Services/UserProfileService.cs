using Core.DTOs;
using Core.Interfaces;
using Core.Interfaces.Authentication;
using Core.Response;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserProfileService : IUserProfileService
    {
        readonly IUserProfileRepository _userProfileRepository;
        readonly ILoggedUserProvider _loggedUserProvider;
        readonly IFriendshipService _friendshipService;

        public UserProfileService(IUserProfileRepository userProfileRepository, ILoggedUserProvider loggedUserProvider)
        {
            _userProfileRepository = userProfileRepository;
            _loggedUserProvider = loggedUserProvider;
        }

        public async Task<ServiceResponse> GetInfo()
        {
            var currentId = _loggedUserProvider.GetUserId();
            UserDTO userInfo = await _userProfileRepository.GetInfo();
            return ServiceResponse<UserDTO>.Success(userInfo);
        }

        public async Task<ServiceResponse> GetInfo(Guid userId)
        {
            var currentId = _loggedUserProvider.GetUserId();
            var isFriend = await _friendshipService.IsFriend(currentId);
            if (isFriend)
            {
                UserDTO userInfo = await _userProfileRepository.GetInfo(userId);
                return ServiceResponse<UserDTO>.Success(userInfo);
            }
            return ServiceResponse.Error($"User {userId} is not your friend");
        }

        public async Task<ServiceResponse> GetStats()
        {
            StatsDTO stats = await _userProfileRepository.GetStats();
            return ServiceResponse<StatsDTO>.Success(stats);
        }

        public async Task<ServiceResponse> GetStats(Guid userId)
        {
            StatsDTO stats = await _userProfileRepository.GetStats(userId);
            return ServiceResponse<StatsDTO>.Success(stats);
        }

    }
}
