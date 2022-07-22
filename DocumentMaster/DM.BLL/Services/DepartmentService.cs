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
    public class DepartmentService
    {
        private readonly DMContext? _db;
        private readonly IMapper? _mapper;
        public DepartmentService(DMContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetDepartmentsListAsync()
        {
            var devices = await _db.Departments.Where(d=>d.IsDeleted==false).ToListAsync();
            var result = _mapper.Map<IEnumerable<DepartmentDTO>>(devices);
            return result;
        }

        public async Task<DepartmentDTO> GetDepartmentByIdAsync(int id)
        {
            Department department = await _db.Departments.FindAsync(id);
            if (department == null||department.IsDeleted==true)
            {
                return null;
            }
            var result = _mapper.Map<DepartmentDTO>(department);
            return result;
        }

        public async Task<DepartmentDTO> AddDepartmentAsync(DepartmentDTO departmentDTO)
        {
            Department department = new Department
            {
                Name = departmentDTO.Name,
                Description = departmentDTO.Description
            };
            await _db.Departments.AddAsync(department);
            await _db.SaveChangesAsync();
            return await GetDepartmentByIdAsync(department.Id);
        }

        public async Task UpdateDepartmentAsync(int id, DepartmentDTO departmentDTO)
        {
            var element = _db.Departments.FirstOrDefault(c => c.Id == id);
            if (element is null)
            {
                element = new Department();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Id = id;
            element.Name = departmentDTO.Name;
            element.Description = departmentDTO.Description;     
            element.IsDeleted = departmentDTO.IsDeleted;
            _db.Departments.Update(element);
            await _db.SaveChangesAsync();

        }
        public async Task<bool> DeleteDepartment(int id)
        {
            Department department = await _db.Departments.FindAsync(id);
            if (department != null)
            {
                _db.Departments.Remove(department);
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
