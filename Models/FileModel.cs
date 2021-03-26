using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace L3_DAVH_AFPE.Models
{
    public class FileModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
