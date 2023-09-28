using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IPropertyRepository
    {
        public Task<Property> Delete(Guid? id);

        public Task<Property> Get(Guid? id);

        public Task<List<Property>> GetAll();

        public Property Insert(Property @object);

        public Task<Property> Update(Property @object);

        public Task<Property> UpdatePrice(Guid? id, decimal price);

        public Task<Property> UpdateEnable(Guid? id, bool enable);

        public Task<List<Property>> GetAllForZones(List<Guid> isZones);

    }
}
