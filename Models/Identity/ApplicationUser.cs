using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Econoterm.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
        }
    }
}
