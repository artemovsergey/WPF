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

namespace ItemTemplate
{
    public partial class TemplatFromResource : Window
    {
        public ObservableCollection<User> Users { get; set; }

        public TemplatFromResource()
        {
            InitializeComponent();

            Users = new ObservableCollection<User>
                {
                new User {Id=1, ImagePath="/Images/1.jpg", Name="user1", Age=1 },
                new User {Id=2, ImagePath="/Images/2.jpg", Name="user2", Age=2 },
                new User {Id=3, ImagePath="/Images/3.jpg", Name="user3", Age=3 }   
                };
                userList.ItemsSource = Users;
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User u = (User)userList.SelectedItem;
            MessageBox.Show(u.Name);
        }
    }
}
