using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobilesApi.Migrations;
using MobilesApi.Models;
using MobilesApi.Service;

namespace MobilesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _iUserService;

        public UserApiController(IUserService iUserService)
        {
            _iUserService = iUserService;
        }
    
    
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] CreateModel user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Érvénytelen adatok");
        }

        IdentityResult result = await _iUserService.RegisterUser(user);
        if (result.Succeeded)
        {
            return Ok("Felhasználó regisztrálva");
        }

        return BadRequest(result.Errors);
    }
    }
}