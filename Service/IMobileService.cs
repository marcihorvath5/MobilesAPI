using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobilesApi.Models;

namespace MobilesApi.Service
{
    public interface IMobileService
    {
        public IEnumerable<MobileDTO> GetMobiles(string? model, string? os, int? minPrice, int? maxPrice, 
                                                int? releaseYear, string[]? brandNames);
        public MobileDTO GetMobile(int id);
        public List<GroupedByBrandDTO> GroupedMobiles();
        public bool DeleteMobile(int id);
        public bool AddMobile(CreateMobileDTO mobile);
    }
}