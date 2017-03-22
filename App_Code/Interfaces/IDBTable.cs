using System.Collections.Generic;


namespace Modulewijzer.Interfaces
{
    /// <summary>
    /// Interface for database tables.
    /// </summary>
    /// <typeparam name="T">A datamodel this table works with.</typeparam>
    public interface IDBTable<T> where T : class
    {
        /// <summary>
        /// Inserts a new row into the database.
        /// </summary>
        /// <param name="row">The row to insert.</param>
        void Create(T row);

        /// <summary>
        /// Updates the specified row in the database.
        /// </summary>
        /// <param name="row">The row to update.</param>
        void Update(T row);

        /// <summary>
        /// Deletes the given row from the database.
        /// </summary>
        /// <param name="row">The ID of the row to delete.</param>
        void Delete(int id);

        /// <summary>
        /// Gets a row with the given ID from the database.
        /// </summary>
        /// <param name="id">The row's ID.</param>
        /// <returns>A row with the given ID.</returns>
        T GetById(int id);

        /// <summary>
        /// Gets all rows in the table.
        /// </summary>
        /// <returns>A collection of all rows in the table.</returns>
        T[] GetAll();
    }
}