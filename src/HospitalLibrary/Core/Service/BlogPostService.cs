using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public void Create(BlogPost blogPost)
        {
            _blogPostRepository.Create(blogPost);
        }

        public void Delete(BlogPost blogPost)
        {
            _blogPostRepository.Delete(blogPost);
        }

        public IEnumerable<BlogPost> GetAll()
        {
            return _blogPostRepository.GetAll();
        }

        public BlogPost GetById(int id)
        {
            return _blogPostRepository.GetById(id);
        }

        public void Update(BlogPost blogPost)
        {
            _blogPostRepository.Update(blogPost);
        }
    }
}
