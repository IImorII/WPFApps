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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// Variant_3
    public partial class MainWindow : Window
    {
        private int count = 0;
        private int countGood = 0;
        public void ClearTextOnFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Text = string.Empty;
            // if you want this to happen only the first time you can remove the event handler like this
            //text.GotFocus -= ClearTextOnFocus;
        }
        bool isHit(double x, double y, double r)
        {
            if (((x * x) + (y * y) <= r * r) && (((x >= 0) && (y >= 0) && (y >= x)) || ((x <= 0) && (y <= 0) && (y <= x))))
            {
                return true;
            }
            return false;
        }
        public MainWindow()
        {
            InitializeComponent();
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 2 - this.Width / 2;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - this.Height / 2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double x = 0, y = 0, r = 0;
            try
            {
                x = double.Parse(Input_X.Text);
                y = double.Parse(Input_Y.Text);
                r = double.Parse(Input_R.Text);
                if (r <= 0)
                {
                    throw new Exception("r < 0");
                }
                Input_R.IsEnabled = false;
                if (isHit(x, y, r))
                {
                    Output_result.Items.Add("Попал");
                    countGood++;
                }
                else
                {
                    Output_result.Items.Add("Промазал");
                }
                count++;
                Counter.Text = count + "/10";
            }
            catch
            {
                Output_result.Items.Add("Неверный ввод");
            }
            if (count == 10)
            {
                WindowResults dialogBox = new WindowResults(countGood);
                dialogBox.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 2 - dialogBox.Width / 2;
                dialogBox.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - dialogBox.Height / 2; 
                dialogBox.Show();
                Start.IsEnabled = false;
            }          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Output_result.Items.Clear();
            count = 0;
            countGood = 0;
            Input_R.IsEnabled = true;
            Start.IsEnabled = true;
            Counter.Text = "0/10";
        }
    }
}
