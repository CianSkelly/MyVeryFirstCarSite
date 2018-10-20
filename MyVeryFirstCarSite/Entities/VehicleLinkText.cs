using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyVeryFirstCarSite.Entities
{
    [Table("VehicleLinkText")]
    public class VehicleLinkText
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(25)]
        [Required]
        public string Title { get; set; }
    }
}