using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class BlogPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public BlogPostDTO() { }

        public BlogPostDTO(int id, string title, string text)
        {
            Id = id;
            Title = title;
            Text = text;
        }
    }
}
