using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Infrastructure
{
    public interface IFireBaseServices
    {
        public Task<string> UpLoadImage(Stream stream, string fileName, IConfiguration config);

        public bool IsImage(string image);
    }
}
