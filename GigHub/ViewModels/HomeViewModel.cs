using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.ViewModels
{
    public class HomeViewModel
    {
        public string Heading { get; set; }
        public bool ShowActions { get; set; }
        public IEnumerable<Gig> UpcomingGigs { get; set; }
    }
}