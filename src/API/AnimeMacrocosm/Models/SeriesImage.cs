using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMacrocosm.Models
{
    public class SeriesImage
    {
        [ForeignKey("Series")]
        public int SeriesId { get; set; }
        public Series Series { get; set; }

        [ForeignKey("Image")]
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
