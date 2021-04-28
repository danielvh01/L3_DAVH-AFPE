using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace L3_DAVH_AFPE.Models
{
    public class FileModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
