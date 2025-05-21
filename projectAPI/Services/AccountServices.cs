using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using projectAPI.Core.Domain.IdentityEntities;
using projectAPI.Core.DTO;
using projectAPI.Core.ServiceCotract;
using projectAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Services
{
    public class AccountServices:IAccountServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration; 
        private readonly IJwtServices jwtService;

        public AccountServices(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              RoleManager<IdentityRole> roleManager,
                               IConfiguration configuration,
                              IJwtServices jwtService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtService = jwtService;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }

        public async Task<OperationResult> AddRoleAsync(string role)
        {
            if (await roleManager.RoleExistsAsync(role))
                return new OperationResult() { success = false, message = "Role already Exits" };
            var result = await roleManager.CreateAsync(new IdentityRole(role));
            if (result.Succeeded)
                return new OperationResult() { success = true, message = "Role Added successfully" };
            return new OperationResult() { success = false, message = string.Join(",",result.Errors.Select(x=>x.Description))};
        }

        public async Task<OperationResult> AssignUserRole(UserModelRoleDTO model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return new OperationResult() { success = false,message="User Not Found",statusCode=System.Net.HttpStatusCode.NotFound };
            var result =await userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return new OperationResult() { success = true, message = "Role assigned successfully" ,statusCode=System.Net.HttpStatusCode.OK};
            }
            return new OperationResult() { success = false, message = string.Join(",", result.Errors.Select(x => x.Description)) ,statusCode=System.Net.HttpStatusCode.BadRequest};
        }

        public async Task<AuthenticationRespose> LoginAsync(LoginDto loginDTO)
        {
            var user = await userManager.FindByNameAsync(loginDTO.name);
            if (user == null || !await userManager.CheckPasswordAsync(user, loginDTO.password))
            {
                // throw new Exception("Invalid username or password.");
                return new AuthenticationRespose() { error = "Invalid username or password." };
            }
          
            await signInManager.SignInAsync(user, false);
            return await jwtService.CreateTokenAsync(user);
        }
        public async Task<AuthenticationRespose> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                UserName = registerDTO.name,
                Email = registerDTO.email
            };

            var result = await userManager.CreateAsync(user, registerDTO.password);
            if (!result.Succeeded)
            {
                // throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
                return new AuthenticationRespose() { error = string.Join(",", result.Errors.Select(e => e.Description)) };
            }

            await signInManager.SignInAsync(user, false);

            return await jwtService.CreateTokenAsync(user);
        }
    }
}
