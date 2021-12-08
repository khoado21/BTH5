using Microsoft.AspNetCore.Mvc;
using test.Models;

namespace test.Controllers
{
    public class CongNhan:Controller
    {
        public IActionResult Nhap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LietKe(int SoTC)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(DataContext)) as DataContext;
            return View(context.LietKeCN(SoTC));   
        }
    }
}
