using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5.Repository.Abstract
{
    public interface IPostRepository
    {
        void AddPost(Post post);
        Task<IEnumerable<Post>> GetAllPostsAsync();
    }
}
