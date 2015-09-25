namespace WebApiSeed.Common.User
{
    /// <summary>
    ///     Service result
    /// </summary>
    public enum UserServiceResultEnum
    {
        /// <summary>
        ///     Success updating user
        /// </summary>
        Success = 0,

        /// <summary>
        ///     Updating user failed: username exists
        /// </summary>
        UsernameExists = 1,

        /// <summary>
        ///     User update failed with unexpected error
        /// </summary>
        Fail = 100
    }
}