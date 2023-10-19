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
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        private readonly IMapper? _mapper;
        public DepartmentService(IDbContextFactory<DMContext>? contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetDepartmentsListAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            {
                var devices = await context.Departments.ToListAsync();
                var result = _mapper.Map<IEnumerable<DepartmentDTO>>(devices);
                return result;
            }
        }

        public async Task<DepartmentDTO> GetDepartmentByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            {
                Department department = await context.Departments.FindAsync(id);
                if (department == null || department.IsDeleted == true)
                {
                    return null;
                }
                var result = _mapper.Map<DepartmentDTO>(department);
                return result;
            }
        }

        public async Task<DepartmentDTO> AddDepartmentAsync(DepartmentDTO departmentDTO)
        {
            using var context = _contextFactory.CreateDbContext();

            Department department = new Department
            {
                Name = departmentDTO.Name,
                Description = departmentDTO.Description
            };
            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();
            var result = _mapper.Map<DepartmentDTO>(department);
            return result;

        }

        public async Task UpdateDepartmentAsync(DepartmentDTO departmentDTO)
        {
            using var context = _contextFactory.CreateDbContext();

            var element = context.Departments.Find(departmentDTO.Id);
            if (element is null)
            {
                element = new Department();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }

            element.Name = departmentDTO.Name;
            element.Description = departmentDTO.Description;
            element.IsDeleted = departmentDTO.IsDeleted;
            context.Departments.Update(element);
            await context.SaveChangesAsync();


        }
        public async Task<bool> DeleteDepartment(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            Department department = await context.Departments.FindAsync(id);
            if (department != null)
            {
                context.Departments.Remove(department);
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
