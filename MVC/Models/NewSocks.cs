using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class NewSocks
    {
        public int ID { get; set; }
        [MaxLength(200)]
        public string Brand { get; set; }
        public SockSize Size { get; set; }
        public decimal Price { get; set; }
        [Range(0,100,ErrorMessage ="Chyba rozsahu")]
        public int OnStock { get; set; }


    }
}
