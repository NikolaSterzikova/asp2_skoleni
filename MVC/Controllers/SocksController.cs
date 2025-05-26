using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Services;
using System.Data;

namespace MVC.Controllers
{
    // Vlastní routing - kontroler bude dostupný na /ponozky, musíme přidat atribut route i pro všechny metody.
    [Route("ponozky")] //u action pak bude:  [Route("[action]")]
    public class SocksController : Controller
    {
        private readonly ISocksService socksService;

        public SocksController(ISocksService socksService)
        {
            this.socksService = socksService;
        }
       [Route("[action]")]
        public IActionResult Index()
        {
            var model =  socksService.GetSocks();
            return View(model);
        }
        [Route("[action]/{id}")]
        public IActionResult GetById(int id)
        {
            var data = socksService.GetSocks().Where(s => s.ID == id).FirstOrDefault();
            
            return View(data);
        }
        [Route("[action]/min/{priceMin:int}/max/{priceMax:int}")]
        public IActionResult searchByPrice(int priceMin, int priceMax)
        {
            var data = socksService.GetSocks().Where(x => x.Price >= priceMin && x.Price <= priceMax);
            return View("Index", data);
        }
    }
}
