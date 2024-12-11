using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilesApi.Models
{
    public class CreateMobileDTO
    {
        public string Model { get; set; }
        
        public string Os { get; set; }

        public int? ReleaseYear { get; set; }

        public int Price { get; set; }

        public int BrandId { get; set; }
    }
}