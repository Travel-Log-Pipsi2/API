using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class ConfirmEmailRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
