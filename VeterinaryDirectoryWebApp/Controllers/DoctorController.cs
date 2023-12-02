using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using VeterinaryDirectoryWebApp.Models;
using VeterinaryDirectoryWebApp.Services;

namespace VeterinaryDirectoryWebApp.Controllers
{
    public class DoctorController : Controller
    {

        HttpClientService _client = new HttpClientService();

        [HttpGet]
        public IActionResult Index()
        {
            List<DoctorViewModel> doctorList = new List<DoctorViewModel>();
            try
            {
                HttpResponseMessage response = _client.client.GetAsync("/proxy-vets/api/AllDoctors").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    doctorList = JsonConvert.DeserializeObject<List<DoctorViewModel>>(data);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                // For demonstration purposes, outputting the exception message
                Console.WriteLine("Exception: " + ex.Message);
            }
            return View(doctorList);
        }


        // GET by ID
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _client.client.GetAsync("/proxy-vets/api/Doctor/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var doctor = JsonConvert.DeserializeObject<DoctorViewModel>(data);
                return View(doctor);
            }
            return NotFound();
        }


        [HttpGet]
        public  IActionResult Create()
        {
            
            return View();
        }

        // POST: Create a new doctor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Doctor doctor)
        {
            try
            {
                using HttpResponseMessage response = await _client.client.PostAsJsonAsync("/proxy-vets/api/Doctor", doctor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View(doctor);
            }
        }


        // GET: Edit doctor details
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _client.client.GetAsync("/proxy-vets/api/Doctor/" + id);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var doctor = JsonConvert.DeserializeObject<DoctorViewModel>(data);
                return View(doctor);
            }

            return NotFound();
        }

        // POST: Update existing doctor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DoctorViewModel doctor)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(doctor), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.client.PutAsync("/proxy-vets/api/Doctor/" + id, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Failed to update the doctor.");
                return View(doctor);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View(doctor);
            }
        }


        //PUT: Update existing doctor
        //[HttpPut]
        //[ValidateAntiForgeryToken]
        // public async Task<ActionResult> Update(int id, DoctorViewModel doctor)
        // {
        //     var content = new StringContent(JsonConvert.SerializeObject(doctor), System.Text.Encoding.UTF8, "application/json");
        //     HttpResponseMessage response = await _client.client.PutAsync("/api/Doctor/" + id, content);

        //     if (response.IsSuccessStatusCode)
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }

        //     return View(doctor);
        // }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.client.GetAsync("/proxy-vets/api/Doctor/" + id);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var doctor = JsonConvert.DeserializeObject<DoctorViewModel>(data);
                return View(doctor);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.client.DeleteAsync("/proxy-vets/api/Doctor/" + id);

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
