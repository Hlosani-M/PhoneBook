using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.Repository
{
    public class SqlEntryRepository : IEntryRepository
    {
        private readonly PhoneBookDbContext _db;
        List<Entry> entries;

        public SqlEntryRepository(PhoneBookDbContext db)
        {
            _db = db;
        }

        public Entry AddEntry(Entry newEntry)
        {
            _db.Add(newEntry);
            return  newEntry;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public Entry DeleteEntry(int id)
        {
            var entry = GetEntryById(id);
            if (entry != null)
            {
                _db.Entries.Remove(entry);
            }

            return entry;
        }

        public Entry GetEntryById(int? id)
        {
            return _db.Entries.Find(id);
        }

        public IEnumerable<Entry> GetAllEntries()
        {
            return from e in _db.Entries
                   orderby e.Name
                   select e;
        }
        public IEnumerable<Entry> GetEntryByName(string name)
        {
            var query = from e in _db.Entries
                        where string.IsNullOrEmpty(name) || e.Name.StartsWith(name)
                        orderby e.Name
                        select e;

            return query;
        }

        public Entry UpdateEntry(Entry updatedEntry)
        {
            var entity = _db.Entries.Attach(updatedEntry);
            entity.State = EntityState.Modified;
            return updatedEntry;
        }
    }
}
