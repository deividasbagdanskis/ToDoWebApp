using ToDoApp.Business.Models;
using System.ComponentModel.DataAnnotations;
using System;
using ToDoApp.Commons.Enums;

namespace ToDoApp.Web.ViewModels
{
    public class ToDoItemViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeadlineDate { get; set; }

        private int _priority = 3;

        [Required]
        [Range(1, 5)]
        public int Priority
        {
            get { return _priority; }
            set
            {
                if (value > 5)
                    _priority = 5;
                else if (value > 0 && value < 6)
                    _priority = value;
            }
        }

        public StatusEnum Status { get; set; } = StatusEnum.Backlog;

        public int? CategoryId { get; set; }

        public CategoryDao Category { get; set; }
    }
}
