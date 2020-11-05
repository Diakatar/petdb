using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmployeePets.Models
{
    public class AnimalType: IEntityModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}