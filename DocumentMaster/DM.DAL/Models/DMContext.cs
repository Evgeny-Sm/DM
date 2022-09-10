using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class DMContext:DbContext
    {
        public DMContext(DbContextOptions<DMContext> options) : base(options)
        { 
        }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<FileUnit>? FileUnits { get; set; }
        public DbSet<Person>? Persons { get; set; }
        public DbSet<Project>? Projects { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<UserAction>? UserActions { get; set; }
        public DbSet<Position>? Positions { get; set; }
        public DbSet<Section>? Sections { get; set; }
        public DbSet<Control>? Controls { get; set; }
        public DbSet<Challenge>? Challenges { get; set; }

    }
}
