using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnimeMacrocosm.Models;

namespace AnimeMacrocosm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // GET: api/Posts
        [HttpGet]
        public List<Post> GetPosts()
        {
            List<Post> posts = new List<Post>();
            return posts;
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "Get")]
        public string GetPostById(int id)
        {
            return "value";
        }

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
