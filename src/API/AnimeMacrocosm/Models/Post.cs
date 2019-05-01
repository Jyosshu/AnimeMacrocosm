using System;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [StringLength(100)]
        public string PostTitle { get; set; }

        public DateTime PostDate { get; set; }

        [MaxLength]
        public string PostContent { get; set; }

        public User User { get; set; }
    }
}
