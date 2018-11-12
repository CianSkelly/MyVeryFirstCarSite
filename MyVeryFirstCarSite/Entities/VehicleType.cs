using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyVeryFirstCarSite.Entities
{
    [Table("VehicleType")]
    public class VehicleType
    {
        private string title;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Title {
            get { return Make + " " + this.Model; }
            set { this.title = this.Make + " " + this.Model; } }
        [MaxLength(25)]
        [Required]
        public string Make { get; set; }
        [MaxLength(25)]
        [Required]
        public string Model { get; set; }

        public virtual ICollection <Vehicle> Vehicles { get; set; }

    }
}