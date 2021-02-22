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

namespace Task7
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        double num = 0;
        double pow = 0;
        double result =1;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (double.TryParse(numberTextBox.Text, out num) && double.TryParse(powTextBox.Text,out pow))
            {
                Thread t = new Thread(Pow);
                t.Start();


            }
        }
        private void Pow()
        {
            try
            {

       
                result=Math.Pow(num, pow);

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {

                    resultTextBlock.Text = result.ToString();

                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
