using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Resources
{

    public partial class DynamicResourceFromCodebehind : Window
    {
        public DynamicResourceFromCodebehind()
        {
            InitializeComponent();

            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.GradientStops.Add(new GradientStop(Colors.LightGray, 0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.White, 1));
            this.Resources.Add("buttonGradientBrush", gradientBrush);

            button1.SetResourceReference(Button.BackgroundProperty, "buttonGradientBrush");
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Resources["buttonGradientBrush"] = new SolidColorBrush(Colors.LimeGreen);
        }

    }
}
