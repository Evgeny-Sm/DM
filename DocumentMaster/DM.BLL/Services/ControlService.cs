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
    public class ControlService
    {
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        private readonly IMapper? _mapper;
        public ControlService(IDbContextFactory<DMContext>? contextFactory, IMapper? mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ControlDTO>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.Controls.Include(f => f.Person).ToListAsync();
            var result = _mapper.Map<IEnumerable<ControlDTO>>(element);
            return result;
        }
        public async Task<IEnumerable<ControlDTO>> GetControlsByFileIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.FileUnits.Where(c => c.Id == id).Include(c => c.Controls).ThenInclude(p=>p.Person).SingleAsync();
            var result = _mapper.Map<IEnumerable<ControlDTO>>(element.Controls);
            return result;
        }
        public async Task<ControlDTO> GetControlByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            Control element = await context.Controls.FindAsync(id);
            if (element == null)
            {
                return null;
            }
            var result = _mapper.Map<ControlDTO>(element);
            return result;
        }
        public async Task<ControlDTO> GetActualControlForPersonAsync(int fileId,int personId)
        {
            using var context = _contextFactory.CreateDbContext();
            Control element = await context.Controls.Where
                (c=>c.FileUnitId==fileId && c.PersonId==personId && c.IsInAction==true).FirstOrDefaultAsync();
            if (element == null)
            {
                return null;
            }
            var result = _mapper.Map<ControlDTO>(element);
            return result;
        }
        public async Task<int> GetCountControlsByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.Controls.Where(c=>c.PersonId==id && c.IsConfirmed==false && c.IsInAction==true).ToListAsync();
            if (element == null)
            {
                return 0;
            }
            var result = element.Count;
            return result;
        }

        public async Task<IEnumerable<ControlDTO>> GetControlsToCheckAsync(int personId)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.Controls.Where(c => c.PersonId == personId && c.IsConfirmed == false && c.IsInAction == true).ToListAsync();
            var result = _mapper.Map<IEnumerable<ControlDTO>>(element);
            return result;
        }

        public async Task<ControlDTO> AddControlAsync(ControlDTO controlDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            Control control = new Control()
            {
                FileUnitId=controlDTO.FileUnitId,
                PersonId=controlDTO.PersonId,
                IsConfirmed=controlDTO.IsConfirmed,
                TimeForChecking=controlDTO.TimeForChecking,
                IsInAction=controlDTO.IsInAction,
                Description=controlDTO.Description,
                DateTime=controlDTO.DateTime,

            };
            await context.Controls.AddAsync(control);
            await context.SaveChangesAsync();
            return await GetControlByIdAsync(control.Id);
        }
        public async Task UpdateControlAsync(ControlDTO controlDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = context.Controls.Find(controlDTO.Id);
            if (element is null)
            {
                element = new Control();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.FileUnitId = controlDTO.FileUnitId;
            element.PersonId = controlDTO.PersonId;
            element.IsConfirmed = controlDTO.IsConfirmed;
            element.TimeForChecking = controlDTO.TimeForChecking;
            element.IsInAction = controlDTO.IsInAction;
            element.Description = controlDTO.Description;
            element.DateTime = controlDTO.DateTime;
            context.Controls.Update(element);
            await context.SaveChangesAsync();

        }

        public async Task RemoveControlAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            Control element = await context.Controls.FindAsync(id);
            if (element != null)
            {
                context.Controls.Remove(element);
                await context.SaveChangesAsync();              
            }
        }


    }
}
