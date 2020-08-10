namespace SampleWebApp.Models
{
    public class ToDoItemTag
    {
        public int ToDoItemId { get; set; }
        public ToDoItem ToDoItem { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
