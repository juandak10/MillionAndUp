using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Persistence.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    //This class is a repository that connects us to the database
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private MillionAndUpContext millionAndUpContext;

        public PropertyTraceRepository(MillionAndUpContext millionAndUpContext)
        {
            this.millionAndUpContext = millionAndUpContext;
        }


        //Add trace of property from database
        public PropertyTrace Insert(PropertyTrace propertyTraceInsert)
        {

            using (var dbContextTransaction = millionAndUpContext.Database.BeginTransaction())
            {
                try
                {
                    var date = DateTime.Now;
                    propertyTraceInsert.Create = date;

                    var properties = millionAndUpContext.Properties.Where(x => x.Id == propertyTraceInsert.PropertyId).ToList();

                    if (properties != null && properties.Any())
                    {
                        var property = properties.FirstOrDefault();
                        if (property != null)
                        {
                            property.OwnerId = propertyTraceInsert.OwnerNewId;
                            property.Price = propertyTraceInsert.Value;
                            property.Update = date;
                            propertyTraceInsert.Property = property;
                            millionAndUpContext.SaveChanges();
                            var propertyTrace = millionAndUpContext.PropertyTraces.Add(propertyTraceInsert);
                            millionAndUpContext.SaveChanges();
                            dbContextTransaction.Commit();
                            return propertyTrace.Entity;
                        }

                    }

                    dbContextTransaction.Rollback();
                    return null;

                }
                catch
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }

            return null;
        }


        //Get all traces for property from database
        public async Task<List<PropertyTrace>> GetAllForIdProperty(Guid? idProperty)
        {
            return await millionAndUpContext.PropertyTraces.Where(x => x.PropertyId == idProperty).ToListAsync();
        }

    }
}
