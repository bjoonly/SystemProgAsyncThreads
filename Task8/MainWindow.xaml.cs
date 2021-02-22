using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task8
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string text;
        char[] vowels = new char[] {'a','e','i','o','u' };
        char[] consonants = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z', 'y' };
 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            text = textBox.Text;
            Thread t1 = new Thread(CountVowels);
            Thread t2 = new Thread(CountConsonants);
            Thread t3 = new Thread(CountSymbols);

            t1.Start();
            t2.Start();
            t3.Start();
        }
        private void CountSymbols()
        {
            int count = 0;
            count = text.Where(t => t != ' ' && t!='\n' && t!='\r').Count();
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                symbolsTextBlock.Text = count.ToString();
            }));
        }
        private void CountVowels()
        {
            int count = 0;
            foreach (var symb in text)
            {
                if (vowels.Contains(symb))
                    count++;
            }
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                vowelsTextBlock.Text = count.ToString();
            }));
        }
        private void CountConsonants()
        {
            int count = 0;
            foreach (var symb in text)
            {
                if (consonants.Contains(symb))
                    count++;
            }
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                consonantsTextBlock.Text = count.ToString();
            }));
        }
    }
}
