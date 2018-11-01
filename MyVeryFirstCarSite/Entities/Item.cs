using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyVeryFirstCarSite.Entities
{
    [Table("Item")]
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength(1024)]
        public string Url { get; set; }
        [MaxLength(1024)]
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }
        [AllowHtml]
        public string HTML { get; set; }
        [DefaultValue(0)]
        [DisplayName("Wait Days")]
        public int WaitDays { get; set; }
        public string HTMLShort {
            get
            {
                return HTML == null || HTML.Length < 50 ? HTML : HTML.Substring(0, 50);
            }
        }
        public int VehicleId { get; set; }
        public int ItemTypeId { get; set; }
        public int SectionId { get; set; }
        public int PartId { get; set; }
        public bool IsFree { get; set; }
        //used the following ICollections to allow lazy loading
        //the use of the "DisplayName" attribute is so the text that's displayed is better for the user - it's "Item Type" as opposed to "ItemTypes"
        [DisplayName("Item Type")]
        public ICollection<ItemType> ItemTypes { get; set; }
        public ICollection<Section> Sections { get; set; }
        public ICollection<Part> Parts { get; set; }

    }
}