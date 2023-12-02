using _301290835_Maria_Josue_3013473439_Project.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeterinariansAPI.DTO;
using VeterinariansAPI.Services;

namespace VeterinariansAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class VeterinariansController : Controller
    {

        private IVeterinariansRepository _veterinariansRepository;
        private readonly IMapper _mapper;

        public VeterinariansController(IVeterinariansRepository veterinariansRepository, IMapper mapper)
        {
            _veterinariansRepository = veterinariansRepository;
            _mapper = mapper;
        }


        // GET: Veterinarians
        [HttpGet]
        [Route("/api/AllVeterinarians")]
        public async Task<ActionResult<Veterinarian>> GetAllVeterinarians()
        {
            var veterinarians = await _veterinariansRepository.GetAllVeterinarians();
            var results = _mapper.Map<IEnumerable<VeterinarianDTO>>(veterinarians);
            return Ok(results);
        }

        // GET: VeterinariansController/Details/5
        [HttpGet]
        [Route("/api/Veterinarian/{id}")]
        public async Task<ActionResult<VeterinarianWithDoctorsDTO>> Details(int id)
        {
            var veterinarians = await _veterinariansRepository.GetVeterinarianById(id);
            var results = _mapper.Map<VeterinarianWithDoctorsDTO>(veterinarians);
            return Ok(results);
        }

        // POST: VeterinariansController/Create
        [HttpPost]
        [Route("/api/Veterinarian")]
        public ActionResult Create([FromBody]Veterinarian veterinarian)
        {
            _veterinariansRepository.CreateVeterinarian(veterinarian);
            return Ok();   
        }

        // PUT: VeterinariansController/Update
        [HttpPut]
        [Route("/api/Veterinarian/{id}")]
        public ActionResult Update(int id, [FromBody] Veterinarian veterinarian)
        {

            if (id != veterinarian.Id)
            {
                return BadRequest("Invalid doctor ID");
            }


            _veterinariansRepository.UpdateVeterinarian(veterinarian);
            return Ok();
        }

        // DELETE: VeterinariansController/Delete/:id
        [HttpDelete]
        [Route("/api/Veterinarian/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _veterinariansRepository.RemoveVeterinarian(id);
            return Ok();
        }

        // GET: Veterinarians
        [HttpGet]
        [Route("/api/Veterinarians/Search/{searchFilter}")]
        public async Task<ActionResult<VeterinarianWithDoctorsDTO>> GetVeterinariansBySearchFilter(string searchFilter)
        {
            var veterinarians = await _veterinariansRepository.GetVeterinariansBySearchFilter(searchFilter);
            var results = _mapper.Map<IEnumerable<VeterinarianWithDoctorsDTO>>(veterinarians);
            return Ok(results);
        }
    }
}
