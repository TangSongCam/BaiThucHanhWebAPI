using Microsoft.AspNetCore.Mvc;
using WebThucHanhMVC.Models.DTO;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Net.Mime;
using System.Net.Http;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace WebThucHanhMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index([FromQuery]string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true)
        {
            List<BookDTO> response = new List<BookDTO>();
            try
            {
                // Lấy dữ liệu books từ API
                var client = _httpClientFactory.CreateClient();
                /*var httpResponseMess = await client.GetAsync($"https://localhost:7183/api/Books/get-all-books");*/
                var httpResponseMess = await client.GetAsync($"https://localhost:7183/api/Books/get-all-books-sorted-by-field?field=id&ascending=true" + filterOn + "filterQuery=" + filterQuery + "&sortBy=" + sortBy + "&isAscending=" + isAscending);
                httpResponseMess.EnsureSuccessStatusCode();
                var books = await httpResponseMess.Content.ReadFromJsonAsync<IEnumerable<BookDTO>>();
                if (books != null)
                {
                    response.AddRange(books);
                }
                else
                {
                    ViewBag.Error = "No data available.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddBook(addBookDTO addBookDTO)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMess = await client.PostAsJsonAsync("https://localhost:7085/api/Books/Add-Book", addBookDTO);
                httpResponseMess.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(addBookDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> editBook(int id, editDTO bookDTO)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMess = await client.PutAsJsonAsync($"https://localhost:7085/api/Books/update-book-by-id/{id}", bookDTO);
                httpResponseMess.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(bookDTO);
            }
        }

        [HttpGet]
        public async Task<IActionResult> delBook(int id)
        {
            try
            {
                // Xóa sách từ API
                var client = _httpClientFactory.CreateClient();
                var httpResponseMess = await client.DeleteAsync($"https://localhost:7085/api/Books/delete-book-by-id/{id}");
                httpResponseMess.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Index");
            }
        }


    }
}