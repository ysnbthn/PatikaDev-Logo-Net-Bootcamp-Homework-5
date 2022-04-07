using Domain.Entities;
using HW5.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace HW5.Repository.Concreate
{
    public class PostRepository : IPostRepository
    {
        private readonly HW5DbContext _context;

        public PostRepository(HW5DbContext context)
        {
            _context = context;
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }
    }
}
