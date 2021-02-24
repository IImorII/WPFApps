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
using System.Text.RegularExpressions;

namespace WpfApp5_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = @"Text.txt";
        string[] letters = { "а", "е", "и", "у", "о", "ю", "я", "ё" };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            //Добавить способ через String
            string st = "";
            StringBuilder sb = new StringBuilder("");
            int counter = 0;

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
            {
                Regex regex = new Regex(@"[\W{L},_]");
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = regex.Replace(line, " ");
                    string[] words = line.Split(' ');
                    for (int i = 0; i < words.Length; i++)
                    {
                        words[i].Trim();
                        if (letters.Any(letter => words[i].ToLower().StartsWith(letter)) || letters.Any(letter => words[i].ToLower().EndsWith(letter)))
                        {
                            st += words[i] + "\n";
                            sb.AppendLine(words[i]);
                            counter++;
                        }
                    }
                }
            }
            countWords.Text = "Количество " + counter.ToString();
            outputBox.Text = sb.ToString();
            outputBoxString.Text = st;
        }
    }
}
