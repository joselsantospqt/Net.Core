using DTO.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;
using static System.Guid;

namespace DTO.Service
{
    public class PostService
    {
        //private IBancoDeDados db;
        //public PostService(IBancoDeDados bancoDeDados)
        //{
        //    db = bancoDeDados;
        //}
        private BancoDeDados db;
        public PostService(BancoDeDados bancoDeDados)
        {
            db = bancoDeDados;
        }

        public List<Post> GetAll()
        {
            var list = new List<Post>();
            list.AddRange(db.Post.ToList());

            return list;
        }
        public Post GetPost(Guid id)
        {
            var post = db.Post.Find(id);
            return post;
        }

        public Post GetAuthor(string author)
        {
            if (IsNullOrWhiteSpace(author))
            {
                return db.Post.Find(author);
            }

            return db.Post.Where(x => x.Author == author).FirstOrDefault();

        }

        public Post CreatePost(Post create)
        {

            var post = new Post();
            post = create;
            post.Id = NewGuid();
            db.Post.Add(post);
            db.SaveChanges();

            return post;
        }

        public Post UpdatePost(Guid id, Post update)
        {

            var post = db.Post.Find(id);
            post.Subject = update.Subject;
            post.UpdatedAt = DateTime.UtcNow;

            return post;
        }

        public List<Comment> GetComments(Guid postId, Guid commentId)
        {
            var list = new List<Comment>();

            if (commentId != default)
            {
                var comment = db.Comment.Find(commentId);
                list.Add(comment);
            }
            else
            {
                var comments = db.Comment.Where(x => x.PostId == postId).ToList();
                list.AddRange(comments);
            }


            return list;
        }

        public Comment CreateComment(Guid id, Comment create)
        {
            var comment = new Comment();
            comment = create;
            comment.Id = NewGuid();
            comment.PostId = id;
            db.Comment.Add(comment);
            db.SaveChanges();
            return comment;
        }

        public void DeletePost(Guid id)
        {
            var post = db.Post.Find(id);
            db.Post.Remove(post);
            db.SaveChanges();
        }

        public interface IBancoDeDados
        {

            void Save(Post post);
            void Remove(Guid post);
            Post Find(Guid id);
            Comment FindComment(Guid id);
            List<Comment> GetComments();
        }

    }
}
