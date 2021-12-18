using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models.Identity
{
    [Table("Users")]
    public class User : IdentityUser<Guid>
    {
        //public List<Marker> Markers { get; set; }

        //public User()
        //{
        //    Markers = new();
        //}
    }
}
