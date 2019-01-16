using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMacrocosm.Models
{
    public class SeriesItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Series")]
        public int SeriesId { get; set; }
        public Series Series { get; set; }

        public string Title { get; set; }

        [MaxLength]
        public string Description { get; set; }
        
        public List<Image> SeriesItemImages { get; set; }

        [ForeignKey("ProductionStudio")]
        public int ProductionId { get; set; }
        public ProductionStudio ProductionStudio { get; set; }

        [ForeignKey("Distributor")]
        public int DistributorId { get; set; }
        public Distributor Distributor { get; set; }

        [ForeignKey("Creator")]
        public int CreatorAuthorId { get; set; }
        public List<CreatorAuthor> CreatorAuthors { get; set; }

        public string Length { get; set; }

        [ForeignKey("Format")]
        public int FormatId { get; set; }
        public Format Format { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}
