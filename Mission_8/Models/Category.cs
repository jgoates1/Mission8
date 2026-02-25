using System.ComponentModel.DataAnnotations;

namespace Mission08.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = string.Empty;

        // Navigation property
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}