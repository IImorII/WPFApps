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

namespace WpfApp3_2
{
    //ListBox
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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] data;
        private int sizeOfMatrix = 10;
        public MainWindow()
        {
            InitializeComponent();
        }


        int localElementsCount(int[,] a)
        {
            int cnt = 0;
            for (int i = 0; i < a.GetLength(0); ++i)
                for (int j = 0; j < a.GetLength(1); ++j)
                {
                    if (i != 0 && a[i - 1, j] <= a[i, j]) continue;
                    if (i != a.GetLength(0) - 1 && a[i + 1, j] <= a[i, j]) continue;
                    if (j != 0 && a[i, j - 1] <= a[i, j]) continue;
                    if (j != a.GetLength(1) - 1 && a[i, j + 1] <= a[i, j]) continue;
                    ++cnt;
                    localeList.Items.Add("[" + (i+1) + "]" + "[" + (j+1) + "]");
                }
            return cnt;
        }
        int DiagonalAbs(int[,] m)
        {
            int res = 0;
            for (int i = 0; i < m.GetLength(0); ++i)
                for (int j = i+1; j < m.GetLength(1); ++j)
                    res += Math.Abs(m[i, j]);
            return res;
        }
        private void buttonGenerateData_Click(object sender, RoutedEventArgs e)
        {
            sizeOfMatrix = int.Parse(matrixSize.Text);
            Random rnd = new Random();
            data = new int[sizeOfMatrix, sizeOfMatrix];
            for (int i = 0; i < sizeOfMatrix; i++)
            {
                for (int j = 0; j < sizeOfMatrix; j++)
                {
                    data[i, j] = rnd.Next(0, 10);
                }
            }
            dataTable.ItemsSource = data.ToDataTable().DefaultView;
        }

        private void diagonalSumFound_Click(object sender, RoutedEventArgs e)
        {
            int sum = DiagonalAbs(data);
            diagonalSum.Text = sum.ToString();
        }

        private void localCountFoud_Click(object sender, RoutedEventArgs e)
        {
            int count = localElementsCount(data);
            localCount.Text = count.ToString();
        }
    }
}

