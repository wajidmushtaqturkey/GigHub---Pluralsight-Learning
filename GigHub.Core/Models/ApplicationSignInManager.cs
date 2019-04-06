using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GigHub.Core.Models
{
    public class ApplicationSignInManager<TUser> : SignInManager<TUser> where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;

        public ApplicationSignInManager(UserManager<TUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<TUser>> logger, IAuthenticationSchemeProvider authenticationScheme, ApplicationDbContext dbContext)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, authenticationScheme)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public override Task<SignInResult> PasswordSignInAsync(TUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        public override Task<bool> CanSignInAsync(TUser user)
        {
            return base.CanSignInAsync(user);
        }

        public override Task<bool> IsTwoFactorClientRememberedAsync(TUser user)
        {
            return base.IsTwoFactorClientRememberedAsync(user);
        }

        public override bool IsSignedIn(ClaimsPrincipal principal)
        {
            return base.IsSignedIn(principal);
        }

        public override Task<SignInResult> CheckPasswordSignInAsync(TUser user, string password, bool lockoutOnFailure)
        {
            return base.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }

        public override Task<SignInResult> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberClient)
        {
            return base.TwoFactorSignInAsync(provider, code, isPersistent, rememberClient);
        }

        public override async Task SignOutAsync()
        {
            await base.SignOutAsync();
        }

        public override Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null)
        {
            return base.SignInAsync(user, isPersistent, authenticationMethod);
        }

        public async Task<IdentityResult> CreateAsync(TUser user, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            var existingUser = await _userManager.FindByIdAsync(userId);

            if (existingUser == null)
            {
                throw new Exception("existing user not found");
            }

            return await _userManager.ConfirmEmailAsync(existingUser, code);
        }

        public async Task<TUser> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> IsEmailConfirmedAsync(string email)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser == null)
            {
                throw new Exception("existing user not found");
            }

            return await _userManager.IsEmailConfirmedAsync(existingUser);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string userId, string resetCode, string newPassword)
        {
            var existingUser = await _userManager.FindByIdAsync(userId);

            if (existingUser == null)
            {
                throw new Exception("existing user not found");
            }

            return await _userManager.ResetPasswordAsync(existingUser, resetCode, newPassword);
        }


        public override Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent)
        {
            return base.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent);
        }


        public override Task<ClaimsPrincipal> CreateUserPrincipalAsync(TUser user)
        {
            return base.CreateUserPrincipalAsync(user);
        }

        public override Task SignInAsync(TUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {
            return base.SignInAsync(user, authenticationProperties, authenticationMethod);
        }

        public override Task RememberTwoFactorClientAsync(TUser user)
        {
            return base.RememberTwoFactorClientAsync(user);
        }

        public override Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient)
        {
            return base.TwoFactorAuthenticatorSignInAsync(code, isPersistent, rememberClient);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(string userId)
        {
            var existingUser = await _userManager.FindByIdAsync(userId);

            if (existingUser == null)
            {
                throw new Exception("existing user not found");
            }

            return await _userManager.GetLoginsAsync(existingUser);
        }

        public async Task<TUser> FindByIdAsync(string userId)
        {
            userId = userId ?? throw new ArgumentNullException(nameof(userId));

            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> ChangePhoneNumberAsync(TUser user, string phoneNumber, string token)
        {
            return await _userManager.ChangePhoneNumberAsync(user, phoneNumber, token);
        }

        public async Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return await _userManager.GetTwoFactorEnabledAsync(user);
        }

        public async Task<string> GetPhoneNumberAsync(TUser user)
        {
            return await _userManager.GetPhoneNumberAsync(user);
        }

        public async Task<IdentityResult> RemoveLoginAsync(TUser user, string loginProvider, string providerKey)
        {
            return await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);
        }

        public async Task<IdentityResult> SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            return await _userManager.SetTwoFactorEnabledAsync(user, enabled);
        }

        public override Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return base.GetExternalAuthenticationSchemesAsync();
        }

        public async Task<IdentityResult> AddPasswordAsync(TUser user, string password) 
        {
            return await _userManager.AddPasswordAsync(user, password);
        }

        public async Task<IdentityResult> ChangePasswordAsync(TUser user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<IdentityResult> SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            return await _userManager.SetPhoneNumberAsync(user, phoneNumber);
        }

        public async Task<string> GenerateChangePhoneNumberTokenAsync(TUser user, string phoneNumber)
        {
            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        }
    }
}
