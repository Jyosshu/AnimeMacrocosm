using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class ProductionStudio
    {
        [Key]
        public int ProductionStudioId { get; set; }
        public string ProductionStudioName { get; set; }
        public string Country { get; set; }

        public List<SeriesItem> SeriesItems { get; set; }
    }
}
