using Explorer.Models.Entities;
using System.Collections.Generic;

namespace Explorer.Models.DTO
{
    public class FolderDto
    {
        public int FolderId { get; set; }
        public string Name { get; set; }
        public List<FolderDto> Folders { get; set; }
        public virtual List<File> Files { get; set; }
        public int? ParentId { get; set; }
    }
}
