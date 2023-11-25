using _301290835_Maria_Josue_3013473439_Project.Models;
using VeterinariansAPI.DTO;

namespace VeterinariansAPI.Services
{
    public interface IDoctorsRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctors();

        Task<DoctorDTO> GetDoctorById(int id);

        void CreateDoctor(Doctor doctor);

        void UpdateDoctor(Doctor doctor);

        Task RemoveDoctor(int id);

        Task<IEnumerable<DoctorDTO>> GetDoctorsBySearchFilter(string filter);

    }
}
