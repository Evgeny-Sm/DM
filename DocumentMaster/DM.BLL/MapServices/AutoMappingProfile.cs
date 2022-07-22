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
            CreateMap<Person, PersonDTO>().ForMember("FirstName",p=>p.MapFrom(u=>u.UserProfile.FirstName))
                                           .ForMember("LastName",p=>p.MapFrom(u=>u.UserProfile.LastName))
                                           ;
            CreateMap<Project, ProjectDTO>().ForMember("MainIngId", p => p.MapFrom(u=>u.PersonId));
            CreateMap<UserAction, UserActionDTO>();

        }
    }
}