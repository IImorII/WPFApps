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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public class ValuesF
    {
        public String x { get; set; }
        public String y { get; set; }
    }
    public partial class MainWindow : Window
    {
        List<ValuesF> func(double xMin, double xMax, double dx)
        {
            List<ValuesF> tab = new List<ValuesF>();
            double y = 0;
            double x = xMin;
            
            while (x <= xMax)
            {
                if (x < -10 || x > 4)
                {
                    tab.Add(new ValuesF() { x = x.ToString(), y = "Не определена" });
                    x += dx;
                    continue;
                }

                if (x >= -10 && x <= -6) { y = 2 - Math.Sqrt(4 - (x + 8) * (x + 8)); }
                if (x >= -6 && x <= -4) { y = 2; }
                if (x >= -4 && x <= 2) { y = -0.5 * x; }
                if (x >= 2 && x <= 4) { y = x - 3; }
                tab.Add(new ValuesF() { x = x.ToString(), y = y.ToString() });
                x += dx;
            }

            return tab;
        }
        public MainWindow()
        {
            InitializeComponent();
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 2 - this.Width / 2;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - this.Height / 2;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonData_Click(object sender, RoutedEventArgs e)
        {
            double xMin = 0;
            double xMax = 0;
            double dx = 0;
            try
            {
                xMin = double.Parse(InputBox_1.Text);
                xMax = double.Parse(InputBox_2.Text);
                dx = double.Parse(InputBox_3.Text);
                if (dx <= 0) { throw new Exception("dx <0"); }
                List<ValuesF> tab = func(xMin, xMax, dx);
                BoxData.ItemsSource = tab;
            }
            catch
            {
                Window1 error = new Window1();
                error.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 2 - error.Width / 2;
                error.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - error.Height / 2;
                error.Show();
            }
           

            //foreach (KeyValuePair<double, double> kv in tab)
            //{
            //    BoxData.Items.Add(kv);
            //}
        }

        public void YourGotFocusEvent(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Text = string.Empty;
            // if you want this to happen only the first time you can remove the event handler like this
            text.GotFocus -= YourGotFocusEvent;
        }
    }
}
