using AutoMapper;
using DM.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DM.BLL.Services
{
    public class PersonService
    {
        private readonly IMapper? _mapper;
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        public PersonService(IMapper mapper, IDbContextFactory<DMContext>? contextFactory)
        {
            _mapper = mapper;
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<PersonDTO>> GetPersonsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var persons = await context.Persons.Include(p=>p.Account).ToListAsync();
            var result = _mapper.Map<IEnumerable<PersonDTO>>(persons);
            return result;
        }
        public async Task<PersonDTO> GetPersonByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var person = await context.Persons.FindAsync(id);
            if (person == null)
            {
                return null;
            }
            var result = _mapper.Map<PersonDTO>(person);
            return result;
        }
        public async Task<PersonDTO> GetPersonByNameAsync(string name)
        {
            using var context = _contextFactory.CreateDbContext();
            var person = await context.Persons.Include(a => a.Account).Where(p => p.Account.UserName == name).SingleAsync();
            if (person == null)
            {
                return null;
            }
            var result = _mapper.Map<PersonDTO>(person);
            return result;
        }
        public async Task<PersonDTO> AddPersonAsync(PersonDTO personDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            Person person = new Person { FirstName = personDTO.FirstName, 
                LastName = personDTO.LastName, 
                DepartmentId = personDTO.DepartmentId, 
                PositionId = personDTO.PositionId, 
                IsDeleted = false,
                TelegramContact=personDTO.TelegramContact,
                SalaryPerH=personDTO.SalaryPerH,
                IsConfirmed=personDTO.IsConfirmed,
            };
            await context.Persons.AddAsync(person);
            await context.SaveChangesAsync();
            return await GetPersonByIdAsync(person.Id);
        }
        public async Task UpdatePersonAsync(PersonDTO personDTO)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = context.Persons.Where(p=>p.Id==personDTO.Id).Include(a=>a.Account).Single();
            if (element is null)
            {
                element = new Person();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.FirstName = personDTO.FirstName;
            element.LastName = personDTO.LastName;
            element.DepartmentId = personDTO.DepartmentId;
            element.PositionId = personDTO.PositionId;
            element.IsDeleted = personDTO.IsDeleted;
            element.TelegramContact = personDTO.TelegramContact;
            element.SalaryPerH = personDTO.SalaryPerH;
            element.IsConfirmed = personDTO.IsConfirmed;
            element.Account.Role = personDTO.Role;


            context.Persons.Update(element);
            context.Accounts.Update(element.Account);


            await context.SaveChangesAsync();

        }
        public async Task RemovePerson(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = context.Persons.Find(id);
            if (element != null)
            {
                context.Persons.Remove(element);
                await context.SaveChangesAsync();
            }
        }
        public void Dispose()
        {

        }
    }
}
