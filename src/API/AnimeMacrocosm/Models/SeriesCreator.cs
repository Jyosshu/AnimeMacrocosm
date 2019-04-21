using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMacrocosm.Models
{
    public class SeriesCreator
    {
        [ForeignKey("Series")]
        public int SeriesId { get; set; }
        public Series Series { get; set; }

        [ForeignKey("CreatorAuthor")]
        public int CreatorId { get; set; }
        public CreatorAuthor CreatorAuthor { get; set; }
    }
}
