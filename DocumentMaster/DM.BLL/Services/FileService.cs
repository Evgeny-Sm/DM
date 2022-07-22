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
        /*public VirtualFileResult GetFileAsync(int id)
        {
            var element = _db.FileUnits.Find(id);
            var filePath = Path.Combine("~/Files", "КМ.dwg");
            return File(filePath, " application/octet-stream");
        }*/


    }
}
