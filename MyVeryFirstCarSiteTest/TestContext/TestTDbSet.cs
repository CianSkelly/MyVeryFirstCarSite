using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyVeryFirstCarSite.Entities;
using MyVeryFirstCarSite.Models;

namespace MyVeryFirstCarSiteTest.TestContext
{

    class TestVehicleDbSet : TestDbSet<Vehicle>
    {
        public override Vehicle Find(params object[] keyValues)
        {
            return this.SingleOrDefault(v => v.Id == (int)keyValues.Single());
        }

        public override Task<Vehicle> FindAsync(params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }

        public override Task<Vehicle> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }
    }

    class TestVehicleTypeDbSet : TestDbSet<VehicleType>
    {
        public override VehicleType Find(params object[] keyValues)
        {
            return this.SingleOrDefault(v => v.Id == (int)keyValues.Single());
        }

        public override Task<VehicleType> FindAsync(params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }

        public override Task<VehicleType> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }
    }

    class TestVehicleLinkTextDbSet : TestDbSet<VehicleLinkText>
    {
        public override VehicleLinkText Find(params object[] keyValues)
        {
            return this.SingleOrDefault(v => v.Id == (int)keyValues.Single());
        }

        public override Task<VehicleLinkText> FindAsync(params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }

        public override Task<VehicleLinkText> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }
    }
}
