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
        public Property Delete(Guid? id);

        public Task<Property> Get(Guid? id);

        public Property GetNotAsync(Guid? id);

        public Task<List<Property>> GetAll();

        public List<Property> GetAllNotAsync();

        public Property Insert(Property @object);

        public Property Update(Property @object);

        public Property UpdatePrice(Guid? id, decimal price);

        public Property UpdateEnable(Guid? id, bool enable);

        public Task<List<Property>> GetAllForZones(List<Guid> isZones);

    }
}
