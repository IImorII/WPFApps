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
using System.IO;

namespace WpfApp7_1
{
    class MarshComparerToNum : IComparer<Marsh>
    {
        public int Compare(Marsh m1, Marsh m2)
        {
            if (m1.num > m2.num)
                return 1;
            else if (m1.num < m2.num)
                return -1;
            else
                return 0;
        }
    }
    class MarshComparerToStart : IComparer<Marsh>
    {
        public int Compare(Marsh m1, Marsh m2)
        {
            return String.Compare(m1.start, m2.start);
        }
    }
    class MarshComparerToEnd : IComparer<Marsh>
    {
        public int Compare(Marsh m1, Marsh m2)
        {
            return String.Compare(m1.end, m2.end);
        }
    }
    struct Marsh
    {
        public static int counter = 0;
        public int num { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public Marsh(string start, string end)
        {
            counter++;
            this.start = start;
            this.end = end;
            this.num = counter;
        }
    }
    public partial class MainWindow : Window
    {
        string path = @"data.txt";
        List<Marsh> Marshes = new List<Marsh>();

        public MainWindow()
        {
            InitializeComponent();
            MarshesOutput.ItemsSource = Marshes;
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            Marshes.Add(new Marsh(StartPosition.Text, EndPosition.Text));
            MarshesOutput.ItemsSource = Marshes;
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }

        private void NumSort_Click(object sender, RoutedEventArgs e)
        {
            MarshComparerToNum NumComparer = new MarshComparerToNum();
            Marshes.Sort(NumComparer);
            MarshesOutput.ItemsSource = Marshes;
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }

        private void StartSort_Click(object sender, RoutedEventArgs e)
        {
            MarshComparerToStart StartComparer = new MarshComparerToStart();
            Marshes.Sort(StartComparer);
            MarshesOutput.ItemsSource = Marshes;
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }

        private void EndSort_Click(object sender, RoutedEventArgs e)
        {
            MarshComparerToEnd EndComparer = new MarshComparerToEnd();
            Marshes.Sort(EndComparer);
            MarshesOutput.ItemsSource = Marshes;
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(SearchNum.Text);
            List<Marsh> marsh = new List<Marsh>();
            marsh.Add(Marshes[num-1]);
            MarshesOutput.ItemsSource = marsh;
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> values = new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    values.Add(line);
                }
            }
            for (int i = 0; i < values.Count/2; i++)
            {
                Marshes.Add(new Marsh(values[i], values[values.Count-i-1]));
            }
            MarshesOutput.ItemsSource = Marshes;
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }
    }
}
