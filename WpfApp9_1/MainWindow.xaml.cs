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

namespace WpfApp9_1
{
    public class Element
    {
        private int countInput;
        private int countOutput;
        public string Name { get; }

        public int CountInput
        {
            get
            {
                return countInput;
            }
            set
            {
                countInput = value;
            }
        }

        public int CountOutput
        {
            get
            {
                return countOutput;
            }
            set
            {
                countOutput = value;
            }
        }

        public Element()
        {
            Name = "";
            CountInput = 0;
            CountOutput = 0;
        }

        public Element(string name)
        {
            Name = name;
            CountInput = 1;
            CountOutput = 1;
        }

        public Element(string name, int countInput, int countOutput)
        {
            Name = name;
            CountInput = countInput;
            CountOutput = countOutput;
        }
    }

    public class ComboElement : Element
    {
        private bool[] input;

        public bool Output()
        {
            bool result = false;
            foreach (bool i in input)
            {
                result |= i;
            }
            return result;
        }
        public ComboElement(string name, int countInput, int countOutput) : base(name, countInput, countOutput)
        {
            input = new bool[countInput];
        }

        public ComboElement()
        {
            CountInput = 3;
            input = new bool[CountInput];
        }

        public bool this[int num]
        {
            get
            {
                return input[num];
            }
            set
            {
                if (num >= CountInput || num < 0)
                {
                    throw new Exception("Bad CountInput");
                }
                input[num] = value;
            }
        }
    }

    public class Register
    {
        bool reset;

        bool set;

        private Memory[] triggers = new Memory[8];

        public Memory this[int index]
        {
            get
            {
                return triggers[index];
            }
            set
            {
                triggers[index] = value;
            }
        }

        public bool Reset
        {
            get
            {
                return reset;
            }
            set
            {
                reset = value;
            }
        }

        public bool Set
        {
            get
            {
                return set;
            }
            set
            {
                set = value;
            }
        }
        public class Memory : Element
        {
            private int input = 2;
            private int output = 2;
            private bool[] inputValues = new bool[2];
            private bool quit;

            private void findOutput()
            {
                quit = (inputValues[0] && inputValues[1]) || (!inputValues[0] && !inputValues[1]);
            }

            public Memory()
            {
                Reset();
            }
            public Memory(Memory other)
            {
                input = other.input;
                output = other.output;
                inputValues = other.inputValues;
                quit = other.quit;
            }
            virtual public void setInput(bool J, bool K)
            {
                inputValues[0] = J;
                inputValues[1] = K;
                findOutput();
            }
            public void Reset()
            {
                inputValues = new bool[2] { false, false };
                quit = true;
            }

            public bool getJ() { return inputValues[0]; }
            public bool getK() { return inputValues[1]; }

            public bool getQ() { return quit; }

            public static bool operator ==(Memory a, Memory b)
            {
                return a.inputValues[0] == b.inputValues[0] && a.inputValues[1] == b.inputValues[1] && a.quit == b.quit ? true : false;
            }
            public static bool operator !=(Memory a, Memory b)
            {
                return !(a == b);
            }

        }
    }

    public partial class MainWindow : Window
    {
        int size = 8;
        Register reg = new Register();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void setAll()
        {
            setValue(0, reg[0].getK(), reg[0].getJ(), q_1);
            setValue(1, reg[1].getK(), reg[1].getJ(), q_2);
            setValue(2, reg[2].getK(), reg[2].getJ(), q_3);
            setValue(3, reg[3].getK(), reg[3].getJ(), q_4);
            setValue(4, reg[4].getK(), reg[4].getJ(), q_5);
            setValue(5, reg[5].getK(), reg[5].getJ(), q_6);
            setValue(6, reg[6].getK(), reg[6].getJ(), q_7);
            setValue(7, reg[7].getK(), reg[7].getJ(), q_8);
        }

        private void setValue(int index, bool K, bool J, TextBox Q)
        {
            reg[index].setInput(K, J);
            Q.Text = reg[index].getQ() ? "+" : "-";
        }

        private void setValue(int index, CheckBox K, CheckBox J, TextBox Q)
        {
            reg[index].setInput((bool)K.IsChecked, (bool)J.IsChecked);

            try
            {
                Q.Text = reg[index].getQ() ? "+" : "-";
                
            }
            catch
            {
                Q.Text = "";
            }
        }

        private void IsLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < size; i++) reg[i] = new Register.Memory();
            setAll();

        }

        private void offsetbutton_Click(object sender, RoutedEventArgs e)
        {
            if (offset.Text == "")
            {

            }
            int bit = Convert.ToInt32(offset.Text);
            Register bufer = new Register();
            for (int i = 0; i < size; i++) bufer[i] = new Register.Memory();


            for (int i = 0; i < size; i++)
            {
                bufer[(i + bit) % size] = reg[i];
            }
            reg = bufer;
            setAll();
        }

        private void j_1_Click(object sender, RoutedEventArgs e)
        {
            setValue(0, k_1, j_1, q_1);

        }

        private void k_1_Click(object sender, RoutedEventArgs e)
        {
            setValue(0, k_1, j_1, q_1);
        }


        private void j_2_Click(object sender, RoutedEventArgs e)
        {
            setValue(1, k_2, j_2, q_2);

        }

        private void k_2_Click(object sender, RoutedEventArgs e)
        {
            setValue(1, k_2, j_2, q_2);
        }


        private void j_3_Click(object sender, RoutedEventArgs e)
        {
            setValue(2, k_3, j_3, q_3);

        }

        private void k_3_Click(object sender, RoutedEventArgs e)
        {
            setValue(2, k_3, j_3, q_3);
        }


        private void j_4_Click(object sender, RoutedEventArgs e)
        {
            setValue(3, k_4, j_4, q_4);

        }

        private void k_4_Click(object sender, RoutedEventArgs e)
        {
            setValue(3, k_4, j_4, q_4);
        }


        private void j_5_Click(object sender, RoutedEventArgs e)
        {
            setValue(4, k_5, j_5, q_5);

        }

        private void k_5_Click(object sender, RoutedEventArgs e)
        {
            setValue(4, k_5, j_5, q_5);
        }


        private void j_6_Click(object sender, RoutedEventArgs e)
        {
            setValue(5, k_6, j_6, q_6);

        }

        private void k_6_Click(object sender, RoutedEventArgs e)
        {
            setValue(5, k_6, j_6, q_6);
        }


        private void j_7_Click(object sender, RoutedEventArgs e)
        {
            setValue(6, k_7, j_7, q_7);

        }

        private void k_7_Click(object sender, RoutedEventArgs e)
        {
            setValue(6, k_7, j_7, q_7);
        }


        private void j_8_Click(object sender, RoutedEventArgs e)
        {
            setValue(7, k_8, j_8, q_8);
        }

        private void k_8_Click(object sender, RoutedEventArgs e)
        {
            setValue(7, k_8, j_8, q_8);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string path = @"Register.bin";
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < size; i++)
                {
                    writer.Write(reg[i].getJ());
                    writer.Write(reg[i].getK());
                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            string path = @"Register.bin";
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                for (int i = 0; i < size; i++)
                {
                    bool J = reader.ReadBoolean();
                    bool K = reader.ReadBoolean();
                    reg[i].setInput(J, K);
                }
                setAll();
            }
        }
    }
}
