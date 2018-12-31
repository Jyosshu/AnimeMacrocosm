using System.Collections.Generic;
using AnimeMacrocosm.Models;

namespace AnimeMacrocosm.Interface
{
    public interface IPostRepository
    {
        List<Post> GetAllPosts();
        Post GetPostById(int postId);
    }
}
