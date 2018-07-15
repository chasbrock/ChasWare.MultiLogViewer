using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ChasWare.LogParsing.Common
{
    public class JSONSerialiser<T>
    {
        #region Constants and fields 

        private readonly string _fileName;

        #endregion

        #region Constructors

        public JSONSerialiser(string fileName)
        {
            _fileName = fileName;
        }

        #endregion

        #region public methods

        public IList<T> Load()
        {
            if (!File.Exists(_fileName))
            {
                return new List<T>();
            }

            using (StreamReader file = File.OpenText(_fileName))
            {
                return JsonConvert.DeserializeObject<List<T>>(file.ReadToEnd());
            }
        }

        public void Save(IEnumerable<T> items)
        {
            using (StreamWriter file = File.CreateText(_fileName))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, items);
            }
        }

        #endregion
    }
}