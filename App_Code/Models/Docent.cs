namespace Modulewijzer.Models
{
    /// <summary>
    /// Represents the `Docent` table within the database.
    /// </summary>
    public sealed class Docent
    {
        /// <summary>
        /// Gets or sets this docent's unique ID.
        /// </summary>
        public int Id { get; set; } = -1;

        /// <summary>
        /// Gets or sets this docent's first letters.
        /// </summary>
        public string Voorletters { get; set; }

        /// <summary>
        /// Gets or sets this docent's last name.
        /// </summary>
        public string Achternaam { get; set; }

        /// <summary>
        /// Gets or sets this docent's middle name.
        /// </summary>
        public string Tussenvoegsel { get; set; }
    }
}
