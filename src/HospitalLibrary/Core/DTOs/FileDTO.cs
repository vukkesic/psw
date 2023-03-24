using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class FileDTO
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
