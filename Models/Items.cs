using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Items
    {
        [Key]
        public int id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsAvailableforOthers { get; set; }
        public bool IsViewable { get; set; }
        public bool IsDownloadable { get; set; }
        public string UserName { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
