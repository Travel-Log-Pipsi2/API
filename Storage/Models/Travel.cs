using Storage.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("travels")]
    public class Travel : IDbModel
    {
        [Key]
        public int Id { get; set; }        
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public int MarkerId { get; set; }

    }
}
