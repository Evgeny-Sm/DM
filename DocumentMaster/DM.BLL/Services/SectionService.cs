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
    public class SectionService
    {
        private readonly DMContext? _db;
        private readonly IMapper? _mapper;
        public SectionService(DMContext? db, IMapper? mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SectionDTO>> GetAllAsync()
        {
            var element = await _db.Sections.ToListAsync();
            var result = _mapper.Map<IEnumerable<SectionDTO>>(element);
            return result;
        }

        public async Task<SectionDTO> GetItemByIdAsync(int id)
        {
            var element = await _db.Sections.FindAsync(id);
            if (element == null)
            {
                return null;
            }
            var result = _mapper.Map<SectionDTO>(element);
            return result;
        }

        public async Task<SectionDTO> AddItemAsync(SectionDTO sectionDTO)
        {
            Section section = new Section
            {
                Name = sectionDTO.Name,
                Description = sectionDTO.Description
            };
            await _db.Sections.AddAsync(section);
            await _db.SaveChangesAsync();
            return await GetItemByIdAsync(section.Id);
        }

        public async Task UpdateItemAsync(SectionDTO sectionDTO)
        {
            var s = _db.Sections.Find(sectionDTO.Id);
            if (s is null)
            {
                s = new Section();
                throw new ArgumentNullException($"Unknown {s.GetType().Name}");
            }
            s.Name = sectionDTO.Name;
            s.Description = sectionDTO.Description;
            _db.Sections.Update(s);
            await _db.SaveChangesAsync();
                
        }
        public async Task RemoveItem(int id)
        {
            var element = _db.Sections.Find(id);
            if (element != null)
            {
                _db.Sections.Remove(element);
                await _db.SaveChangesAsync();
            }

        }


    }
}
