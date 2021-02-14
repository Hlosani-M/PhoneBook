using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PhoneBook.Core.Models;
using PhoneBook.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IEntryRepository _entryRepository;

        [BindProperty(SupportsGet = true)]
        public  Entry Entry { get; set; }
        public List<Entry> Entries { get; set; }
        [TempData]
        public string Message { get; set; }
        [TempData]
        public string SearchMessage { get; set; }
        [TempData]
        public bool ShowAllEntries { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public bool JustEditted { get; set; }
        public string FormTitle { get; set; } = "Add New Entry";
        public IndexModel(ILogger<IndexModel> logger, IEntryRepository entryRepository)
        {
            _logger = logger;
            _entryRepository = entryRepository;
        }

        public IActionResult OnGet(int? entryId, bool? edit)
        {
            Entry = null;
            ShowAllEntries = true;

            if (edit.HasValue && edit.Value && !JustEditted)
            {
                Entry = _entryRepository.GetEntryById(entryId);
                FormTitle = $"Edit {Entry.Name}'s Entry.";
                ShowAllEntries = false;
            }

            if (SearchTerm != null)
            {
                SearchMessage = null;
                TempData.Save();

                Entries = _entryRepository.GetEntryByName(SearchTerm).ToList();
                if (Entries.Count == 0) SearchMessage = "No Entries found matching your search";

                return Page();
            }

            Entries = _entryRepository.GetAllEntries().ToList();
            if (Entries.Count == 0) SearchMessage = "No Entries stored in the database, add entry.";

            return Page();
        }
        public IActionResult OnPost()
        {
            ModelState.Remove("Entry.Id");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (IsDuplicate(Entry))
            {
                Message = $"{Entry.PhoneNumber} already exists";
                return RedirectToPage();
            }

            if (Entry.Id > 0)
            {
                _entryRepository.UpdateEntry(Entry);
                Message = $"{Entry.Name}'s entry has been updated.";
                _entryRepository.Commit();
            }
            else
            {
                Entry = _entryRepository.AddEntry(Entry);
                _entryRepository.Commit();

                Entries = _entryRepository.GetAllEntries().ToList();
                Message = $"Entry Saved. {Entries.Count} entries in total";

                //Clear 'SearchMessage' TempData
                SearchMessage = null;
                TempData.Save();
            }
            
            if (RouteData.Values.ContainsKey("edit"))
            {
                RouteData.Values.Remove("edit");
                Entry = null;
                JustEditted = true;
            }
            return RedirectToPage(new { edit = false });
        }

        public IActionResult OnPostDelete(int entryId)
        {
            var entry = _entryRepository.GetEntryById(entryId);
            if (entry == null)
            {
                SearchMessage = "Entry not found.";
                return Page();
            }

            Entry = _entryRepository.DeleteEntry(entryId);
            _entryRepository.Commit();

            SearchMessage = $"{Entry.Name}'s entry deleted successfully";

            return RedirectToPage();
        }

        public bool IsDuplicate(Entry entry)
        {
            var entries = _entryRepository.GetAllEntries();

            var result = from e in entries
                         where e.PhoneNumber == entry.PhoneNumber
                         select e;

            if (result.Count() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
