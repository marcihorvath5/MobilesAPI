using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobilesApi.Models;
using MobilesApi.Service;

namespace MobilesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MobileApiController : ControllerBase
    {
        private readonly IMobileService _ims;

        public MobileApiController(IMobileService ims)
        {
            _ims = ims;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string? model, [FromQuery] string? os, [FromQuery] int? minPrice, 
                                            [FromQuery] int? maxPrice, [FromQuery] int? releaseYear, [FromQuery] string[]? brandNames)
        {
                        if (minPrice.HasValue && maxPrice.HasValue && minPrice.Value > maxPrice.Value)
            {
                return BadRequest("Érvénytelen paraméterek");
            }


            var mobiles = _ims.GetMobiles(model, os, minPrice, maxPrice, releaseYear, brandNames);
            
            return Ok(mobiles);
        }

        [HttpGet("{id}")]
        public IActionResult GetMobile(int id)
        {
            return Ok(_ims.GetMobile(id));
        }

        [HttpGet("groupedMobiles")]
        public IActionResult GroupedMobiles()
        {
            return Ok(_ims.GroupedMobiles());
        }

        [HttpDelete]
        public IActionResult DeleteMobile(int id)
        {
            return Ok(_ims.DeleteMobile(id));
        }

        [HttpPost]
        public IActionResult PostMobile(CreateMobileDTO mobile)
        {            
            bool success = _ims.AddMobile(mobile);

            if (success)
            {
                return Ok("Sikeres feltöltés");
            }            

            else
            {
                return BadRequest("Érvénytelen készülék adatok");
            }
        }
    }
}