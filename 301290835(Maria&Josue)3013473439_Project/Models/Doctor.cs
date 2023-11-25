using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _301290835_Maria_Josue_3013473439_Project.Models;

public partial class Doctor
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
