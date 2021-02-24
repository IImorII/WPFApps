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

namespace WpfApp8_1
{
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
        public void InputData()
        {

        }
        public MainWindow()
        {
            InitializeComponent();
            MarshesOutput.ItemsSource = Marshes;
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            Marshes.Add(new Marsh(StartPosition.Text, EndPosition.Text));
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }

        private void NumSort_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Marsh> orderedMarshes = Marshes.OrderBy(m => m.num);
            Marshes = new List<Marsh>(orderedMarshes);
            MarshesOutput.ItemsSource = Marshes;
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }

        private void StartSort_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Marsh> orderedMarshes = Marshes.OrderBy(m => m.start);
            Marshes = new List<Marsh>(orderedMarshes);
            MarshesOutput.ItemsSource = Marshes;
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }

        private void EndSort_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Marsh> orderedMarshes = Marshes.OrderBy(m => m.end);
            Marshes = new List<Marsh>(orderedMarshes);
            MarshesOutput.ItemsSource = Marshes;
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(SearchNum.Text);
            IEnumerable<Marsh> marsh = Marshes.Where(m => m.num == num);
            MarshesOutput.ItemsSource = marsh;
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }

        private void LoadButton_Click_1(object sender, RoutedEventArgs e)
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
            for (int i = 0; i < values.Count / 2; i++)
            {
                Marshes.Add(new Marsh(values[i], values[values.Count - i - 1]));
            }
            MarshesOutput.ItemsSource = Marshes;
            CollectionViewSource.GetDefaultView(MarshesOutput.ItemsSource).Refresh();
        }
    }
}
