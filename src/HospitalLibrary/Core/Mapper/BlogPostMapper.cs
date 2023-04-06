using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Mapper
{
    public class BlogPostMapper
    {
        public BlogPostMapper() { }
        public BlogPostDTO MapBlogPostToBlogPostDTO(BlogPost blogPost)
        {
            return new BlogPostDTO(blogPost.Id, blogPost.Title, blogPost.Text);
        }

        public BlogPost MapBlogPostDTOToBlogPost(BlogPostDTO blogPostDTO)
        {
            return new BlogPost(blogPostDTO.Id, blogPostDTO.Title, blogPostDTO.Text);
        }
    }
}
