using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2_2
{
    /// <summary>
    /// Логика взаимодействия для WindowResults.xaml
    /// </summary>
    public partial class WindowResults : Window
    {
        int count = 0;
        public WindowResults(int countGood)
        {
            count = countGood;
            InitializeComponent();
            ResultText.Text = "Вы попали " + count + " раз!";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
