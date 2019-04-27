using System;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        public string GenreType { get; set; }
    }
}
