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
    public class NotifyService
    {
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        private readonly IMapper? _mapper;
        public NotifyService(IDbContextFactory<DMContext>? contextFactory, IMapper? mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<NotifyCountDTO> GetNotifyCountByIdAsync(int id)
        {
            NotifyCountDTO notifyCountDTO = new();
            using var context = _contextFactory.CreateDbContext();
            try
            {
                notifyCountDTO.ToCheckCount = context.Controls.Where(c => c.PersonId == id && c.IsConfirmed == false && c.IsInAction == true).Count();

            }
            catch (NullReferenceException)
            {
                notifyCountDTO.ToCheckCount = 0;
            }

            try
            {
                var item = await context.Questions.Where(q => q.IsDeleted == false && q.Persons.Select(p => p.Id).Contains(id))
                    .Include(n => n.Notes).ThenInclude(p => p.Persons)
                    .ToListAsync();

                foreach (var q in item)
                {
                    notifyCountDTO.UnreadedCount += q.Notes.Select(n => n.Persons.Select(p => p.Id)).Where(t => !t.Contains(id)).Count();
                }
            }
            catch
            {
                notifyCountDTO.UnreadedCount=0;
            };

            try
            {
                var curQuestIdList = await context.QuestionsToDo.Where(c => c.PersonId == id && c.IsDoing).Select(q=>q.QuestionId).ToListAsync();

                notifyCountDTO.ToDoCount = context.NotesToDo.Where(n => n.Note != null && curQuestIdList.Contains(n.Note.QuestionId) && n.PersonId == id && !n.IsDoing).Count();

            }
            catch
            {
                notifyCountDTO.ToDoCount = 0;
            }
            return notifyCountDTO;

        }
    }
}
