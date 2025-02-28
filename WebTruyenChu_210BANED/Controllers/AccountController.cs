using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTruyenChu_210BANED.Models;

namespace WebTruyenChu_210BANED.Controllers
{
    public class AccountController : Controller
    {
        public static List<userModel> _users = new List<userModel>
        {
            new userModel { Username = "admin", Email = "admin@gmail.com", Password = "123456" } // Thêm admin vào danh sách
        };

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(userModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            if (_users.Any(u => u.Username == model.Username || u.Email == model.Email))
            {
                ViewBag.ErrorMessage = "Tên người dùng hoặc Email đã tồn tại!";
                return View("Register", model);
            }

            _users.Add(model);
            HttpContext.Session.SetString("Username", model.Username);
            TempData["SuccessMessage"] = "Đăng ký thành công!";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(userModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            var user = _users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
             
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                TempData["SuccessMessage"] = "Đăng nhập thành công!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Sai thông tin đăng nhập!";
                return View("Login", model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            TempData["SuccessMessage"] = "Đã đăng xuất!";
            return RedirectToAction("Index", "Home");
        }
    }

}
