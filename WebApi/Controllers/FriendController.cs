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
        readonly IFriendService _friendService;

        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpGet]
        [Route("Friends")]
        public Task<ServiceResponse> GetFriends()
        {
            return _friendService.GetFriendsList();
        }

        [HttpGet]
        [Route("Invites")]
        public Task<ServiceResponse> GetFriendInvites()
        {
            return _friendService.GetInvitesList();
        }

        [HttpPost]
        [Route("Invite/{toId}")]
        public Task<ServiceResponse> SendInvite(Guid toId)
        {
            return _friendService.SendRequest(toId);
        }

        [HttpPut]
        [Route("Accept/{requestId}")]
        public Task<ServiceResponse> AcceptFriendRequest(int requestId)
        {
            return _friendService.AcceptFriend(requestId);
        }

        [HttpDelete]
        [Route("Delete-request/{requestId}")]
        public Task<ServiceResponse> DeleteFriendRequest(int requestId)
        {
            return _friendService.DeleteRequest(requestId);
        }

        [HttpDelete]
        [Route("Delete-friend/{friendId}")]
        public Task<ServiceResponse> DeleteFriend(Guid friendId)
        {
            return _friendService.DeleteFriend(friendId);
        }
    }
}
