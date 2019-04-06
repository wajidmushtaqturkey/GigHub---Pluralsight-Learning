using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GigHub.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }

        public ApplicationUser()
        {
            Followers = new Collection<Following>();
            Followees = new Collection<Following>();
            UserNotifications = new Collection<UserNotification>();
        }

        //public async Task<IdentityResult> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        //{
        //    var identity = new ClaimsIdentity();
        //    identity.AddClaim(new Claim(ClaimTypes.Name, Name));
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.AddClaimsAsync(user: this, claims: identity);
        //    // Add custom user claims here 
        //    return userIdentity;
        //}

        //public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    if (user == null) throw new ArgumentNullException(nameof(user));

        //    return await user.CreateAsync(user);
        //}

        public void Notify(Notification notification)
        {
            var userNotification = new UserNotification(this, notification);

            UserNotifications.Add(userNotification);
        }
    }
}