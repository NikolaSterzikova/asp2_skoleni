using System.ComponentModel.DataAnnotations;

namespace MVC.Data
{
    public class Request
    {
        public int ID { get; set; }
        public DateTime RequestDate { get; set; }
        [MaxLength(40)]
        public string IP { get; set; }
        public string Url { get; set; }
    }
}
