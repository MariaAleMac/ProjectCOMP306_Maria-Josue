using System;
using System.Collections.Generic;

namespace _301290835_Maria_Josue_3013473439_Project.Models;

public partial class Veterinarian
{

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Rate { get; set; }

    public string? Address { get; set; }

    public string Schedule { get; set; } = null!;

    public string? Phone { get; set; }

    public string Keywords { get; set; } = null!;

    public string? City { get; set; }
}
