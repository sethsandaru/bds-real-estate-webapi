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
    public class CommentController : ApiController
    {
        [HttpGet]
        public IEnumerable<Comment> GetAllCommentByPostID(int PostID)
        {
            using (MasterDbContext db = new MasterDbContext())
            {
                IQueryable<Comment> qr = db.Comments.AsNoTracking();

                qr = qr.Where(x => x.PostID == PostID);

                return qr.ToList();
            }
        }

        [HttpGet]
        public PagingResult<Comment> GetByPaginate(int PostID, int take, int skip)
        {
            using (MasterDbContext db = new MasterDbContext())
            {
                IQueryable<Comment> query = db.Comments.AsNoTracking();
                query = query.Where(x => x.PostID == PostID);

                // back data
                PagingResult<Comment> data = new PagingResult<Comment>();

                // including what?
                query = query.Include("Category").Include("Comments").Include("Images");

                // get now
                data.Total = query.Count();
                data.Data = query.Skip(skip).Take(take).ToList();

                return data;
            }
        }

        [HttpPost]
        public int AddComment(Comment cmt)
        {
            if (cmt != null && cmt.Name.Length > 0 && cmt.Message.Length > 0 && cmt.PostID > 0)
            {
                using (MasterDbContext db = new MasterDbContext())
                {
                    db.Comments.Add(cmt);
                    db.SaveChanges();

                    return cmt.ID;
                }
            }

            return -1;
        }
    }
}
