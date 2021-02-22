using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task23
{
    public class CopyFileInfo : INotifyPropertyChanged
    {
        private int progress;
        private string sourceFile;
        private string destFolder;
        public string SourceFile { get { return sourceFile; } set { if (value != sourceFile) { sourceFile = value;OnPropertyChanged(); } } }
        public string DestFolder { get { return destFolder; } set { if (value != destFolder) { destFolder = value; OnPropertyChanged(); } } }

        public int Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
