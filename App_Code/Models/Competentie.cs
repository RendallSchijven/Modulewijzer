
namespace Modulewijzer.Models
{
    /// <summary>
    /// Represents the `Competentie` table within the database.
    /// </summary>
    public sealed class Competentie
    {
        /// <summary>
        /// Gets or sets this competentie's unique ID.
        /// </summary>
        public int Id { get; set; } = -1;

        /// <summary>
        /// Gets or sets this competentie's name.
        /// </summary>
        public string Naam { get; set; }

        /// <summary>
        /// Gets or sets this module's level.
        /// </summary>
        public int Niveau { get; set; }

        /// <summary>
        /// Gets or sets this competentie's description.
        /// </summary>
        public string Beschrijving { get; set; }
    }
}
