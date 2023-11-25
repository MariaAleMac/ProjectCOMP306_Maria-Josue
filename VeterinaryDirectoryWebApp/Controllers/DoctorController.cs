using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using VeterinaryDirectoryWebApp.Models;

namespace VeterinaryDirectoryWebApp.Controllers
{
    public class DoctorController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7203/api/");
        private readonly HttpClient _client;

        public DoctorController() 
        {
        
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [HttpGet]
        public IActionResult Index()
        {
            List<DoctorViewModel> doctorList = new List<DoctorViewModel>();
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "AllDoctors").Result;
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
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Doctor/" + id);
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
        public async Task<IActionResult> Create(DoctorViewModel doctor)
        {
            try
            {
                string data = JsonConvert.SerializeObject(doctor);
                StringContent content = new StringContent(data,Encoding.UTF8, "application/json");
                //var content = new StringContent(JsonConvert.SerializeObject(doctor), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response =  _client.PostAsync(_client.BaseAddress + "Doctor", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // If the response is not successful, handle the error
                    ModelState.AddModelError(string.Empty, "Failed to create the doctor.");
                    return View(doctor);
                }
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
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Doctor/" + id);

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
        public async Task<IActionResult> Edit(int id, DoctorViewModel doctor)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(doctor), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "Doctor/" + id, content);

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


        // PUT: Update existing doctor
        [HttpPut]
        public async Task<IActionResult> Update(int id, DoctorViewModel doctor)
        {
            var content = new StringContent(JsonConvert.SerializeObject(doctor), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "Doctor/" + id, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(doctor);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Doctor/" + id);

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
                HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + "Doctor/" + id);

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
