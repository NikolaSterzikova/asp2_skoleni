using MVC.Models;

namespace MVC.Services
{
    public interface ISocksService
    {
        IEnumerable<Socks> GetSocks();
    }
}
