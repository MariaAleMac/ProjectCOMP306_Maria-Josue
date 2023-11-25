using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VeterinaryDirectoryWebApp.Models;
using VeterinaryDirectoryWebApp.Services;

namespace VeterinaryDirectoryWebApp.Controllers
{
    public class VeterinarianController : Controller
    {

        HttpClientService _client = new HttpClientService();

        // GET: VeterinarianController
        public async Task<ActionResult> Index()
        {
            Console.WriteLine("HERE");
            using HttpResponseMessage response = await _client.client.GetAsync("/api/AllVeterinarians");

            if (response is null)
            {
                Console.WriteLine("AGAIN");
                return View();
            }

            var request = response.RequestMessage;
            Console.Write($"{request?.Method} ");
            Console.Write($"{request?.RequestUri} ");
            Console.WriteLine($"HTTP/{request?.Version}");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            List<Veterinarian> vets = JsonConvert.DeserializeObject<List<Veterinarian>>(jsonResponse);

            return View(vets);
        }

        // GET: VeterinarianController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using HttpResponseMessage response = await _client.client.GetAsync("/api/Veterinarian/"+id);

            if (response is null)
            {
                return View();
            }

            var request = response.RequestMessage;

            var jsonResponse = await response.Content.ReadAsStringAsync();

            VeterinarianWithDoctors vet = JsonConvert.DeserializeObject<VeterinarianWithDoctors>(jsonResponse);

            return View(vet);
        }

        // GET: VeterinarianController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VeterinarianController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Name,Rate,Address,Schedule,Phone,Keywords,City")] Veterinarian veterinarian)
        {
            try
            {
                string json = JsonConvert.SerializeObject(veterinarian);
                HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"); ;
                using HttpResponseMessage response = await _client.client.PostAsJsonAsync("/api/Veterinarian", veterinarian);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VeterinarianController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VeterinarianController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VeterinarianController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VeterinarianController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
