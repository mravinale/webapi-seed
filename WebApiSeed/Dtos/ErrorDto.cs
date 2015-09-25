namespace WebApiSeed.Dtos
{
    using Newtonsoft.Json;

    /// <summary>
    ///     Generic error for controllers
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// Error code
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        ///     Error message
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}