namespace RedmineClient.Models.Models.Issues
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Newtonsoft.Json;

    using RedmineClient.Models.Models.Attachments;
    using RedmineClient.Models.Models.Common;
    using RedmineClient.Models.Models.Journal;

    /// <summary>
    /// The issue.
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        [JsonProperty("project")]
        public Label Project { get; set; }

        /// <summary>
        /// Gets or sets the tracker.
        /// </summary>
        [JsonProperty("tracker")]
        public Label Tracker { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [JsonProperty("status")]
        public Label Status { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        [JsonProperty("priority")]
        public Label Priority { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        [JsonProperty("author")]
        public Label Author { get; set; }

        /// <summary>
        /// Gets or sets the assign to.
        /// </summary>
        [JsonProperty("assigned_to")]
        public Label AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [JsonProperty("start_date")]
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        [JsonProperty("due_date")]
        public DateTimeOffset? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the done ration.
        /// </summary>
        [JsonProperty("done_ratio")]
        public int DoneRatio { get; set; }

        /// <summary>
        /// Gets or sets the created_ on.
        /// </summary>
        [JsonProperty("created_on")]
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the updated_ on.
        /// </summary>
        [JsonProperty("updated_on")]
        public DateTimeOffset UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the journals.
        /// </summary>
        [JsonProperty("journals")]
        public List<JournalItem> Journals { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; } 

            /// <summary>
        /// Gets the description array.
        /// </summary>
        [JsonIgnore]
        public List<string> DescriptionArray
        {
            get
            {
               return this.Description.Replace("\r", string.Empty).Split(new[] { '\n' }).ToList();
            }
        }

        /// <summary>
        /// Gets or sets the last bind.
        /// </summary>
        [JsonIgnore]
        public DateTime? LastBind { get; set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        [JsonIgnore]
        public string Title
        {
            get
            {
                return string.Format("{0} #{1} {2}", this.Tracker.Name, this.Id, this.Project.Name);
            }
        }

        /// <summary>
        /// Gets the status string.
        /// </summary>
        [JsonIgnore]
        public string StatusString
        {
            get
            {
                return string.Format("{0} ({1}%)", this.Status.Name, this.DoneRatio);
            }
        }

        /// <summary>
        /// Gets the run time.
        /// </summary>
        public string RunTime
        {
            get
            {
                string datePatern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                string dueDate = !this.DueDate.HasValue
                        ? "-"
                        : this.DueDate.Value.ToString(datePatern);

                return string.Format("{0} / {1}", this.StartDate.ToString(datePatern), dueDate);
            }
        }

        /// <summary>
        /// Gets the tracker with percent.
        /// </summary>
        [JsonIgnore]
        public string TrackerWithPercentString
        {
            get
            {
                return string.Format("{0} #{1} ({2}%)", this.Tracker.Name, this.Id, this.DoneRatio);
            }
        }

        /// <summary>
        /// Gets the start date string.
        /// </summary>
        [JsonIgnore]
        public string StartDateString
        {
            get
            {
                return this.StartDate.ToString("dd-MM-yyyy");
            }
        }

        /// <summary>
        /// Gets the start date string.
        /// </summary>
        [JsonIgnore]
        public string DueDateString
        {
            get
            {
                return this.DueDate.HasValue ? this.DueDate.Value.ToString("dd-MM-yyyy") : "-";
            }
        }

        /// <summary>
        /// Gets the created on string.
        /// </summary>
        [JsonIgnore]
        public string CreatedOnString
        {
            get
            {
                return this.CreatedOn.ToString("dd-MM-yyyy");
            }
        }

        /// <summary>
        /// Gets the updated on string.
        /// </summary>
        [JsonIgnore]
        public string UpdatedOnString
        {
            get
            {
                return this.UpdatedOn.ToString("dd-MM-yyyy");
            }
        }
    }
}
