using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateAPI.Entities
{
    public class Image
    {
        [Key]
        public int ID { get; set; }
        public int PostID { get; set; }

        public string Path { get; set; }

        public virtual Post Post { get; set; }

        public Image()
        {
            this.Path = "";
        }
    }
}