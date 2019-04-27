using DomesticManagement.Common.Dto;
using DomesticManagement.Common.Helpers;
using DomesticManagement.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DomesticManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly RoleManager<Role> _roleManager;
        public UserController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]

        public async Task<IActionResult> Register([FromBody] UserCredentialsDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!PasswordHelper.CheckPassword(model.Password))
            {
                return BadRequest("new password is incorrect");
            }

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = model.IsActive,
                IsPasswordForChange = true,
            };

            var oldUser = await _userManager.FindByEmailAsync(model.Email);

            if (oldUser != null)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, model.RoleName);



            if (result.Succeeded)
            {
                return Ok(user);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("error", error.Description);
            }
            return BadRequest(result.Errors);
        }
    }
}