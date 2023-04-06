using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IBlogPostService
    {
        IEnumerable<BlogPost> GetAll();
        BlogPost GetById(int id);
        void Create(BlogPost blogPost);
        void Update(BlogPost blogPost);
        void Delete(BlogPost blogPost);
    }
}
