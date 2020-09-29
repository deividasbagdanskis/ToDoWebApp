using Newtonsoft.Json;
using System;

namespace ToDoApp.Projects.Data.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DeadlineDate { get; set; }

        public int ClientId { get; set; }

        [JsonIgnore]
        public Client Client { get; set; }
        
        public string UserId { get; set; }
    }
}
