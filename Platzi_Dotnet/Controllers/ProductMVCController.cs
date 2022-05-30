using Microsoft.AspNetCore.Mvc;
using Platzi_Dotnet.VIewModels;

namespace Platzi_Dotnet.Controllers
{
    public class ProductMVCController : Controller
    {
        public async Task<ActionResult> ProductMVC()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7117/api/");
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.GetAsync("products");

                if (response.IsSuccessStatusCode)
                {
                    products = response.Content.ReadAsAsync<List<ProductViewModel>>().Result;
                   
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            TempData["ProductList"] = products;
            return View(products);
        }

        public async Task<ActionResult> Edit(int id)
        {
            ProductViewModel product = new ProductViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7117/api/");
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.GetAsync("products/"+id);


                if (response.IsSuccessStatusCode)
                {
                    product = response.Content.ReadAsAsync<ProductViewModel>().Result;

                }
                else 
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            TempData["Product"] = product;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductViewModel product)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7117/api/");
                    client.DefaultRequestHeaders.Clear();
                    HttpResponseMessage HttpResponse = await client.PutAsJsonAsync("products/" + product.Id, product);

                    if (HttpResponse.IsSuccessStatusCode)
                    {
                        product = HttpResponse.Content.ReadAsAsync<ProductViewModel>().Result;

                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return RedirectToAction(nameof(ProductMVC));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            ProductViewModel product = new ProductViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7117/api/");
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync("products/" + id);

                if (response.IsSuccessStatusCode)
                {
                    product = response.Content.ReadAsAsync<ProductViewModel>().Result;

                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return RedirectToAction(nameof(ProductMVC));
        }

        public ActionResult Create(ProductViewModel product)
        {
            try
            {
                return RedirectToAction(nameof(ProductMVC));
            }
            catch
            {
                return View();
            }
        }

    }
}
