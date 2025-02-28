using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebTruyenChu_210BANED.Models;
using System.Collections.Generic;

namespace WebTruyenChu_210BANED.Controllers
{
    //[Route("StoryCard")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //category page
        public IActionResult Category(string genre, int page = 1)
        {
            var filteredStories = string.IsNullOrEmpty(genre)
            ? stories
            : stories.Where(s => s.Genre == genre).ToList();

            int pageSize = 4;
            var paginatedStories = filteredStories.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(filteredStories.Count / (double)pageSize);
            ViewBag.SelectedGenre = genre;

            return View("Index", paginatedStories);

        }


        //info page
        [Route("InfoCardStory/{id}")]
        public IActionResult InfoCardStory(int id)
        {
            var story = stories.FirstOrDefault(s => s.Id == id);

            if (story == null)
            {
                return NotFound();
            }

            var viewModel = new StoryCard
            {
                Id = story.Id,
                Title = story.Title,
                ImageUrl = story.ImageUrl,
                Genre = story.Genre,
                FirstChapterId = 1, // Giả định chap đầu tiên có ID = 1
                LatestChapterId = 10 // Giả định chap mới nhất có ID = 10
            };

            //return View(viewModel);
            return View("InfoCardStory", story);
        }

        // Action xử lý tìm kiếm
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View("SearchResults", new List<StoryCard>()); // Trả về trang tìm kiếm rỗng nếu không có từ khóa
            }

            var results = stories.Where(s => s.Title.ToLower().Contains(query.ToLower())).ToList();
            return View("SearchResults", results);
        }

        //data tess
        private static List<StoryCard> stories = new List<StoryCard>
        {
            new () { Id = 1, Title = "Nguyện Vọng cuối cùng", ImageUrl = "https://storage.googleapis.com/july-bucket/Rny4s5DwqrVVX1nvM8WZij4R",Genre = "Tiểu thuyết", Author = "Fujiko F. Fujio" },
            new () { Id = 2, Title = "SherLockHomes", ImageUrl = "https://img.websosanh.vn/v2/users/root_product/images/sherlock-holmes-toan-tap-tap-2/0maews0YWSfU.jpg?width=300", Genre = "Trinh thám" , Author = "Gosho Aoyama"},
            new () { Id = 3, Title = "Vì anh gặp em", ImageUrl = "https://designs.vn/wp-content/images/18-04-2013/1A.jpg", Genre = "Ngôn tình", Author = "Fujiko F. Fujio" },
            new () { Id = 4, Title = "Truyện Thúy Kiều", ImageUrl = "https://laodongthudo.vn/stores/news_dataimages/baogiay/112015/20/09/c57fd662d573a0e6894e60ba59136c24_bia_sach.jpg" ,Genre = "Văn học cổ điển", Author = "Fujiko F. Fujio" },
            new () { Id = 5, Title = "Doraemon", ImageUrl = "https://upload.wikimedia.org/wikipedia/vi/6/6b/B%C3%ACa_truy%E1%BB%87n_Doraemon_t%E1%BA%ADp_1_1992-2009_VN.jpg", Genre = "Truyện tranh", Author = "Fujiko F. Fujio"},
            new () { Id = 6, Title = "One Piece", ImageUrl = "https://product.hstatic.net/200000343865/product/1_cd3e91d598a942589a0c7cc6fde58b0b_5a7b3e5e12e54693b540607f7a3d6638_master.jpg", Genre = "Truyện tranh", Author = "Eiichiro Oda" },
            new () { Id = 7, Title = "Naruto", ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/94/NarutoCoverTankobon1.jpg",  Genre = "Phiêu lưu", Author = "Masashi Kishimoto" }
        };
        private const int ITEMS_PER_PAGE = 6;
        public IActionResult Index(int page = 1)
        {
            int totalStories = stories.Count;
            int totalPages = (int)System.Math.Ceiling((double)totalStories / ITEMS_PER_PAGE);

            var currentStories = stories.Skip((page - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(currentStories);
        }

        public IActionResult follow_p(int page = 1)
        {
            int totalStories = stories.Count;
            int totalPages = (int)System.Math.Ceiling((double)totalStories / ITEMS_PER_PAGE);

            var currentStories = stories.Skip((page - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(currentStories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
