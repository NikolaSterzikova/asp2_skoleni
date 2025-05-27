using MVC.Models;

namespace MVC.Services
{
    public interface ISocksService: IService
    {
        IEnumerable<Socks> GetSocks();
    }
}
