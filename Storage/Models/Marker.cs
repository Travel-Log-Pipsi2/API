using Storage.Interfaces;
using System;
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
        public string Description { get; set; }
        //[Required]
        public float Longitude { get; set; }
        //[Required]
        public float Latitude { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        //[Required]
        public Guid UserID { get; set; }


    }
}
