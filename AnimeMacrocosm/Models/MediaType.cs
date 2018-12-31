using System;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class MediaType
    {
        public MediaType()
        {
        }

        [Key]
        public int Id { get; set; }

        public string MediaTypeName { get; set; }
    }
}
