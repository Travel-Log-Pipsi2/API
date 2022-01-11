using System;

namespace Core.DTOs
{
    public class FriendInvitationDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public bool Notification { get; set; }
    }
}