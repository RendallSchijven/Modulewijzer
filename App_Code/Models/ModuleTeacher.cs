﻿namespace Modulewijzer.Models
{
    /// <summary>
    /// A model representing the `Teacher` table within the database.
    /// </summary>

    public sealed class ModuleTeacher
    {
        /// <summary>
        /// Gets this teacher's unique ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets this teacher's id.
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// Gets or sets this modules id.
        /// </summary>
        public int ModuleId { get; set; }
    }
}
