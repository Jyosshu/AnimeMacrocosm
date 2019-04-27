using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class Series
    {
        [Key]
        public int SeriesId { get; set; }
        public string Title { get; set; }

        // TODO: Simplify Series SeriesId, Title.  Create a SeriesSeriesItem table.

        public List<SeriesCreator> SeriesCreators { get; set; }

        public List<SeriesItem> SeriesItems { get; set; }

        public List<SeriesImage> SeriesImages { get; set; }
    }
}
