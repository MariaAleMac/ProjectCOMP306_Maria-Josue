using _301290835_Maria_Josue_3013473439_Project.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;
using VeterinariansAPI.DTO;

namespace VeterinariansAPI.Services
{
    public class DoctorRepository : IDoctorsRepository
    {
        private Comp306ProjectContext _context;
        private readonly IMapper _mapper;

        public DoctorRepository(Comp306ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            var result = _context.Doctors.OrderBy(v => v.Id);
            return await result.ToListAsync();
        }
        
        public async Task<DoctorDTO> GetDoctorById(int id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
            {
                return null; 
            }

            var doctorDTO = _mapper.Map<DoctorDTO>(doctor);
            return doctorDTO;
        }

        public void CreateDoctor(Doctor doctor)
        {
            Console.WriteLine("HEREEEEEE-----"+ doctor.Gender + " " + doctor.Name);
            using (var myContext = new Comp306ProjectContext())
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
            }
        }
        public void UpdateDoctor(Doctor doctor)
        {
            using (var myContext = new Comp306ProjectContext())
            {
                _context.Doctors.Update(doctor); // Attach and update the entity
                _context.SaveChanges();
            }
        }


        public async Task RemoveDoctor(int id)
        {
            var resultVet = await _context.Doctors.Where(v => v.Id == id).FirstOrDefaultAsync();
            if (resultVet != null)
            {
                _context.Doctors.Remove(resultVet);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<DoctorDTO>> GetDoctorsBySearchFilter(string filter)
        {

            var result = _context.Doctors.Where(v => v.SpecialtyId.Contains(filter) || v.Gender.Contains(filter) || v.Location.Contains(filter) || v.Lastname.Contains(filter) || v.Schedule.Contains(filter) || v.Name.Contains(filter));
            var doctors = await result.ToListAsync();

            var doctorDTOs = _mapper.Map<IEnumerable<DoctorDTO>>(doctors);
            return doctorDTOs;

        }
    }
}
