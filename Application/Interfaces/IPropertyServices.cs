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

        public BaseResponse<PropertyDto> Insert(PropertySaveDto propertyInfoEntity);

        public BaseResponse<PropertyDto> Delete(Guid? id);

        public BaseResponse<PropertyDto> Update(PropertySaveDto propertyInfoEntity);

        public BaseResponse<PropertyDto> UpdatePropertyPrice(Guid? id, decimal price);

        public BaseResponse<PropertyDto> UpdatePropertyEnable(Guid? id, bool enable);

    }
}
