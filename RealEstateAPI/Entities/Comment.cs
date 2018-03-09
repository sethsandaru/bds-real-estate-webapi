using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateAPI.Entities
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        public int PostID { get; set; }

        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Post Post { get; set; }

        public Comment()
        {
            this.Name = "";
            this.Message = "";
            this.CreatedDate = DateTime.Now;
        }
    }
}