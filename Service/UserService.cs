using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MobilesApi.Migrations;
using MobilesApi.Models;

namespace MobilesApi.Service
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(RoleManager<IdentityRole> roleManager,
                                                 UserManager<User >userManager,
                                                 SignInManager<User> signInManager,
                                                 MobileDb db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUser(CreateModel model)
        {
            User user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                string roleName = "User";

                await _userManager.AddToRoleAsync(user,roleName);
            }

            return result;
        }
    }
}