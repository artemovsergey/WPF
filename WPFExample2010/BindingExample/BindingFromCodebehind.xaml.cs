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

namespace BindingExample
{
    public partial class BindingFromCodebehind : Window
    {
        public BindingFromCodebehind()
        {
            InitializeComponent();

            Binding binding = new Binding();

            binding.ElementName = "myTextBox"; // элемент-источник
            binding.Path = new PropertyPath("Text"); // свойство элемента-источника
            myTextBlock.SetBinding(TextBlock.TextProperty, binding); // установка привязки для элемента-приемника
        }
    }
}
