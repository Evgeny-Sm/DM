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
        public DbSet<Worker>? Workers { get; set; }
        public DbSet<WorkerAction>? WorkerActions { get; set; }

    }
}
