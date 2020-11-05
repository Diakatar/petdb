using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePets.Models
{
    public class Pet: IEntityModel
    {
        public long Id { get; set; }
        
        public AnimalType Type { get; set; }
        public string Name { get; set; }
        public virtual Person Owner { get; set; }
    }
}