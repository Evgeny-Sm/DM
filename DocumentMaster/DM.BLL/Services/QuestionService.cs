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
    public class QuestionService
    {
        private readonly IMapper? _mapper;
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        public QuestionService(IMapper? mapper, IDbContextFactory<DMContext>? contextFactory)
        {
            _mapper = mapper;
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<QuestionDTO>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var quests = await context.Questions.Where(p => p.IsDeleted == false).Include(p => p.Persons)
                .Include(n=>n.Notes).ToListAsync();
            var result = _mapper.Map<IEnumerable<QuestionDTO>>(quests);
            return result;
        }

        public async Task<QuestionDTO> GetItemByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var item = await context.Questions.Where(c => c.Id == id)
                .Include(p => p.Persons)
                .Include(n => n.Notes).SingleAsync();
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
                LinkedFile = questionDTO.LinkedFile,
                CreatorId = questionDTO.CreatorId,
                IsDeleted = questionDTO.IsDeleted,
                Persons = await context.Persons.Where(p => questionDTO.PersonIds.Contains(p.Id)).ToListAsync()
            };
            await context.Questions.AddAsync(question);
            await context.SaveChangesAsync();
            return await GetItemByIdAsync(question.Id);
        }


    }
}
