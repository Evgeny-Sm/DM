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
    public class ProjectService
    {
        private readonly DMContext? _db;
        private readonly IMapper? _mapper;
        public ProjectService(DMContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsListAsync()
        {
            var element = await _db.Projects.Where(d => d.IsDeleted == false).ToListAsync();
            var result = _mapper.Map<IEnumerable<ProjectDTO>>(element);
            return result;
        }

        public async Task<ProjectDTO> GetProjectByIdAsync(int id)
        {
            Project element = await _db.Projects.FindAsync(id);
            if (element == null || element.IsDeleted == true)
            {
                return null;
            }
            var result = _mapper.Map<ProjectDTO>(element);
            return result;
        }

        public async Task<ProjectDTO> AddProjectAsync(ProjectDTO unit)
        {
            Project element = new Project
            {
                Name = unit.Name,
                Description = unit.Description,
                Client=unit.Client,
                PersonId=unit.MainIngId
            };
            await _db.Projects.AddAsync(element);
            await _db.SaveChangesAsync();
            string path = "wwwroot";
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

        public async Task UpdateProjectAsync(int id, ProjectDTO unit)
        {
            var element = _db.Projects.FirstOrDefault(c => c.Id == id);
            if (element is null)
            {
                element = new Project();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Id = id;
            element.Name = unit.Name;
            element.Description = unit.Description;
            element.IsDeleted = unit.IsDeleted;
            element.Client = unit.Client;
            element.PersonId = unit.MainIngId;
            _db.Projects.Update(element);
            await _db.SaveChangesAsync();

        }
        public async Task<bool> DeleteDevice(int id)
        {
            Project element = await _db.Projects.FindAsync(id);
            if (element != null)
            {
                _db.Projects.Remove(element);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        private void CreateFolder(int id)
        {
            string path =$"~/{id}";


        }


        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
