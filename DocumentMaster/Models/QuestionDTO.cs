﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public DateTime DateTime { get; set; }
        public List<int> PersonIds { get; set; } = new ();
        public List<int> NoteIds { get; set; } = new ();
        public bool IsDeleted { get; set; }
        public List<int> FileUnitsId { get; set; }=new ();
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }=string.Empty;
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }=string.Empty;
        public int UnreadedCount { get; set; }
        public int ToDoCount { get; set; }
        public bool IsDoing { get; set; }
        public List<string> Participants { get; set; } = new();
    }
}
