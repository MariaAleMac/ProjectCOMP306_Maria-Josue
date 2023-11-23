using _301290835_Maria_Josue_3013473439_Project.Models;
using AutoMapper;
using VeterinariansAPI.DTO;

namespace VeterinariansAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Veterinarian, VeterinarianDTO>();
            CreateMap<Doctor, DoctorDTO>();
            CreateMap<Pet, PetDTO>();
            CreateMap<Specialty, SpecialtyDTO>();
        }

    }
}
