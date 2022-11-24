using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace BindingExample
{

    /*
     Когда объект класса изменяет значение свойства,
     то он через событие PropertyChanged извещает систему об изменении свойства.
     А система обновляет все привязанные объекты.
     */

    class User  : INotifyPropertyChanged
    {

        private int id;
        private string name;
        private int age;

        public int Id
        {
            get {return id; }

            set {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }

            set
            {
                name = value;
                OnPropertyChanged("Name");
            }

        }

        public int Age
        {
            get { return age; }

            set
            {
                age = value;
                OnPropertyChanged("Age");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

            

        }

    }
}
