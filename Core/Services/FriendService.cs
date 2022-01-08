using Core.Interfaces;
using Core.Response;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FriendService : IFriendService
    {
        public Task<ServiceResponse> AcceptFriend(int requestId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteFriend(Guid friendId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteRequest(int requestId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> GetFriendsList()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> GetInvitesList()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> SendRequest(Guid toId)
        {
            throw new NotImplementedException();
        }
    }
}
