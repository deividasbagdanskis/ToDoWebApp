namespace ToDoApp.Business.Models
{
    public class ToDoItemTagVo
    {
        public int ToDoItemId { get; set; }
        public ToDoItemVo ToDoItem { get; set; }

        public int TagId { get; set; }
        public TagVo Tag { get; set; }

        public string UserId { get; set; }
    }
}
