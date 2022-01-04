using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("map_markers")]
    public class Marker : IDbModel
    {
        [Key]
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Country { get; set; }
        [Required]
        public float Longitude { get; set; }
        [Required]
        public float Latitude { get; set; }
        [Required]
        public Guid UserID { get; set; }
        public List<Travel> Travels { get; set; }


    }
}
