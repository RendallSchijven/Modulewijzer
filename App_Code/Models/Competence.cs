namespace Modulewijzer.Models
{
    /// <summary>
    /// A model representing the `Competence` table within the database.
    /// </summary>
    public sealed class Competence
    {
        /// <summary>
        /// Gets this competence's unique ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets this competence's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets this competence's level.
        /// </summary>
        public int Level { get; set; }
    }
}
