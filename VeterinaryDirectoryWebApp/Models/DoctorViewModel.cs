using System.ComponentModel;

namespace VeterinaryDirectoryWebApp.Models
{
    public class DoctorViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Lastname { get; set; }

        public string? Gender { get; set; }

        public int? VeterianId { get; set; }

        public int? Price { get; set; }

        public string? SpecialtyId { get; set; }

        public string? SpecialtyDesc { get; set; }

        public string? About { get; set; }

        public string? Location { get; set; }

        public string? Tel { get; set; }

        public string? Schedule { get; set; }

    }
}
