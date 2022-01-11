using Storage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFriendshipRepository
    {
        public Task<Friendship> GetFriendship(int requestId);
        public Task<IEnumerable<Friendship>> GetFriendships(bool accepted);
        public Task<bool> CreateFriendship(Guid toId);
        public Task SaveFriendship(Friendship frienship);
        public Task<bool> DeleteFrienshipByFriend(Guid friendId);
        public Task<bool> DeleteFrienshipByInvitation(int requestId);
    }
}
