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
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        private readonly IMapper? _mapper;
        public SectionService(IDbContextFactory<DMContext>? contextFactory, IMapper? mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SectionDTO>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.Sections.ToListAsync();
            var result = _mapper.Map<IEnumerable<SectionDTO>>(element);
            return result;
        }

        public async Task<SectionDTO> GetItemByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.Sections.FindAsync(id);
            if (element == null)
            {
                return null;
            }
            var result = _mapper.Map<SectionDTO>(element);
            return result;
        }

        public async Task<SectionDTO> AddItemAsync(SectionDTO sectionDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            Section section = new Section
            {
                Name = sectionDTO.Name,
                Description = sectionDTO.Description
            };
            await context.Sections.AddAsync(section);
            await context.SaveChangesAsync();
            var result = _mapper.Map<SectionDTO>(section);
            return result;
        }

        public async Task UpdateItemAsync(SectionDTO sectionDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            var s = context.Sections.Find(sectionDTO.Id);
            if (s is null)
            {
                s = new Section();
                throw new ArgumentNullException($"Unknown {s.GetType().Name}");
            }
            s.Name = sectionDTO.Name;
            s.Description = sectionDTO.Description;
            context.Sections.Update(s);
            await context.SaveChangesAsync();
                
        }
        public async Task RemoveItem(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = context.Sections.Find(id);
            if (element != null)
            {
                context.Sections.Remove(element);
                await context.SaveChangesAsync();
            }

        }


    }
}
