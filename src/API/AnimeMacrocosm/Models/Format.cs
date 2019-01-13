using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class Format
    {
        public Format()
        {
        }

        [Key]
        public int FormatId { get; set; }
        public string FormatName { get; set; }

        public List<SeriesItem> SeriesItems { get; set; }
    }
}
