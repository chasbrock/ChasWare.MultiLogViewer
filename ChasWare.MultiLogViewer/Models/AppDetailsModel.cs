using System.Runtime.Serialization;

namespace ChasWare.MultiLogViewer.Models
{
    [DataContract]
    public class AppDetailsModel
    {
        #region public properties

        [DataMember]
        public string AppName { get; set; }

        [DataMember]
        public string Format { get; set; }

        public bool IsOpen { get; set; }

        [DataMember]
        public string LogPath { get; set; }

        [DataMember]
        public bool OpenOnStartup { get; set; }

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