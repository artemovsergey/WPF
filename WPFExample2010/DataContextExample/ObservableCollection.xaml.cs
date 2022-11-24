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
using System.Collections.ObjectModel;

namespace DataContextExample
{
    /*

   В качестве источника применяется класс ObservableCollection, который находится в пространстве
   имен System.Collections.ObjectModel. Его преимущество заключается в том,
   что при любом изменении ObservableCollection может уведомлять элементы,
   которые применяют привязку, в результате чего обновляется не только сам объект ObservableCollection,
   но и привязанные к нему элементы интерфейса.
   
   Замечание: обновляются объекты, а не свойство отдельных объектов.
   Чтобы следить за свойствами надо реализовать у моделей INotifiPropertyChanged
    
    */


    public partial class ObservableCollection : Window
    {
        //List<string> users;
        ObservableCollection<string> users;

        public ObservableCollection()
        {
            InitializeComponent();

            //users = new List<string>() { "user1", "user2", "user3" };

            users = new ObservableCollection<string>() { "user1", "user2", "user3" };


            userBox.ItemsSource = users;
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(userField.Text))
            {

                users.Add(userField.Text);
                MessageBox.Show("Пользователь добавлен");
            }
        }

        private void delUser_Click(object sender, RoutedEventArgs e)
        {
            if(userBox.SelectedItem != null)
            users.Remove(userBox.SelectedItem.ToString());
        }
    }
}
