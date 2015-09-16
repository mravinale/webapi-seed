namespace WebApiSeed.Dtos
{
    /// <summary>
    ///     Service result
    /// </summary>
    public enum UserUpdateResult
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