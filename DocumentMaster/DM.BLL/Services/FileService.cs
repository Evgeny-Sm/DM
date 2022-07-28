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
        private readonly DMContext? _db;
        private readonly IMapper? _mapper;

        public FileService(DMContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;

        }
        public async Task<IEnumerable<FileDTO>> GetAllAsync()
        {
            var elements = await _db.FileUnits.ToListAsync();
            var result = _mapper.Map<IEnumerable<FileDTO>>(elements);
            return result;
        }
        public async Task<IEnumerable<FileDTO>> GetAllNotDeletedAsync()
        {
            var elements = await _db.FileUnits.Where(d => d.IsDeleted == false).ToListAsync();
            var result = _mapper.Map<IEnumerable<FileDTO>>(elements);
            return result;
        }

        public async Task<FileDTO> GetItemByIdAsync(int id)
        {
            var element = await _db.FileUnits.FindAsync(id);
            if (element == null)
            {
                return null;
            }
            var result = _mapper.Map<FileDTO>(element);
            return result;
        }

        public async Task<FileDTO> AddFileAsync(FileDTO fileDTO, int personId)
        {
            FileUnit element = new FileUnit
            {
                Name = fileDTO.Name,
                Description = fileDTO.Description,
                ProjectId = fileDTO.ProjectId,
                DepartmentId = fileDTO.DepartmentId,
                PathFile = $"{fileDTO.ProjectId}/{fileDTO.Name}"
            };

            await _db.FileUnits.AddAsync(element);
            await _db.SaveChangesAsync();

            var action = new UserAction
            {
                FileUnitId = element.Id,
                PersonId = personId,
                ActionNumber = 1,
                IsConfirmed = true
            };
            await _db.UserActions.AddAsync(action);          
            await _db.SaveChangesAsync();

            return await GetItemByIdAsync(element.Id);
        }

        public async Task<string> GetFullFilePathAsync(int id)
        {
            var element = await _db.FileUnits.FindAsync(id);
            
            if (element == null)
            {
                return null;
            }
            return element.PathFile;

        }


    }
}
