using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Mapper;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        private readonly BlogPostMapper _blogPostMapper;
        public BlogsController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
            _blogPostMapper = new BlogPostMapper();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_blogPostService.GetAll());
        }

        // GET api/blogs/2
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var blog = _blogPostService.GetById(id);
            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        [Authorize(Roles = "DOCTOR")]
        [HttpPost]
        public IActionResult Create(BlogPostDTO blogPostDTO)
        {
            BlogPost blogPost = _blogPostMapper.MapBlogPostDTOToBlogPost(blogPostDTO);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _blogPostService.Create(blogPost);
            return CreatedAtAction("GetById", new { id = blogPost.Id }, blogPost);
        }
    }
}
