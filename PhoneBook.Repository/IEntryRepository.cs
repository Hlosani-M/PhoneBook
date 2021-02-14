using PhoneBook.Core.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
    public interface IEntryRepository
    {
        IEnumerable<Entry> GetAllEntries();
        IEnumerable<Entry> GetEntryByName(string name);
        Entry AddEntry(Entry newEntry);
        Entry DeleteEntry(int id);
        Entry GetEntryById(int? entryId);
        Entry UpdateEntry(Entry updatedEntry);
        int Commit();
    }
}
