using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationMaui.Models
{
    public class BreedResponse
    {
        public List<Breed> Data { get; set; }
        public Links Links { get; set; }
    }
}
