namespace Modulewijzer.Models
{
    /// <summary>
    /// A model representing the `Teacher` table within the database.
    /// </summary>
    
    public sealed class Teacher
    {
        /// <summary>
        /// Gets this teacher's unique ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets this teacher's initials.
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// Gets or sets this teacher's last name.
        /// </summary>
        public string LastName { get; set; }
    }
}
