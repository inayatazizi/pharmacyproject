using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PathoLabSystem.Models;
using PathologyLabManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathologyLabManagment.Data
{
    public class PathoContext : IdentityDbContext<PathoUser>
    {
        public PathoContext(DbContextOptions<PathoContext> options) : base(options)
        {

        }
        public DbSet<PatientSignUp> PatientSignUps { get; set; }
        public DbSet<Proffessional> Proffessionals { get; set; }
        public DbSet<Empolyee> Empolyees { get; set; }
        public DbSet<Test> Tests { get; set; }

    
        
    }
}
