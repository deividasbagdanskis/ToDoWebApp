using ToDoApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
	public class ToDoItem : IHasId
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime CreationDate { get; set; }

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

		public Category Category { get; set; }

		public List<ToDoItemTag> ToDoItemTags { get; set; }

		public ToDoItem()
		{

		}

		public ToDoItem(int id, string name, string description, int priority)
		{
			Id = id;
			Name = name;
			Description = description;
			Priority = priority;
		}
	}
}
