using Core.Interfaces;
using Core.Response;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FriendshipService : IFriendshipService
    {
        readonly IFriendshipRepository _friendshipRepository;

        public FriendshipService(IFriendshipRepository friendRepository)
        {
            _friendshipRepository = friendRepository;
        }

        public async Task<ServiceResponse> GetFriends()
        {
            var friends = await _friendshipRepository.GetFriendships(true);

            return ServiceResponse<IEnumerable<Friendship>>.Success(friends, "Friendships retrieved");
        }

        public async Task<ServiceResponse> GetInvites()
        {
            var friends = await _friendshipRepository.GetFriendships(false);

            return ServiceResponse<IEnumerable<Friendship>>.Success(friends, "Friendships invites retrieved");
        }

        public async Task<ServiceResponse> SendRequest(Guid toId)
        {
            await _friendshipRepository.CreateFriendship(toId);

            return ServiceResponse.Success("Send friend initation");
        }

        public async Task<ServiceResponse> AcceptFriend(int requestId)
        {
            var request = await _friendshipRepository.GetFriendship(requestId);
            request.IsAccepted = true;
            await _friendshipRepository.SaveFriendship(request);

            return ServiceResponse.Success("Friend accepted");
        }

        public async Task<ServiceResponse> DeleteFriend(Guid friendId)
        {
            var deleted = await _friendshipRepository.DeleteFrienshipByFriend(friendId);
            if (deleted)
                return ServiceResponse.Success("Friend deleted");
            return ServiceResponse.Error("Error deleting friend");
        }

        public async Task<ServiceResponse> DeleteRequest(int requestId)
        {
            var deleted = await _friendshipRepository.DeleteFrienshipByInvitation(requestId);
            if (deleted)
                return ServiceResponse.Success("Invitation deleted");
            return ServiceResponse.Error("Error deleting friend invitation");
        }

        public async Task<ServiceResponse> ReadRequest(int requestId)
        {
            var request = await _friendshipRepository.GetFriendship(requestId);
            request.Notification = false;
            await _friendshipRepository.SaveFriendship(request);

            return ServiceResponse.Success("Notificated");

        }
    }
}
