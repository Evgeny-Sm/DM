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
            /*
            using (var context1 = new DMContext(serviceProvider.GetRequiredService<DbContextOptions<DMContext>>()))
            {
                if (context1.Persons.Any())
                {
                    return;
                }
                
                context1.Departments.AddRange(
                    new Department
                    {
                        Name = "A",
                        Description = "Управление"
                    });
                context1.SaveChanges();

                context1.Positions.AddRange(
                    new Position
                    {
                        Name = "Администратор",
                        
                    });
                context1.SaveChanges();
                context1.Persons.AddRange(
                    new Person
                    {

                        FirstName = "Евгений",
                        LastName = "Смирнов",
                        DepartmentId = 1,
                        PositionId = 1,
                        IsConfirmed = true,

                    });
                context1.SaveChanges();

                context1.Accounts.AddRange(
                   new Account
                   {
                       UserName = "user@user.com",
                       //1111
                       Password = "33275A8AA48EA918BD53A9181AA975F15AB0D0645398F5918A006D08675C1CB27D5C645DBD084EEE56E675E25BA4019F2ECEA37CA9E2995B49FCB12C096A032E",
                       Role = "admin",
                       PersonId = 1
                   });
                context1.SaveChanges();
                context1.Dispose();
                
            }
         */   
        }
        
    }
}
