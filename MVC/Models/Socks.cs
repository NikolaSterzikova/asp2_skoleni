namespace MVC.Models
{
    public class Socks
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public SockSize Size{ get; set; }
        public decimal Price { get; set; }
        public int OnStock { get; set; }
    } 
    public enum SockSize
    {
        S,
        M,
        L
    }
}
