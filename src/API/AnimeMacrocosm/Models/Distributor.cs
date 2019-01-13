using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class Distributor
    {
        public Distributor()
        {
        }

        [Key]
        public int Id { get; set; }
        public string DistributorName { get; set; }
        public string Country { get; set; }

        public List<SeriesItem> SeriesItems { get; set; }
    }
}
