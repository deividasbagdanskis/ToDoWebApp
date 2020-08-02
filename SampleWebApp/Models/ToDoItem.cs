﻿using SampleWebApp.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace SampleWebApp.Models
{
	public class ToDoItem : IHasId
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string? Description { get; set; }

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

		private Category _category;
		public Category Category
		{
			get
			{
				if (_category == null)
				{
					return new Category() { Name = "Uncategorized" };
				}

				return _category;
			}
			set
			{
				_category = value;
			}
		}

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
