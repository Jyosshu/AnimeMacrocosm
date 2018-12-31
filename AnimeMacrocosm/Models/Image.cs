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
    }
}
