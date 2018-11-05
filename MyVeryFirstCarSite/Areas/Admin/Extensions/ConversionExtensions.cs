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
using System.Transactions;

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

        public static async Task<IEnumerable<VehicleItemModel>> Convert(this IQueryable<VehicleItem> vehicleItems, ApplicationDbContext db)
        {
            if (vehicleItems.Count().Equals(0))
                return new List<VehicleItemModel>();

        //"vi" below is a vehicle item
        //the link query-for each vehicle item in the collection we want to create a new vehicle item model for each of the vehcle items
            return await(from vi in vehicleItems
                   select new VehicleItemModel
                   {
                       ItemId = vi.ItemId,
                       VehicleId = vi.VehicleId,
                   //below I'm geting the title for the item that matches the item id so that the text is returned not just the ID value
                       ItemTitle = db.Items.FirstOrDefault(i => i.Id.Equals(vi.ItemId)).Title,
                   //same thing for the vehicle title below...
                       VehicleTitle = db.Vehicles.FirstOrDefault(v => v.Id.Equals(vi.VehicleId)).Title
                   }).ToListAsync();
        }

        //in the below convert method I take a vehicle item and conert it in to a vehicle item model using the application
        //db context to fill the collections and use the vehicle item thats sent in to fill the item id and vehicle id properties
        //and then return the model
        public static async Task<VehicleItemModel> Convert(this VehicleItem vehicleItem, ApplicationDbContext db,
            bool addListData = true)
        {
            var model = new VehicleItemModel
            {
                ItemId = vehicleItem.ItemId,
                VehicleId = vehicleItem.VehicleId,
                Items = addListData ? await db.Items.ToListAsync() : null,
                Vehicles = addListData ? await db.Vehicles.ToListAsync() : null,
                ItemTitle = (await db.Items.FirstOrDefaultAsync( i => 
                    i.Id.Equals (vehicleItem.ItemId))).Title,
                VehicleTitle = (await db.Vehicles.FirstOrDefaultAsync (v =>
                    v.Id.Equals(vehicleItem.VehicleId))).Title
            };

            return model;
        }

        public static async Task<bool> CanChange(this VehicleItem vehicleItem, ApplicationDbContext db)
        {
            var oldVI = await db.VehicleItems.CountAsync(vi => vi.VehicleId.Equals(vehicleItem.OldVehicleId) &&
            vi.ItemId.Equals(vehicleItem.OldItemId));

            var newVI = await db.VehicleItems.CountAsync(vi => vi.VehicleId.Equals(vehicleItem.VehicleId) &&
            vi.ItemId.Equals(vehicleItem.ItemId));

            return oldVI.Equals(1) && newVI.Equals(0);
        }

        public static async Task Change(this VehicleItem vehicleItem, ApplicationDbContext db)
        {
            var oldVehicleItem = await db.VehicleItems.FirstOrDefaultAsync(
                vi => vi.VehicleId.Equals(vehicleItem.OldVehicleId) &&
                vi.ItemId.Equals(vehicleItem.OldItemId));

            var newVehicleItem = await db.VehicleItems.FirstOrDefaultAsync(
                vi => vi.VehicleId.Equals(vehicleItem.VehicleId) &&
                vi.ItemId.Equals(vehicleItem.ItemId));

            if(oldVehicleItem != null && newVehicleItem == null)
            {
                newVehicleItem = new VehicleItem
                {
                    ItemId = vehicleItem.ItemId,
                    VehicleId = vehicleItem.VehicleId
                };

                using (var transaction = new TransactionScope(
                    TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        db.VehicleItems.Remove(oldVehicleItem);
                        db.VehicleItems.Add(newVehicleItem);

                        await db.SaveChangesAsync();
                        transaction.Complete();
                    }
                    catch
                    {transaction.Dispose(); }
                }
            }
        }
    }

}