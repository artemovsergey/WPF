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
    /// Логика взаимодействия для List.xaml
    /// </summary>
    public partial class List : Window
    {
        public List()
        {
            InitializeComponent();

            list.Items.Add("Xiaomi mi 15".ToString());

            string message = String.Format("В коллекции {0} элементов",list.Items.Count);
            MessageBox.Show(message);





        }


        /// <summary>
        /// Выделение элемента в списке list1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MessageBox.Show(  (((ListBox)sender).SelectedItem as User).Name  );
            MessageBox.Show( (list1.SelectedItem as User).Name);
        }
    }
}
