using _301290835_Maria_Josue_3013473439_Project.Models;
using VeterinariansAPI.DTO;

namespace VeterinariansAPI.Services
{
    public interface IVeterinariansRepository
    {
        Task<IEnumerable<Veterinarian>> GetAllVeterinarians();

        Task<VeterinarianWithDoctorsDTO> GetVeterinarianById(int id);

        void CreateVeterinarian(Veterinarian veterinarian);

        void UpdateVeterinarian(Veterinarian veterinarian);

        Task RemoveVeterinarian(int id);

        Task<IEnumerable<VeterinarianWithDoctorsDTO>> GetVeterinariansBySearchFilter(string filter);

    }
}
