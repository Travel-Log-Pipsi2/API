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
        public Task<ServiceResponse> GetFriendInvites()
        {
            return _friendshipService.GetInvites();
        }

        [HttpPost]
        [Route("Invite/{toId}")]
        public Task<ServiceResponse> SendInvite(Guid toId)
        {
            return _friendshipService.SendRequest(toId);
        }

        [HttpPut]
        [Route("Accept/{requestId}")]
        public Task<ServiceResponse> AcceptFriendRequest(int requestId)
        {
            return _friendshipService.AcceptFriend(requestId);
        }

        [HttpDelete]
        [Route("Delete-request/{requestId}")]
        public Task<ServiceResponse> DeleteFriendRequest(int requestId)
        {
            return _friendshipService.DeleteRequest(requestId);
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
