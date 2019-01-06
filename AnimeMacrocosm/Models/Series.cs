﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class Series
    {
        public Series()
        {
        }

        [Key]
        public int SeriesId { get; set; }

        public string Title { get; set; }

        public List<SeriesCreator> SeriesCreators { get; set; }
    }
}
