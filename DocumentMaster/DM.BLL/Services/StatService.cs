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
    public class StatService
    {
        private readonly IMapper? _mapper;
        private readonly IDbContextFactory<DMContext>? _contextFactory;

        public StatService(IMapper? mapper, IDbContextFactory<DMContext>? contextFactory)
        {
            _mapper = mapper;
            _contextFactory = contextFactory;
        }

        public async Task<StatUserModel> GetStatForPerson(int personId, DateTime dateFrom, DateTime dateTo)
        {
            StatUserModel model = new();

            model.DateFrom = dateFrom;
            model.DateTo= dateTo;

            using var context = _contextFactory.CreateDbContext();
            var person = context.Persons.Find(personId);
            model.Person = _mapper.Map<PersonDTO>(person);

            var files = await context.FileUnits.Where(f => f.PersonId == personId && f.Status == StatusFile.Archive)
                .Include(c => c.Controls.Where(ch => ch.IsConfirmed))
                .Where(f => dateFrom < f.Controls.First().DateTime && dateTo > f.Controls.First().DateTime).ToListAsync();

            model.DevelopedFiles = _mapper.Map<List<FileDTO>>(files);

            var controls = await context.Controls
                .Where(c => c.IsConfirmed && c.PersonId == personId && c.DateTime > dateFrom && c.DateTime < dateTo).ToListAsync();
            model.Controls = _mapper.Map<List<ControlDTO>>(controls);

            return model;
        }

        public async Task<StatUserModel> GetStatForPersonInProject(int personId, int projectId, DateTime dateFrom, DateTime dateTo)
        {
            StatUserModel model = new();

            model.DateFrom = dateFrom;
            model.DateTo = dateTo;

            using var context = _contextFactory.CreateDbContext();

            var person = context.Persons.Find(personId);
            model.Person = _mapper.Map<PersonDTO>(person);

            var project = context.Projects.Find(projectId);
            model.Project = _mapper.Map<ProjectDTO>(project);

            var files = await context.FileUnits.Where(f => f.ProjectId==projectId && f.PersonId == personId && f.Status == StatusFile.Archive )
                .Include(c => c.Controls.Where(ch => ch.IsConfirmed))
                .Where(f => dateFrom < f.Controls.First().DateTime && dateTo > f.Controls.First().DateTime).ToListAsync();
            model.DevelopedFiles = _mapper.Map<List<FileDTO>>(files);

            var controls = await context.Controls.Where(c => c.IsConfirmed && c.PersonId == personId && c.DateTime > dateFrom && c.DateTime < dateTo).Include(f=>f.FileUnit)
                .Where(t=>t.FileUnit.ProjectId==projectId).ToListAsync();

            model.Controls = _mapper.Map<List<ControlDTO>>(controls);

            return model;
        }

    }
}
