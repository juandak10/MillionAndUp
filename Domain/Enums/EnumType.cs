using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public class EnumType
    {

        // To find out if an account belongs to a natural person or a construction company
        public enum AccountType
        {
            Person = 1,
            Builder = 2
        }

        // To know the role of an account
        public enum RoleType
        {
            Admin = 1,
            Client = 2
        }

        // To know the type of property you are looking for
        public enum PropertyType
        {
            None = 0,
            Apartment = 1,
            House = 2,
            Farm = 3,
            Local = 4,
            Office = 5
        }

        //To know in what condition or the state of the property
        public enum ConditionType
        {
            None = 0,
            Resale = 1,
            Built = 2,
            New = 3
        }

        //To know if the property is safe or not
        public enum SecurityType
        {
            None = 0,
            Good = 1,
            Medium = 2,
            Low = 3
        }

        //To know in which area the property is located
        public enum AreaType
        {
            None = 0,
            Residential = 1,
            Urban = 2,
            Rural = 3
        }

        //To search by furnished property
        public enum WithFurnished
        {
            NotFurnished = 2,
            Furnished = 1,
            Both = 0
        }

        //To search by property with a garage
        public enum WithGarages
        {
            NotGarages = 2,
            Garages = 1,
            Both = 0
        }

        //To search by property with a swimming pool
        public enum WithSwimmingPool
        {
            NotSwimmingPool = 2,
            SwimmingPool = 1,
            Both = 0
        }

        //To search by property with a gym
        public enum WithGym
        {
            NotGym = 2,
            Gym = 1,
            Both = 0

        }

        //To search by property with a oceanfront
        public enum WithOceanfront
        {
            NotOceanfront = 2,
            Oceanfront = 1,
            Both = 0
        }

        //To search by Enabled property
        public enum EnabledProperty
        {
            NotEnabled = 2,
            Enabled = 1,
            Both = 0
        }

        //To search by Images property
        public enum WithImages
        {
            NotImages = 2,
            Images = 1,
            Both = 0
        }

        //Types of api response messages
        public enum MessageType
        {
            None = 0,
            Success = 1,
            Error = 2
        }

        //Sort property search
        public enum OrderProperty
        {
            None = 0,
            PriceMin = 1,
            PriceMax = 2,
            YearMax = 3
        }



    }
}
