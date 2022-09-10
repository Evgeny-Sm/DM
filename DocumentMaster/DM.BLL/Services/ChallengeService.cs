using AutoMapper;
using DM.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.BLL.Services
{
    public class ChallengeService
    {
        private readonly IMapper? _mapper;
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        public ChallengeService(IMapper mapper, IDbContextFactory<DMContext>? contextFactory)
        {
            _mapper = mapper;
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<ChallengeDTO>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var challenges = await context.Challenges.Include(p => p.Persons).ToListAsync();
            var result = _mapper.Map<IEnumerable<ChallengeDTO>>(challenges);
            return result;
        }
        public async Task<IEnumerable<ChallengeDTO>> GetAllFofUserAsync(int userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var challenges = await context.Challenges.Include(p => p.Persons).Where(p=>p.Persons.Select(s=>s.Id).Contains(userId)).ToListAsync();
            var result = _mapper.Map<IEnumerable<ChallengeDTO>>(challenges);
            return result;
        }
        public async Task<ChallengeDTO> GetItemByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var item = await context.Challenges.Where(c=>c.Id==id).Include(p=>p.Persons).SingleAsync();
            if (item == null)
            {
                return null;
            }
            var result = _mapper.Map<ChallengeDTO>(item);
            return result;
        }

        public async Task<ChallengeDTO> AddChallengeAsync(ChallengeDTO challengeDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            Challenge chlg = new Challenge
            {
                Name = challengeDTO.Name,
                Description = challengeDTO.Description,
                Start = challengeDTO.Start,
                End = challengeDTO.End,
                Persons =await context.Persons.Where(p => challengeDTO.PersonIds.Contains(p.Id)).ToListAsync()
            };
            await context.Challenges.AddAsync(chlg);
            await context.SaveChangesAsync();
            return await GetItemByIdAsync(chlg.Id);
        }
        public async Task UpdateItemAsync(ChallengeDTO challengeDTO)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = context.Challenges.Where(p => p.Id == challengeDTO.Id).Include(a => a.Persons).Single();
            if (element is null)
            {
                element = new Challenge();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Name = challengeDTO.Name;
            element.Description = challengeDTO.Description;
            element.Start = challengeDTO.Start;
            element.End = challengeDTO.End;
            element.Persons = await context.Persons.Where(p => challengeDTO.PersonIds.Contains(p.Id)).ToListAsync();

            context.Challenges.Update(element);       

            await context.SaveChangesAsync();
        }

        public async Task RemoveItem(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = context.Challenges.Find(id);
            if (element != null)
            {
                context.Challenges.Remove(element);
                await context.SaveChangesAsync();
            }
        }
    }
}
