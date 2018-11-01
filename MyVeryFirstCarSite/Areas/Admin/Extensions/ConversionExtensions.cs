using MyVeryFirstCarSite.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Collections;
using MyVeryFirstCarSite.Entities;
using MyVeryFirstCarSite.Models;
using System.Data.Entity;

namespace MyVeryFirstCarSite.Areas.Admin.Extensions
{
    public static class ConversionExtensions
    {
        //the conversion method takes an IEnumerable of vehicle and converts in to an IEnumerable of VehicleModel, 
        //we convert one collection in to another type of collection, and get data from the DB asynchronously
        public static async Task<IEnumerable<VehicleModel>> Convert(this IEnumerable<Vehicle> vehicles, ApplicationDbContext db)
        {
            if (vehicles.Count().Equals(0))
                return new List<VehicleModel>();

            var texts = await db.VehicleLinkTexts.ToListAsync();
            var types = await db.VehicleTypes.ToListAsync();

            return from v in vehicles
                   select new VehicleModel
                   {
                       Id = v.Id,
                       Title = v.Title,
                       Description = v.Description,
                       ImageUrl = v.ImageUrl,
                       VehicleLinkTextId = v.VehicleLinkTextId,
                       VehicleTypeId = v.VehicleTypeId,
                       VehicleLinkTexts = texts,
                       VehicleTypes = types
                   };
        }
            
    }
}