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
        public async Task<IEnumerable<NoteDTO>> GetItemsByQuestionIdForUserIdAsync(int questId, int userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var quests = await context.Notes.Where(n=>n.QuestionId==questId)
                .Include(p => p.Persons).Include(nt=>nt.NoteToDos.Where(ntd=>ntd.PersonId==userId))
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
            SetNotesToDo(note.QuestionId, note.Id);
            return _mapper.Map<NoteDTO>(note); ;
        }

        private async Task SetNotesToDo(int questId,int noteId)
        {
            using var context = _contextFactory.CreateDbContext();
            var persons = await context.QuestionsToDo.Where(q => q.QuestionId == questId && q.IsDoing)
                .Select(q=>q.PersonId)
                .ToListAsync();
            if (persons.Count > 0)
            {
                foreach (var p in persons)
                {
                    try
                    {               
                        NoteToDo ntD = new NoteToDo
                        {
                            PersonId = p,
                            NoteId = noteId,
                            IsDoing = false
                        };
                        await context.NotesToDo.AddAsync(ntD);
                        await context.SaveChangesAsync();
                    }
                    catch
                    {
                        return;
                    }
                }
            }

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
        public async Task UpdateNoteStateAsync(bool currstate,int noteid, int userid)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var item = await context.Notes.Where(c => c.Id == noteid).Include(p => p.NoteToDos.Where(nt => nt.PersonId == userid))
                    .SingleAsync();
                item.NoteToDos.Single().IsDoing = currstate;
                context.NotesToDo.Update(item.NoteToDos.Single());
                await context.SaveChangesAsync();
            }
            catch
            {
                return;
            }

        }

        public async Task AddToDoNoteAsync(int noteId, int personId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                NoteToDo ntD = new NoteToDo
                {
                    PersonId = personId,
                    NoteId = noteId,
                    IsDoing = false
                };
                await context.NotesToDo.AddAsync(ntD);
                await context.SaveChangesAsync();
            }
            catch
            {
                return;
            }
        }

        public async Task RemoveItemAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = context.Notes.Find(id);
            if (element != null)
            {
                context.Notes.Remove(element);
                await context.SaveChangesAsync();
            }
        }

    }
}
