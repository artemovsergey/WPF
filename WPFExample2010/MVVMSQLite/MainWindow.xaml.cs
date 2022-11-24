using System.Data.Entity;
using System.Windows;
using MVVMSQLServer.Models;
using System.Linq;
using System.Collections.ObjectModel;
using MVVMSQLServer.ViewModels;

namespace MVVMSQLServer
{
    public partial class MainWindow : Window
    {
        //UserContext db = new UserContext();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();

            //Loaded += MainWindow_Loaded;
        }

        /*
         
        // при загрузке окна
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            //db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Users.Load();
            // и устанавливаем данные в качестве контекста
           
            ObservableCollection<User> Users = new ObservableCollection<User>();
            Users = db.Users.Local;
            DataContext = Users;
        }

        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            UserWindow UserWindow = new UserWindow(new User());
            if (UserWindow.ShowDialog() == true)
            {
                User User = UserWindow.User;
                db.Users.Add(User);
                db.SaveChanges();
            }
        }
        
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            User user = usersList.SelectedItem as User;
            // если ни одного объекта не выделено, выходим
            if (user == null) return;
 
            UserWindow UserWindow = new UserWindow(new User
            {
                Id = user.Id,
                Age = user.Age,
                Name = user.Name
            });
 
            if (UserWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                user = db.Users.Find(UserWindow.User.Id);
                if (user != null)
                {
                    user.Age = UserWindow.User.Age;
                    user.Name = UserWindow.User.Name;
                    db.SaveChanges();
                    usersList.Items.Refresh();
                }
            }
        }
        
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            User user = usersList.SelectedItem as User;
            // если ни одного объекта не выделено, выходим
            if (user == null) return;
            db.Users.Remove(user);
            db.SaveChanges();
        }
        
        */ 
         
    }
}