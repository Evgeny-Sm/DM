using AutoMapper;
using DM.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DM.BLL.Services
{
    public class QuestionService
    {
        private readonly IMapper? _mapper;
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        private readonly IConfiguration _configuration;
        public QuestionService(IMapper? mapper, IDbContextFactory<DMContext>? contextFactory, IConfiguration configuration)
        {
            _mapper = mapper;
            _contextFactory = contextFactory;
            _configuration = configuration;
        }

        public async Task<IEnumerable<QuestionDTO>> GetAllAsync(int persId)
        {
            using var context = _contextFactory.CreateDbContext();
            var quests = await context.Questions.Where(p => p.IsDeleted == false).Include(p => p.Persons)
                .Include(n => n.Notes).ThenInclude(p => p.Persons)
                .Include(f => f.FileUnits)
                .Include(p => p.Project).ToListAsync();
            var result = _mapper.Map<IEnumerable<QuestionDTO>>(quests);
            foreach (var question in result)
            {
                var countUnread = quests.Where(q=>q.Id==question.Id).Single()
                    .Notes.Select(n => n.Persons.Select(p => p.Id)).Where(t => !t.Contains(persId)).Count();
                question.UnreadedCount= countUnread;

                var item = context.QuestionsToDo
                    .Where(c => c.QuestionId == question.Id && c.PersonId == persId && c.IsDoing).Count();
                if (item > 0)
                {
                    var notesToDo = await context.NotesToDo.Include(n => n.Note)
                        .Where(n => n.Note != null && n.Note.QuestionId == question.Id && n.PersonId == persId && !n.IsDoing)
                        .ToListAsync();
                    question.ToDoCount = notesToDo.Count;
                }
                else
                {
                    question.ToDoCount = 0;
                }
            }

            return result;
        }

        public async Task<QuestionDTO> GetItemByIdAsync(int id, int persId)
        {
            using var context = _contextFactory.CreateDbContext();
            var checkItem = await context.Questions.FindAsync(id);
            if (checkItem == null)
            {
                return null;
            }
            var item = await context.Questions.Where(c => c.Id == id)
                .Include(p => p.Persons)
                .Include(n => n.Notes).ThenInclude(p => p.Persons)
                .Include(f => f.FileUnits)
                .Include(p => p.Project).SingleAsync();

            var result = _mapper.Map<QuestionDTO>(item);

            var countUnread = item.Notes.Select(n => n.Persons.Select(p => p.Id)).Where(t => !t.Contains(persId)).Count();
            result.UnreadedCount = countUnread;

            var QuestToDo = context.QuestionsToDo
                    .Where(c => c.QuestionId == item.Id && c.PersonId == persId && c.IsDoing).Count();
            if (QuestToDo > 0)
            {
                var notesToDo = await context.NotesToDo.Include(n => n.Note)
                    .Where(n => n.Note != null && n.Note.QuestionId == item.Id && n.PersonId == persId && !n.IsDoing)
                    .ToListAsync();
                result.ToDoCount = notesToDo.Count;
            }
            else
            {
                result.ToDoCount = 0;
            }


            return result;
        }
        public async Task<string> CheckItemByNameContainsStringAsync(string fileNumber)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                var item = await context.Questions.Where(c => c.Title.Contains(fileNumber) && c.IsDeleted == false)
                .FirstOrDefaultAsync();
                if (item == null)
                {
                    return null;
                }
                return item.Id.ToString();
            }
            catch
            {
                return null;
            }
        }
        public async Task<int> GetCountUnreaded(int personId, int questId)
        {
            using var context = _contextFactory.CreateDbContext();
            var item = await context.Questions.Where(c => c.Id == questId)
                .Include(n => n.Notes).ThenInclude(p => p.Persons)
                .SingleAsync();
            var count = item.Notes.Select(n => n.Persons.Select(p => p.Id)).Where(t => !t.Contains(personId)).Count();
            return count;
        }

        public async Task<int> GetCountUnreaded(int personId)
        {
            int countUnreaded = 0;
            using var context = _contextFactory.CreateDbContext();
            try
            {
                var item = await context.Questions.Where(q=>q.IsDeleted==false && q.Persons.Select(p=>p.Id).Contains(personId))
                    .Include(n => n.Notes).ThenInclude(p => p.Persons)
                    .ToListAsync();

                foreach (var q in item)
                {
                    var count = q.Notes.Select(n => n.Persons.Select(p => p.Id)).Where(t => !t.Contains(personId)).Count();
                    countUnreaded += count;
                }

                return countUnreaded;
            }
            catch
            {
                return 0;
            };
          
        }
        public async Task<int> GetCountToDo(int personId, int questId)
        {
            using var context = _contextFactory.CreateDbContext();
            var item = await context.QuestionsToDo.Where(c => c.QuestionId == questId && c.PersonId==personId && c.IsDoing).ToListAsync();
            if (item.Count!=0)
            {
                var notesToDo = await context.NotesToDo.Where(n=>n.Note != null && n.Note.QuestionId==item.FirstOrDefault().QuestionId && n.PersonId==personId && !n.IsDoing).ToListAsync();
                if (notesToDo.Count!=0)
                {
                    return notesToDo.Count;
                }
                return 0;
            }          
            return 0;
        }
        public async Task ChangeToDoStatusAsync(bool currentStatus, int questId, int personId)
        {
            using var context = _contextFactory.CreateDbContext();
            var item = await context.QuestionsToDo.Where(c => c.QuestionId == questId && c.PersonId == personId).ToListAsync();
            if (item.Count!=0)
            {
                if (item.FirstOrDefault().IsDoing == currentStatus)
                { 
                    return;
                }

                item.FirstOrDefault().IsDoing = currentStatus;
                context.QuestionsToDo.Update(item.FirstOrDefault());
                await context.SaveChangesAsync();
            }
            else
            {
                QuestionsToDo questionsToDo = new QuestionsToDo
                {
                    QuestionId = questId,
                    PersonId = personId,
                    IsDoing = currentStatus
                };
                await context.QuestionsToDo.AddAsync(questionsToDo);
                await context.SaveChangesAsync();
            }

            //
            var notes = await context.Notes.Where(n => n.QuestionId == questId).Include(t => t.NoteToDos).ToListAsync();
            if (notes.Count != 0)
            {
                List<NoteToDo> notesToDo = new List<NoteToDo>();
                foreach (var note in notes)
                {
                    if (note.NoteToDos.Where(n => n.PersonId == personId).ToList().Count == 0)
                    {
                        NoteToDo ntD = new NoteToDo
                        {
                            PersonId = personId,
                            NoteId = note.Id,
                            IsDoing = false
                        };
                        notesToDo.Add(ntD);
                    }
                }
                await context.NotesToDo.AddRangeAsync(notesToDo);
                await context.SaveChangesAsync();
            }

        }

        private async Task CreateNoteToDOsAsync(int questId, int personId)
        {
            using var context = _contextFactory.CreateDbContext();
            var notes = await context.Notes.Where(n => n.QuestionId == questId).Include(t => t.NoteToDos).ToListAsync();
            if (notes.Count != 0)
            {
                List<NoteToDo> notesToDo = new List<NoteToDo>();
                foreach (var note in notes)
                {
                    if (note.NoteToDos.Where(n => n.PersonId == personId).ToList().Count == 0)
                    {
                        NoteToDo ntD = new NoteToDo
                        {
                            PersonId = personId,
                            NoteId = note.Id,
                            IsDoing = false
                        };
                        notesToDo.Add(ntD);
                    }
                }
                await context.NotesToDo.AddRangeAsync(notesToDo);
                await context.SaveChangesAsync();

            }
            
        }

        public async Task<bool> IsQuestionDoing(int questId, int personId)
        {
            using var context = _contextFactory.CreateDbContext();
            var item = context.QuestionsToDo.Where(c => c.QuestionId == questId && c.PersonId == personId).ToList();
            if (item.Count!=0)
            {
                return item.FirstOrDefault().IsDoing;
            }
            else
            {
                return false;
            }

        }


        public async Task<QuestionDTO> AddItemAsync(QuestionDTO questionDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            Question question = new Question
            {
                Title = questionDTO.Title,
                DateTime = DateTime.Now,
                CreatorId = questionDTO.CreatorId,
                IsDeleted = questionDTO.IsDeleted,
                ProjectId = questionDTO.ProjectId,
                Persons = await context.Persons.Where(p => questionDTO.PersonIds.Contains(p.Id)).ToListAsync(),
                FileUnits = await context.FileUnits.Where(p => questionDTO.FileUnitsId.Contains(p.Id)).ToListAsync(),
            };
            await context.Questions.AddAsync(question);
            await context.SaveChangesAsync();
            string path = $"wwwroot/{_configuration.GetRequiredSection("PathToQuestPictures").Value}";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string subPath = $"{question.Id}";
            if (!Directory.Exists($"{path}/{subPath}"))
            {
                Directory.CreateDirectory($"{path}/{subPath}");
            }
            var result = _mapper.Map<QuestionDTO>(question);
            return result;
        }
        public async Task UpdateItemAsync(QuestionDTO questionDTO)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = await context.Questions.Where(p => p.Id == questionDTO.Id).Include(a => a.Persons)
                .Include(a => a.FileUnits).SingleAsync();
            if (element is null)
            {
                element = new Question();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Title = questionDTO.Title;
            element.IsDeleted = questionDTO.IsDeleted;

            element.Persons = await context.Persons.Where(p => questionDTO.PersonIds.Contains(p.Id)).ToListAsync();
            element.FileUnits = await context.FileUnits.Where(p => questionDTO.FileUnitsId.Contains(p.Id)).ToListAsync();

            context.Questions.Update(element);

            await context.SaveChangesAsync();
        }

        public async Task HideItem(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = context.Questions.Find(id);
            if (element != null)
            {
                element.IsDeleted = true;
                context.Questions.Update(element);
                await context.SaveChangesAsync();
            }
        }
        public async Task RemoveItem(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = context.Questions.Find(id);
            if (element != null)
            {
                context.Questions.Remove(element);
                await context.SaveChangesAsync();
            }
        }



    }
}
