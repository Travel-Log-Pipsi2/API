using Core.Response;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFriendshipService
    {
        public Task<ServiceResponse> GetFriends();
        public Task<ServiceResponse> GetInvites();
        public Task<ServiceResponse> SendRequest(Guid toId);
        public Task<ServiceResponse> AcceptFriend(int requestId);
        public Task<ServiceResponse> DeleteInvitation(int requestId);
        public Task<ServiceResponse> DeleteFriend(Guid friendId);
        public Task<ServiceResponse> ReadRequest(int requestId);
    }
}
