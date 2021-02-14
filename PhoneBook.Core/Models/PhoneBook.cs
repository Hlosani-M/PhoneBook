using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Core.Models
{
    public class PhoneBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
