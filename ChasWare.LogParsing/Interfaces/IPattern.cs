using ChasWare.LogParsing.Enums;

namespace ChasWare.LogParsing.Interfaces
{
    /// <summary>
    ///     defines a patternelement
    /// </summary>
    public interface IPattern
    {
        #region public properties

        /// <summary>
        ///     Gets the name of the pattern
        /// </summary>
        PatternName PatternName { get; }

        #endregion
    }
}