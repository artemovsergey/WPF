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
    /// <summary>
    /// Логика взаимодействия для ResourceFromCodebehind.xaml
    /// </summary>
    public partial class ResourceFromCodebehind : Window
    {
        public ResourceFromCodebehind()
        {
            InitializeComponent();

            // определение объекта-ресурса
            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.GradientStops.Add(new GradientStop(Colors.LightGray, 0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.White, 1));

            // добавление ресурса в словарь ресурсов окна
            this.Resources.Add("buttonGradientBrush", gradientBrush);

            // установка ресурса у кнопки
            button1.Background = (Brush)this.TryFindResource("buttonGradientBrush");
            // или так
            //button1.Background = (Brush)this.Resources["buttonGradientBrush"];

        }
    }
}
