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
            using (var context = new DMContext(serviceProvider.GetRequiredService<DbContextOptions<DMContext>>()))
            {
                if (context.Projects.Any())
                {
                    return;
                }
                context.Persons.AddRange(
                    new Person
                    {
                        Login = "user@user.com",
                        Password = "1111",
                        Role = "admin"
                    }
                    );
                context.Departments.AddRange(
                    new Department
                    {
                        Name = "CO"
                    });
                context.SaveChanges();

                context.Worker.AddRange(
                    new Worker
                    {
                        FirstName = "Евгений",
                        LastName = "Смирнов",
                        DepartmentId = 1,
                        PersonId = 1
                    });
                context.SaveChanges();

                context.Projects.AddRange(
               new Project
               {
                   Name = "Волна",
                   Description = "проект реконструкции",
                   Client = "ЛСР",
                   WorkerId=1


               });
                context.SaveChanges();

                context.FileUnits.AddRange(
                    new FileUnit
                    {
                        Name = "План демонтажа",
                        Description = "Описание",
                        PathFile = AppDomain.CurrentDomain.BaseDirectory,
                        DepartmentId=1,
                        ProjectId=1,
                        WorkerCreatorId=1


                    });


                context.SaveChanges();
                

            }
        }
        
    }
}
