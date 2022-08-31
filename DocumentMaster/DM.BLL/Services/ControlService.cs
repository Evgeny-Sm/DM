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
        public async Task<int> GetCountControlsByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.Controls.Where(c=>c.PersonId==id && c.IsConfirmed==false).ToListAsync();
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
            var element = await context.Controls.Where(c => c.PersonId == personId && c.IsConfirmed == false).ToListAsync();
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
            context.Controls.Update(element);
            await context.SaveChangesAsync();

        }

        public async Task RemoveControlAsynk(int id)
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
