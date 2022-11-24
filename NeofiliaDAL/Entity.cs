using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Neofilia.DAL
{
    public abstract class Entity
    {
        [Name("Id")]
        [Key]
        public int Id { get; set; }
    }
}
