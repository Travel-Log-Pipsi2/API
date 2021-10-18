using Storage.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("test_models")]
    public class Test : IDbModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TestValue { get; set; }
    }
}
