using Newtonsoft.Json;
using System.Collections.Generic;

namespace ToDoApp.Projects.Data.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Project> Projects { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
