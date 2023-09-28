using Domain.Dtos;
using Domain.Entities;
using Domain.References;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPropertyImageServices
    {
        public Task<PropertyImage> Get(Guid? id);

        public BaseResponse<PropertyImage> New(NewImageRequest newImageRequest);

        public Task<List<PropertyImageBasicDto>> GetAllForProperty(Guid? idProperty);

        public Task<string> GetFirstForProperty(Guid? idProperty);

        public Task<BaseResponse<PropertyImage>> UpdatePropertyImageEnable(Guid? id, bool enable);


    }
}
