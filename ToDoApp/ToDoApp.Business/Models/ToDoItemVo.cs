using System;
using System.Collections.Generic;
using ToDoApp.Commons.Interfaces;
using ToDoApp.Commons.Enums;

namespace ToDoApp.Business.Models
{
	public class ToDoItemVo : IHasId
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime CreationDate { get; set; }

		public DateTime? DeadlineDate { get; set; }

		private int _priority = 3;
		
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

		public CategoryVo Category { get; set; }

		public List<ToDoItemTagVo> ToDoItemTags { get; set; }

		public int ProjectId { get; set; }

		public string ProjectName { get; set; }

		public ToDoItemVo()
		{

		}

		public ToDoItemVo(int id, string name, string description, int priority)
		{
			Id = id;
			Name = name;
			Description = description;
			Priority = priority;
		}
	}
}
