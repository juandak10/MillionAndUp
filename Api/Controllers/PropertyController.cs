using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    //In this class, all the services associated with the properties or dwellings are consumed.
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {

        private readonly IPropertyServices propertyLogic;

        //Controller
        public PropertyController(IPropertyServices propertyLogic)
        {
            this.propertyLogic = propertyLogic;
        }

        // GET api/<PropertyController>
        //Method to get all system properties
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var  properties = await propertyLogic.GetAll();
            if (properties.Any()) return Ok(properties);
            return NotFound(properties);
        }

        // GET api/<PropertyController>/c9f60fd2-1a6a-415c-9fc2-10fb73d62b46
        //Method to get a specific property,with detailed information
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get(Guid? id)
        {
            var property = await propertyLogic.Get(id);
            if (property != null) return Ok(property);
            return NotFound(property);
        }

        // POST api/<PropertyController>/Find
        //Method to search for properties by city, area, price, year, room number among other filters
        [HttpPost]
        [Route("Find")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Find([FromForm] FindPropertyDto findPropertyRequest)
        {
            var  properties = await propertyLogic.Find(findPropertyRequest);
            if (properties.Any()) return Ok(properties);
            return NotFound(properties);
        }

        // POST api/<PropertyController>
        //Method to add a new property
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] PropertyDto propertyRequest)
        {
            var response = await propertyLogic.Insert(propertyRequest);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        // PUT api/<PropertyController>
        //Method to update a property
        [HttpPut()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] PropertyDto propertyRequest)
        {
            var response = await propertyLogic.Update(propertyRequest);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        // DELETE api/<PropertyController>/c9f60fd2-1a6a-415c-9fc2-10fb73d62b46
        //Method to delete a property
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var response = await propertyLogic.Delete(id);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        // PATCH api/<PropertyController>/Enable
        //Method to delete a property
        [HttpPatch()]
        [Route("Enable")]
        [Authorize(Roles = "Admin")]
        public IActionResult Enable(Guid? id, bool enable)
        {
            var response = propertyLogic.UpdatePropertyEnable(id, enable);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        // PATCH api/<PropertyController>/Price
        //Method to delete a property
        [HttpPatch()]
        [Route("Price")]
        [Authorize(Roles = "Admin")]
        public IActionResult Price(Guid? id, decimal price)
        {
            var response = propertyLogic.UpdatePropertyPrice(id, price);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        // Get api/<PropertyController>
        //Method to validate a property exists to be  add, update or delete
        [HttpPost()]
        [Authorize(Roles = "Admin")]
        [Route("Validate")]
        public async Task<IActionResult> Validate([FromBody] PropertyDto propertyRequest,bool add)
        {
            var response = await propertyLogic.Validate(propertyRequest,add);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

    }
}
