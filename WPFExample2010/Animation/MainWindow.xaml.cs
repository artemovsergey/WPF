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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace Animation
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DoubleAnimation buttonAnimation = new DoubleAnimation();
            buttonAnimation.From = helloButton.ActualWidth;
            buttonAnimation.To = 150;
            buttonAnimation.Duration = TimeSpan.FromSeconds(3);

            // повторение анимации
            //buttonAnimation.RepeatBehavior = new RepeatBehavior(2);

            // время повторения

            /*

             Здесь время повторения - 7 секунд. Анимация длится 3 секунды, а это значит,
             что будет 7 / 3 повторений: два полноценных повторения и в последнем случае
             ширина увелится только до трети требуемой ширины.
             
             */
            //buttonAnimation.RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(7));


            buttonAnimation.AutoReverse = true;

            // событие по завершению анимации
            buttonAnimation.Completed+=new EventHandler(ButtonAnimation_Completed);

            helloButton.BeginAnimation(Button.WidthProperty, buttonAnimation);
        }


        private void ButtonAnimation_Completed(object sender, EventArgs e)
        {
            MessageBox.Show("Анимация завершена");
        }

    }
}
