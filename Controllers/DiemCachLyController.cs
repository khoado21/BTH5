using Microsoft.AspNetCore.Mvc;
using test.Models;

namespace test.Controllers
{
    public class DiemCachLyController:Controller
    {
        public IActionResult ThemDCL()
        {
            return View();
        }

        [HttpPost]
        public string AddDCL(DiemCachLyModel dcl)
        {
            int count;
            DataContext context = HttpContext.RequestServices.GetService(typeof(DataContext)) as DataContext;
            count = context.ThemDCL(dcl);
            if (count == 1)
            {
                return "Thêm Thành Công!";
            }
            return "Thêm thất bại";
        }
    }
}
