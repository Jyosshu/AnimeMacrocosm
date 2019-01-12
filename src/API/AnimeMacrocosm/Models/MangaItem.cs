using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMacrocosm.Models
{
    public class MangaItem
    {
        public MangaItem()
        {
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Series")]
        public int SeriesId { get; set; }
        public Series Series { get; set; }

        public string Title { get; set; }

        [MaxLength]
        public string Description { get; set; }

        public List<Image> MangaItemImages { get; set; }

        [ForeignKey("Distributor")]
        public int DistributorId { get; set; }
        public Distributor Distributor { get; set; }

        [ForeignKey("Creator")]
        public int CreatorAuthorId { get; set; }
        public CreatorAuthor CreatorAuthor { get; set; }

        [ForeignKey("Format")]
        public int FormatId { get; set; }
        public Format Format { get; set; }

        public int PageCount { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}
