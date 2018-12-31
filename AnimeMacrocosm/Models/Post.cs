using System;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [StringLength(100)]
        public string PostTitle { get; set; }

        [StringLength(30)]
        public string PostCreator { get; set; }

        public DateTime PostDate { get; set; }

        [MaxLength]
        public string PostContent { get; set; }
    }
}
