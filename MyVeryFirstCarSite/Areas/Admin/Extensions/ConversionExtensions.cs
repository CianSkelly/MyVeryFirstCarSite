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

        public static async Task<VehicleModel> Convert(this Vehicle vehicle, ApplicationDbContext db)
        {

            var text = await db.VehicleLinkTexts.FirstOrDefaultAsync(v => v.Id.Equals(vehicle.VehicleLinkTextId));
            var type = await db.VehicleTypes.FirstOrDefaultAsync(v => v.Id.Equals(vehicle.VehicleTypeId));

            var model = new VehicleModel
                   {
                       Id = vehicle.Id,
                       Title = vehicle.Title,
                       Description = vehicle.Description,
                       ImageUrl = vehicle.ImageUrl,
                       VehicleLinkTextId = vehicle.VehicleLinkTextId,
                       VehicleTypeId = vehicle.VehicleTypeId,
                       VehicleLinkTexts = new List<VehicleLinkText>(),
                       VehicleTypes = new List<VehicleType>()
                   };
            model.VehicleLinkTexts.Add(text);
            model.VehicleTypes.Add(type);

            return model;
        }


    }

}