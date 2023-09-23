using Application.Interfaces;
using Domain.References;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices accountServices;
        private readonly IConfiguration config;

        public AccountController(IAccountServices accountServices, IConfiguration config)
        {
            this.accountServices = accountServices;
            this.config = config;
        }


        // GET api/<AccountController>
        //Method to get a specific logged account
        [HttpGet]
        [Route("Logged")]
        [Authorize(Roles = "Admin,Client")]
        public IActionResult Logged()
        {
            string srtoken = string.Empty;

            //configExtensions.TryRetrieveToken(Request, out srtoken);

            //var account = configExtensions.GetToken(srtoken);
            //if (account != null && !string.IsNullOrEmpty(account.Email)) account = accountServices.Get(account.Id).Result;

            //if (account != null && !string.IsNullOrEmpty(account.Email))
            return Ok();
            //return NotFound(null);
        }

        // POST api/<AccountController>
        //In this method an account is validated for login and a jwt token is generated
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {

            var response = await accountServices.GetAccountLogin(loginRequest);

            if (response != null && response.Data != null && !string.IsNullOrEmpty(response.Token))
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}
