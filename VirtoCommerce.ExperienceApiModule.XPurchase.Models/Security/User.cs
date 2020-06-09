using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using VirtoCommerce.ExperienceApiModule.XPurchase.Models.Common;

namespace VirtoCommerce.ExperienceApiModule.XPurchase.Models.Security
{
    public partial class User : CloneableEntity
    {
        /// <summary>
        /// Store id.
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// Security account user name.
        /// </summary>
        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Returns the email address of the customer.
        /// </summary>
        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string DefaultLanguage { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string TwoFactorAuthenticatorKey { get; set; }

        public bool IsLockedOut => LockoutEndDateUtc != null && LockoutEndDateUtc.Value > DateTime.UtcNow;

        /// <summary>
        ///  Used to record failures for the purposes of lockout.
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// Is lockout enabled for this user.
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Returns true if user authenticated  returns false if it anonymous.
        /// </summary>
        public bool IsRegisteredUser { get; set; }

        /// <summary>
        /// The flag indicates that the user is an administrator.
        /// </summary>
        public bool IsAdministrator { get; set; }

        public string UserType { get; set; }

        /// <summary>
        /// The user ID of an operator who has loggen in on behalf of a customer.
        /// </summary>
        public string OperatorUserId { get; set; }

        /// <summary>
        /// The user name of an operator who has loggen in on behalf of a customer.
        /// </summary>
        public string OperatorUserName { get; set; }

        // Selected and persisted currency code
        public string SelectedCurrencyCode { get; set; }

        public string ContactId { get; set; }

        /// <summary>
        /// All user permissions.
        /// </summary>
        public IEnumerable<string> Permissions { get; set; }

        /// <summary>
        /// Single user role.
        /// </summary>
        public Role Role => Roles?.FirstOrDefault();

        /// <summary>
        /// All user roles.
        /// </summary>
        public IEnumerable<Role> Roles { get; set; }
    }
}