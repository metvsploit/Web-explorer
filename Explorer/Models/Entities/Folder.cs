using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Explorer.Models.Entities
{
    public partial class Folder
    {
        public int FolderId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public List<Folder> Folders { get; set; }
        public virtual List<File> Files { get; set; }
    }
}
