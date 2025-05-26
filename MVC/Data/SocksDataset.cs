using MVC.Models;

namespace MVC.Data
{
    public class SocksDataset
    {
        static string[] _brands = { "Nike", "Adidas", "Puma", "Reebok" };
        static List<Socks> _socks = null; 
        public static IEnumerable<Socks> GetSocks()
        {
            if(_socks != null)
            {
                return _socks;
            }
            _socks = Enumerable.Range(1, 10).Select(i => new Socks // Fixed incorrect usage of Enumerable.Range
            {
                ID = i,
                Brand = _brands[Random.Shared.Next(_brands.Length)],
                Size = (SockSize)Random.Shared.Next(0, 3),
                Price = Random.Shared.Next(100, 500),
                OnStock = Random.Shared.Next(0, 100)
            }).ToList();
            return _socks;
        }
    }
}
