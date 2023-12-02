using _301290835_Maria_Josue_3013473439_Project.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;
using VeterinariansAPI.DTO;

namespace VeterinariansAPI.Services
{
    public class VeterinarianRepository : IVeterinariansRepository
    {
        private Comp306ProjectContext _context;
        private readonly IMapper _mapper;

        public VeterinarianRepository( Comp306ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Veterinarian>> GetAllVeterinarians()
        {
            var result = _context.Veterinarians.OrderBy( v => v.Id );
            return await result.ToListAsync();
        }

        public async Task<VeterinarianWithDoctorsDTO> GetVeterinarianById(int id)
        {
            var resultVet = await  _context.Veterinarians.Where(v => v.Id == id).FirstOrDefaultAsync();
            VeterinarianWithDoctorsDTO vetWithDoc = new VeterinarianWithDoctorsDTO(resultVet);
            var resultDoctors = _context.Doctors.Where(d => d.VeterianId == id);
            Console.WriteLine(resultDoctors.Count());
            var doctors = await resultDoctors.ToListAsync();

           vetWithDoc.Doctors = doctors;
            return vetWithDoc;

        }

        public void CreateVeterinarian(Veterinarian veterinarian)
        {
            //_ = _context.Veterinarians.Add(veterinarian);
            using (var myContext = new Comp306ProjectContext())
            {
                _context.Veterinarians.Add(veterinarian);
                _context.SaveChanges();
            }
        }

        public void UpdateVeterinarian(Veterinarian veterinarian)
        {
            using (var myContext = new Comp306ProjectContext())
            {
                _context.Veterinarians.Update(veterinarian);
                _context.SaveChanges();
            }
        }

        public async Task RemoveVeterinarian(int id)
        {
            var resultVet = await _context.Veterinarians.Where(v => v.Id == id).FirstOrDefaultAsync();
            if (resultVet != null)
            {
                _context.Veterinarians.Remove(resultVet);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<VeterinarianWithDoctorsDTO>> GetVeterinariansBySearchFilter(string filter)
        {
            var result = _context.Veterinarians.Where(v => v.City.Contains(filter) || v.Keywords.Contains(filter) || v.Address.Contains(filter) || v.Schedule.Contains(filter) || v.Name.Contains(filter));
            var resultVet = await result.ToListAsync();
            List<VeterinarianWithDoctorsDTO> vetsWithDoc = new List<VeterinarianWithDoctorsDTO>();

            foreach ( Veterinarian veterinarian in resultVet )
            {
                VeterinarianWithDoctorsDTO vetWithDoc = new VeterinarianWithDoctorsDTO(veterinarian);
                var resultDoctors = _context.Doctors.Where(d => d.VeterianId == veterinarian.Id);
                var doctors = await resultDoctors.ToListAsync();
                vetWithDoc.Doctors = doctors;
                vetsWithDoc.Add(vetWithDoc);
            }

            return vetsWithDoc;
        }
    }
}
