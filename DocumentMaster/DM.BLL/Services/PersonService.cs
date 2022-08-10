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
            var persons = await _db.Persons.Where(p=>p.IsDeleted==false).ToListAsync();
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
        public async Task<PersonDTO> GetPersonByNameAsync(string name)
        {

            var person = await _db.Persons.Include(a=>a.Account).Where(p=>p.Account.UserName==name).SingleAsync();
            if (person == null)
            {
                return null;
            }
            var result = _mapper.Map<PersonDTO>(person);
            return result;
        }
        public async Task<PersonDTO> AddPersonAsync(PersonDTO personDTO)
        {
            Person person = new Person
            {
                FirstName = personDTO.FirstName,
                LastName = personDTO.LastName,
                DepartmentId = personDTO.DepartmentId,
                PositionId = personDTO.PositionId,
                IsDeleted = false

            };
            await _db.Persons.AddAsync(person);
            await _db.SaveChangesAsync();
            return await GetPersonByIdAsync(person.Id);
        }
        public async Task UpdatePersonAsync(int id, PersonDTO personDTO)
        {
            var element = _db.Persons.Find(id);
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

            _db.Persons.Update(element);
            await _db.SaveChangesAsync();
        }
        public async Task RemovePerson(int id)
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
