using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToDoApp.Business.Models;
using ToDoApp.Projects.ApiClient;

namespace ToDoApp.Web.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset DeadlineDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        public Client Client { get; set; }

        public IEnumerable<ToDoItemVo> toDoItems { get; set; }
    }
}
