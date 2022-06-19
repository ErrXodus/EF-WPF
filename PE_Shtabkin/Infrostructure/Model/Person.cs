using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrostructure
{
    public class Person
    {
        [Key]
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Premission_Level { get; set; }
    }
}
