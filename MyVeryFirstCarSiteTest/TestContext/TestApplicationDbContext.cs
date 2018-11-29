using MyVeryFirstCarSite.Entities;
using MyVeryFirstCarSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVeryFirstCarSiteTest.TestContext
{
    class TestApplicationDbContext : IApplicationDbContext
    {
        public TestApplicationDbContext()
        {
            this.Vehicles = new TestVehicleDbSet();
            this.VehicleTypes = new TestVehicleTypeDbSet();
            this.VehicleLinkTexts = new TestVehicleLinkTextDbSet();
        }
        public DbSet<Vehicle> Vehicles {get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }

        public DbSet<VehicleLinkText> VehicleLinkTexts { get; set; }

        public void Dispose()
        {
       
        }

        public void MarkAsModified(object item)
        {
          
        }

        public async Task<int> SaveChangesAsync()
        {
            return 0;
        }
    }
}
