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
    /// Логика взаимодействия для ComboBox.xaml
    /// </summary>
    public partial class ComboBox : Window
    {
        public ComboBox()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Selection from ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void phonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MessageBox.Show((phonesList.SelectedItem as TextBlock).Text.ToString());

            MessageBox.Show(  ((TextBlock)phonesList.SelectedItem).Text.ToString());
        }
    }
}
