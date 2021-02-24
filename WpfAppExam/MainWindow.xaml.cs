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

namespace WpfAppExam
{

    class Item
    {
        private string name;
        private string shop;
        private double cost;
        public double Cost { 
            get 
            { 
                return cost; 
            } 
            set 
            { 
                if (value >= 0) 
                { 
                    cost = value; 
                } 
                else
                {
                    cost = 0;
                }
            } 
        }
        public string Shop { get { return shop; } set { shop = value; } }
        public string Name { get { return name; } set { name = value; } }

        public Item()
        {
            Name = "Item";
            Shop = "Shop";
            Cost = 0;
        }
        public Item(string n, string s, double c)
        {
            Name = n;
            Shop = s;
            Cost = c;
        }

        public override string ToString()
        {
            return "Название: " + Name + "\nМагазин: " + Shop + "\nСтоимость: " + Cost + " рублей";
        }
    }

    class ItemSale : Item
    {
        
        public static bool operator <(ItemSale first, ItemSale second)
        {
            return first.sale < second.sale;
        }

        public static bool operator >(ItemSale first, ItemSale second)
        {
            return first.sale > second.sale;
        }

        private double sale;
        private double costWithSale;

        private double CostWithSale
        {
            get
            {
                return costWithSale;
            }
            set
            {
                costWithSale = value;
            }

        }
        public double Sale
        {
            get { return sale; }
            set 
            { 
                if (value > 0 && value <= 1)
                {
                    sale = value;
                }
                else 
                {
                    sale = 0;
                }
            }
        }
        public ItemSale() : base()
        {
            sale = 0;
        }

        public ItemSale(double s) : base()
        {
            Sale = s;
        }

        public ItemSale(string n, string sh, double c, double s) : base(n, sh, c)
        {
            Sale = s;
            CostSale();
        }
        public void CostSale()
        {
            CostWithSale = Cost * (1 - sale); 
        }

        public override string ToString()
        {
            return base.ToString() + "\nСкидка: " + (Sale * 100) + " %" + "\nЦена со скидкой: " + CostWithSale + " рублей";
        }
    }

    public partial class MainWindow : Window
    {
        List<ItemSale> collection = new List<ItemSale>();

        public void SortBySale()
        {
            IEnumerable<ItemSale> ordered = collection.OrderByDescending(i => i.Sale);
            collection = new List<ItemSale>(ordered);
        }
        void FileWriter(string path)
        {

            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                {
                    for (int i = 0; i < collection.Count; i++)
                    {
                        writer.Write(collection[i].Name);
                        writer.Write(collection[i].Shop);
                        writer.Write(collection[i].Cost);
                        writer.Write(collection[i].Sale);
                    }
                }
            }
            catch (Exception e)
            {
                OutList.Items.Add(e.Message);
            }
        }

        void FileReader(string path)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        string shop = reader.ReadString();
                        double cost = reader.ReadDouble();
                        double sale = reader.ReadDouble();
                        collection.Add(new ItemSale(name, shop, cost, sale));
                    }
                }
            }
            catch (Exception e)
            {
                OutList.Items.Add(e.Message);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                collection.Add(new ItemSale(NameBox.Text, ShopBox.Text, double.Parse(CostBox.Text), double.Parse(SaleBox.Text)));
            }
            catch 
            {
                OutList.Items.Add("Неверный ввод");
            }
        }

        private void OutButton_Click(object sender, RoutedEventArgs e)
        {
            OutList.Items.Clear();
            foreach (ItemSale i in collection)
            {
                OutList.Items.Add(i);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FileWriter(@"data.bin");
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            FileReader(@"data.bin");
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            SortBySale();
        }

        private void ClearList_Click(object sender, RoutedEventArgs e)
        {
            OutList.Items.Clear();
        }
    }
}
