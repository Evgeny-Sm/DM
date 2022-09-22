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
                .Include(n=>n.Notes)
                .Include(f => f.FileUnits).ToListAsync();
            var result = _mapper.Map<IEnumerable<QuestionDTO>>(quests);
            return result;
        }

        public async Task<QuestionDTO> GetItemByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var item = await context.Questions.Where(c => c.Id == id)
                .Include(p => p.Persons)
                .Include(n => n.Notes)
                .Include(f => f.FileUnits).SingleAsync();
            if (item == null)
            {
                return null;
            }
            var result = _mapper.Map<QuestionDTO>(item);
            return result;
        }

        public async Task<QuestionDTO> AddItemAsync(QuestionDTO questionDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            Question question = new Question
            {
                Title = questionDTO.Title,
                DateTime= DateTime.Now,              
                CreatorId = questionDTO.CreatorId,
                IsDeleted = questionDTO.IsDeleted,
                ProjectId = questionDTO.ProjectId,
                Persons = await context.Persons.Where(p => questionDTO.PersonIds.Contains(p.Id)).ToListAsync(),
                FileUnits=await context.FileUnits.Where(p => questionDTO.FileUnitsId.Contains(p.Id)).ToListAsync(),
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


    }
}
