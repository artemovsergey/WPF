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
using Commands.Models;

namespace Commands
{


    /*
     
     Использование команд помогает нам сократить объем кода и использовать одну и ту же команду
     для нескольких элементов управления в различных местах программы.
     Таким образом, команды позволяют абстрагировать набор действий
     от конкретных событий конкретных элементов.
      
     
    есть два варианта
        1) мы реализуем ICommand
        и тогда просто в элементы пишем
        <button content="Добавить" command="{Binding AddUser}">

        2) когда используем встроенные команды,
        тогда нужно реализовывать привязку,
        как у Вас в примере:

        <button x:name="helpButton" command="ApplicationCommands.Help" content="Help">
        <button.commandbindings>
        <commandbinding command="Help" executed="CommandBinding_Executed"/>
        </button.commandbindings>
        </button>
     */

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (UserContext context = new UserContext()) { 

                // подключение к базе данных

                User user = context.Users.Where(u => u.Name == "user1").FirstOrDefault();
                MessageBox.Show(user.Name.ToString());
            
            }

        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("log.txt", true))
            {
                writer.WriteLine("Выход из приложения: " + DateTime.Now.ToShortDateString() + " " +
                DateTime.Now.ToLongTimeString());
                writer.Flush();
            }


            /*
             
             При выходе из блока using автоматически вызывовется метод writer.Dispose(),
             который вызовет writer.Close(), который запишет буфер на диск и закроет поток. 
              
             */

            this.Close();
        }

    }
}
