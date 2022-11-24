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



namespace DataContextExample
{

    public partial class MainWindow : Window
    {


        public static readonly DependencyProperty UserProperty;

        public User User
        {
            get { return (User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        static MainWindow()
        {
            UserProperty = DependencyProperty.Register(
                "User",
                typeof(User),
                typeof(MainWindow));
        }


        public MainWindow()
        {
            InitializeComponent();

            User = new User()
            {

                Id = 1,
                Name = "user1",
                Age = 10,
                Company = new Company() { Title = "Google" }
            };

            DataContext = User;


        }
    }
}
