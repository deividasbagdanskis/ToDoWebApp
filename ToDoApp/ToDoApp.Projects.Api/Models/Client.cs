﻿using System.Collections.Generic;

namespace ToDoApp.Projects.Api.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Project> Projects { get; set; }
    }
}
