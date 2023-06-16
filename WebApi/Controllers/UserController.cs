using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UserController : MainController
    {
        private readonly IUserService _userService;
        private readonly IClaimService _claimService;
        public UserController(IUserService userService, IClaimService claimService)
        {
            _userService = userService;
            _claimService = claimService;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(string email,string password)
        {
         bool isRegister= await  _userService.RegisterAsync(email, password);
            if(!isRegister)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
