using System;
using Econoterm.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Econoterm.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public ApplicationDbContext()
        {

        }

        public DbSet<ApplicationUser> User
        {
            get;
            set;
        }

        public DbSet<Calculo> Calculos
        {
            get;
            set;
        }
    }
}
