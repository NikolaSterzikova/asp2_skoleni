using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;

namespace MVC.Controllers
{
    //[Route("[controller]")]
    //[ApiController]
    public class APIController : ControllerBase
    {
        public Socks GetById(int id, [FromServices] ISocksService socksService)
        {
            var socks = socksService.GetSocks().FirstOrDefault(s => s.ID == id);
            if (socks == null)
            {
                return null; // or throw an exception, or return a NotFound result
            }
            return socks;
        }
    }
}
