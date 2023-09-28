using Domain.Dtos;
using Domain.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPropertyServices
    {
        public Task<List<PropertyBasicDto>> Find(FindPropertyDto find);

        public Task<PropertyDto> Get(Guid? id);

        public Task<List<PropertyBasicDto>> GetAllForCity(Guid? idCity);

        public Task<List<PropertyDto>> GetAll();

        public Task<BaseResponse<PropertyDto>> Insert(PropertyDto propertyInfoEntity);

        public Task<BaseResponse<PropertyDto>> Delete(Guid? id);

        public Task<BaseResponse<PropertyDto>> Update(PropertyDto propertyInfoEntity);

        public BaseResponse<PropertyDto> UpdatePropertyPrice(Guid? id, decimal price);
        public BaseResponse<PropertyDto> UpdatePropertyEnable(Guid? id, bool enable);

        public Task<BaseResponse<PropertyDto>> Validate(PropertyDto propertyInfoEntity, bool add);
    }
}
