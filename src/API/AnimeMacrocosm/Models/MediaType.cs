using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class MediaType
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
