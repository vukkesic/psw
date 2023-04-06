using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly HospitalDbContext _context;
        public BlogPostRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(BlogPost blogPost)
        {
            _context.blogs.Add(blogPost);
            _context.SaveChanges();
        }

        public void Delete(BlogPost blogPost)
        {
            _context.blogs.Remove(blogPost);
            _context.SaveChanges();
        }

        public IEnumerable<BlogPost> GetAll()
        {
            return _context.blogs.ToList();
        }

        public BlogPost GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Update(BlogPost blogPost)
        {
            _context.Entry(blogPost).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
