namespace TimecardsCore.Interfaces
{
    /// <summary>
    /// The AppConstants class holds constant values for either production or test use
    /// </summary>
    public interface IAppConstants
    {
        /// <summary>
        /// The connection string used by EF to access the database
        /// </summary>
        string ConnectionStringName { get; }
    }
}
