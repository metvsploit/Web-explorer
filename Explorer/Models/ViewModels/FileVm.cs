using System.ComponentModel.DataAnnotations;

namespace Explorer.Models.ViewModels
{
    public class FileVm
    {
        [Required]
        public string FileName { get; set; }
        public string Description { get; set; }
        public int? TypeId { get; set; }
        public int? FolderId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
