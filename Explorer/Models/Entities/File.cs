using System.ComponentModel.DataAnnotations.Schema;



namespace Explorer.Models.Entities
{
    public partial class File
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public int? TypeId { get; set; }
        public string Content { get; set; }
        public int? FolderId { get; set; }
        public virtual Folder Folder { get; set; }
        [ForeignKey("TypeId")]
        public virtual Type Type { get; set; }
    }
}
