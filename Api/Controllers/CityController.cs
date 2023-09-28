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
    //In this class all the services associated with the city are consumed
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityServices cityLogic;

        //Controller
        public CityController(ICityServices cityLogic)
        {
            this.cityLogic = cityLogic;
        }

        // GET: api/<CityController>
        //Method to get all system countries
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> GetAll()
        {
            var cities = await cityLogic.GetAll();
            if (cities.Any()) return Ok(cities);
            return NotFound(cities);
        }


        // GET: api/<CityController>
        //Method to get all system cities
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get(Guid? id)
        {
            var cities = await cityLogic.Get(id);
            if (cities != null) return Ok(cities);
            return NotFound(cities);
        }




    }
}
