using MVC.Data;
using MVC.Models;

namespace MVC.Services
{
    public class SocksService : ISocksService, IService
    {
        public IEnumerable<Socks> GetSocks()
        {
            return SocksDataset.GetSocks();
        }
    }
}
