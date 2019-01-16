using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class CreatorAuthor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<SeriesCreator> SeriesCreators { get; set; }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }        
    }
}
