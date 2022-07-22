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
    public class PositionService
    {
        private readonly DMContext? _db;
        private readonly IMapper? _mapper;
        public PositionService(DMContext context, IMapper mapper)
        { 
            _db = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PositionDTO>> GetAllAsync()
        {
            var elements = await _db.Positions.ToListAsync();
            var result = _mapper.Map<IEnumerable<PositionDTO>>(elements);
            return result;
        }

        public async Task<PositionDTO> GetPositiontByIdAsync(int id)
        {
            var element = await _db.Positions.FindAsync(id);
            if (element == null)
            {
                return null;
            }
            var result = _mapper.Map<PositionDTO>(element);
            return result;
        }

        public async Task<PositionDTO> AddPositionAsync(PositionDTO positionDTO)
        {
            Position position = new Position
            {
                Name = positionDTO.Name
            };
            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
            return await GetPositiontByIdAsync(position.Id);
        }

        public async Task UpdateAsync(int id, PositionDTO positionDTO)
        {
            var element = _db.Positions.FirstOrDefault(c => c.Id == id);
            if (element is null)
            {
                element = new Position();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Id = id;
            element.Name = positionDTO.Name;
            _db.Positions.Update(element);
            await _db.SaveChangesAsync();

        }
        public async Task<bool> Delete(int id)
        {
            var element = await _db.Positions.FindAsync(id);
            if (element != null)
            {
                _db.Positions.Remove(element);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
