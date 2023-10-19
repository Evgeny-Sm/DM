using AutoMapper;
using DM.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DM.BLL.Services
{
    public class PositionService
    {
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        private readonly IMapper? _mapper;
        public PositionService(IDbContextFactory<DMContext>? contextFactory, IMapper mapper)
        { 
            _mapper = mapper;
            _contextFactory = contextFactory;
        }
        public async Task<IEnumerable<PositionDTO>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var elements = await context.Positions.ToListAsync();
            var result = _mapper.Map<IEnumerable<PositionDTO>>(elements);
            return result;
        }

        public async Task<PositionDTO> GetPositiontByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.Positions.FindAsync(id);
            if (element == null)
            {
                return null;
            }
            var result = _mapper.Map<PositionDTO>(element);
            return result;
        }

        public async Task<PositionDTO> AddPositionAsync(PositionDTO positionDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            Position position = new Position
            {
                Name = positionDTO.Name
            };
            await context.Positions.AddAsync(position);
            await context.SaveChangesAsync();
            var result = _mapper.Map<PositionDTO>(position);
            return result;
        }

        public async Task UpdateAsync(PositionDTO positionDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = context.Positions.Find(positionDTO.Id);
            if (element is null)
            {
                element = new Position();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Name = positionDTO.Name;
            context.Positions.Update(element);
            await context.SaveChangesAsync();

        }
        public async Task<bool> Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.Positions.FindAsync(id);
            if (element != null)
            {
                context.Positions.Remove(element);
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
