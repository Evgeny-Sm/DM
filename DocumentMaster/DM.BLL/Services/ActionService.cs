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
    public class ActionService
    {
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        private readonly IMapper? _mapper;
        public ActionService(IDbContextFactory<DMContext>? contextFactory, IMapper mapper)
        {
            _contextFactory= contextFactory;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserActionDTO>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.UserActions.Include(f => f.Person).Include(t => t.FileUnit).ToListAsync();
            var result = _mapper.Map<IEnumerable<UserActionDTO>>(element);
            return result;
        }
        public async Task<int> GetCountActionsOnCheckAsync(int personId)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.UserActions.Where(u=>u.ActionNumber > 1&&u.PersonId==personId).ToListAsync();
            var result = element.Count;
            return result;
        }

        public async Task<UserActionDTO> GetActionByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            UserAction element = await context.UserActions.Where(e=>e.Id==id).Include(f=>f.Person).Include(t=>t.FileUnit).SingleAsync();
            if (element == null )
            {
                return null;
            }
            var result = _mapper.Map<UserActionDTO>(element);
            return result;
        }
        public async Task<IEnumerable<UserActionDTO>> GetAllActionsByNumberAsync(int actionNumber)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.UserActions.Where(a=>a.ActionNumber==actionNumber).Include(f => f.Person).Include(t => t.FileUnit).ToListAsync();
            var result = _mapper.Map<IEnumerable<UserActionDTO>>(element);
            return result;
        }
        public async Task<IEnumerable<UserActionDTO>> GetAllActionsByFileIdAsync(int fileId)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.UserActions.Where(a => a.FileUnitId == fileId).Include(f => f.Person).Include(t => t.FileUnit).ToListAsync();
            var result = _mapper.Map<IEnumerable<UserActionDTO>>(element);
            return result;
        }
        public async Task<IEnumerable<UserActionDTO>> GetAllActionsOnDateAsync(DateTime? dateFrom,DateTime? dateTo)
        {
            using var context = _contextFactory.CreateDbContext();
            if (dateFrom == null)
            {
                dateFrom = new DateTime(2000, 1, 1);
            }
            if (dateTo == null)
            {
                dateTo = new DateTime(2200, 1, 1);
            }

            var element = await context.UserActions.Where(d => d.CreatedDate > dateFrom && d.CreatedDate < dateTo).Include(f => f.Person).Include(t => t.FileUnit).ToListAsync();
            var result = _mapper.Map<IEnumerable<UserActionDTO>>(element);
            return result;
        }
        public async Task<UserActionDTO> AddAction(UserActionDTO actionDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            UserAction userAction = new UserAction()
            {
                FileUnitId=actionDTO.FileUnitId,
                PersonId=actionDTO.PersonId,
                ActionNumber= actionDTO.ActionNumber,
                TimeForAction=actionDTO.TimeForAction,
                CreatedDate=actionDTO.CreatedDate,
                IsConfirmed= actionDTO.IsConfirmed               
            };
            await context.UserActions.AddAsync(userAction);
            await context.SaveChangesAsync();
            return await GetActionByIdAsync(userAction.Id);

        }
        public async Task<bool> Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            UserAction element = await context.UserActions.FindAsync(id);
            if (element != null)
            {
                context.UserActions.Remove(element);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public void Dispose()
        {
            
        }





    }
}
