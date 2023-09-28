﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.References
{
    public class NewImageRequest
    {
        public Guid? IdProperty {  get; set; }
        public IFormFile File { get; set; }
    }
}
