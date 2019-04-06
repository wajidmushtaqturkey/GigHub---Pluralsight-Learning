using System;
namespace GigHub.Core.Models
{
    public struct SignInStatus
    {
        public const string SUCCESS = "Success";
        public const string LOCKED_OUT = "LockedOut";
        public const string REQUIRES_VERFICIATION = "RequiresVerification";
        public const string FAILURE = "Failure";
    }
}
