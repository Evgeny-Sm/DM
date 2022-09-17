using AutoMapper;
using DM.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DM.BLL.Services
{
    public class ProjectService
    {
        private readonly IMapper? _mapper;
        private readonly IDbContextFactory<DMContext> _contextFactory;
        private readonly IConfiguration _configuration;
        public ProjectService(IMapper mapper, IDbContextFactory<DMContext> contextFactory, IConfiguration configuration)
        {
            _mapper = mapper;
            _contextFactory = contextFactory;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsListAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.Projects.Where(d => d.IsDeleted == false).ToListAsync();
            var result = _mapper.Map<IEnumerable<ProjectDTO>>(element);
            return result;
        }

        public async Task<ProjectDTO> GetProjectByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            Project element = await context.Projects.Where(p=>p.Id==id).Include(f=>f.FileUnits).SingleAsync();
            if (element == null)
            {
                return null;
            }
            var result = _mapper.Map<ProjectDTO>(element);
            return result;
        }

        public async Task<ProjectDTO> AddProjectAsync(ProjectDTO unit)
        {
            using var context = _contextFactory.CreateDbContext();
            Project element = new Project
            {
                Name = unit.Name,
                Description = unit.Description,
                Client=unit.Client,
                PersonId=unit.MainIngId,
                IsDeleted=unit.IsDeleted,
            };
            await context.Projects.AddAsync(element);
            await context.SaveChangesAsync();

            string path = $"wwwroot/{_configuration.GetConnectionString("ProjectRootDir")}";
            if (!Directory.Exists(path))
            { 
                Directory.CreateDirectory(path);
            }        
            string subPath = $"{element.Id}";
            if (!Directory.Exists($"{path}/{subPath}"))
            {
                Directory.CreateDirectory($"{path}/{subPath}");
            }

            return await GetProjectByIdAsync(element.Id);
        }

        public async Task UpdateProjectAsync(ProjectDTO unit)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = context.Projects.FirstOrDefault(c => c.Id == unit.Id);
            if (element is null)
            {
                element = new Project();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Name = unit.Name;
            element.Description = unit.Description;
            element.IsDeleted = unit.IsDeleted;
            element.Client = unit.Client;
            element.PersonId = unit.MainIngId;
            context.Projects.Update(element);
            await context.SaveChangesAsync();

        }
        public async Task<bool> RemoveProject(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            Project element = await context.Projects.FindAsync(id);
            if (element != null)
            {
                context.Projects.Remove(element);
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
