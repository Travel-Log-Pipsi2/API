using Storage.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("connections")]
    public class Connection : IDbModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string ProvierId { get; set; }
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public DateTime lastConnection { get; set; }
    }
}
