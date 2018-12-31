using System.Collections.Generic;
using AnimeMacrocosm.Models;

namespace AnimeMacrocosm.Interface
{
    public interface IPostRepository
    {
        List<Post> GetPosts();
        Post GetPostById(int postId);
    }
}
