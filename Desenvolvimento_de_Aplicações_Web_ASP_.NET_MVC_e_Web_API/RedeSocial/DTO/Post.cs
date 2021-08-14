using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Subject { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
