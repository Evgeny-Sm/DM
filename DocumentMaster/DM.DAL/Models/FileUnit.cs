﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class FileUnit
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }=false;
        [Required]
        public string Name { get; set; }=String.Empty;
        public string Description { get; set; }=string.Empty;
        public string PathFile { get; set; } = String.Empty;
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<UserAction>? UserActions { get; set; }
        public Project? Project { get; set; }
        public int ProjectId { get; set; }
        public FileUnit()
        { 
            UserActions = new List<UserAction>();
        }

    }
}
