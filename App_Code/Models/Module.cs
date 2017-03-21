using System;
using System.Collections.Generic;


namespace Modulewijzer.Models
{
    /// <summary>
    /// A model representing the `Module` table within the database.
    /// </summary>
    public sealed class Module
    {
        /// <summary>
        /// Gets this module's unique ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets this module's name. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets this module's number of required EC's.
        /// </summary>
        public int NumberEcs { get; set; }

        /// <summary>
        /// Gets or sets this module's start date.
        /// </summary>
        public int StudyYear { get; set; }

        /// <summary>
        /// Gets or sets this module's period.
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// Gets a list of competences associated with this module.
        /// </summary>
        public List<Competence> Competences { get; } = new List<Competence>();

        /// <summary>
        /// Gets a list of methods associated with this module.
        /// </summary>
        public List<Method> Methods { get; } = new List<Method>();

        /// <summary>
        /// Gets a list of teachers associated with this module.
        /// </summary>
        public List<Teacher> Teachers { get; } = new List<Teacher>();
    }
}
