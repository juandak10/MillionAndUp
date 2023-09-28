using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IPropertyTraceRepository
    {
        public PropertyTrace Insert(PropertyTrace @object);

        public Task<List<PropertyTrace>> GetAllForIdProperty(Guid? idProperty);

    }
}
