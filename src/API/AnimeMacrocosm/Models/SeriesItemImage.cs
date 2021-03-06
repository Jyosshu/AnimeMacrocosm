﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMacrocosm.Models
{
    public class SeriesItemImage
    {
        [ForeignKey("SeriesItem")]
        public int SeriesItemId { get; set; }
        public SeriesItem SeriesItem { get; set; }

        [ForeignKey("Image")]
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
