using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class Format
    {
        [Key]
        public int FormatId { get; set; }
        public string FormatName { get; set; }
    }
}
