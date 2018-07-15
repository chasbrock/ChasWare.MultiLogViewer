using System.Runtime.Serialization;

namespace ChasWare.LogParsing.Models
{
    [DataContract]
    public class AppDetailsModel
    {
        #region public properties

        [DataMember]
        public string AppName { get; set; }

        public bool IsOpen { get; set; }

        [DataMember]
        public string LogPath { get; set; }

        [DataMember]
        public bool OpenOnStartup { get; set; }

        [DataMember]
        public string Pattern { get; set; }

        #endregion

        #region public methods

        /// <inheritdoc />
        public override string ToString()
        {
            return AppName;
        }

        #endregion
    }
}