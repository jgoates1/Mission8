using System.ComponentModel.DataAnnotations;

namespace Mission8.Models
{
    // Category is in its own table so the Add/Edit Task form can use a dropdown (Home, School, Work, Church).
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = string.Empty;

        public List<TaskItem> Tasks { get; set; } = new();
    }
}