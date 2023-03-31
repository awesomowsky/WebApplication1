namespace WebApplication1.Models
{
    public class Preview_Files
    {
        public int id { get; set; }
        public string? FileName { get; set; }
        public string? comments { get; set; }

        public DateTime? UploadedDate { get; set; }
        public string FilePath { get; set; }
        public string? UserName { get; set; }
    }
}
