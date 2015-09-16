namespace WebApiSeed.Dtos
{
    /// <summary>
    ///     Result of updating a user
    /// </summary>
    public class UpdateUserResultDto
    {
        /// <summary>
        ///     User update result
        /// </summary>
        public UserUpdateResult Result { get; set; }

        /// <summary>
        ///     User dto
        /// </summary>
        public UserDto User { get; set; }
    }
}