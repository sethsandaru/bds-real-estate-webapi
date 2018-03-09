using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateAPI.Entities
{
    public class Post
    {
        [Key]
        public int ID { get; set; }
        public int CategoryID { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public double Latt { get; set; }
        public double Long { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool Activate { get; set; }

        // one mapping
        public virtual Category Category { get; set; }

        // multi mapping
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Image> Images { get; set; }

        public Post()
        {
            this.Title = "";
            this.Content = "";
            this.CreatedBy = "";
            this.Phone = "";
            this.Address = "";
            this.Latt = 0;
            this.Long = 0;
            this.Activate = false;
            this.CreatedDate = DateTime.Now;
            this.Comments = new HashSet<Comment>();
            this.Images = new HashSet<Image>();
        }
    }
}