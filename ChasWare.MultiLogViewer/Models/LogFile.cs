using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChasWare.MultiLogViewer.Interfaces;

namespace ChasWare.MultiLogViewer.Models
{
    internal class LogFile
    {
        public LogFile(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }


    }

}
