using AutoMapper;
using DM.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.BLL.Services
{
    public class FileService
    {

        private readonly IMapper? _mapper;
        private readonly IDbContextFactory<DMContext>? _contextFactory;

        public FileService(IDbContextFactory<DMContext> contextFactory, IMapper mapper)
        {

            _mapper = mapper;
            _contextFactory = contextFactory;

        }
        public async Task<IEnumerable<FileDTO>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var elements = await context.FileUnits.ToListAsync();
            var result = _mapper.Map<IEnumerable<FileDTO>>(elements);
            return result;

        }
        public async Task<IEnumerable<FileDTO>> GetAllNotDeletedAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var elements = await context.FileUnits.Where(d => d.IsDeleted == false).ToListAsync();
            var result = _mapper.Map<IEnumerable<FileDTO>>(elements);
            return result;

        }

        public async Task<FileDTO> GetItemByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = await context.FileUnits.Where(f=>f.Id==id).SingleAsync();

            if (element == null)
            {
                return null;
            }
            var result = _mapper.Map<FileDTO>(element);
            return result;

        }

        public async Task<FileDTO> AddFileAsync(FileDTO fileDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            FileUnit element = new FileUnit { Name = fileDTO.Name, 
                Description = fileDTO.Description, 
                ProjectId = fileDTO.ProjectId, 
                DepartmentId = fileDTO.DepartmentId,
                PathFile = $"{fileDTO.ProjectId}/{fileDTO.Name}", 
                SectionId = fileDTO.SectionId, 
                NumbersDrawings = fileDTO.NumbersDrawings,          
                Status=fileDTO.Status,
                IsDeleted= fileDTO.IsDeleted,
                PersonId=fileDTO.PersonId,
                TimeToCreate= fileDTO.TimeToCreate
            };
            await context.FileUnits.AddAsync(element);
            await context.SaveChangesAsync();
            return await GetItemByIdAsync(element.Id);
        }

        public async Task<int> UpdateFileAsync(FileDTO fileDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.FileUnits.Where(f => f.Id == fileDTO.Id).SingleAsync();
            if (element is null)
            {
                element = new FileUnit();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Name = fileDTO.Name;
            element.Description = fileDTO.Description;
            element.PathFile = $"{fileDTO.ProjectId}/{fileDTO.Name}";
            element.DepartmentId = fileDTO.DepartmentId;
            element.ProjectId = fileDTO.ProjectId;
            element.IsDeleted = fileDTO.IsDeleted;
            element.NumbersDrawings = fileDTO.NumbersDrawings;
            element.SectionId = fileDTO.SectionId;
            element.Status= fileDTO.Status;
            element.TimeToCreate= fileDTO.TimeToCreate;
            context.FileUnits.Update(element);
            
            return await context.SaveChangesAsync();

        }

        public async Task RemoveFile(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            {
                var element = context.FileUnits.Find(id);
                if (element != null)
                {
                    context.FileUnits.Remove(element);
                    await context.SaveChangesAsync();
                }
            }

        }

        public async Task<string> GetFullFilePathAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = await context.FileUnits.FindAsync(id);
            if (element == null)
            {
                return null;
            }
            return element.PathFile;

        }


    }
}
