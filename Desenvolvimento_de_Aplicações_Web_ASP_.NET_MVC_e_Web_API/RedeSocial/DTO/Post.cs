using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Post
    {
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Subject { get; set; }
        public Guid Id { get; set; }
    }
}
