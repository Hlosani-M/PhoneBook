using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Core.Models
{
    public class Entry
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, MinimumLength = 10)]
        public string PhoneNumber { get; set; }
    }
}