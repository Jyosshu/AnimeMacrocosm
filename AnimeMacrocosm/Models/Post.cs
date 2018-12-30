using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

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
