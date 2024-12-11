using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilesApi.Models
{
    public class GroupedByBrandDTO
    {
        public string Name { get; set; }
        public List<MobileDTO> Mobiles { get; set; }
    }
}