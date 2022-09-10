using AutoMapper;
using DM.DAL.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.BLL.MapServices
{
    public class AutoMappingProfile:Profile
    {
        public AutoMappingProfile()
        {
            //Add as many of lines as many objects you need map to
            CreateMap<Department, DepartmentDTO>();
            CreateMap<FileUnit, FileDTO>();
            CreateMap<Person, PersonDTO>().ForMember("Role", p => p.MapFrom(a => a.Account.Role)).ForMember("UserName", p => p.MapFrom(a => a.Account.UserName));
            CreateMap<Project, ProjectDTO>().ForMember("MainIngId", p => p.MapFrom(u=>u.PersonId)).ForMember("FilesCount",p=>p.MapFrom(k=>k.FileUnits.Count));
            CreateMap<UserAction, UserActionDTO>();
            CreateMap<Account, AccountDTO>().ForMember("PersonId", p=>p.MapFrom(a=>a.Person.Id));
            CreateMap<Section, SectionDTO>();
            CreateMap<Position, PositionDTO>();
            CreateMap<Control, ControlDTO>().ForMember("PersonName", 
                p => p.MapFrom(a => $"{a.Person.FirstName} {a.Person.LastName}"));
            CreateMap<Challenge, ChallengeDTO>().ForMember("PersonIds", c => c.MapFrom(p => p.Persons.Select(s => s.Id).ToList()));
        }
    }
}
