using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MobilesApi.Models;

namespace MobilesApi.Service
{
    public class MobileService : IMobileService
    {
        private readonly MobileDb _db;

        public MobileService(MobileDb db)
        {
            _db = db;
        }

        public IEnumerable<MobileDTO> GetMobiles(string? model, string? os, int? minPrice, 
                                                int? maxPrice, int? releaseYear, string[]? brandNames)
        {
            var query = _db.Mobiles.Include(m => m.Brand).AsQueryable();
            
            if (!string.IsNullOrEmpty(model))
            {
                query = query.Where(x => x.Model.Contains(model));
            }

            if (!string.IsNullOrEmpty(os))
            {
                query = query.Where(x => x.Os.Contains(os));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(x => x.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= maxPrice.Value);
            }

            if (releaseYear.HasValue)
            {
                query = query.Where(x => x.ReleaseYear == releaseYear.Value);
            }

            if (brandNames != null && brandNames.Any())
            {
                query = query.Where(m => brandNames.Contains(m.Brand.Name));
            }

            var mobiles = query.Select(m => new MobileDTO
                                                    {
                                                        Id = m.Id,
                                                        Brand = m.Brand,
                                                        Model = m.Model,
                                                        Os = m.Os,
                                                        Price = m.Price
                                                    })
                                                    .OrderBy(x => x.Id);
                                                     
            return mobiles;
        }

        public MobileDTO? GetMobile(int id)
        {
             var mobile = _db.Mobiles.Include(m => m.Brand)
                                .Where(x => x.Id == id)
                                .Select(m => new MobileDTO
                                {
                                    Id = m.Id,
                                    Brand = m.Brand,
                                    Price = m.Price,
                                    Model = m.Model,
                                    Os = m.Os
                                }).FirstOrDefault();
                                return mobile;
                                
        }

        public List<GroupedByBrandDTO> GroupedMobiles()
        {
            var brandsWithMobiles = _db.Mobiles.Include(m => m.Brand)
                                                .AsEnumerable()
                                                .GroupBy(m => m.Brand.Name)
                                                .Select(g => new GroupedByBrandDTO
                                                {
                                                    Name = g.Key,
                                                    Mobiles = g.Select(m => new MobileDTO
                                                    {
                                                        Id = m.Id,
                                                        Model = m.Model,
                                                        Price = m.Price,
                                                        Os = m.Os,
                                                    ReleaseYear = m.ReleaseYear
                                                    }).ToList()
                                                }).OrderByDescending(g => g.Mobiles.Count()).ToList();
                                                return brandsWithMobiles;
        }
        
        public bool DeleteMobile(int id)
        {   
            Mobile mobile = _db.Mobiles.Where(m => m.Id == id).FirstOrDefault();

            if (mobile != null)
            {
                _db.Mobiles.Remove(mobile);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddMobile(CreateMobileDTO mobile)
        {
            Brand brand = _db.Brands.FirstOrDefault(b => b.Id == mobile.BrandId);

            if (mobile == null || brand == null)
            {
                return false;
            }

            Mobile m = new Mobile
            {
                Model =  mobile.Model,
                Brand = brand,
                Os = mobile.Os,
                ReleaseYear = mobile.ReleaseYear,
                Price = mobile.Price
            };

            _db.Mobiles.Add(m);

            _db.SaveChanges();

            return true;
        }
    }
}