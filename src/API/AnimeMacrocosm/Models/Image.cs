using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMacrocosm.Models
{
    public class Image
    {
        public Image()
        {
        }

        [Key]
        public int Id { get; set; }

        public string ImagePath { get; set; }

        [ForeignKey("AnimeItem")]
        public int AnimeItemId { get; set; }
        public AnimeItem AnimeItem { get; set; }

        [ForeignKey("MangaItem")]
        public int MangaItemId { get; set; }
        public MangaItem MangaItem { get; set; }
    }
}
