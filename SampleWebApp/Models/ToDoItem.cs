using SampleWebApp.Services;

namespace SampleWebApp.Models
{
	public class ToDoItem : IHasId
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
		
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
