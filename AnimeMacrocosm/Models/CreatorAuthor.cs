using System;
using System.ComponentModel.DataAnnotations;

namespace AnimeMacrocosm.Models
{
    public class CreatorAuthor
    {
        public CreatorAuthor()
        {
        }

        [Key]
        public int Id { get; set; }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
