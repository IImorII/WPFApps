using System;
using System.Collections.Generic;
using System.Text;
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
using System.Globalization;

namespace WpfApp1
{
    public partial class Window1 : Window
    {

        double Xmin = -10;
        double Xmax = 10;
        double Ymin = -10;
        double Ymax = 10;
        double dx = 0.1;
        Pen functionColor = new Pen(Brushes.Aqua, 0.005);
        List<double> data = new List<double>();

        DrawingGroup drawingGroup = new DrawingGroup();

        public Window1()
        {
            InitializeComponent();
            image1.Source = new DrawingImage(drawingGroup);
        }

        void DataFill()
        {
            double y;
            double x = Xmin;
            while (x <= Xmax)
            {
                y = Math.Pow(x, 2) + Math.Sin(Math.Pow(x, 2));
                data.Add(y);
                x += dx;
            }
        }

        void Execute()
        {
            data.Clear();
            DataFill();
            BackgroundDraw();   
            GridDraw();
            AxesDraw(); 
            FunctionDraw();        
            MarkerDraw();        
        }

      
        private void BackgroundDraw()
        {
           
            GeometryDrawing geometryDrawing = new GeometryDrawing();

           
            RectangleGeometry rectGeometry = new RectangleGeometry();
            rectGeometry.Rect = new Rect(0, 0, 2, 2);
            geometryDrawing.Geometry = rectGeometry;

           
            geometryDrawing.Pen = new Pen(Brushes.Red, 0.005);
            geometryDrawing.Brush = Brushes.Black;

            
            drawingGroup.Children.Add(geometryDrawing);
        }

        
        private void GridDraw()
        {
           
            GeometryGroup geometryGroup = new GeometryGroup();

            for (int i = 1; i < 20; i++)
            {
                LineGeometry line = new LineGeometry(new Point(2.0, i * 0.1),
                    new Point(-0.1, i * 0.1));
                geometryGroup.Children.Add(line);
            }

            for (int i = 1; i < 20; i++)
            {
                LineGeometry line = new LineGeometry(new Point(i * 0.1, 2),
                    new Point(i * 0.1, -0.1));
                geometryGroup.Children.Add(line);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;
            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.002);
            double[] dashes = { 1, 1, 1, 1, 1 };
            geometryDrawing.Pen.DashStyle = new DashStyle(dashes, -.1);
            geometryDrawing.Brush = Brushes.Beige;
            drawingGroup.Children.Add(geometryDrawing);
        }

        private void AxesDraw()
        {
            
            GeometryGroup geometryGroup = new GeometryGroup();

            LineGeometry lineX = new LineGeometry(new Point(2, Ymax * 0.1), new Point(-0.1, Ymax * 0.1));
            geometryGroup.Children.Add(lineX);

            LineGeometry lineY = new LineGeometry(new Point(Xmax * 0.1, 2), new Point(Xmax * 0.1, -0.1));
            geometryGroup.Children.Add(lineY);
               
            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup; 
            geometryDrawing.Pen = new Pen(Brushes.White, 0.002); 
            geometryDrawing.Brush = Brushes.Beige;  
            drawingGroup.Children.Add(geometryDrawing);
        }


        private void FunctionDraw()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < data.Count - 1; i++)
            {
                if ((data[i] <= Ymax && data[i] >= Ymin) && (data[i + 1] <= Ymax && data[i + 1] >= Ymin))
                {
                    LineGeometry line = new LineGeometry(
                        new Point(((double)i / (data.Count - 1)) * 2,
                            1 - data[i] / (data.Count - 1) * (2/dx)),
                        new Point(((double)(i + 1) / (data.Count - 1)) * 2,
                            1 - (data[i + 1] / (data.Count - 1) * (2/dx))));
                    geometryGroup.Children.Add(line);
                }
            }
           
            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;
            geometryDrawing.Pen = functionColor;
            drawingGroup.Children.Add(geometryDrawing);
        }
 
        
        private void MarkerDraw()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < 20; i++)
            {
                FormattedText formattedTextY = new FormattedText(((int)(Ymax - i)).ToString(), CultureInfo.InvariantCulture,FlowDirection.LeftToRight,new Typeface("Verdana"),0.03,Brushes.Black);
                FormattedText formattedTextX = new FormattedText(((int)(Xmin + i)).ToString(), CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Verdana"),0.03, Brushes.Black);
                formattedTextY.SetFontWeight(FontWeights.Normal);
                formattedTextX.SetFontWeight(FontWeights.Normal);

                Geometry numsY = formattedTextY.BuildGeometry(new Point(-0.1, i * 0.1 - 0.01));
                Geometry numsX = formattedTextX.BuildGeometry(new Point(i * 0.1 - 0.01, 2.1));
                geometryGroup.Children.Add(numsY);
                geometryGroup.Children.Add(numsX);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Brush = Brushes.LightGray;
            geometryDrawing.Pen = new Pen(Brushes.Black, 0.003);

            drawingGroup.Children.Add(geometryDrawing);
        }

        public void SaveDrawingToFile(DrawingGroup drawing, string fileName, double scale)
        {
            var drawingImage = new Image { Source = new DrawingImage(drawing) };
            var width = drawing.Bounds.Width * scale;
            var height = drawing.Bounds.Height * scale;
            drawingImage.Arrange(new Rect(0, 0, width, height));

            var bitmap = new RenderTargetBitmap((int)width, (int)height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(drawingImage);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                encoder.Save(stream);
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            dx = Double.Parse(dxBox.Text);
            string hex = ColorPicker.SelectedColor.ToString();
            functionColor.Brush = (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
            Execute();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int size = int.Parse(SizeOfImage.Text);
            SaveDrawingToFile(drawingGroup, @"saveImage.png", size);
        }
    }
}