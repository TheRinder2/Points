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

namespace POINTS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Vector size = new Vector(30, 30);
        public double sizeKey = 1;
        public double MainLeft;
        public double MainTop;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void UpdateCanvasLoaded(object sender, RoutedEventArgs e)
        {
            MainLeft = Background.ActualWidth / 2;
            MainTop = Background.ActualHeight / 2;
            UpdateBackPattern(null, null);
        }

        void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {

            var w = Background.ActualWidth;
            var h = Background.ActualHeight;

            Background.Children.Clear();

            AddLineToBackground(MainLeft, 0, MainLeft, h, 3);
            AddLineToBackground(0, MainTop, w, MainTop, 3);


            for (double x = MainLeft; x < w; x += size.X * sizeKey)
                AddLineToBackground(x, 0, x, h, 1);
            for (double x = MainLeft; x > 0; x -= size.X * sizeKey)
                AddLineToBackground(x, 0, x, h, 1);

            for (double y = MainTop; y < h; y += size.Y * sizeKey)
                AddLineToBackground(0, y, w, y, 1);
            for (double y = MainTop; y > 0; y -= size.Y * sizeKey)
                AddLineToBackground(0, y, w, y, 1);
        }
        
        void AddLineToBackground(double x1, double y1, double x2, double y2, int thickness)
        {
            var line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = thickness,
                SnapsToDevicePixels = true
            };
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            //if ((Math.Round((MainLeft - x1) / size.X * sizeKey) % 5 == 0 && y1 == 0) || (Math.Round((MainTop - y1) / size.Y * sizeKey) % 5 == 0 && x1 == 0))
            //    line.StrokeThickness = 2;

            Background.Children.Add(line);

        }

        public double left;
        public double top;

        private void UpdateCanvasWheel(object sender, MouseWheelEventArgs e)
        {
            float k;
            k = e.Delta > 0 ? 1.1f : 0.9f;
            var mosePos = Mouse.GetPosition(MainCanvas);
            MainLeft = mosePos.X - (mosePos.X - MainLeft) * k;
            MainTop = mosePos.Y - (mosePos.Y - MainTop) * k;

            left = Mouse.GetPosition(MainCanvas).X - MainLeft;
            top = Mouse.GetPosition(MainCanvas).Y - MainTop;

            size *= k;
            UpdateBackPattern(null, null);
            lbl.Content = size.ToString();
        }

        private void UpdateCanvasRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            left = e.GetPosition(MainCanvas).X - MainLeft;
            top = e.GetPosition(MainCanvas).Y - MainTop;
        }

        private void UpdateCanvasRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void UpdateCanvasMouseMove(object sender, MouseEventArgs e)
        {
            if(Mouse.RightButton == MouseButtonState.Pressed)
            {
                MainLeft = e.GetPosition(MainCanvas).X - left;
                MainTop = e.GetPosition(MainCanvas).Y - top;
                UpdateBackPattern(null, null);
            }
        }

    }
}
