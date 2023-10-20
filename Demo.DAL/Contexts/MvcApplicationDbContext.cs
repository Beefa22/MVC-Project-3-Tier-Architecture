using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MvcApplicationDbContext:IdentityDbContext<ApplicationUser>
    {

        public MvcApplicationDbContext(DbContextOptions<MvcApplicationDbContext> options) : base(options)
        {
                
        }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Server = .; Database= MvcProjectDb;Trusted_Connection = true");

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }

  
}

