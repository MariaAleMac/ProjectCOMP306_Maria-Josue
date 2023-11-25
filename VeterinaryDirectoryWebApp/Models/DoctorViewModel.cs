using System.ComponentModel;

namespace VeterinaryDirectoryWebApp.Models
{
    public class DoctorViewModel
    {

        public int Id { get; set; }

//        [DisplayName("Name")]
        public string? Name { get; set; }
        //        [DisplayName("Lastame")]
        public string? Lastname { get; set; }

        //[DisplayName("Gender")]
        public string? Gender { get; set; }

        //[DisplayName("Veterian Id")]
        public int? VeterianId { get; set; }

        //[DisplayName("Price")]
        public int? Price { get; set; }
        //[DisplayName("Specialty Id")]
        public string? SpecialtyId { get; set; }

        //[DisplayName("SpecialtyDesc")]
        public string? SpecialtyDesc { get; set; }

        //[DisplayName("About")]
        public string? About { get; set; }

        //[DisplayName("Location")]
        public string? Location { get; set; }

        //[DisplayName("Tel")]
        public string? Tel { get; set; }

        //[DisplayName("Schedule")]
        public string? Schedule { get; set; }

    }
}
