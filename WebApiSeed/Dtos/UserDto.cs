namespace WebApiSeed.Dtos
{
    using Newtonsoft.Json;

    /// <summary>
    ///     User DTO
    /// </summary>
    public class UserDto : BaseDto
    {
        /// <summary>
        ///     Username
        /// </summary>
        [JsonProperty("username")]
        public string UserName { get; set; }

        /// <summary>
        ///     User gender
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}