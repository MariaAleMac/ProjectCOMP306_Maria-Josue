namespace VeterinariansAPI.DTO
{
    public class VeterinarianDTO
    {
        public int? Id { get; set; }

        public string Name { get; set; } = null!;

        public double Rate { get; set; }

        public string? Address { get; set; }

        public string Schedule { get; set; } = null!;

        public string? Phone { get; set; }

        public string Keywords { get; set; } = null!;

        public string? City { get; set; }

    }
}
