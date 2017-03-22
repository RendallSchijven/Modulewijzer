namespace Modulewijzer.Models
{
    /// <summary>
    /// Represents the `Module` table within the database.
    /// </summary>
    public sealed class Module
    {
        /// <summary>
        /// Gets or sets this module's unique ID.
        /// </summary>
        public int Id { get; set; } = -1;

        /// <summary>
        /// Gets or sets this module's name.
        /// </summary>
        public string Naam { get; set; }

        /// <summary>
        /// Gets or sets this modules number of EC's.
        /// </summary>
        public int AantalEcs { get; set; }

        /// <summary>
        /// Gets or sets this module's study year.
        /// </summary>
        public int StudieJaar { get; set; }

        /// <summary>
        /// Gets or sets this module's period.
        /// </summary>
        public int Periode { get; set; }

        /// <summary>
        /// Gets or sets this module's method.
        /// </summary>
        public string Werkvorm { get; set; }

        /// <summary>
        /// Gets or sets this module's learning outcomes.
        /// </summary>
        public string Leeruitkomsten { get; set; }

        /// <summary>
        /// Gets or sets this module's literature.
        /// </summary>
        public string Literatuur { get; set; }

        /// <summary>
        /// gets or sets this module's planning.
        /// </summary>
        public string Planning { get; set; }
    }
}
