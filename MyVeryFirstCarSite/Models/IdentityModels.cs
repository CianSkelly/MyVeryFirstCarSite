﻿using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyVeryFirstCarSite.Entities;

namespace MyVeryFirstCarSite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        //the following property specifies if a user is active or not
        public bool IsActive { get; set; }
        public DateTime Registered { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Section> Sections { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleLinkText> VehicleLinkTexts { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<VehicleItem> VehicleItems { get; set; }
        public DbSet<SubscriptionVehicle> SubscriptionVehicles { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        public System.Data.Entity.DbSet<MyVeryFirstCarSite.Areas.Admin.Models.VehicleModel> VehicleModels { get; set; }
    }
}