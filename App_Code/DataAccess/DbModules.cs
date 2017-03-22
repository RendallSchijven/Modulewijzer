namespace Modulewijzer.DataAccess
{
    // TODO: Better name?
    public sealed class DbModules
    {
        #region Singleton code.
        private static readonly DbModules instance = new DbModules();


        /// <summary>
        /// Returns the database's instance.
        /// </summary>
        public static DbModules Instance => instance;
        #endregion


        public TabelModule Modules { get; } = new TabelModule();
        public TabelCompetentie Competenties { get; } = new TabelCompetentie();
        public TabelDocent Docenten { get; } = new TabelDocent();
    }
}
