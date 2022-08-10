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
                        Name = "CO",
                        Description = "Строительный отдел"
                    });
                context1.SaveChanges();

                context1.Positions.AddRange(
                    new Position
                    {
                        Name = "Начальнике",
                        
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
                       UserName = "user@user.com",
                       Password = "1111",
                       Role = "admin",
                       PersonId = 1
                   });
                context1.SaveChanges();
                context1.Persons.AddRange(
                new Person
                {
                    FirstName = "Раб",
                    LastName = "Рабонский",
                    DepartmentId = 1,
                    PositionId = 1,

                });
                context1.SaveChanges();

               
                context1.Accounts.AddRange(
                new Account
                {
                    UserName = "rab@user.com",
                    Password = "11111",
                    Role = "user",
                    PersonId = 2
                });
                context1.SaveChanges();

                context1.Projects.AddRange(
               new Project
               {
                   Name = "Волна",
                   Description = "проект реконструкции",
                   Client = "ЛСР",
                   PersonId=1
               });
                context1.SaveChanges();

                context1.FileUnits.AddRange(
                    new FileUnit
                    {
                        Name = "План демонтажа",
                        Description = "Описание",
                        PathFile = AppDomain.CurrentDomain.BaseDirectory,
                        DepartmentId=1,
                        ProjectId=1,
                    });
                context1.SaveChanges();

                context1.UserActions.AddRange(
                    new UserAction
                    {
                        FileUnitId = 1,
                        PersonId = 1,
                        ActionNumber = 1
                    });
                context1.SaveChanges();
            }
        }
        
    }
}
