using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission8.Models
{
    // Represents one task in the Covey matrix: name, due date, quadrant (1-4), category, and completed flag.
    public class TaskItem
    {
        [Key]
        public int TaskItemId { get; set; }

        [Required(ErrorMessage = "Task name is required.")]
        public string TaskName { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Quadrant is required.")]
        [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4.")]
        public int Quadrant { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }  // Navigation property for dropdown display (e.g. CategoryName).

        public bool Completed { get; set; } = false;
    }
}