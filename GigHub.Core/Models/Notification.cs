using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }

        [Required]
        public Gig Gig { get; set; }

        public Notification()
        {
        }

        private Notification(NotificationType type, Gig gig)
        {
            Gig = gig ?? throw new ArgumentNullException(nameof(gig));
            Type = type;
            DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            Notification notification = new Notification(NotificationType.GigUpdated, newGig)
            {
                OriginalDateTime = originalDateTime,
                OriginalVenue = originalVenue
            };

            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}