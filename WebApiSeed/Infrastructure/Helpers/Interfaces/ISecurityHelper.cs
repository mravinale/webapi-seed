namespace WebApiSeed.Infrastructure.Helpers.Interfaces
{
    using Data.Domain;
    using Dtos;

    /// <summary>
    /// </summary>
    public interface ISecurityHelper
    {
        /// <summary>
        ///     Generate mobile phone number verification code
        /// </summary>
        /// <returns></returns>
        string GenerateVerificationCode();

        /// <summary>
        ///     Generate API token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ApiTokenDto GenerateApiToken(User user);

        /// <summary>
        ///     Retrieve a user ID from a token
        /// </summary>
        /// <param name="token">Token string</param>
        /// <returns>User ID</returns>
        int GetUserIdForToken(string token);

        /// <summary>
        ///     Validate a user id and a token belong together
        /// </summary>
        /// <param name="token">Token string</param>
        /// <returns>True if token belongs to a user</returns>
        bool ValidateToken(string token);
    }
}