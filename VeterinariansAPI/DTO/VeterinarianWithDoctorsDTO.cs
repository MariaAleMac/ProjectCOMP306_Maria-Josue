using _301290835_Maria_Josue_3013473439_Project.Models;

namespace VeterinariansAPI.DTO
{
    public class VeterinarianWithDoctorsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public double Rate { get; set; }

        public string? Address { get; set; }

        public string Schedule { get; set; } = null!;

        public string? Phone { get; set; }

        public string Keywords { get; set; } = null!;

        public string? City { get; set; }

        public List<Doctor>? Doctors { get; set; }

        public VeterinarianWithDoctorsDTO(Veterinarian veterinarian)
        {
            this.Id = veterinarian.Id;
            this.Name= veterinarian.Name;
            this.Rate= veterinarian.Rate;
            this.Address= veterinarian.Address;
            this.Schedule= veterinarian.Schedule;
            this.Phone= veterinarian.Phone;
            this.Keywords= veterinarian.Keywords;
            this.City= veterinarian.City;
        }
    }
}
