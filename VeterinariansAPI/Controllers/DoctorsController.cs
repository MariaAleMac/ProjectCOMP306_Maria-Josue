using _301290835_Maria_Josue_3013473439_Project.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeterinariansAPI.DTO;
using VeterinariansAPI.Services;

namespace VeterinariansAPI.Controllers
{

    [Route("api/[controller]/[action]")]
    public class DoctorsController : Controller
    {

        private IDoctorsRepository _doctorsRepository;
        private readonly IMapper _mapper;


        public DoctorsController(IDoctorsRepository doctorsRepository, IMapper mapper)
        {
            _doctorsRepository = doctorsRepository;
            _mapper = mapper;
        }


        // GET: Doctors
        [HttpGet]
        [Route("/api/AllDoctors/")]
        public async Task<ActionResult<Doctor>> GetAllDoctors()
        {
            var doctors = await _doctorsRepository.GetAllDoctors();
            var results = _mapper.Map<IEnumerable<DoctorDTO>>(doctors);
            return Ok(results);


        }

        [HttpGet]
        [Route("/api/Doctor/{id}")]
        public async Task<ActionResult<DoctorDTO>> Details(int id)
        {
            var doctor = await _doctorsRepository.GetDoctorById(id);

            if (doctor == null)
            {
                return NotFound(); 
            }

            var doctorDTO = _mapper.Map<DoctorDTO>(doctor);
            return Ok(doctorDTO);
        }

        // POST: DoctorsController/Create
        [HttpPost]
        [Route("/api/Doctor")]
        public ActionResult Create([FromBody] Doctor doctor)
        {
            _doctorsRepository.CreateDoctor(doctor);
            return Ok();
        }


        //// PUT: DoctorsController/Update
        //[HttpPut]
        //[Route("/api/Doctor")]
        //public ActionResult Update([FromBody] Doctor doctor)
        //{
        //    _doctorsRepository.UpdateDoctor(doctor);
        //    return Ok();
        //}


        // GET: DoctorsController/Edit
        [HttpPut]
        [Route("/api/Doctor/{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody]Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest("Invalid doctor ID");
            }

            //var existingDoctor = await _doctorsRepository.GetDoctorById(id);
            //if (existingDoctor == null)
            //{
            //    return NotFound("Doctor not found");
            //}

            _doctorsRepository.UpdateDoctor(doctor);
            return Ok("Doctor updated successfully");
        }


        // DELETE: DoctorsController/Delete/:id
        [HttpDelete]
        [Route("/api/Doctor/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _doctorsRepository.RemoveDoctor(id);
            return Ok();
        }

        // GET: Doctors
        [HttpGet]
        [Route("/api/Doctors/Search/{searchFilter}")]
        public async Task<ActionResult<DoctorDTO>> GetDoctorsBySearchFilter(string searchFilter)
        {
            var doctors = await _doctorsRepository.GetDoctorsBySearchFilter(searchFilter);
            var results = _mapper.Map<IEnumerable<DoctorDTO>>(doctors);
            return Ok(results);
        }
    }
}
