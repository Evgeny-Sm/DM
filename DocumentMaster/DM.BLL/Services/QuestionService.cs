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

        public async Task<IEnumerable<QuestionDTO>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var quests = await context.Questions.Where(p => p.IsDeleted == false).Include(p => p.Persons)
                .Include(n => n.Notes).ThenInclude(p => p.Persons)
                .Include(f => f.FileUnits)
                .Include(p => p.Project).ToListAsync();
            var result = _mapper.Map<IEnumerable<QuestionDTO>>(quests);
            return result;
        }

        public async Task<QuestionDTO> GetItemByIdAsync(int id)
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
            var item = await context.QuestionsToDo.Where(c => c.QuestinId == questId && c.PersonId==personId && c.IsDoing).SingleAsync();
            if (item is not null)
            {
                var notesToDo = await context.NotesToDo.Where(n=>n.Note != null && n.Note.QuestionId==item.QuestinId && n.PersonId==personId && n.IsDoing).ToListAsync();
                if (notesToDo is not null)
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
            var item = await context.QuestionsToDo.Where(c => c.QuestinId == questId && c.PersonId == personId).SingleAsync();
            if (item is not null)
            {
                item.IsDoing = currentStatus;
                context.QuestionsToDo.Update(item);
                await context.SaveChangesAsync();
                return;
            }
            else
            {
                QuestionsToDo questionsToDo = new QuestionsToDo
                {
                    QuestinId = questId,
                    PersonId = personId,
                    IsDoing = currentStatus
                };
                await context.QuestionsToDo.AddAsync(questionsToDo);
                await context.SaveChangesAsync();            
            }
        }

        public async Task<bool> IsQuestionDoing(int questId, int personId)
        {
            using var context = _contextFactory.CreateDbContext();
            var item = await context.QuestionsToDo.Where(c => c.QuestinId == questId && c.PersonId == personId).SingleAsync();
            if (item is not null)
            {
                return item.IsDoing;
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
            return await GetItemByIdAsync(question.Id);
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
