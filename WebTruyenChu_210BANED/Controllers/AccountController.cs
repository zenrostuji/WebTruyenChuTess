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

        //public IActionResult Login()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Login(userModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Sai o ưhere!";
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
            // Xóa toàn bộ avatar trong thư mục
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/avatars");
            if (Directory.Exists(uploadsFolder))
            {
                string[] allAvatars = Directory.GetFiles(uploadsFolder);
                foreach (string avatar in allAvatars)
                {
                    System.IO.File.Delete(avatar);
                }
            }

            // Xóa session & chuyển hướng
            HttpContext.Session.Remove("Username");

            HttpContext.Session.SetString("AvatarPath", "/images/avatar.jpg");

            TempData["SuccessMessage"] = "Đã đăng xuất!";
            return RedirectToAction("Index", "Home");
        }

        //upload avatar
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile avatarFile, string username)
        {
            if (avatarFile != null && avatarFile.Length > 0)
            {
                try
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/avatars");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // 🔥 Xóa avatar cũ nếu tồn tại
                    string[] existingAvatars = Directory.GetFiles(uploadsFolder, $"{username}_*");
                    foreach (string oldAvatar in existingAvatars)
                    {
                        System.IO.File.Delete(oldAvatar);
                    }

                    // Lưu avatar mới
                    string fileExtension = Path.GetExtension(avatarFile.FileName);
                    string newFileName = $"{username}_{Guid.NewGuid()}{fileExtension}";
                    string filePath = Path.Combine(uploadsFolder, newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await avatarFile.CopyToAsync(stream);
                    }

                    string avatarPath = $"/uploads/avatars/{newFileName}";
                    HttpContext.Session.SetString("AvatarPath", avatarPath);

                    return RedirectToAction("UserPage", "Home");
                }
                catch (Exception ex)
                {
                    TempData["UploadError"] = "Lỗi khi tải lên avatar: " + ex.Message;
                }
            }
            else
            {
                TempData["UploadError"] = "Vui lòng chọn một tệp ảnh hợp lệ.";
            }

            return RedirectToAction("UserPage", "Home");
        }


    }

}
