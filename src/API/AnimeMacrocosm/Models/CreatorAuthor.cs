
namespace AnimeMacrocosm.Models
{
    public class CreatorAuthor
    {
        public int CreatorAuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }        
    }
}
