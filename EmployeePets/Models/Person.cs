using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EmployeePets.Models
{
    public class Person: IEntityModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsEmployee { get; set; }
        
        [JsonIgnore] public virtual ICollection<Pet> Pets { get; set; }
    }
}