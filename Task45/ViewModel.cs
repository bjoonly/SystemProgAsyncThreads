
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Task45
{
    class ViewModel : INotifyPropertyChanged
    {
        private uint countThreads;
        public uint CountThreads { get { return countThreads; } set { if (countThreads != value) { countThreads = value; OnPropertyChanged(); } } }
        private bool isStarted;
        bool IsStarted { get { return isStarted; } set { if (isStarted != value) { isStarted = value; OnPropertyChanged(); } } }
        public CopyDirectoryInfo copyInfo;
        public CopyDirectoryInfo CopyInfo { get { return copyInfo; } set { if (value != copyInfo) { copyInfo = value; OnPropertyChanged(); } } }

        public ViewModel()
        {
            CopyInfo = new CopyDirectoryInfo();
            startCommand = new DelegateCommand(Start, () => IsStarted == false); ;
            fromCommand = new DelegateCommand(SelectFolderSource, () => IsStarted == false);
            toCommand = new DelegateCommand(SelectFolder, () => IsStarted == false);
            stopCommand = new DelegateCommand(Stop, () => IsStarted == true);
            pauseCommand = new DelegateCommand(Pause, () => IsStarted == true);
            PropertyChanged += (sender, args) =>
            {


                if (args.PropertyName == nameof(IsStarted))
                {
                    startCommand.RaiseCanExecuteChanged();
                    fromCommand.RaiseCanExecuteChanged();
                    toCommand.RaiseCanExecuteChanged();
                    stopCommand.RaiseCanExecuteChanged();
                    pauseCommand.RaiseCanExecuteChanged();
                }
            };
        }


        private Command startCommand;
        public ICommand StartCommand => startCommand;

        private Command fromCommand;
        public ICommand FromCommand => fromCommand;
        private Command toCommand;
        public ICommand ToCommand => toCommand;
        private Command stopCommand;
        public ICommand StopCommand => stopCommand;
        private Command pauseCommand;
        public ICommand PauseCommand => pauseCommand;

        List<Thread> currentThreads = new List<Thread>();
        private void Start()
        {
            if (countThreads > 0)
            {
                for (int i = 1; i <= countThreads; i++)
                {
                    Thread copyThread = new Thread(CopyDirectory);
                    var Copy = new CopyDirectoryInfo() { DestFolder = CopyInfo.DestFolder, SourceFolder = CopyInfo.SourceFolder, Progress = 0 };
                    threads.Add(Copy);
                    currentThreads.Add(copyThread);
                    copyThread.Start(Copy);
                    IsStarted = true;
                 
                }
            }
        }

        private void Pause()
        {
            foreach (var item in currentThreads)
            {
                item.Suspend();
            }

        }
        private void Stop()
        {
            try
            {
                foreach (var item in currentThreads)
                {


                    item.Abort();
                }

                IsStarted = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private readonly ICollection<CopyDirectoryInfo> threads = new ObservableCollection<CopyDirectoryInfo>();
        public IEnumerable<CopyDirectoryInfo> Threads => threads;


        private void CopyDirectory(object data)
        {
            lock (this)
            {
                try
                {

                    CopyDirectoryInfo info = data as CopyDirectoryInfo;


                    if (info == null) return;

                    DirectoryInfo dir = new DirectoryInfo(info.SourceFolder);

                    DirectoryInfo[] dirs = dir.GetDirectories();


                    int countFiles = 0;
                    FileInfo[] files = dir.GetFiles();
                    countFiles = files.Length;
                    decimal percent = 100 / (decimal)files.Count();


                    foreach (FileInfo file in files)
                    {
                        string tempPath = Path.Combine($"{info.DestFolder}", file.Name);
                        file.CopyTo(tempPath, true);
                        info.Progress += (int)percent;




                    }

                    Thread.Sleep(1000);
                    currentThreads.Remove(Thread.CurrentThread);





                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
        }
        

        private void SelectFolderSource()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                CopyInfo.SourceFolder = fbd.SelectedPath;

            }

           
        }

        private void SelectFolder()
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                CopyInfo.DestFolder = fbd.SelectedPath;

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
