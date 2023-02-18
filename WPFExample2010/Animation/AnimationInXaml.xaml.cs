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

namespace Animation
{
    /// <summary>
    /// Логика взаимодействия для AnimationInXaml.xaml
    /// </summary>
    public partial class AnimationInXaml : Window
    {
        public AnimationInXaml()
        {
            InitializeComponent();
        }


        private void ButtonAnimation_Completed(object sender, EventArgs e)
        {
            MessageBox.Show("Анимация завершена");
        }
    }
}
