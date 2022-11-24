using CsvHelper.Configuration.Attributes;

namespace Neofilia.DAL.Models
{
    public class Question : Entity
    {
        [Name("Description")]
        public string? Description { get; set; }
        [Name("ImagePath")]
        public string? ImagePath { get; set; }
        [Name("Option1")]
        public string? Option1 { get; set; }
        [Name("Option2")]
        public string? Option2 { get; set; }
        [Name("Option3")]
        public string? Option3 { get; set; }
        [Name("Option4")]
        public string? Option4 { get; set; }
        [Name("Answer")]
        public int Answer { get; set; }
        [Name("Category")]
        public string? Category { get; set; }
        [Name("Difficulty")]
        public string? Difficulty { get; set; }

    }
}
