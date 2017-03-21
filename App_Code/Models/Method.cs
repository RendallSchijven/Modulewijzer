namespace Modulewijzer.Models
{
    /// <summary>
    /// A model representing the `Method` table within the database.
    /// </summary>
    public sealed class Method
    {
        /// <summary>
        /// Gets this method's unique ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets this method's name.
        /// </summary>
        public string Name { get; set; }
    }
}
