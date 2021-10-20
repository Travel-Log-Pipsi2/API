using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models.Identity
{
    [Table("Users")]
    public class User : IdentityUser<Guid>
    {

    }
}
