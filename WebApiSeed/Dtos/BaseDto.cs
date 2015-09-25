namespace WebApiSeed.Dtos
{
    using Newtonsoft.Json;

    /// <summary>
    ///     Basic DTO model entity
    /// </summary>
    public class BaseDto
    {
        /// <summary>
        ///     Model ID
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        ///     Serialize object to JSON string
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}