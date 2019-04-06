using System.Linq;
using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.ViewModels
{
    public class GigsViewModel
    {
        public string Heading { get; set; }
        public bool ShowActions { get; set; }
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public string SearchTerm { get; set; }
    }
}