using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
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
            using HttpResponseMessage response = await _client.client.GetAsync("/proxy-vets/api/AllVeterinarians");

            if (response is null)
            {
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
            using HttpResponseMessage response = await _client.client.GetAsync("/proxy-vets/api/Veterinarian/" + id);

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
                using HttpResponseMessage response = await _client.client.PostAsJsonAsync("/proxy-vets/api/Veterinarian", veterinarian);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: VeterinarianController/Edit/5
        public async  Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _client.client.GetAsync("/proxy-vets/api/Veterinarian/" + id);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var vet = JsonConvert.DeserializeObject<Veterinarian>(data);
                return View(vet);
            }

            return NotFound();
        }

        // POST: VeterinarianController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Name,Rate,Address,Schedule,Phone,Keywords,City")] Veterinarian veterinarian)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(veterinarian), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.client.PutAsync("/proxy-vets/api/Veterinarian/" + id, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                Console.WriteLine(response.StatusCode + " " +response.RequestMessage);

                ModelState.AddModelError(string.Empty, "Failed to update the veterinarian.");
                return View(veterinarian);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View(veterinarian);
            }
        }

        // GET: VeterinarianController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.client.GetAsync("/proxy-vets/api/Veterinarian/" + id);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var vet = JsonConvert.DeserializeObject<Veterinarian>(data);
                return View(vet);
            }

            return NotFound();
        }

        // POST: VeterinarianController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpResponseMessage response = await _client.client.DeleteAsync("/proxy-vets/api/Veterinarian/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
    }
}
