using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MobilesApi.Models;

namespace MobilesApi.Service
{
    public interface IUserService
    {
       Task<IdentityResult> RegisterUser(CreateModel model);
    }
}