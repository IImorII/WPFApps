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
using System.Data;

namespace WpfApp6_1
{
    public static class ArrayExtension
    {
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add("-" + (i + 1) + "-", typeof(T));
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }
                res.Rows.Add(row);
            }
            return res;
        }
    }
    public class Matrix
    {
        public double[,] matrix;
        private int Row = 0, Col = 0;

        public Matrix(int row, int col)
        {
            matrix = new double[row, col];
            Row = row; Col = col;
        }

        public Matrix(Matrix matr)
        {
            matrix = new double[matr.Row, matr.Col];
            Row = matr.Row; Col = matr.Col;
            for (int i = 0; i < Col; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    matrix[i, j] = matr[i, j];
                }
            }
        }

        public void Generate()
        {
            int size = Col;
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = Convert.ToDouble(rnd.Next(-1000, 1000)/10.0);
                }
            }
        }
        public Matrix(int N)
        {
            matrix = new double[N, N];
            Row = Col = N;
        }

        public double this[int i, int j]
        {
            get { return matrix[i, j]; }
            set { matrix[i, j] = value; }
        }

        public double[] this[int i]
        {
            get {
                int size = Col;
                double[] buf = new double[size];
                for (int j = 0; j < size; j++)
                {
                    buf[j] = matrix[i, j];
                }
                return buf; 
            }
            set {
                for (int j = 0; j < this.Col; j++)
                {
                    matrix[i, j] = value[j];
                }
            }
        }

        public static bool operator !=(Matrix first, Matrix second)
        {
            if (first.Col != second.Col || first.Row != second.Row)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (first[i, j] != second[i, j])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static double Determinant(Matrix matr1)
        {
            Matrix matr = new Matrix(matr1);
            double EPS = 1E-9;
            double det = 1;
            if (matr.Col != matr.Row)
            {
                throw new Exception("Matrix is not square!");
            }
            for (int i = 0; i < matr.Col; ++i)
            {
                int k = i;
                for (int j = i + 1; j < matr.Col; ++j)
                    if (Math.Abs(matr[j, i]) > Math.Abs(matr[k, i]))
                        k = j;

                if (Math.Abs(matr[k, i]) < EPS)
                {
                    det = 0;
                    break;
                }
                double[] buf = matr[i];
                matr[i] = matr[k];
                matr[k] = buf;

                if (i != k)
                    det = -det;
                det *= matr[i, i];
                for (int j = i + 1; j < matr.Col; ++j)
                    matr[i, j] /= matr[i, i];

                for (int j = 0; j < matr.Col; ++j)
                    if ((j != i) && (Math.Abs(matr[j, i]) > EPS))
                        for (k = i + 1; k < matr.Col; ++k)
                            matr[j, k] -= matr[i, k] * matr[j, i];
            }
            return det;
        }
        public static bool operator ==(Matrix first, Matrix second)
        {
            if (first != second)
            {
                return false;
            }
            return true;
        }
        public static Matrix operator +(Matrix first, Matrix second)
        {
            Matrix mat = new Matrix(first.Row, first.Col);
            for (int i = 0; i < first.Row; i++)
                for (int j = 0; j < first.Col; j++)
                    mat[i, j] = first[i, j] + second[i, j];
            return mat;
        }

        public static Matrix operator -(Matrix first, Matrix second)
        {
            Matrix mat = new Matrix(first.Row, first.Col);
            for (int i = 0; i < first.Row; i++)
                for (int j = 0; j < first.Col; j++)
                    mat[i, j] = first[i, j] - second[i, j];
            return mat;
        }

        public static Matrix operator *(Matrix m, int t)
        {
            Matrix mat = new Matrix(m.Row, m.Col);
            for (int i = 0; i < m.Row; i++)
                for (int j = 0; j < m.Col; j++)
                    mat[i, j] = m[i, j] * t;
            return mat;
        }

        public static Matrix operator *(Matrix first, Matrix second)
        {
            Matrix matr = new Matrix(first.Row, first.Col);
            for (int i = 0; i < first.Row; i++)
            {
                for (int j = 0; j < second.Col; j++)
                {
                    double sum = 0;
                    for (int r = 0; r < first.Col; r++)
                        sum += first[i, r] * second[r, j];
                    matr[i, j] = sum;
                }
            }
            return matr;
        }

        public double Norma()
        {
            double max = double.MinValue;
            double temp;
            for (int i = 0; i < Col; i++)
            {
                temp = 0;
                for (int j = 0; j < Col; j++)
                {
                    temp += matrix[i, j];
                }
                if (temp > max) max = temp;
            }
            return max;
        }

        public static Matrix operator ^(Matrix first, int pow)
        {
            Matrix matr = new Matrix(first.Row, first.Col);
            matr = first;
            for (int z = 1; z < pow; z++)
            {
                Matrix bufer = new Matrix(first.Row, first.Col);
                for (int i = 0; i < first.Row; i++)
                {
                    for (int j = 0; j < first.Row; j++)
                    {
                        double sum = 0;
                        for (int r = 0; r < first.Row; r++)
                            sum += matr[i, r] * first[r, j];
                        bufer[i, j] = sum;
                    }
                }
                matr = bufer;
            }
            return matr;
        }
    }
    public partial class MainWindow : Window
    {
        Matrix matrix1, matrix2, matrix3;

        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Norma_Click(object sender, RoutedEventArgs e)
        {
            double norm = matrix1.Norma();
            NormaOut.Text = norm.ToString();
        }

        private void Det_Click(object sender, RoutedEventArgs e)
        {
            double det = Matrix.Determinant(matrix1);
            DetOut.Text = det.ToString();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            matrix3 = matrix1 + matrix2;
            Matrix3.ItemsSource = matrix3.matrix.ToDataTable().DefaultView;
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            matrix3 = matrix1 - matrix2;
            Matrix3.ItemsSource = matrix3.matrix.ToDataTable().DefaultView;
        }

        private void Multiply_Click(object sender, RoutedEventArgs e)
        {
            matrix3 = matrix1 * matrix2;
            Matrix3.ItemsSource = matrix3.matrix.ToDataTable().DefaultView;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            bool isEquals = matrix1 == matrix2;
            EqualsOut.Text = (isEquals) ? "Равны" : "Не равны";
        }

        private void Matrix1Generate_Click(object sender, RoutedEventArgs e)
        {
            int size = int.Parse(Matrix1Size.Text);
            matrix1 = new Matrix(size);
            matrix1.Generate();
            Matrix1.ItemsSource = matrix1.matrix.ToDataTable().DefaultView;
        }

        private void Matrix2Generate_Click(object sender, RoutedEventArgs e)
        {
            int size = int.Parse(Matrix1Size.Text);
            matrix2 = new Matrix(size);
            matrix2.Generate();
            Matrix2.ItemsSource = matrix2.matrix.ToDataTable().DefaultView;
        }
    }
}
