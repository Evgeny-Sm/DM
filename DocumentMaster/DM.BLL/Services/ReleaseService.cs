using AutoMapper;
using DM.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.BLL.Services
{
    public class ReleaseService
    {
        private readonly IMapper? _mapper;
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        private readonly IConfiguration _configuration;
        public ReleaseService(IMapper? mapper, IDbContextFactory<DMContext>? contextFactory, IConfiguration configuration)
        {
            _mapper = mapper;
            _contextFactory = contextFactory;
            _configuration = configuration;
        }
        public async Task<IEnumerable<ReleaseDTO>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var releases = await context.Releases.Include(p => p.FileUnits).Include(t=>t.Creator).ToListAsync();
            var result = _mapper.Map<IEnumerable<ReleaseDTO>>(releases);
            return result;
        }

        public async Task<ReleaseDTO> GetItemByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var item = await context.Releases.Where(c => c.Id == id)
                .Include(p => p.FileUnits).Include(t => t.Creator).SingleAsync();
            if (item == null)
            {
                return null;
            }
            var result = _mapper.Map<ReleaseDTO>(item);
            return result;
        }
        public async Task<IEnumerable<ReleaseDTO>> GetItemByProjIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var items = await context.Releases.Where(c => c.ProjectId == id)
                .Include(p => p.FileUnits).Include(t => t.Creator)
                .Include(k => k.Project).ToListAsync();

            var result = _mapper.Map<IEnumerable<ReleaseDTO>>(items); ;
            return result;
        }
        public async Task<ReleaseDTO> AddItemAsync(ReleaseDTO relDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            ICollection<FileUnit> files=new List<FileUnit>();

            foreach (var f in relDTO.FilesIds)
            {
                files.Add(context.FileUnits.Find(f));
            }

            Release release = new Release
            {
                ProjectCode = relDTO.ProjectCode,
                PersonId = relDTO.PersonId,
                Description = relDTO.Description,
                ProjectId = relDTO.ProjectId,
                IsLocked=false,
                IsRemoved=false,
                FileUnits=  files
            };
            await context.Releases.AddAsync(release);
            await context.SaveChangesAsync();
            return await GetItemByIdAsync(release.Id);
        }

        public async Task UpdateItemAsync(ReleaseDTO relDTO)
        {
            using var context = _contextFactory.CreateDbContext();

            ICollection<FileUnit> files = new List<FileUnit>();
            foreach (var f in relDTO.FilesIds)
            {
                files.Add(context.FileUnits.Find(f));
            }

            var element = await context.Releases.Where(p => p.Id == relDTO.Id).Include(a => a.FileUnits)
                .Include(t => t.Creator)
                .SingleAsync();
            if (element is null)
            {
                element = new Release();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.ProjectCode = relDTO.ProjectCode;
            element.IsLocked = relDTO.IsLocked;
            

            element.FileUnits = files;
            context.Releases.Update(element);

            await context.SaveChangesAsync();
        }
        public async Task LockUnlockItem(ReleaseDTO relDTO)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = context.Releases.Find(relDTO.Id);
            if (element is null)
            {
                element = new Release();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.IsLocked = relDTO.IsLocked;
            element.CreateDate = DateTime.Now;

            context.Releases.Update(element);
            await context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = context.Releases.Find(id);
            if (element != null)
            {
                context.Releases.Remove(element);
                await context.SaveChangesAsync();
            }
        }

    }
}
