using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string Text{ get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
