using System;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class Genre
    {
        public Genre()
        {
        }

        [Key]
        public int Id { get; set; }

        public string GenreType { get; set; }
    }
}
