using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.References;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    //In this class all the services associated with the trace of property are consumed
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PropertyTraceController : ControllerBase
    {
        private IPropertyTraceServices propertyTraceServices;

        //Controller
        public PropertyTraceController(IPropertyTraceServices propertyTraceServices)
        {
            this.propertyTraceServices = propertyTraceServices;
        }

        // GET: api/<PropertyTraceController>/Property/c9f60fd2-1a6a-415c-9fc2-10fb73d62b46
        //Method to obtain all the traces of a property
        [HttpGet("GetAllForProperty/{idProperty}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get(Guid? idProperty)
        {
            var propertyTraces = await propertyTraceServices.GetAllForProperty(idProperty);
            if (propertyTraces.Any()) return Ok(propertyTraces);
            return NotFound(propertyTraces);
        }

        // POST api/<PropertyTraceController>
        //Method to add a trace of property
        [HttpPost]
        [Route("Insert")]
        [Authorize(Roles = "Admin")]
        public IActionResult Post(PropertyTraceRequest propertyTraceRequest)
        {
            var responseLogic = propertyTraceServices.Insert(propertyTraceRequest);
            if (responseLogic != null) return Ok(responseLogic);
            return BadRequest(responseLogic);
        }

    }
}
