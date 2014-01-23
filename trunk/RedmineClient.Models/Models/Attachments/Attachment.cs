namespace RedmineClient.Models.Models.Attachments
{
    using System;

    using Newtonsoft.Json;

    using RedmineClient.Models.Models.Common;

    /// <summary>
    /// The attachment.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        [JsonProperty("filename")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file size.
        /// </summary>
        [JsonProperty("filesize")]
        public long FileSize { get; set; }

        /// <summary>
        /// Gets or sets the content type.
        /// </summary>
        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the content url.
        /// </summary>
        [JsonProperty("content_url")]
        public string ContentUrl { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        [JsonProperty("created_on")]
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        [JsonProperty("author")]
        public Label Author { get; set; }

        /// <summary>
        /// Gets the created on string.
        /// </summary>
        [JsonIgnore]
        public string CreatedOnString
        {
            get
            {
                return this.CreatedOn.ToString("g");
            }
        }

        /// <summary>
        /// Gets the file size string.
        /// </summary>
        [JsonIgnore]
        public string FileSizeString
        {
            get
            {
                return string.Format("{0} KB", this.FileSize / 1024);
            }
        }
    }
}
