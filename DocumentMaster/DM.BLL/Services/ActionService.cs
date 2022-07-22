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
        private readonly DMContext? _db;
        private readonly IMapper? _mapper;
        public ActionService(DMContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserActionDTO>> GetAllAsync()
        {
            var element = await _db.UserActions.Include(f => f.Person).Include(t => t.FileUnit).ToListAsync();
            var result = _mapper.Map<IEnumerable<UserActionDTO>>(element);
            return result;
        }

        public async Task<UserActionDTO> GetActionByIdAsync(int id)
        {
            UserAction element = await _db.UserActions.Where(e=>e.Id==id).Include(f=>f.Person).Include(t=>t.FileUnit).SingleAsync();
            if (element == null )
            {
                return null;
            }
            var result = _mapper.Map<UserActionDTO>(element);
            return result;
        }
        public async Task<IEnumerable<UserActionDTO>> GetAllActionsByNumberAsync(int actionNumber)
        {
            var element = await _db.UserActions.Where(a=>a.ActionNumber==actionNumber).Include(f => f.Person).Include(t => t.FileUnit).ToListAsync();
            var result = _mapper.Map<IEnumerable<UserActionDTO>>(element);
            return result;
        }
        public async Task<IEnumerable<UserActionDTO>> GetAllActionsOnDateAsync(DateTime? dateFrom,DateTime? dateTo)
        {
            if (dateFrom == null)
            {
                dateFrom = new DateTime(2000, 1, 1);
            }
            if (dateTo == null)
            {
                dateTo = new DateTime(2200, 1, 1);
            }

            var element = await _db.UserActions.Where(d => d.CreatedDate > dateFrom && d.CreatedDate < dateTo).Include(f => f.Person).Include(t => t.FileUnit).ToListAsync();
            var result = _mapper.Map<IEnumerable<UserActionDTO>>(element);
            return result;
        }





    }
}
