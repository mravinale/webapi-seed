namespace $rootnamespace$.Dtos
{
    /// <summary>
    ///     Azure file details
    /// </summary>
    public class AzureFileDetailsDto
    {
        /// <summary>
        ///     File name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     File size
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        ///     File type
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        ///     Blob url
        /// </summary>
        public string Location { get; set; }
    }
}