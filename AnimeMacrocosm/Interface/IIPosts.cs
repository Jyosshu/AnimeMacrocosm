using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeMacrocosm.Models;

namespace AnimeMacrocosm.Interface
{
    public interface IIPosts
    {
        List<Post> GetPosts();
        Post GetPostById(int postId);
    }
}
