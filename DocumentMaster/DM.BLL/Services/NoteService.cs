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
    public class NoteService
    {
        private readonly IMapper? _mapper;
        private readonly IDbContextFactory<DMContext>? _contextFactory;

        public NoteService(IMapper? mapper, IDbContextFactory<DMContext>? contextFactory)
        {
            _mapper = mapper;
            _contextFactory = contextFactory;
        }
        public async Task<IEnumerable<NoteDTO>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var quests = await context.Notes.Include(p => p.Persons)
                .Include(n => n.Question).ToListAsync();
            var result = _mapper.Map<IEnumerable<NoteDTO>>(quests);
            return result;
        }
        public async Task<NoteDTO> GetItemByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var item = await context.Notes.Where(c => c.Id == id).Include(p => p.Persons)
                .Include(n => n.Question).SingleAsync();
            if (item == null)
            {
                return null;
            }
            var result = _mapper.Map<NoteDTO>(item);
            return result;
        }
        public async Task<IEnumerable<NoteDTO>> GetItemsByQuestionIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var quests = await context.Notes.Include(n => n.Question).Where(n=>n.QuestionId==id)
                .Include(p => p.Persons)
                .ToListAsync();
            var result = _mapper.Map<IEnumerable<NoteDTO>>(quests);
            return result;
        }
        public async Task<NoteDTO> AddItemAsync(NoteDTO noteDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            Note note = new Note
            {
                UserName = noteDTO.UserName,
                Content = noteDTO.Content,
                DateTime = noteDTO.DateTime,
                QuestionId=noteDTO.QuestionId,
                HasFile = noteDTO.HasFile,
                Path = noteDTO.Path,
                Persons = await context.Persons.Where(p => noteDTO.PersonIds.Contains(p.Id)).ToListAsync(),               
            };
            await context.Notes.AddAsync(note);
            await context.SaveChangesAsync();
            return await GetItemByIdAsync(note.Id);
        }
        public async Task UpdateItemAsync(NoteDTO noteDTO)
        {
            using var context = _contextFactory.CreateDbContext();

            var element =await context.Notes.Where(p => p.Id == noteDTO.Id).Include(a => a.Persons)
                .SingleAsync();
            if (element is null)
            {
                element = new Note();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Persons = await context.Persons.Where(p => noteDTO.PersonIds.Contains(p.Id)).ToListAsync();
            context.Notes.Update(element);

            await context.SaveChangesAsync();

        }

    }
}
