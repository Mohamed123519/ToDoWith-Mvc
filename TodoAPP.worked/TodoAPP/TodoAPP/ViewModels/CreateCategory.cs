using System.ComponentModel.DataAnnotations;

namespace TodoAPP.ViewModels
{
    public class CreateCategory
    {
        [Required]
        [MinLength(4, ErrorMessage = "Min Length Must Be Bigger than 4 Char")]
        [MaxLength(40, ErrorMessage = "Max Length Must Be Less than 40 Char")]
        public string? Name { get; set; }

        public int Id { get; set; }
    }
}
