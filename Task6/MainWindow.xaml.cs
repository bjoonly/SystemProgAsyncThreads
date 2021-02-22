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

namespace Task6
{
  
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ulong num=0;
        ulong result=0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            if (ulong.TryParse(numberTextBox.Text, out num))
            {
               Thread t=new Thread(Factorial);
                t.Start();


            }
        }
        private void Factorial()
        {
            try
            {

                result = 1;
                for (ulong i = 1; i <= num; i++)
                {
                    result = checked(result * i);
                }

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {

                    resultTextBlock.Text = result.ToString();

                }));
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
