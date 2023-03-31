using Microsoft.Build.Construction;
using Microsoft.CodeAnalysis.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Commnents
    {
        [Key]
        public int id { get; set; }
        public int Itemid { get; set; }
        public string commnents { get; set; }
        public string UserName { get; set; }

        public DateTime CreatedTime { get; set; }
        [NotMapped]
        public string Titlle { get; set; }
        [NotMapped]
        public int typ { get; set; }
    }
}
