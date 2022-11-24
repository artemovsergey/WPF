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
    /// Логика взаимодействия для TreeView.xaml
    /// </summary>
    public partial class TreeView : Window
    {
        public TreeView()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Событие выбора меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = (TreeViewItem)sender;

            MessageBox.Show(String.Format("Выбрано: {0}", tvi.Header.ToString()));
        }
    }
}
