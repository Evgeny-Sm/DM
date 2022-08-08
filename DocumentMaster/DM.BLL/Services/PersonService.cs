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
        private readonly DMContext? _db;
        private readonly IMapper? _mapper;
        public PersonService(DMContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDTO>> GetPersonsAsync()
        {
            var persons = await _db.Persons.Include(p=>p.UserProfile).Where(p=>p.IsDeleted==false).ToListAsync();
            var result = _mapper.Map<IEnumerable<PersonDTO>>(persons);
            return result;
        }
        public async Task<PersonDTO> GetPersonByIdAsync(int id)
        {
            
            var person =await _db.Persons.Include(p => p.UserProfile).Where(p => p.IsDeleted == false).Where(p=>p.Id==id).FirstOrDefaultAsync();
            if (person == null)
            {
                return null;
            }
            var result = _mapper.Map<PersonDTO>(person);
            return result;
        }
        public async Task<PersonDTO> AddPersonAdminAsync(string username, string password)
        {
            Person person = new Person
            {
                Login = username,
                Password = password,
                Role = "admin"
            };
            await _db.Persons.AddAsync(person);
            await _db.SaveChangesAsync();
            return await GetPersonByIdAsync(person.Id);
        }
        public async Task<PersonDTO> AddPersonUserAsync(string username, string password)
        {
            Person person = new Person
            {
                Login = username,
                Password = password,
                Role = "user"
            };
            await _db.Persons.AddAsync(person);
            await _db.SaveChangesAsync();
            return await GetPersonByIdAsync(person.Id);
        }
        public async Task UpdatePersonAsync(int id, Person person)
        {
            var element = _db.Persons.Find(id);
            if (element is null)
            {
                element = new Person();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Login = person.Login;
            element.Password = person.Password;
            _db.Persons.Update(person);
            await _db.SaveChangesAsync();
        }
        public async Task RemoveAccount(int id)
        {
            var element = _db.Persons.Find(id);
            if (element != null)
            {
                _db.Persons.Remove(element);
                await _db.SaveChangesAsync();
            }

        }
        public Task<string> GetRole(string name)
        {
            var role = _db.Persons.Where(p => p.Login == name).Single().Role;
            return Task.FromResult(role);
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
