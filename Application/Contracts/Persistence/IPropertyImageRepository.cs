using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IPropertyImageRepository
    {
        public Task<PropertyImage> Get(Guid? id);

        public PropertyImage Insert(PropertyImage @object);

        public Task<PropertyImage> UpdateEnable(Guid? id, bool enable);

        public Task<PropertyImage> GetFirstForIdProperty(Guid? idProperty);

        public Task<List<PropertyImage>> GetAllForIdProperty(Guid? idProperty);

    }
}
