using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilesApi.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Mobile> Mobiles{ get; set; } = new List<Mobile>();
    }
}