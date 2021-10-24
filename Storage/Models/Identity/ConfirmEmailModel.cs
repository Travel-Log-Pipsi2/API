using System.ComponentModel.DataAnnotations;

namespace Storage.Models.Identity
{
    class ConfirmEmailModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
