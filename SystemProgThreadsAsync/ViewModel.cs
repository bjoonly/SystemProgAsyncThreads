using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SystemProgThreadsAsync
{
    class ViewModel : INotifyPropertyChanged
    {

        public ViewModel()
        {
            startCommand = new DelegateCommand(Start);
            priorities.Add(ThreadPriority.Lowest);
            priorities.Add(ThreadPriority.Normal);
            priorities.Add(ThreadPriority.AboveNormal);
            priorities.Add(ThreadPriority.BelowNormal);
            priorities.Add(ThreadPriority.Highest);


        }


        public void Start()
        {
            ThreadPool.QueueUserWorkItem(GenerateNumbers);
            ThreadPool.QueueUserWorkItem(GenerateLetters);
            ThreadPool.QueueUserWorkItem(GenerateSymbols);


        }
        public void GenerateNumbers(object state)
        {
            Thread.CurrentThread.Priority = selectedPriorityNumbers;
           
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {

                    numbers.Add(random.Next(-1000, 1001));
                }));
                Thread.Sleep(200);
            }
        }

        public void GenerateLetters(object state)
        {
            Thread.CurrentThread.Priority = selectedPriorityLetters;
           
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {

                    letters.Add((char)random.Next(65, 91));
                }));
                Thread.Sleep(200);
            }
        }
        public void GenerateSymbols(object state)
        {

            Thread.CurrentThread.Priority = selectedPrioritySymbols;
           
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {

                    symbols.Add((char)random.Next(33, 48));
                }));
                Thread.Sleep(200);
            }
        }

        private ThreadPriority selectedPriorityNumbers;
        private ThreadPriority selectedPriorityLetters;
        private ThreadPriority selectedPrioritySymbols;

        public ThreadPriority SelectedPriorityNumbers { get { return selectedPriorityNumbers; } set { if (value != selectedPriorityNumbers) { selectedPriorityNumbers = value; OnPropertyChanged(); } } }
        public ThreadPriority SelectedPriorityLetters { get { return selectedPriorityLetters; } set { if (value != selectedPriorityLetters) { selectedPriorityLetters = value; OnPropertyChanged(); } } }
        public ThreadPriority SelectedPrioritySymbols { get { return selectedPrioritySymbols; } set { if (value != selectedPrioritySymbols) { selectedPrioritySymbols = value; OnPropertyChanged(); } } }


        private readonly ICollection<int> numbers = new ObservableCollection<int>();
        private readonly ICollection<char> letters = new ObservableCollection<char>();
        private readonly ICollection<char> symbols = new ObservableCollection<char>();
        private readonly ICollection<ThreadPriority> priorities = new ObservableCollection<ThreadPriority>();

        public IEnumerable<int> Numbers => numbers;
        public IEnumerable<char> Letters => letters;
        public IEnumerable<char> Symbols => symbols;
        public IEnumerable<ThreadPriority> Priorities => priorities;




        private Command startCommand;
        public ICommand StartCommand => startCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
