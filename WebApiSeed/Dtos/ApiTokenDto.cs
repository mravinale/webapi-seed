namespace WebApiSeed.Dtos
{
    using Newtonsoft.Json;

    /// <summary>
    ///     API token DTO
    /// </summary>
    public class ApiTokenDto
    {
        /// <summary>
        ///     User Dto
        /// </summary>
        [JsonProperty("user")]
        public UserDto User { get; set; }

        /// <summary>
        ///     Access token
        /// </summary>
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        /// <summary>
        ///     Token type
        /// </summary>
        [JsonProperty("tokenType")]
        public string TokenType { get; set; }

        /// <summary>
        ///     Expires in (seconds)
        /// </summary>
        [JsonProperty("expiresIn")]
        public string ExpiresIn { get; set; }

        /// <summary>
        ///     Issued at
        /// </summary>
        [JsonProperty("issued")]
        public string Issued { get; set; }

        /// <summary>
        ///     Expires at
        /// </summary>
        [JsonProperty("expires")]
        public string Expires { get; set; }
    }
}