using Microsoft.AspNetCore.Mvc;
using WebThucHanhMVC.Models.DTO;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
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
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true)
        {
            List<BookDTO> response = new List<BookDTO>();
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                string apiUrl = $"https://localhost:7183/api/Books/get-all-books-sorted-by-field?field=id&ascending=true&filterOn={filterOn}&filterQuery={filterQuery}&sortBy={sortBy}&isAscending={isAscending}";

                var httpResponse = await httpClient.GetAsync(apiUrl);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var jsonString = await httpResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<List<BookDTO>>(jsonString);
                }
                else
                {
                    ViewBag.Error = "Error: Unable to retrieve data from the API.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> addBook(addBookDTO addBookDTO)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpRequestMess = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7245/api/Books/add-book"),
                    Content = new StringContent(JsonConvert.SerializeObject(addBookDTO), Encoding.UTF8, MediaTypeNames.Application.Json) // Sử dụng JsonConvert.SerializeObject
                };

                //Console.WriteLine(JsonSerializer.Serialize(addBookDTO));
                var httpResponseMess = await client.SendAsync(httpRequestMess);
                httpResponseMess.EnsureSuccessStatusCode();
                var response = await httpResponseMess.Content.ReadFromJsonAsync<addBookDTO>();
                if (response != null)
                {
                    return RedirectToAction("Index", "Books");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public async Task<IActionResult> listBook(int id)
        {
            BookDTO response = new BookDTO();
            try
            {
                // lấy dữ liệu books from API
                var client = _httpClientFactory.CreateClient();
                var httpResponseMess = await client.GetAsync("https://localhost:7245/api/Books/get-book-by-id/" + id);
                httpResponseMess.EnsureSuccessStatusCode();
                var stringResponseBody = await httpResponseMess.Content.ReadAsStringAsync();
                response = await httpResponseMess.Content.ReadFromJsonAsync<BookDTO>();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> editBook(int id)
        {
            BookDTO responseBook = new BookDTO();
            var client = _httpClientFactory.CreateClient();
            var httpResponseMess = await client.GetAsync("https://localhost:7245/api/Books/get-book-by-id/" + id);
            httpResponseMess.EnsureSuccessStatusCode();
            responseBook = await httpResponseMess.Content.ReadFromJsonAsync<BookDTO>();
            ViewBag.Book = responseBook;

            List<authorDTO> responseAu = new List<authorDTO>();
            var httpResponseAu = await client.GetAsync("https://localhost:7245/api/Authors/get-all-author");
            httpResponseAu.EnsureSuccessStatusCode();
            responseAu.AddRange(await httpResponseAu.Content.ReadFromJsonAsync<IEnumerable<authorDTO>>());
            ViewBag.listAuthor = responseAu;

            List<publisherDTO> responsePu = new List<publisherDTO>();
            var httpResponsePu = await client.GetAsync("https://localhost:7245/api/Publishers/get-all-publisher");
            httpResponsePu.EnsureSuccessStatusCode();
            responsePu.AddRange(await httpResponsePu.Content.ReadFromJsonAsync<IEnumerable<publisherDTO>>());
            ViewBag.listPublisher = responsePu;

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> editBook([FromRoute] int id, editDTO bookDTO)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpRequestMess = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri("https://localhost:7245/api/Books/update-book-by-id/" + id),
                    Content = new StringContent(JsonConvert.SerializeObject(bookDTO), Encoding.UTF8, MediaTypeNames.Application.Json) // Sử dụng JsonConvert.SerializeObject
                };

                var httpResponseMess = await client.SendAsync(httpRequestMess);
                httpResponseMess.EnsureSuccessStatusCode();
                var response = await httpResponseMess.Content.ReadFromJsonAsync<addBookDTO>();
                if (response != null)
                {
                    return RedirectToAction("Index", "Books");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> delBook([FromRoute] int id)
        {
            try
            {
                // lấy dữ liệu books from API
                var client = _httpClientFactory.CreateClient();
                var httpResponseMess = await client.DeleteAsync("https://localhost:7245/api/Books/delete-book-by- id/" + id); 
                httpResponseMess.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View("Index");
        }
    }
}