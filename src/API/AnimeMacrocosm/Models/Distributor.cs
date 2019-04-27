using System.Collections.Generic;

namespace AnimeMacrocosm.Models
{
    public class Distributor
    {
        public int DistributorId { get; set; }
        public string DistributorName { get; set; }
        public string Country { get; set; }

        public List<SeriesItem> SeriesItems { get; set; }
    }
}
