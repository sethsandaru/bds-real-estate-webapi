using RealEstateAPI.Entities;
using RealEstateAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RealEstateAPI.Controllers
{
    public class PostController : ApiController
    {
        [HttpGet]
        public Post GetByID(int ID)
        {
            using (MasterDbContext db = new MasterDbContext())
            {
                IQueryable<Post> query = db.Posts.AsNoTracking();
                query = query.Where(x => x.ID == ID);

                // including what?
                query = query.Include("Category").Include("Comments").Include("Images");

                // get now
                return query.ToList().FirstOrDefault();
            }
        }


        [HttpGet]
        public PagingResult<Post> GetByPaginate(string keyword, int? categoryId, bool? activate, int take, int skip)
        {
            using (MasterDbContext db = new MasterDbContext())
            {
                IQueryable<Post> query = db.Posts.AsNoTracking();

                if (string.IsNullOrEmpty(keyword))
                {
                    keyword = "";
                }
                if (activate == null)
                {
                    activate = true;
                }

                if (categoryId != null)
                    query = query.Where(x => x.CategoryID == categoryId && x.Activate == activate && (x.Title.Contains(keyword) || x.Content.Contains(keyword) || x.CreatedBy.Contains(keyword) || x.Phone.Contains(keyword) || x.Address.Contains(keyword))).OrderByDescending(x => x.CreatedDate);
                else
                    query = query.Where(x => (x.Title.Contains(keyword) || x.Content.Contains(keyword) || x.CreatedBy.Contains(keyword) || x.Phone.Contains(keyword) || x.Address.Contains(keyword)) && x.Activate == activate).OrderByDescending(x => x.CreatedDate);

                // back data
                PagingResult<Post> data = new PagingResult<Post>();

                // including what?
                query = query.Include("Category").Include("Images");

                // get now
                data.Total = query.Count();
                data.Data = query.Skip(skip).Take(take).ToList();

                return data;
            }
        }

        private bool Validate(Post post)
        {
            if (post != null && !string.IsNullOrEmpty(post.Title) && !string.IsNullOrEmpty(post.Content))
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        public int AddOrUpdate(Post post)
        {
            if (Validate(post))
            {
                using (MasterDbContext db = new MasterDbContext())
                {
                    if (post.ID > 0)
                    {
                        db.Entry(post).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        // add
                        post.CreatedDate = DateTime.Now;
                        var imgs = post.Images;
                        post.Images = null;

                        db.Posts.Add(post);
                        db.SaveChanges();

                        if (post.ID > 0)
                        {
                            foreach (var img in imgs)
                            {
                                img.PostID = post.ID;
                                db.Images.Add(img);
                            }

                            db.SaveChanges();
                        }
                    }

                    return post.ID;
                }
            }

            return -1;
        }

        [HttpGet]
        public int ActivatePost(int postID)
        {
            using (MasterDbContext db = new MasterDbContext())
            {
                var post = db.Posts.Find(postID);
                post.Activate = true;
                post.CreatedDate = DateTime.Now;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                return post.ID;
            }
        }

        [HttpPost]
        public int Delete(int ID)
        {
            try
            {
                using (MasterDbContext db = new MasterDbContext())
                {
                    var data = db.Posts.Find(ID);
                    if (db.Entry(data).State == EntityState.Detached)
                    {
                        db.Posts.Attach(data);
                    }
                    db.Posts.Remove(data);
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
