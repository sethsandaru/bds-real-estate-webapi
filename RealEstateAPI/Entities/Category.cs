using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateAPI.Entities
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public bool Activate { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Category()
        {
            this.Name = "";
            this.Activate = true;
            this.Posts = new HashSet<Post>();
        }
    }
}