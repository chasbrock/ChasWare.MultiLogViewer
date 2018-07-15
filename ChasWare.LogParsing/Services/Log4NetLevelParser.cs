using ChasWare.LogParsing.Enums;

namespace ChasWare.LogParsing.Services
{
    internal static class Log4NetLevelParser
    {
        #region public methods

        public static LoggingLevels Parse(string source)
        {
            char first = !string.IsNullOrEmpty(source) ? source[0] : 'D';
            switch (first)
            {
                case 'E':
                case 'e':
                    return LoggingLevels.Error;
                case 'F':
                case 'f':
                    return LoggingLevels.Fatal;
                case 'I':
                case 'i':
                    return LoggingLevels.Info;
                case 'W':
                case 'w':
                    return LoggingLevels.Warn;
                default:
                    return LoggingLevels.Debug;
            }
        }

        #endregion
    }
}