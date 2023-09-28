using Application.Interfaces;
using AutoMapper;
using Domain.References;
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
    //In this class all the services associated with the image of property are consumed
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImageController : ControllerBase
    {
        private readonly IPropertyImageServices propertyImageLogic;
        
        //Controller
        public PropertyImageController(IPropertyImageServices propertyImageLogic)
        {
            this.propertyImageLogic = propertyImageLogic;
        }


        //GET: api/<PropertyImageController>/Property/c9f60fd2-1a6a-415c-9fc2-10fb73d62b46
        //Method to obtain all the images of a property
        [HttpGet("GetAllForProperty/{idProperty}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> GetAllForProperty(Guid? idProperty)
        {
            var propertyImages = await propertyImageLogic.GetAllForProperty(idProperty);
            if (propertyImages.Any()) return Ok(propertyImages);
            return NotFound(propertyImages);
        }

        //GET: api/<PropertyImageController>/Property/c9f60fd2-1a6a-415c-9fc2-10fb73d62b46
        //Method to obtain all the images of a property
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Get(Guid? id)
        {
            var propertyImage = await propertyImageLogic.Get(id);
            if (propertyImage != null) return Ok(propertyImage);
            return NotFound(propertyImage);
        }

        //POST api/<PropertyImageController>
        //Method to add a image of property
        [HttpPost]
        [Route("Insert")]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromForm] NewImageRequest newImageRequest)
        {
            var responseLogic = propertyImageLogic.New(newImageRequest);
            if (responseLogic != null) return Ok(responseLogic);
            return BadRequest(responseLogic);
        }

        //PATCH api/<PropertyImageController>/Enable
        //Method to enable a image of property
        [HttpPatch()]
        [Route("Enable")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Enable(Guid? id, bool enable)
        {
            var response = await propertyImageLogic.UpdatePropertyImageEnable(id, enable);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

    }
}
