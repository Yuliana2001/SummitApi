using Microsoft.AspNetCore.Mvc;
using User.Api.Core;
using UserModel = User.Api.Core.User;

namespace User.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase

    {

        [HttpPost("GenerateUser")]
        public async Task<IActionResult> GenerateUser()
        {
            var business = new CreateUserBusiness();
            var user = await business.Process();
            return Ok(user);
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers(string username = null)
        {
            var business = new GetUserBusiness(null);
            var result = business.Process(username);
            if (result == null)
                return NotFound();
            return Content(result, "application/json");
        }

        [HttpPost("SaveUser")]
        public IActionResult SaveUser([FromBody] UserModel user)
        {
            var business = new SaveUserBusiness(user);
            var state = business.Process(user.FirstName, user.LastName, user.Email, user.Password, user.Username, user.Birthday);
            if (state == ServiceState.Accepted)
                return Ok();
            return BadRequest();
        }

        [HttpPost("ValidateUser")]
        public IActionResult ValidateUser([FromBody] UserModel user)
        {
            var business = new GetUserBusiness(null);
            var userJson = business.Process(user.Username);
            if (userJson == null)
                return NotFound(new { message = "Usuario no existe" });
            var dbUser = System.Text.Json.JsonSerializer.Deserialize<UserModel>(userJson);
            bool valid = PasswordHash.Validate(user.Password, dbUser, dbUser.Password);
            if (valid)
                return Ok(new { message = "Validación exitosa", username = dbUser.Username });
            return StatusCode(403, new { message = "Los datos no son correctos" });
        }


    }
}
