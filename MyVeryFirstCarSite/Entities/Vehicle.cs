using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyVeryFirstCarSite.Entities
{
    public enum CC { cc500, cc800, cc900, cc1000, cc1100, cc1200, cc1300};
    public enum County { Antrim, Armagh, Carlow, Cavan, Clare, Cork, Derry, Donegal, Down, Dublin,
        Fermanagh, Galway, Kerry, Kildare, Kilkenny, Laois, Leitrim, Limerick, Longford, Louth, Mayo,
        Meath, Monaghan, Offaly, Roscommon, Sligo, Tipperary, Tyrone, Waterford, Westmeath, Wexford,
        Wicklow, Other };
    public enum Fuel { Petrol, Diesel, Hybrid, Electric, Other };


    [Table("Vehicle")]
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength(1024)]
        public string ImageUrl { get; set; }
        public int VehicleLinkTextId { get; set; }
        public int VehicleTypeId { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public CC CubicCapicity { get; set; }
        [Required]
        public Fuel FuelType { get; set; }
        public string Colour { get; set; }
        public County CountySoldFrom { get; set; }
        [Range(1980, 2018)]
        public int ManufacturerYear { get; set; }
        public int NumberOfPreviousOwners { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}