
namespace SampleWebApp.Models
{
	public class ToDoItem
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
		
		private int _priority;
		
		public int Priority
		{
			get { return _priority; }
			set
			{
				if (value > 0 && value < 6)
					_priority = value;
				else
					_priority = 3;
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
