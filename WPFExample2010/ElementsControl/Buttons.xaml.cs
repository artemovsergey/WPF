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

namespace ElementsControl
{
    /// <summary>
    /// Логика взаимодействия для Buttons.xaml
    /// </summary>
    public partial class Buttons : Window
    {
        int a = 0;

        public Buttons()
        {
            InitializeComponent();
            button1.Click += button1_Click;
           
            textBlock.Text = "1";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            a = 1 + Convert.ToInt32(textBlock.Text);
            textBlock.Text = a.ToString();  
        }


        /// <summary>
        /// Обработчик ToogleButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {

            tb1.Foreground = Brushes.White;
            
            if (tb1.IsChecked == false)
            {
                tb1.Background = Brushes.Red;
                tb1.Content = "Кнопка выключена";  
            }

            if (tb1.IsChecked == true)
            {
                tb1.Foreground = Brushes.Green;
                tb1.Content = "Кнопка включена";    
            }
   
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseEnter_1(object sender, MouseEventArgs e)
        {
            popup1.IsOpen = true;
        }
    }
}
