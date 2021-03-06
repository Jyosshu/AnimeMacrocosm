﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        public string ImagePath { get; set; }

        public string ImageCaption { get; set; }

        public string ImageDimensions { get; set; }

        public string ImageFormat { get; set; }

        public List<SeriesImage> SeriesImages { get; set; }

        public List<SeriesItemImage> SeriesItemImages { get; set; }

        //[ForeignKey("Series")]
        //public int SeriesId { get; set; }

        //[ForeignKey("SeriesItem")]
        //public int SeriesItemId { get; set; }
    }
}
