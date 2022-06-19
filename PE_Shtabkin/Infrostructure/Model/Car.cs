using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Infrostructure
{
    public class Car
    {
            [Key]
            public int ID { get; set; }
            public string Country { get; set; }
            public string Town { get; set; }
            public string Place { get; set; }
            public DateTime Rent_Starts { get; set; }
            public DateTime Rent_Ends { get; set; }
            public string Model { get; set; }
            public decimal Price_PerDay { get; set; }
            public bool Is_Busy { get; set; }
    }
}
