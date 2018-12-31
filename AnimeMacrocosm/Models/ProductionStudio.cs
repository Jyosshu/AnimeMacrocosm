using System;
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
    }
}
