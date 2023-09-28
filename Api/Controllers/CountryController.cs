using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    //In this class all the services associated with the country are consumed
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryServices countryLogic;

        //Controller
        public CountryController(ICountryServices countryLogic)
        {
            this.countryLogic = countryLogic;
        }

        // GET: api/<CountryController>
        //Method to get all system countries
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> GetAll()
        {
            var countries = await countryLogic.GetAll();
            if (countries.Any()) return Ok(countries);
            return NotFound(countries);
        }

        // GET: api/<CountryController>
        //Method to get all system countries
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get(Guid? id)
        {
            var contry = await countryLogic.Get(id);
            if (contry != null) return Ok(contry);
            return NotFound(contry);

        }



    }
}
