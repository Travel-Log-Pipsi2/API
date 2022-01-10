using Core.Interfaces;
using Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        readonly IFriendshipService _friendshipService;

        public FriendController(IFriendshipService friendService)
        {
            _friendshipService = friendService;
        }

        [HttpGet]
        [Route("Friends")]
        public Task<ServiceResponse> GetFriends()
        {
            return _friendshipService.GetFriends();
        }

        [HttpGet]
        [Route("Invites")]
        public Task<ServiceResponse> GetInvites()
        {
            return _friendshipService.GetInvitations();
        }

        [HttpPost]
        [Route("Invite/{toId}")]
        public Task<ServiceResponse> SendInvite(Guid toId)
        {
            return _friendshipService.SendInvitation(toId);
        }

        [HttpPut]
        [Route("Accept/{invitationId}")]
        public Task<ServiceResponse> AcceptFriendInvitation(int invitationId)
        {
            return _friendshipService.AcceptFriend(invitationId);
        }

        [HttpDelete]
        [Route("Delete-invitation/{invitationId}")]
        public Task<ServiceResponse> DeleteFriendInvitation(int invitationId)
        {
            return _friendshipService.DeleteInvitation(invitationId);
        }

        [HttpDelete]
        [Route("Delete-friend/{friendId}")]
        public Task<ServiceResponse> DeleteFriend(Guid friendId)
        {
            return _friendshipService.DeleteFriend(friendId);
        }

        [HttpPut]
        [Route("Read/{requestId}")]
        public Task<ServiceResponse> ReadFriendRequest(int requestId)
        {
            return _friendshipService.ReadRequest(requestId);
        }
    }
}
