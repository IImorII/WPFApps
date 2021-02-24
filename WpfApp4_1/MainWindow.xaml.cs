using System;
using System.IO;
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

namespace WpfApp4_1
{
    public class SquarePolynomial
    {
        private List<double> _array;

        public SquarePolynomial(IEnumerable<double> arrayCoefficients)
        {
            _array = new List<double>(arrayCoefficients);
        }

        public SquarePolynomial(int headPow)
        {
            _array = new List<double>(headPow);

            for (int i = 0; i < headPow; i++)
                _array.Add(0D);
        }

        //По умолчанию создает полином степени 2
        public SquarePolynomial()
        {
            _array = new List<double>(3);

            for (int i = 0; i < 3; i++)
                _array.Add(0D);
        }

        public double this[int i]
        {
            get
            {
                if (i < 0 || i >= _array.Count) throw new IndexOutOfRangeException();
                return _array[i];
            }

            set
            {
                if (i < 0 || i >= _array.Count) throw new IndexOutOfRangeException();
                _array[i] = value;
            }
        }

        public int HeadPow
        {
            get
            {
                return _array.Count;
            }
        }

        public IEnumerable<double> LineCoefficients
        {
            get
            {
                return _array;
            }
        }

        public static SquarePolynomial operator +(SquarePolynomial poly1, SquarePolynomial poly2)
        {
            SquarePolynomial maxPoly = maxPowPoly(poly1, poly2);
            SquarePolynomial minPoly = minPowPoly(poly1, poly2);

            SquarePolynomial resultPoly = new SquarePolynomial(maxPoly.LineCoefficients);

            for (int i = 0; i < minPoly.HeadPow; i++)
                resultPoly[i] = maxPoly[i] + minPoly[i];

            return resultPoly;
        }
        public static SquarePolynomial operator -(SquarePolynomial poly1, SquarePolynomial poly2)
        {
            SquarePolynomial maxPoly = maxPowPoly(poly1, poly2);
            SquarePolynomial minPoly = minPowPoly(poly1, poly2);

            SquarePolynomial resultPoly = new SquarePolynomial(maxPoly.LineCoefficients);

            for (int i = 0; i < minPoly.HeadPow; i++)
                resultPoly[i] = maxPoly[i] - minPoly[i];

            return resultPoly;
        }

        public static SquarePolynomial operator *(SquarePolynomial poly1, double k)
        {
            SquarePolynomial resultPoly = new SquarePolynomial(poly1.LineCoefficients);

            for (int i = 0; i < poly1.HeadPow; i++) resultPoly[i] *= k;

            return resultPoly;
        }

        public static SquarePolynomial operator *(SquarePolynomial poly1, SquarePolynomial poly2)
        {
            SquarePolynomial resultPoly = new SquarePolynomial(poly1.HeadPow + poly2.HeadPow - 1);
            resultPoly._array.Select(x => 0);

            for (int i = 0; i < poly1.HeadPow; i++)
                for (int j = 0; j < poly2.HeadPow; j++)
                    resultPoly[i + j] += poly1[i] * poly2[j];

            return resultPoly;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < this.HeadPow; i++)
            {
                if (this[i] == 0) continue;
                if (this[i] > 0 && sb.Length > 0) sb.Append('+');
                if (i == this.HeadPow - 1)
                {
                    sb.Append(this[i] + "");
                    continue;
                }
                
                if (i == this.HeadPow - 2)
                {
                    if (this[i] == 1)
                    {
                        sb.Append("x ");
                        continue;
                    }
                    sb.Append(this[i] + "*x ");
                    continue;
                }

                if (this[i] == 1)
                {
                    sb.Append("x^" + (this.HeadPow - i - 1) + ' ');
                    continue;
                }
                sb.Append(this[i] + "*x^" + (this.HeadPow - i - 1) + ' ');
            }

            return sb.ToString();
        }

        public double GetSolution(double x)
        {
            double res = this[0];

            for (int i = 1; i < this.HeadPow; i++)
                res += this[i] * Math.Pow(x, i);

            return res;
        }

        private static Func<SquarePolynomial, SquarePolynomial, SquarePolynomial> maxPowPoly = (x, y) =>
            Math.Max(x.HeadPow, y.HeadPow) == x.HeadPow ? x : y;
        private static Func<SquarePolynomial, SquarePolynomial, SquarePolynomial> minPowPoly = (x, y) =>
            maxPowPoly(x, y).Equals(x) ? y : x;
    }
    public partial class MainWindow : Window
    {
        SquarePolynomial poly1, poly2;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Write2_Click(object sender, RoutedEventArgs e)
        {
            poly2 = new SquarePolynomial(new List<double> { double.Parse(a2.Text), double.Parse(b2.Text), double.Parse(c2.Text) });
        }

        private void SolutionButton_Click(object sender, RoutedEventArgs e)
        {
            double answer = poly1.GetSolution(double.Parse(Argument.Text));
            Solution.Text = answer.ToString();
        }

        private void SumButton_Click(object sender, RoutedEventArgs e)
        {
            SquarePolynomial polySum = poly1 + poly2;
            Sum.Text = polySum.ToString();
        }

        private void DecButton_Click(object sender, RoutedEventArgs e)
        {
            SquarePolynomial polyDec = poly1 - poly2;
            Dec.Text = polyDec.ToString();
        }

        private void MultButton_Click(object sender, RoutedEventArgs e)
        {
            SquarePolynomial polyMult = poly1 * poly2;
            Mult.Text = polyMult.ToString();
        }

        private void Write1_Click(object sender, RoutedEventArgs e)
        {
            poly1 = new SquarePolynomial(new List<double> { double.Parse(a1.Text), double.Parse(b1.Text), double.Parse(c1.Text) });
        }
    }
}
