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

namespace WpfApp2_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Results
        {
            public double x { get; set; }
            public double y { get; set; }
            public double steps { get; set; }
            public double yCheck { get; set; }
            public Results(double x, double y, double steps, double yCheck)
            {
                this.x = x;
                this.y = y;
                this.steps = steps;
                this.yCheck = yCheck;
            }
        }
        static ulong Factorial(ulong x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                return x * Factorial(x - 1);
            }
        }
        Results funcResult(double x, double e)
        {
            double y = 0;
            double yPrev = 0;
            ulong step = 0;
            double yCheck = 0;
            {
                y += (Math.Pow(x, step) / Factorial(step));
                step++;
            }

            do
            {
                yPrev = y;
                y += (Math.Pow(x, step) / Factorial(step));
                step++;
            }
            while (Math.Abs(y - yPrev) > e);
            yCheck = Math.Exp(x);
            Results results = new Results(x, y, step, yCheck);
            return results;
        }

        List<Results> funcInterval(double xMin, double xMax, double dx, double e)
        {
            List<Results> results = new List<Results>();
            try
            {
                if (xMin > xMax)
                {
                    throw new Exception("Min не может быть больше Max");
                }     
                while (xMin <= xMax)
                {
                    results.Add(funcResult(xMin, e));
                    xMin += dx;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            return results;
        }
        public MainWindow()
        {
            InitializeComponent();
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 2 - this.Width / 2;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - this.Height / 2;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void TextGotFocusEvent(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Text = string.Empty;
            // if you want this to happen only the first time you can remove the event handler like this
            text.GotFocus -= TextGotFocusEvent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double xMin = 0;
                double xMax = 0;
                double dx = 0;
                double exp = 0.1;
                xMin = double.Parse(Box1.Text);
                xMax = double.Parse(Box2.Text);
                dx = double.Parse(Box3.Text);
                exp = double.Parse(Box4.Text);
                if (dx > xMax - xMin)
                {
                    throw new Exception("dx is too big");
                }
                if (exp <= 0)
                {
                    throw new Exception("exp < 0");
                }
                List<Results> results = funcInterval(xMin, xMax, dx, exp);
                DataGridResults.ItemsSource = results;
            }
            catch
            {
                errorWindow error = new errorWindow();
                error.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 2 - error.Width / 2;
                error.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - error.Height / 2;
                error.Show();
            }
        }
    }
}
