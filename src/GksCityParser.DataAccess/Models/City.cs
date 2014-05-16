using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GksCityParser.DataAccess.Models
{
    public class City
    {
        public int Id { get; set; }
        [StringLength(4096)]
        public string City { get; set; }
        [StringLength(4096)]
        public string Subject { get; set; }
        public int Population { get; set; }
    }
}
