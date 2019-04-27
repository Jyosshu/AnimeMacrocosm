using System.Collections.Generic;

namespace AnimeMacrocosm.Models
{
    public class ProductionStudio
    {
        public int ProductionStudioId { get; set; }
        public string ProductionStudioName { get; set; }
        public string Country { get; set; }

        public List<SeriesItem> SeriesItems { get; set; }
    }
}
