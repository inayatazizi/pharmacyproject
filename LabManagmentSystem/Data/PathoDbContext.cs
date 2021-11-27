using LabManagmentSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabManagmentSystem.ViewModels;

namespace LabManagmentSystem.Data
{
    public class PathoDbContext: IdentityDbContext<PathoUser>
    {
        public PathoDbContext(DbContextOptions<PathoDbContext>options): base(options)
        {
            
        }
        public DbSet<Patient_Table> Patient_Tables { get; set; } 
        public DbSet<Proffessional> Proffessionals { get; set; }
        public DbSet<Test> Tests{ get; set; }
        public DbSet<LabManagmentSystem.ViewModels.RoleStore> RoleStore { get; set; }



    }
}
