using System;
using System.Collections.Generic;

namespace AnimeMacrocosm.Models
{
    public class SeriesItem
    {
        public int SeriesItemId { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }
        
        public List<Image> SeriesItemImages { get; set; }

        public ProductionStudio ProductionStudio { get; set; }
                
        public Distributor Distributor { get; set; } // TODO: List?

        public CreatorAuthor CreatorAuthors { get; set; } // TODO: List?

        public string Length { get; set; }
                
        public Format Format { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}
