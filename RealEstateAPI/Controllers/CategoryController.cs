using RealEstateAPI.Entities;
using RealEstateAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RealEstateAPI.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            using (MasterDbContext db = new MasterDbContext())
            {
                return db.Categories.AsNoTracking().Where(x => x.Activate == true).ToList();
            }
        }

        [HttpPost]
        public int AddOrUpdate(Category cate)
        {
            if (cate != null)
            {
                using (MasterDbContext db = new MasterDbContext())
                {
                    if (cate.ID > 0)
                    {
                        db.Entry(cate).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.Categories.Add(cate);
                    }

                    db.SaveChanges();

                    return cate.ID;
                }
            }
            return -1;
        }

        [HttpGet]
        public int Delete(int ID)
        {
            try
            {
                using (MasterDbContext db = new MasterDbContext())
                {
                    var item = db.Categories.Find(ID);

                    if (item == null)
                    {
                        return -1;
                    }

                    item.Activate = false;

                    db.SaveChanges();

                    return ID;
                }
            }
            catch
            {

            }

            return -1;
        }
    }
}
