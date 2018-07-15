namespace ChasWare.MultiLogViewer.Models
{
    internal class LogFile
    {
        #region Constructors

        public LogFile(string fileName)
        {
            FileName = fileName;
        }

        #endregion

        #region public properties

        public string FileName { get; }

        #endregion
    }
}