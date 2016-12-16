namespace WebApiSeed.Infrastructure.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;
    using AutoMapper;
    using Data.Domain;
    using Dtos;
    using Interfaces;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Configuration;
    using Services.Interfaces;
    using Data.Configuration.EF.Interfaces;

    /// <summary>
    ///     Security functions
    /// </summary>
    public class SecurityHelper : ISecurityHelper
    {
        private readonly int MinRandomNumber = Convert.ToInt32(ConfigurationManager.AppSettings["SecurityTokenMinRandomNumber"]);
        private readonly int MaxRandomNumber = Convert.ToInt32(ConfigurationManager.AppSettings["SecurityTokenMaxRandomNumber"]);
        private readonly IMapper _mappingEngine;
        private readonly IDbContext _userRepository;

        /// <summary>
        ///     Security helper
        /// </summary>
        /// <param name="mappingEngine">Automapper engine</param>
        /// <param name="userRepository">User repository</param>
        public SecurityHelper(IMapper mappingEngine, IDbContext userRepository)
        {
            _mappingEngine = mappingEngine;
            _userRepository = userRepository;
        }

        /// <summary>
        ///     Generate a mobile phone number verification code
        /// </summary>
        /// <returns></returns>
        public string GenerateVerificationCode()
        {
            Trace.WriteLine("[SecurityHelper] Generating verification code.");
            return new Random().Next(MinRandomNumber, MaxRandomNumber).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Generate API autentication token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiTokenDto GenerateApiToken(User user)
        {
            Trace.WriteLine("[SecurityHelper] Generating API token.");
            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString(CultureInfo.InvariantCulture)));

            var tokenExpiration = TimeSpan.FromDays(365);
            var props = new AuthenticationProperties
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };

            var ticket = new AuthenticationTicket(identity, props);
            var token = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
            if (ticket.Properties.IssuedUtc == null || ticket.Properties.ExpiresUtc == null) return null;

            var tokenResponse = new ApiTokenDto
            {
                User = _mappingEngine.Map<User, UserDto>(user),
                AccessToken = token,
                TokenType = "bearer",
                ExpiresIn = tokenExpiration.TotalSeconds.ToString(CultureInfo.InvariantCulture),
                Issued = GetUtcDateTime(ticket.Properties.IssuedUtc.Value).ToString("s"),
                Expires = GetUtcDateTime(ticket.Properties.ExpiresUtc.Value).ToString("s")
            };

            return tokenResponse;
        }

        /// <summary>
        ///     Retrieve the user ID from a token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>User ID</returns>
        public int GetUserIdForToken(string token)
        {
            var ticket = Startup.OAuthOptions.AccessTokenFormat.Unprotect(token);
            var stringValue = ticket.Identity.Claims.Single(c => c.Type == ClaimTypes.Sid).Value;
            int id;
            return Int32.TryParse(stringValue, out id) ? id : -1;
        }

        /// <summary>
        ///     Verify a given token exists in the database for the user in that token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>True if token exists in the database</returns>
        public bool ValidateToken(string token)
        {
            var userId = GetUserIdForToken(token);
            var user = _userRepository.Entity<User>().FirstOrDefault(u => u.Id == userId);

            return user.AccessToken == token;
        }

        private static DateTime GetUtcDateTime(DateTimeOffset dateTime)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                DateTimeKind.Utc
                );
        }
    }
}