using MyVeryFirstCarSite.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MyVeryFirstCarSite.Models
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<Vehicle> Vehicles { get; }
        DbSet<VehicleType> VehicleTypes { get; }
        DbSet<VehicleLinkText> VehicleLinkTexts { get; }

        //added Task<int> below
        Task<int> SaveChangesAsync();
        void MarkAsModified(Object item);
    }
}
