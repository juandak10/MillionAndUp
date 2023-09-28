using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Domain.References;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Tools;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices accountServices;
        private readonly IConfiguration config;
        private readonly JwtToken jwtToken;

        public AccountController(IAccountServices accountServices, IConfiguration config)
        {
            this.accountServices = accountServices;
            this.config = config;
            jwtToken = new JwtToken(this.config);
        }

        // POST api/<AccountController>
        //In this method an account is validated for login and a jwt token is generated
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await accountServices.GetAccountLogin(loginRequest);
            if (response != null && response.Data != null && !string.IsNullOrEmpty(response.Token)) return Ok(response);
            return BadRequest(response);
        }

        // GET api/<AccountController>
        //Method to get a specific logged account
        [HttpGet]
        [Route("Logged")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Logged()
        {
            string srtoken = string.Empty;
            jwtToken.TryRetrieveToken(Request, out srtoken);

            var account = await accountServices.GetForToken(srtoken);

            if (account != null && !string.IsNullOrEmpty(account.Email)) return Ok(account);
            return NotFound(null);
        }

        // GET api/<AccountController>
        //Method to get all accounts
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await accountServices.GetAll();
            if (accounts != null && accounts.Any()) return Ok(accounts);
            return NotFound(null);
        }

        // GET api/<AccountController>
        //Method to get all accounts
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get(Guid? id)
        {
            var account = await accountServices.Get(id);
            if (account != null) return Ok(account);
            return NotFound(null);
        }

        // DELETE api/<AccountController>
        //Method delete account
        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var response = await accountServices.Delete(id);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        // POST api/<AccountController>
        //Method insert account
        [HttpPost]
        [Route("Insert")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Insert(AccountDto account)
        {
            var response = await accountServices.Insert(account);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        // PUT api/<AccountController>
        //Method insert account
        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Update(AccountDto account)
        {
            var response = await accountServices.Update(account);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

    }
}
