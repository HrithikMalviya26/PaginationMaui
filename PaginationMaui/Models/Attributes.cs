using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationMaui.Models
{
    public class Attributes
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Life Life { get; set; }
        public Weight MaleWeight { get; set; }
        public Weight FemaleWeight { get; set; }
        public bool Hypoallergenic { get; set; }
    }
}
