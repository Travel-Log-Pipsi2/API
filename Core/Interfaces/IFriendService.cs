using Core.Response;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFriendService
    {
        public Task<ServiceResponse> GetFriendsList();
        public Task<ServiceResponse> GetInvitesList();
        public Task<ServiceResponse> SendRequest(Guid toId);
        public Task<ServiceResponse> AcceptFriend(int requestId);
        public Task<ServiceResponse> DeleteRequest(int requestId);
        public Task<ServiceResponse> DeleteFriend(Guid friendId);
    }
}
