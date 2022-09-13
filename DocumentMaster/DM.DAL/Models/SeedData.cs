using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public static class SeedData
    {
        
        public static void Initialize(IServiceProvider serviceProvider)
        {
            
            using (var context1 = new DMContext(serviceProvider.GetRequiredService<DbContextOptions<DMContext>>()))
            {
                if (context1.Persons.Any())
                {
                    return;
                }
                
                context1.Departments.AddRange(
                    new Department
                    {
                        Name = "СО",
                        Description = "Строительный отдел"
                    });
                context1.SaveChanges();

                context1.Positions.AddRange(
                    new Position
                    {
                        Name = "Начальник отдела",
                        
                    });
                context1.SaveChanges();
                context1.Persons.AddRange(
                    new Person
                    {
                        FirstName = "Евгений",
                        LastName = "Смирнов",
                        DepartmentId = 1,
                        PositionId = 1,

                    });
                context1.SaveChanges();

                context1.Accounts.AddRange(
                   new Account
                   {
                       UserName = "smirnov@eska.pro",
                       Password = "1111",
                       Role = "admin",
                       PersonId = 1
                   });
                context1.SaveChanges();
                context1.Dispose();
                
            }
            
        }
        
    }
}
