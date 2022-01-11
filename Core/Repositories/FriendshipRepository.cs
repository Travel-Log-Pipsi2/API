using Core.Interfaces;
using Core.Interfaces.Authentication;
using Storage.DataAccessLayer;
using Storage.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Repositories
{
    public class FriendshipRepository : BaseRepository<Friendship>, IFriendshipRepository
    {

        readonly ILoggedUserProvider _loggedUserProvider;

        public FriendshipRepository(ApiDbContext context, ILoggedUserProvider loggedUserProvider) : base(context)
        {
            _loggedUserProvider = loggedUserProvider;
        }

        public async Task<IEnumerable<Friendship>> GetFriendships(bool accepted)
        {
            var loggedId = _loggedUserProvider.GetUserId();
            if (accepted)
                return await _context.Friendships.Where(f => f.IsAccepted && f.FromFriend == loggedId || f.ToFriend == loggedId).ToListAsync();
            return await _context.Friendships.Where(f => f.IsAccepted == accepted && f.ToFriend == loggedId).ToListAsync();
        }

        public async Task<bool> DeleteFrienshipByFriend(Guid friendId)
        {
            var loggedId = _loggedUserProvider.GetUserId();

            var frienship = await _context.Friendships.Where(f => f.IsAccepted && (f.FromFriend == loggedId && f.ToFriend == friendId) || f.ToFriend == loggedId && f.FromFriend == friendId).FirstOrDefaultAsync();
            if (frienship == null)
                return false;

            await Delete(frienship);
            return true;
        }

        public async Task<Friendship> GetFriendship(int requestId)
        {
            var loggedId = _loggedUserProvider.GetUserId();

            var frienship = await _context.Friendships.Where(f => f.Id == requestId && f.FromFriend == loggedId || f.ToFriend == loggedId).FirstOrDefaultAsync();
            return frienship;
        }

        public async Task<bool> CreateFriendship(Guid toId)
        {
            var loggedId = _loggedUserProvider.GetUserId();

            if (loggedId == toId)
                return false;

            var friendship = await _context.Friendships.Where(f => (f.FromFriend == loggedId && f.ToFriend == toId) || f.ToFriend == loggedId && f.FromFriend == toId).FirstOrDefaultAsync();
            if (friendship != null)
                return false;

            Friendship newFriendship = new() {
                FromFriend = loggedId,
                ToFriend = toId,
                IsAccepted = false,
                Notification = true
            };
            await Create(newFriendship);

            return true;
        }

        public async Task<bool> DeleteFrienshipByInvitation(int requestId)
        {
            var loggedId = _loggedUserProvider.GetUserId();

            var frienship = await _context.Friendships.Where(f => f.IsAccepted == false && f.Id == requestId && f.ToFriend == loggedId || f.FromFriend == loggedId).FirstOrDefaultAsync();
            if (frienship == null)
                return false;

            await Delete(frienship);
            return true;
        }

        public async Task SaveFriendship(Friendship frienship)
        {
            await Edit(frienship);
        }

    }
}
