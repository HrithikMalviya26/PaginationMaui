using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PaginationMaui.Models
{
    public class Breed
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Attributes Attributes { get; set; }
        public Relationships Relationships { get; set; }
    }
}
