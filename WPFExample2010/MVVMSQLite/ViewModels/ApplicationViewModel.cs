using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMSQLServer.Models;
using MVVMSQLServer.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;

namespace MVVMSQLServer.ViewModels
{
    public class ApplicationViewModel
    {
        UserContext db = new UserContext();
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        public ObservableCollection<User> Users { get; set; }
        
        
        public ApplicationViewModel()
        {
            //db.Database.EnsureCreated();
            db.Users.Load();
            //Users = db.Users.Local.ToObservableCollection();

            //ObservableCollection<User> Users = new ObservableCollection<User>();
            Users = db.Users.Local;
           

        }


        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      UserWindow userWindow = new UserWindow(new User());
                      if (userWindow.ShowDialog() == true)
                      {
                          User user = userWindow.User;
                          db.Users.Add(user);
                          db.SaveChanges();
                      }
                  }));
            }
        }

        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      User user = selectedItem as User;
                      if (user == null) return;

                      User vm = new User
                      {
                          Id = user.Id,
                          Name = user.Name,
                          Age = user.Age
                      };
                      UserWindow userWindow = new UserWindow(vm);


                      if (userWindow.ShowDialog() == true)
                      {
                          user.Name = userWindow.User.Name;
                          user.Age = userWindow.User.Age;
                          db.Entry(user).State = EntityState.Modified;
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      User user = selectedItem as User;
                      if (user == null) return;
                      db.Users.Remove(user);
                      db.SaveChanges();
                  }));
            }
        }
    }
}
