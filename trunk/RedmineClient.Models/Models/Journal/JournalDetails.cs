namespace RedmineClient.Models.Models.Journal
{
    using Newtonsoft.Json;

    /// <summary>
    /// The journal details.
    /// </summary>
    public class JournalDetails
    {
        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        [JsonProperty("property")]
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        [JsonProperty("old_value")]
        public string OldValue { get; set; }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        [JsonProperty("new_value")]
        public string NewValue { get; set; }

        /// <summary>
        /// Gets a value indicating whether show done ratio.
        /// </summary>
        [JsonIgnore]
        public string ShowDoneRatio
        {
            get
            {
                if (this.Name.Equals("done_ratio") && this.Property.Equals("attr"))
                {
                    return "Visible";
                }

                return "Collapsed";
            }
        }

        /// <summary>
        /// Gets the show status.
        /// </summary>
        [JsonIgnore]
        public string ShowStatus
        {
            get
            {
                if (this.Name.Equals("status_id") && this.Property.Equals("attr") && !string.IsNullOrEmpty(this.OldStatus) && !string.IsNullOrEmpty(this.NewStatus))
                {
                    return "Visible";
                }

                return "Collapsed";
            }
        }

        /// <summary>
        /// Gets the show assign.
        /// </summary>
        [JsonIgnore]
        public string ShowAssign
        {
            get
            {
                if (this.Name.Equals("assigned_to_id") && this.Property.Equals("attr") && !string.IsNullOrEmpty(this.NewAssignName) && !string.IsNullOrEmpty(this.OldAssignName))
                {
                    return "Visible";
                }

                return "Collapsed";
            }
        }

        /// <summary>
        /// Gets the show deleted assign.
        /// </summary>
        [JsonIgnore]
        public string ShowDeletedAssign
        {
            get
            {
                if (this.Name.Equals("assigned_to_id") && this.Property.Equals("attr") && string.IsNullOrEmpty(this.NewAssignName) && !string.IsNullOrEmpty(this.OldAssignName))
                {
                    return "Visible";
                }

                return "Collapsed";
            }
        }

        /// <summary>
        /// Gets the show seted assign.
        /// </summary>
        [JsonIgnore]
        public string ShowSetedAssign
        {
            get
            {
                if (this.Name.Equals("assigned_to_id") && this.Property.Equals("attr") && !string.IsNullOrEmpty(this.NewAssignName) && string.IsNullOrEmpty(this.OldAssignName))
                {
                    return "Visible";
                }

                return "Collapsed";
            }
        }

        /// <summary>
        /// Gets the show priority.
        /// </summary>
        [JsonIgnore]
        public string ShowPriority
        {
            get
            {
                if (this.Name.Equals("priority_id") && this.Property.Equals("attr") && !string.IsNullOrEmpty(this.OldPriority) && !string.IsNullOrEmpty(this.NewPriority))
                {
                    return "Visible";
                }

                return "Collapsed";
            }
        }

        /// <summary>
        /// Gets the show set estimate.
        /// </summary>
        [JsonIgnore]
        public string ShowSetEstimate
        {
            get
            {
                if (this.Name.Equals("estimated_hours") && this.Property.Equals("attr") && !string.IsNullOrEmpty(this.NewValue) && string.IsNullOrEmpty(this.OldValue))
                {
                    return "Visible";
                }

                return "Collapsed";
            }
        }

        /// <summary>
        /// Gets the show attachments.
        /// </summary>
        [JsonIgnore]
        public string ShowAttachment
        {
            get
            {
                if (this.Property.Equals("attachment") && !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.NewValue) && string.IsNullOrEmpty(this.OldValue))
                {
                    return "Visible";
                }

                return "Collapsed";
            }
        }

        public string ShowSubject
        {
            get
            {
                if (this.Property.Equals("attr") && this.Name.Equals("subject") && !string.IsNullOrEmpty(this.NewValue) && !string.IsNullOrEmpty(this.OldValue))
                {
                    return "Visible";
                }

                return "Collapsed";
            }
        }


        /// <summary>
        /// Gets or sets the old status.
        /// </summary>
        [JsonIgnore]
        public string OldStatus { get; set; }

        /// <summary>
        /// Gets or sets the new status.
        /// </summary>
        [JsonIgnore]
        public string NewStatus { get; set; }

        /// <summary>
        /// Gets or sets the old assign name.
        /// </summary>
        [JsonIgnore]
        public string OldAssignName { get; set; }

        /// <summary>
        /// Gets or sets the new assign name.
        /// </summary>
        [JsonIgnore]
        public string NewAssignName { get; set; }

        /// <summary>
        /// Gets or sets the new priority.
        /// </summary>
        [JsonIgnore]
        public string NewPriority { get; set; }

        /// <summary>
        /// Gets or sets the old priority.
        /// </summary>
        [JsonIgnore]
        public string OldPriority { get; set; }
    }
}
