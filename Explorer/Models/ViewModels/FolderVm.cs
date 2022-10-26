using System.ComponentModel.DataAnnotations;

namespace Explorer.Models.ViewModels
{
    public class FolderVm
    {
        [Required(ErrorMessage = "Введите название папки")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Название папки должна быть от 1 до 50 символов")]
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
