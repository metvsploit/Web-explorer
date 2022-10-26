using System.ComponentModel.DataAnnotations;

namespace Explorer.Models.ViewModels
{
    public class RenameModelVm
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Название папки должна быть от 1 до 50 символов")]
        public string Name { get; set; }
    }
}
