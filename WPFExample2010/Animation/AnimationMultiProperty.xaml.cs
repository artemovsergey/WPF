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
using System.Windows.Media.Animation;

namespace Animation
{

    public partial class AnimationMultiProperty : Window
    {
        public AnimationMultiProperty()
        {
            InitializeComponent();

            // анимация для ширины
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.From = helloButton.ActualWidth;
            widthAnimation.To = 150;
            widthAnimation.Duration = TimeSpan.FromSeconds(5);

            // анимация для высоты
            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.From = helloButton.ActualHeight;
            heightAnimation.To = 60;
            heightAnimation.Duration = TimeSpan.FromSeconds(5);

            helloButton.BeginAnimation(Button.WidthProperty, widthAnimation);
            helloButton.BeginAnimation(Button.HeightProperty, heightAnimation);
        }
    }
}
