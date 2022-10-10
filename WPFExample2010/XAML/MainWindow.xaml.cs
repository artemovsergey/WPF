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

namespace XAML
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CreateEventButton().ShowDialog();
        }

        private void CreateButtonFromCodeBehind_Click(object sender, RoutedEventArgs e)
        {
            new CreateButtonFromCodebehind().ShowDialog();
        }

        private void ConverterType_Click(object sender, RoutedEventArgs e)
        {
            new ConverterType().ShowDialog();
        }

        private void UsingNamespaceFromCsharp_Click(object sender, RoutedEventArgs e)
        {
            new UsingNamespaceFromCsharp().ShowDialog();
        }
    }
}
