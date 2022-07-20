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
        private readonly DMContext _db;
        private readonly IMapper _mapper;
        public PersonService(DMContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public ClaimsIdentity GetIdentity(string username, string password)
        {
            Person person = _db.Persons.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token",
                                                    ClaimsIdentity.DefaultNameClaimType,
                                                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
        public async Task<IEnumerable<PersonDTO>> GetPersonsAsync()
        {
            var persons = await _db.Persons.Include(p=>p.UserProfile).ToListAsync();
            var result = _mapper.Map<IEnumerable<PersonDTO>>(persons);
            return result;
        }
        public async Task<PersonDTO> GetPersonByIdAsync(int id)
        {
            var person = await _db.Persons.FindAsync(id);
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
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
