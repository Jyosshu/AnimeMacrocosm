using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("CreatorAuthor")]
        public int CreatorAuthorId { get; set; }
    }
}
