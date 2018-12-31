using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AnimeMacrocosm.Interface;

namespace AnimeMacrocosm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: api/Posts
        [HttpGet]
        public IActionResult GetPosts() => Ok(_postRepository.GetPosts().OrderBy(i => i.PostId));


        // GET: api/Posts/5
        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public IActionResult GetPostById(int id) => Ok(_postRepository.GetPostById(id));
        

        // POST: api/Posts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
