using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Provider
{
    public class UserRepository
    {
        private ObservableCollection<User> Users;
 
        public UserRepository()
        {
            Users = new ObservableCollection<User>
            {
                new User {Name="user1", Age=1 },
                new User {Name="user2", Age=2 },
                new User {Name="user3", Age=3 },
                new User {Name="user4", Age=4}
            };
        }

        public ObservableCollection<User> GetUsers()
        {
            return Users;
        }

    }
}
