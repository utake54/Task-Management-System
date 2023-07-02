﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.CommonModel;

namespace TaskManagement.Model.Model.Task.Request
{
    public class TaskRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsFinished { get; set; }
    }
}
