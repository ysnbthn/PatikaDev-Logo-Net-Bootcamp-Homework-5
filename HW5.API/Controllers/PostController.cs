using Domain.Entities;
using HW5.API.Models;
using HW5.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW5.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(new PostResponse { Success = true, Data = posts });
        }
    }
}
