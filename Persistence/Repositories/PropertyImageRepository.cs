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
    public class PropertyImageRepository : IPropertyImageRepository
    {

        private readonly MillionAndUpContext millionAndUpContext;

        public PropertyImageRepository(MillionAndUpContext millionAndUpContext)
        {
            this.millionAndUpContext = millionAndUpContext;
        }

        //Get image of property from database
        public  async Task<PropertyImage> Get(Guid? id)
        {
            return  await millionAndUpContext.PropertyImages.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Add image of property from database
        public  PropertyImage Insert(PropertyImage @object)
        {
            var propertyImage = millionAndUpContext.PropertyImages.Add(@object).Entity;
            millionAndUpContext.SaveChanges();
            return propertyImage;
        }


        //Update enable a image of property from database
        public async Task<PropertyImage> UpdateEnable(Guid? id, bool enable)
        {
            var propertyImage = await Get(id);
            propertyImage.Enabled = enable;
            millionAndUpContext.SaveChanges();
            return propertyImage;
        }

        //Get image for property from database
        public async Task<PropertyImage> GetFirstForIdProperty(Guid? idProperty)
        {
            return await millionAndUpContext.PropertyImages.Where(x => x.PropertyId == idProperty && x.Enabled.Value).FirstOrDefaultAsync();
        }

        //Get all images for property from database
        public async Task<List<PropertyImage>> GetAllForIdProperty(Guid? idProperty)
        {
            return await millionAndUpContext.PropertyImages.Where(x => x.PropertyId == idProperty && x.Enabled.Value).ToListAsync();
        }

    }
}
