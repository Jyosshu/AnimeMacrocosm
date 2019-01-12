using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class ProductionStudio
    {
        public ProductionStudio()
        {
        }

        [Key]
        public int Id { get; set; }
        public string ProductionStudioName { get; set; }
        public string Country { get; set; }

        public List<AnimeItem> AnimeItems { get; set; }
        public List<MangaItem> MangaItems { get; set; }
    }
}
