using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BaseAuditableEntity
    {
        //Audit Properties
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? LastModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active.
        /// </summary>
        [JsonIgnore]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the version number for concurrency handling in PostgreSQL.
        /// </summary>
    }
}
