using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using BusinessObjects;

namespace IdentityAjaxClient.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public ProductsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7224/api/ProductsControllers";
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "(" + id + ")?version=v1");
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Product items = JsonSerializer.Deserialize<Product>(strDate, options);
            return View(items);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId,UnitsInStock,UnitPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response1 = await client.PostAsJsonAsync(ProductApiUrl, product);
                response1.EnsureSuccessStatusCode();

                // return response.Headers.Location;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "(" + id + ")?version=v1");
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Product items = JsonSerializer.Deserialize<Product>(strDate, options);
            return View(items);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,CategoryId,UnitsInStock,UnitPrice")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                HttpResponseMessage response1 = await client.PutAsJsonAsync(
                ProductApiUrl + "(" + id + ")?version=v1", product);
                response1.EnsureSuccessStatusCode();


                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "(" + id + ")?version=v1");
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Product items = JsonSerializer.Deserialize<Product>(strDate, options);
            return View(items);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(
                     ProductApiUrl + "(" + id + ")");
            response1.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }
    }
}
