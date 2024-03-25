
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FallbackController : Controller
    {
        // Chỉ ra xem các tìm router ở đâu 
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}