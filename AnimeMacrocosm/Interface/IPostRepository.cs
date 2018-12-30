using System;
using System.Collections.Generic;
using System.Linq;
using AnimeMacrocosm.Models;

namespace AnimeMacrocosm.Interface
{
    public interface IPostRepository
    {
        List<Post> GetPosts();
        Post GetPostById(int postId);
    }
}
