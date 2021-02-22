
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

namespace Task23
{
    class ViewModel: INotifyPropertyChanged
    {
        private uint countThreads;
        public uint CountThreads { get { return countThreads; } set { if (countThreads != value) { countThreads = value;OnPropertyChanged(); } } }
       private bool isStarted;
        bool IsStarted { get { return isStarted; } set { if (isStarted != value) { isStarted = value;OnPropertyChanged(); } } }
        public CopyFileInfo copyInfo;
        public CopyFileInfo CopyInfo { get { return copyInfo; } set { if (value != copyInfo) { copyInfo = value; OnPropertyChanged(); } } }

        public ViewModel()
        {
            CopyInfo = new CopyFileInfo();
            startCommand = new DelegateCommand(Start, () => IsStarted == false); ;
            fromCommand = new DelegateCommand(SelectFile, () => IsStarted == false);
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
                    Thread copyThread = new Thread(CopyFile);
                    var Copy = new CopyFileInfo() { DestFolder = CopyInfo.DestFolder, SourceFile = CopyInfo.SourceFile, Progress = 0 };
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
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private readonly ICollection<CopyFileInfo> threads = new ObservableCollection<CopyFileInfo>();
        public IEnumerable<CopyFileInfo> Threads => threads;


        private void CopyFile(object data)
        {
            lock(this){
                try
                {

                    CopyFileInfo info = data as CopyFileInfo;


                    if (info == null) return;


                    string copyFileName = $"{Path.GetFileNameWithoutExtension(info.SourceFile)}_copy_{DateTime.Now:mmss}{Path.GetExtension(info.SourceFile)}";

                    string destFile = Path.Combine(info.DestFolder, copyFileName);

                    using (FileStream sourceFs = File.OpenRead(info.SourceFile))
                    {

                        FileStream destFs = new FileStream(destFile, FileMode.OpenOrCreate, FileAccess.Write);

                        destFs.SetLength(sourceFs.Length);

                        long sourceFileLength = sourceFs.Length;
                        byte[] buffer = new byte[1024 * 16];
                        long readBytes = 0;
                        long totalReadBytes = 0;

                        do
                        {

                            readBytes = sourceFs.Read(buffer, 0, buffer.Length);

                            destFs.Write(buffer, 0, (int)readBytes);

                            totalReadBytes += readBytes;

                            info.Progress = (int)(totalReadBytes / (sourceFileLength / 100.0));
                            Thread.Sleep(500);

                        } while (readBytes > 0);


                        destFs.Close();
                        currentThreads.Remove(Thread.CurrentThread);
                    }
                }
                catch { }
            }
        }

        private void SelectFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK )
                CopyInfo.SourceFile = ofd.FileName;
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
