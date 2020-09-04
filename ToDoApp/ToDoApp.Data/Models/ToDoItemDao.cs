using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToDoApp.Commons.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.Commons.Interfaces;

namespace ToDoApp.Data.Models
{
	[Table("ToDoItem")]
	public class ToDoItemDao : IHasId
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		[Required]
		public DateTime CreationDate { get; set; }

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

		public List<ToDoItemTagDao> ToDoItemTags { get; set; }

        public int ProjectId { get; set; }

		[NotMapped]
        public string ProjectName { get; set; }
    }
}
