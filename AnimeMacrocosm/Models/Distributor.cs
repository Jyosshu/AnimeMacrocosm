using System;
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
    }
}
