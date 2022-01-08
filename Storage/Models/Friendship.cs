using Storage.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("frienships")]
    public class Friendship : IDbModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid FromFriend { get; set; }
        [Required]
        public Guid ToFriend { get; set; }

        public bool Notification { get; set; }

        public bool Is { get; set; }
    }
}
