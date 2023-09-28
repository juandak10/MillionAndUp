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
    //In this class all the services associated with the state are consumed
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {

        private readonly IStateServices stateLogic;

        //Controller
        public StateController(IStateServices stateLogic)
        {
            this.stateLogic = stateLogic;
        }

        // GET: api/<StateController>
        //Method to get all system states
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> GetAll()
        {
            var states = await stateLogic.GetAll();
            if (states.Any()) return Ok(states);
            return NotFound(states);
        }


        // GET: api/<CountryController>
        //Method to get all system countries
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get(Guid? id)
        {
            var state = await stateLogic.Get(id);
            if (state != null) return Ok(state);
            return NotFound(state);

        }


    }
}
