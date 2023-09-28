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
    public class PropertyRepository : IPropertyRepository
    {

        private readonly MillionAndUpContext millionAndUpContext;

        public PropertyRepository(MillionAndUpContext millionAndUpContext)
        {
            this.millionAndUpContext = millionAndUpContext;
        }

        //Delete property from database
        public  async Task<Property> Delete(Guid? id)
        {
            var property = new Property();
            using (var dbContextTransaction = millionAndUpContext.Database.BeginTransaction())
            {

                try
                {
                    property = await Get(id);

                    var images = millionAndUpContext.PropertyImages.Where(x => x.PropertyId == property.Id).ToList();
                    images.ForEach(x=> millionAndUpContext.PropertyImages.Remove(x));

                    var traces = millionAndUpContext.PropertyTraces.Where(x => x.PropertyId == property.Id).ToList();
                    traces.ForEach(x => millionAndUpContext.PropertyTraces.Remove(x));

                    property = millionAndUpContext.Properties.Remove(property).Entity;

                    millionAndUpContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch
                {
                    dbContextTransaction.Rollback();
                    property = null;
                }
            }

            return property;
        }

        //Get property from database
        public async Task<Property> Get(Guid? id)
        {
            return await millionAndUpContext.Properties.Where(x=> x.Id == id).FirstOrDefaultAsync();
        }

        //Get all properties from database
        public async Task<List<Property>> GetAll()
        {
            return await millionAndUpContext.Properties.ToListAsync();
        }

        //Add property from database
        public  Property Insert(Property @object)
        {
            var date = DateTime.Now;
            @object.Create = date;
            @object.Update = date;
            var property = millionAndUpContext.Properties.Add(@object).Entity;
            millionAndUpContext.SaveChanges();
            return property;
        }

        //Update property from database
        public  async Task<Property> Update(Property @object)
        {
            var property = await  Get(@object.Id);
            property.Update = DateTime.Now;
            property.Address = @object.Address;
            property.AreaType = @object.AreaType;
            property.Bathrooms = @object.Bathrooms;
            property.ConditionType = @object.ConditionType;
            property.Description = @object.Description;
            property.Elevator = @object.Elevator;
            property.Enabled = @object.Enabled;
            property.Floor = @object.Floor;
            property.Furnished = @object.Furnished;
            property.Garages = @object.Garages;
            property.Gym = @object.Gym;
            property.OwnerId = @object.OwnerId;
            property.ZoneId = @object.ZoneId;
            property.Latitude = @object.Latitude;
            property.Longitude = @object.Longitude;
            property.Name = @object.Name;
            property.Oceanfront = @object.Oceanfront;
            property.Price = @object.Price;
            property.PropertyType = @object.PropertyType;
            property.Rooms = @object.Rooms;
            property.SecurityType = @object.SecurityType;
            property.SwimmingPool = @object.SwimmingPool;
            property.Year = @object.Year;
            millionAndUpContext.SaveChanges();
            return property;
        }

        //Update price of property from database
        public async Task<Property> UpdatePrice(Guid? id, decimal price)
        {
            var property = await Get(id);
            property.Update = DateTime.Now;
            property.Price = price;
            millionAndUpContext.SaveChanges();
            return property;
        }

        //Update property from database
        public async Task<Property> UpdateEnable(Guid? id, bool enable)
        {
            var property = await Get(id);
            property.Update = DateTime.Now;
            property.Enabled = enable;
            millionAndUpContext.SaveChanges();
            return property;
        }

        //Get all properties for zones from database
        public async Task<List<Property>> GetAllForZones(List<Guid> isZones)
        {
            return await millionAndUpContext.Properties.Where(x => isZones.Contains((Guid)x.ZoneId)).ToListAsync();
        }

    }
}
