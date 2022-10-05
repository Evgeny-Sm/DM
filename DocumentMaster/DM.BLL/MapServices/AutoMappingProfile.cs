﻿using AutoMapper;
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
            CreateMap<Account, AccountDTO>();
            CreateMap<Section, SectionDTO>();
            CreateMap<Position, PositionDTO>();
            CreateMap<Control, ControlDTO>().ForMember("PersonName", 
                p => p.MapFrom(a => $"{a.Person.FirstName} {a.Person.LastName}"));
            CreateMap<Challenge, ChallengeDTO>().ForMember("PersonIds", c => c.MapFrom(p => p.Persons.Select(s => s.Id).ToList()));
            CreateMap<SendedMessage, UserMessage>();
            CreateMap<Question, QuestionDTO>().ForMember("PersonIds", q => q.MapFrom(p => p.Persons.Select(s => s.Id).ToList())).
                ForMember("NoteIds", q=>q.MapFrom(n=>n.Notes.Select(s => s.Id).ToList())).
                ForMember("FileUnitsId", q=>q.MapFrom(f=>f.FileUnits.Select(fl=>fl.Id).ToList())).
                ForMember("ProjectName", q=>q.MapFrom(p=>p.Project.Name));
            CreateMap<Note, NoteDTO>().ForMember("PersonIds", q => q.MapFrom(p => p.Persons.Select(s => s.Id).ToList()));
        }
    }
}
