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

namespace WpfApp3_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isZero = false;
        List<double> items = new List<double>();
        double sum = 0;
        double min = double.MaxValue;
        public MainWindow()
        {
            InitializeComponent();
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 2 - this.Width / 2;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - this.Height / 2;
        }
        // Исключения
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = 0;
                x = double.Parse(InputBox.Text);
                items.Add(x);
                OutputBox.Items.Add(x);
                if (x == 0) { isZero = true; }
                if (Math.Abs(x) < Math.Abs(min)) { min = x; }
                if (isZero) { sum += Math.Abs(x); }
                SumBox.Text = sum.ToString();
                MinBox.Text = min.ToString();
            }
            catch
            {
                OutputBox.Items.Add("Неверный ввод!");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<double> odd = new List<double>();
            List<double> even = new List<double>();
            for (int i = 0; i < items.Count; i++)
            {
                if (i % 2 == 0)
                {
                    odd.Add(items[i]);
                }
                else
                {
                    even.Add(items[i]);
                }
            }
            items.Clear();
            OutputBox.Items.Clear();
            for (int i = 0; i < odd.Count; i++)
            {
                items.Add(odd[i]);
            }
            for (int i = 0; i < even.Count; i++)
            {
                items.Add(even[i]);
            }
            for (int i = 0; i < items.Count; i++)
            {
                OutputBox.Items.Add(items[i]);
            }
        }
    }
}
