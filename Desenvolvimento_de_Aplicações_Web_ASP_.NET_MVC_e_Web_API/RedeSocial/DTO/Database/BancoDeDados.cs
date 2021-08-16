using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Microsoft.EntityFrameworkCore;
using static DTO.Service.PostService;

namespace DTO.Database
{
    public class BancoDeDados : DbContext/*, IBancoDeDados*/
    {

        public BancoDeDados(DbContextOptions options) : base(options) { }

        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        //public DbSet<Pessoa> Pessoa { get; set; }


        //public Post Find(Guid id)
        //{
        //    return this.Post.Find(id);
        //}

        //public Comment FindComment(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<Comment> GetComments()
        //{
        //    return this.Comment.ToList();
        //}

        //public void Remove(Guid id)
        //{
        //    var post = this.Find(id);
        //    this.Post.Remove(post);
        //}

        //public void Save(Post post)
        //{
        //    this.Post.Add(post);



    }
}
