using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    //In this class all the services associated with the zone are consumed
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IZoneServices zoneLogic;

        //Controller
        public ZoneController(IZoneServices zoneLogic)
        {
            this.zoneLogic = zoneLogic;
        }

        // GET: api/<ZoneController>
        //Method to get all system zones
        [HttpGet]
        [Route("GetAllForCity/{idCity}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> GetAllForCity(Guid? idCity)
        {
            var zones = await zoneLogic.GetAllForCity(idCity);
            if (zones.Any()) return Ok(zones);
            return NotFound(zones);
        }

        // GET: api/<ZoneController>
        //Method to get all system zones
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get(Guid? id)
        {
            var zone = await zoneLogic.Get(id);
            if (zone != null) return Ok(zone);
            return NotFound(zone);
        }

        // GET: api/<ZoneController>
        //Method to get all system zones
        [HttpGet]
        [Route("GetInfo/{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> GetInfo(Guid? id)
        {
            var zone = await zoneLogic.GetInfo(id);
            if (zone != null) return Ok(zone);
            return NotFound(zone);
        }



    }
}
